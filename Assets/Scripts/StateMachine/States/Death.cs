using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : IState
{

    Animator animator;
    AnimationClip animationClip;
    float animationDuration;
    float animationTime;
    Transform playerTransform;
    ValidateSwitchToDeath switchToDeath;

    public Death(Animator anim, AnimationClip clip, ValidateSwitchToDeath dead,Transform playerTransformParam)
    {
        animator = anim;
        animationClip = clip;
        animationDuration = clip.length;
        switchToDeath = dead;
        playerTransform = playerTransformParam;
    }
    public bool CurrentlyInputLocked()
    {
        return false;
    }

    public void Enter()
    {
        animator.Play(animationClip.name, -1, 0f);
    }

    public void Execute()
    {
        if (animationTime >= animationDuration)
        {
            switchToDeath();
        }
        else
        {
            //if (animationTime < 10)
            //{
            //    playerTransform.Rotate(0f, 0f, 1, Space.Self);
            //}
            //else
            //    playerTransform.rotation = new Quaternion(0, 0, 0, 0);
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
        return false;
    }

    public delegate void ValidateSwitchToDeath();
}
