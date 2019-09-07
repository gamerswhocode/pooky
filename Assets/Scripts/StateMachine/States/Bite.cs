

using UnityEngine;

public class Bite : IState
{

    Animator animator;
    AnimationClip animationClip;
    float animationDuration;
    float animationTime;

    ValidateSwitchToIdle switchToIdle;


    public Bite(Animator anim, AnimationClip clip, ValidateSwitchToIdle switchIdleMethod)
    {
        animator = anim;
        animationClip = clip;
        animationDuration = clip.length;
        switchToIdle = switchIdleMethod;

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
            switchToIdle();
        }
        else
        {
            animationTime += Time.deltaTime;
        }

    }
    public bool CurrentlyInputLocked()
    {
        throw new System.NotImplementedException();
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
