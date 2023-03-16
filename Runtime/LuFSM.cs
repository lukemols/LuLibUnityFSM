using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lu Lib FSM/Create new FSM")]
public class LuFSM : ScriptableObject
{
    public static readonly string FSM_DEFAULT_ENTRY_POINT = "default";
    private LuFSMState currentState;
    private List<LuFSMTransition> pendingTransitions = new List<LuFSMTransition>();

    [SerializeField] public List<LuFSMRelation> relations;
    [SerializeField] public List<LuFSMStateTransition> entryPoints = new(){new LuFSMStateTransition(FSM_DEFAULT_ENTRY_POINT)};

    public bool IsValidFSM()
    {
        return relations is { Count: > 0 };
    }
    
    public void StartFSM(string entryLabel)
    {
        currentState = GetEntryState(entryLabel);
        currentState.EnterState(entryLabel);
    }

    public void UpdateFSM()
    {
        if (pendingTransitions.Count > 0)
        {
            pendingTransitions.Sort();
            string message = null;
            LuFSMStateTransition stateTransition = null;
            LuFSMRelation relation = GetCurrentStateRelation();

            foreach (LuFSMTransition transition in pendingTransitions)
            {
                stateTransition = relation.transitions.Find(t => t.TransitionName == transition.Label);
                if (stateTransition != null)
                {
                    message = transition.Message;
                    break;
                }
            }

            if (stateTransition != null)
            {
                LuFSMState nextState = GetStateFromName(stateTransition.ToStateName);
                currentState.ExitState();
                nextState.EnterState(stateTransition.ToStateName, message);
                currentState = nextState;
            }
            
            pendingTransitions.Clear();
        }
        else
        {
            currentState.UpdateState();
        }
    }

    public void ExitFSM()
    {
        currentState.ExitState();
    }

    private LuFSMState GetEntryState(string entryLabel)
    {
        LuFSMStateTransition entryPoint = entryPoints?.Find(e => e.TransitionName == entryLabel);
        return entryPoint != null ? GetStateFromName(entryPoint.ToStateName) : relations[0].State;
    }

    private LuFSMState GetStateFromName(string stateName)
    {
        LuFSMRelation relation = GetStateRelation(stateName);
        return relation?.State;
    }
    
    private LuFSMRelation GetStateRelation(string stateName)
    {
        return relations.Find(r => r.StateName == stateName);
    }

    private LuFSMRelation GetCurrentStateRelation()
    {
        return GetStateRelation(currentState.StateName);
    }
}
