using System;
using UnityEngine;

class Idle : IState
{

    Animator animator;
    AnimationClip animationClip;

    

    public Idle(Animator anim, AnimationClip clip)
    {
        animator = anim;
        animationClip =clip;
    }
    public void Enter()
    {
        animator.Play(animationClip.name, -1, 0f);
    }

    public bool CurrentlyInputLocked()
    {
        throw new NotImplementedException();
    }

    public void Execute()
    {

    }

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