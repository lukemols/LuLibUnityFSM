using UnityEngine;

[CreateAssetMenu(menuName = "Lu Lib FSM/Create new FSM State")]
public class LuFSMState : ScriptableObject
{
    public string StateName { get; private set; }
    protected LuFSM owningFSM;

    public LuFSM OwningFSM => owningFSM;

    public void SetupState(LuFSM owningFSM, string givenName = null)
    {
        this.owningFSM = owningFSM;
        StateName = givenName ?? StateName;
    }
    
    public virtual void EnterState(string transitionMessage = null) { }
    
    public virtual void UpdateState() { }
    
    public virtual void ExitState() { }
    
    public virtual void HandleAction(string action, string parameter) { }
}
