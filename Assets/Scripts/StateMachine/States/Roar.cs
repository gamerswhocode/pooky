using System;
using UnityEngine;


class Roar : IState
{

    Animator animator;
    AnimationClip animationClip;
    float animationDuration;
    float animationTime;

    ValidateSwitchToIdle switchToIdle;


    public Roar(Animator anim, AnimationClip clip, ValidateSwitchToIdle switchIdleMethod)
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
    public bool CurrentlyInputLocked()
    {
        throw new NotImplementedException();
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

    public void Exit()
    {
        
    }

    public void FixedExecute()
    {
        
    }

    public bool ReadyToEjectCartridge()
    {
        return true;
    }

    public delegate void ValidateSwitchToIdle();
        
}
