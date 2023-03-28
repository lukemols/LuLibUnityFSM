using UnityEngine;

[CreateAssetMenu(menuName = "Lu Lib FSM/Create new FSM Container State")]
public class LuFSMContainerState : LuFSMState
{
    [SerializeField] public LuFSM stateFSM;

    public override void EnterState(string transitionMessage = null)
    {
        stateFSM.StartFSM(transitionMessage, this);
    }

    public override void UpdateState()
    {
        stateFSM.UpdateFSM();
    }

    public override void ExitState()
    {
        stateFSM.ExitFSM();
    }

    public override void HandleAction(string action, string parameter)
    {
        stateFSM.HandleAction(action, parameter);
    }
}
