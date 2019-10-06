using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ghost : MonoBehaviour
{
    public Element element;

    public Animator ghostAnimator;
    StateMachine ghostState;

    Idle ghostIdleState;
    public AnimationClip idleAnimation;
    Stun ghostStuntState;
    public AnimationClip stunAnimation;

    [SerializeField]
    float respawnTimer;
    float timeToRespawn;

    SpriteRenderer sr;
    BoxCollider2D box;

    private void Awake()
    {
        ghostState = new StateMachine();
        ghostIdleState = new Idle(ghostAnimator, idleAnimation);
        ghostStuntState = new Stun(ghostAnimator, stunAnimation, returnStunMethod);

        box = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        switch (element)
        {
            case Element.Fire:
                sr.color = Color.red;
                break;
            case Element.Ice:
                sr.color = Color.cyan;
                break;
            case Element.Water:
                sr.color = Color.blue;
                break;
            default:
                break;
        }

        

    }

    // Start is called before the first frame update
    void Start()
    {
        ghostState.ChangeState(ghostIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (!sr.enabled)
        {
                    timeToRespawn -= Time.deltaTime;
            if(timeToRespawn <= 0)
                Respawn();
        }
        else { 
        ghostState.ExecuteState();
       
        }
    }

    private void Respawn()
    {
        ghostState.ChangeState(ghostIdleState);
        SetActiveObjects(true);
    }

    void returnStunMethod()
    {
        ghostState.ChangeState(ghostIdleState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            ghostState.ChangeState(ghostStuntState);
        }else if (collision.gameObject.CompareTag("Player") && ghostState.GetCurrentState() != ghostStuntState)
        {
            var player = collision.gameObject.GetComponent<Player>();
            player.killPlayer(1);
        }
        else if (collision.gameObject.CompareTag("Bite")&& ghostState.GetCurrentState()==ghostStuntState)
        {
            var player = collision.gameObject.GetComponentInParent<Player>();
            player.setElement(element);
            //Destroy(gameObject);
            //this.gameObject.SetActive(false);
            SetActiveObjects(false);
            timeToRespawn = respawnTimer;

        }

    }

    private void SetActiveObjects(bool active)
    {
        sr.enabled = active;
        box.enabled = active;
    }
}
