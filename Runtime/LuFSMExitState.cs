using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lu Lib FSM/Create new Exit FSM State")]
public class LuFSMExitState : LuFSMState
{
    public override void EnterState(string transitionMessage = null)
    {
        // trigger transition on upper FSM
        var fsmOwner = owningFSM.Owner;
        if (fsmOwner is LuFSMContainerState ownerState)
        {
            ownerState.OwningFSM.TriggerTransition(StateName, transitionMessage);
        }
        else
        {
            Debug.LogError("Cannot find upper FSM, cannot exit from current FSM");
        }
    }
}
