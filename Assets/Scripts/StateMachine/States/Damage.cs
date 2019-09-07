using UnityEngine;


public class Damage : IState
{
    Animator animator;
    AnimationClip animationClip;
    float animationDuration;
    float animationTime;
    Rigidbody playerRidgidBody;
    ValidateSwitchToIdle switchToIdle;

    public Damage(Animator anim, AnimationClip clip, Rigidbody PplayerRidgidBody,int frames, ValidateSwitchToIdle switchIdleMethod)
    {
        animator = anim;
        animationClip = clip;
        animationDuration = frames;
        playerRidgidBody = PplayerRidgidBody;
        switchToIdle = switchIdleMethod;
    }
    public bool CurrentlyInputLocked()
    {
        throw new System.NotImplementedException();
    }

    public void Enter()
    {
        animator.Play(animationClip.name, -1, 0f);
        animationTime = 0f;
    }

    public void Execute()
    {
        if (animationTime >= animationDuration)
        {
            //switchToIdle();
        }
        else
        {
            animationTime += Time.deltaTime;
        }
    }

    public void Exit()
    {
        
    }

    public void FixedExecute()
    {
        
    }

    public bool ReadyToEjectCartridge()
    {
        throw new System.NotImplementedException();
    }

    public delegate void ValidateSwitchToIdle();
}
