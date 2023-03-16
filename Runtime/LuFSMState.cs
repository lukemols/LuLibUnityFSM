using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lu Lib FSM/Create new FSM State")]
public class LuFSMState : ScriptableObject
{
    public string StateName { get; private set; }
    
    public void EnterState(string givenName, string transitionMessage = null)
    {
        StateName = givenName;
        OnEnterState(transitionMessage);
    }
    
    public void UpdateState()
    {
        OnUpdateState();
    }
    
    public void ExitState()
    {
        OnExitState();
    }
    
    protected virtual void OnEnterState(string transitionMessage = null) { }
    protected virtual void OnUpdateState() { }
    protected virtual void OnExitState() { }
}
