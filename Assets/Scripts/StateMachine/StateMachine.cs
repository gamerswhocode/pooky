using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    private IState currentlyRunning;
    private IState previousState;


    public void ChangeState(IState newState)
    {
        if (this.currentlyRunning != null)
        {
            this.currentlyRunning.Exit();
        }
        this.previousState = this.currentlyRunning;

        this.currentlyRunning = newState;
        this.currentlyRunning.Enter();

    }

    public void ExecuteState()
    {
        if (this.currentlyRunning != null)
            this.currentlyRunning.Execute();
    }

    public void FixedExecuteState()
    {
        if (this.currentlyRunning != null)
            this.currentlyRunning.FixedExecute();
    }

    public void SwitchToPreviousState()
    {
        this.currentlyRunning.Exit();
        this.currentlyRunning = this.previousState;
        this.currentlyRunning.Enter();
    }

    public bool CanEjectCartridge()
    {
        if (this.currentlyRunning != null)
            return this.currentlyRunning.ReadyToEjectCartridge();
        else
            return false;
    }

    public bool CurrentlyInputLocked()
    {
        if (this.currentlyRunning != null)
            return this.currentlyRunning.CurrentlyInputLocked();
        else
            return false;
    }

    public IState GetCurrentState()
    {
        return currentlyRunning;
    }

}
