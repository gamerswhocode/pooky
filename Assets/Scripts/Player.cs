﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject roarHitBox;
    public Animator playerAnim;

    StateMachine actionState;
    StateMachine movementState;


    public Direction direction = Direction.Right;

    Idle idle;
    public AnimationClip idleAnimation;
    Roar roar;
    public AnimationClip roarAnimation;


    Moving moving;
    public AnimationClip movingAnimation;



    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private Rigidbody2D playerRigidbody;
    bool isGrounded = false;
    public bool isRoaring = false;


    private void Awake()
    {
        idle = new Idle(playerAnim, idleAnimation);
        moving = new Moving(playerAnim, movingAnimation);
        roar = new Roar(playerAnim, roarAnimation, ReturnFromRoarLogic);
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
        Debug.Log(actionState.GetCurrentState());
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
    }

    void ValidateSwitchToIdle()
    {
        if (actionState.GetCurrentState() != idle &&
            !Input.anyKey &&
            actionState.GetCurrentState() != roar)
        {
            actionState.ChangeState(idle);
        }
    }

    private void ValidateMovement()
    {
        float position = transform.position.x; // y = 1.5


        if (actionState.GetCurrentState() != roar)
        {
            if (Input.GetKey(KeyCode.RightArrow) && (direction == Direction.Left) || (Input.GetKey(KeyCode.LeftArrow)  && (direction == Direction.Right)))
            {
                transform.RotateAround(this.transform.position, Vector3.up, 180f);
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.localScale = new Vector3(transform.localScale.x, 180f, transform.localScale.z);
                //if (direction == Direction.Right)
                //{
                    
                //    transform.Rotate(new Vector3(0, 180f, 0));
                //}
                //direction = Direction.Left;

                transform.Translate(Vector2.right * speed * Time.deltaTime);
                Debug.Log("moving left");
                if (actionState.GetCurrentState() != moving)
                    actionState.ChangeState(moving);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
            //    if (direction == Direction.Left)
            //    {
            //        transform.Rotate(new Vector3(0, 0, 0));
            //    }
            //    direction = Direction.Right;

                transform.Translate(Vector2.right * speed * Time.deltaTime);
                //transform.localScale = new Vector3(transform.localScale.x, 0f, transform.localScale.z);

                Debug.Log("moving right");
                if (actionState.GetCurrentState() != moving)
                    actionState.ChangeState(moving);
            }


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                playerRigidbody.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
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
        //Debug.Log("collision" + collision.tag);
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Destroy(gameObject);
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Floor"))
            isGrounded = true;
    }
}
