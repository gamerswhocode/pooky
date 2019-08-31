using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    void Enter();
    void Execute();
    void Exit();
    void FixedExecute();

    ///<sumary>State is done doing everything; PLEASE EJECT ME!</sumary>
    bool ReadyToEjectCartridge();

    ///<sumary>State is not recieving new inputs atm</sumary>
    bool CurrentlyInputLocked();

}
