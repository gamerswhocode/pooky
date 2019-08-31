using System;
using UnityEngine;

    class Moving : IState
    {

    Animator animator;
    AnimationClip animationClip;



    public Moving(Animator anim, AnimationClip clip)
    {
        animator = anim;
        animationClip = clip;
    }
    public void Enter()
    {
        animator.Play(animationClip.name, -1, 0f);
    }

    public void Execute()
    {

    }

    public bool CurrentlyInputLocked()
        {
            throw new NotImplementedException();
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
