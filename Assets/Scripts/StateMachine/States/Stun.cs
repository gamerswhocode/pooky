using System;
using UnityEngine;


class Stun : IState
{
    Animator animator;
    AnimationClip animationClip;
    float clipDuration;
    float currentClipTime;
    StuntreturnMethod stunMethod;


    public Stun(Animator anim, AnimationClip clip, StuntreturnMethod stuntreturnMethod)
    {
        animator = anim;
        animationClip = clip;
        clipDuration = clip.length;
        stunMethod = stuntreturnMethod;
    }
    public void Enter()
    {
        animator.Play(animationClip.name, -1, 0f);
        currentClipTime = 0f;
    }

    public bool CurrentlyInputLocked()
    {
        throw new NotImplementedException();
    }


    public void Execute()
    {
        if(currentClipTime <=clipDuration)
        {
            currentClipTime += Time.deltaTime;
        }
        else
        {
            stunMethod();
        }
    }
    public delegate void StuntreturnMethod();

    public void Exit()
    {
        
    }

    public void FixedExecute()
    {
        
    }

    public bool ReadyToEjectCartridge()
    {
        throw new NotImplementedException();
    }
}

