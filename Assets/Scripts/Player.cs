﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Element element;
    public GameObject roarHitBox;
    public Animator playerAnim;

    StateMachine actionState;
    StateMachine movementState;

    SpriteRenderer sr;


    [SerializeField]
    bool isInvulnerable;
    
    public bool playerHasKey;

    public Direction direction = Direction.Right;

    Idle idle;
    public AnimationClip idleAnimation;
    Roar roar;
    public AnimationClip roarAnimation;
    Death death;
    public AnimationClip deathAnimation;
    Bite bite;
    public AnimationClip biteAnimation;

    public AnimationClip jumpAnimation;

    Moving moving;
    public AnimationClip movingAnimation;


    [SerializeField]
    int Health;

    [SerializeField]
    Image[] Hearts;


    [SerializeField]
    Color fire;
    [SerializeField]
    Color ice;
    [SerializeField]
    Color water;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D playerRigidbody;
    bool isGrounded = false;
    public bool isRoaring = false;

    #region AudioStuff
    AudioSource audioSource;
    [SerializeField]
    AudioClip jumpAudio;
    [SerializeField]
    AudioClip takeHitAudio;
    [SerializeField]
    AudioClip deathAudio;
    #endregion

    private void Awake()
    {
        float offset = 20;
        idle = new Idle(playerAnim, idleAnimation);
        moving = new Moving(playerAnim, movingAnimation);
        roar = new Roar(playerAnim, roarAnimation, ReturnFromRoarLogic);
        death = new Death(playerAnim, deathAnimation, returnDeathAnimation,transform );
        bite = new Bite(playerAnim, biteAnimation, ReturnFromRoarLogic);
        isInvulnerable = false;
        playerHasKey = false;
        audioSource = GetComponent<AudioSource>();


        
    }


    // Start is called before the first frame update
    void Start()
    {
        actionState = new StateMachine();
        actionState.ChangeState(idle);

        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    public void ToggleRoar()
    {
        //isRoaring = !isRoaring;
    }

    private void Update()
    {
        actionState.ExecuteState();
        ValidateRoar();
    }

    private void ValidateRoar()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            actionState.ChangeState(roar);
            //playerAnim.SetTrigger("roar");
        }
    }


    void ReturnFromRoarLogic()
    {
        actionState.ChangeState(idle);
        setElement(Element.None);
    }

    void ValidateSwitchToIdle()
    {
        if (actionState.GetCurrentState() != idle &&
            !Input.anyKey &&
            actionState.GetCurrentState() != roar && actionState.GetCurrentState() != death)
        {
            actionState.ChangeState(idle);
        }
    }

    private void ValidateMovement()
    {
        float position = transform.position.x; // y = 1.5

        var PlayerState = actionState.GetCurrentState();
        if (PlayerState != roar && PlayerState != death && PlayerState != bite)
        {
            if (Input.GetKey(KeyCode.RightArrow) && (direction == Direction.Left) || (Input.GetKey(KeyCode.LeftArrow)  && (direction == Direction.Right)))
            {
                transform.RotateAround(this.transform.position, Vector3.up, 180f);
            }

            #region Move
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (actionState.GetCurrentState() != moving && isGrounded)
                    actionState.ChangeState(moving);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (actionState.GetCurrentState() != moving && isGrounded)
                    actionState.ChangeState(moving);
            }
            #endregion
            if (Input.GetKeyDown(KeyCode.V))
            {
                actionState.ChangeState(bite);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerAnim.Play(jumpAnimation.name, -1, 0f);
                playerRigidbody.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
                audioSource.PlayOneShot(jumpAudio);
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        direction = gameObject.transform.eulerAngles.y == 0 ? Direction.Right : Direction.Left;
        actionState.FixedExecuteState();
        ValidateMovement();
        ValidateSwitchToIdle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            playerHasKey = true;
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            if(actionState.GetCurrentState() != death)
                actionState.ChangeState(idle);
        }
    }

    public void killPlayer(int totalDamage)
    {
        if (!isInvulnerable)
        {
            Health -= totalDamage;
            if (Health <= 0)
            {
                if (actionState.GetCurrentState() != death)
                {
                    audioSource.PlayOneShot(deathAudio);
                    actionState.ChangeState(death);
                }
            }
            else
            {
                isInvulnerable = true;
                StartCoroutine("Fade");
                audioSource.PlayOneShot(takeHitAudio);
            }
        }
    }
    void returnDeathAnimation()
    {
        Destroy(this);
        SceneManager.LoadScene(1);
    }

    public void setElement(Element pElement)
    {
        element = pElement;

        sr = GetComponent<SpriteRenderer>();

        switch (element)
        {
            case Element.Fire:
                sr.color = fire;
                break;
            case Element.Ice:
                sr.color = ice;
                break;
            case Element.Water:
                sr.color = water;
                break;
            default:
                sr.color = Color.white;
                break;
        }

    }

    public Element getElement()
    {
        return element;
    }

    IEnumerator Fade()
    {
            yield return new WaitForSeconds(1);
            isInvulnerable = false;
    }
}
