using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ghost : MonoBehaviour
{

    public Animator ghostAnimator;
    StateMachine ghostState;

    Idle ghostIdleState;
    public AnimationClip idleAnimation;
    Stun ghostStuntState;
    public AnimationClip stunAnimation;

    private void Awake()
    {
        ghostState = new StateMachine();
        ghostIdleState = new Idle(ghostAnimator, idleAnimation);
        ghostStuntState = new Stun(ghostAnimator, stunAnimation, returnStunMethod);

    }

    // Start is called before the first frame update
    void Start()
    {
        ghostState.ChangeState(ghostIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        ghostState.ExecuteState();
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
        }else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(0);
        }

    }
}
