using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Lu Lib FSM/Create new FSM")]
public class LuFSM : ScriptableObject
{
    public static readonly string FSM_DEFAULT_ENTRY_POINT = "default";
    private LuFSMState currentState;
    private List<LuFSMTransition> pendingTransitions = new List<LuFSMTransition>();
    private Object owner;

    public Object Owner => owner;

    [SerializeField] public List<LuFSMRelation> relations;
    [SerializeField] public List<LuFSMStateTransition> entryPoints = new(){new LuFSMStateTransition(FSM_DEFAULT_ENTRY_POINT)};

    #region FSM flow
    
    public void StartFSM(string entryLabel, Object fsmOwner)
    {
        owner = fsmOwner;
        
        currentState = GetEntryState(entryLabel, out string stateName);
        currentState.SetupState(this, stateName);
        currentState.EnterState(entryLabel);
        
        Debug.Log($"LuFSM.StartFSM: FSM {name} - Entering in state {currentState.StateName}.");
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
                nextState.SetupState(this, stateTransition.ToStateName);
                nextState.EnterState(message);
                currentState = nextState;
                Debug.Log($"LuFSM.UpdateFSM: FSM {name} - Entering in state {currentState.StateName}.");
            }
            else
            {
                Debug.LogError($"LuFSM.UpdateFSM: FSM {name} - There are {pendingTransitions.Count} transitions but no one triggered.");
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
        Debug.Log($"LuFSM.ExitFSM: FSM {name} - Exiting from state {currentState.StateName}.");
        currentState.ExitState();
    }

    public void HandleAction(string action, string parameter)
    {
        Debug.Log($"LuFSM.HandleAction: FSM {name} - Triggering action {action} with parameter {parameter} in state {currentState.StateName}.");
        currentState.HandleAction(action, parameter);
    }

    public void TriggerTransition(string label, string message = null, int priority = 0)
    {
        pendingTransitions.Add(new LuFSMTransition(label, message, priority));
    }

    #endregion

    #region FSM utilities
    
    public bool IsValidFSM()
    {
        return relations is { Count: > 0 };
    }
    
    private LuFSMState GetEntryState(string entryLabel, out string stateName)
    {
        LuFSMStateTransition entryPoint = entryPoints?.Find(e => e.TransitionName == entryLabel);
        if (entryPoint != null)
        {
            stateName = entryPoint.ToStateName;
            return GetStateFromName(entryPoint.ToStateName);
        }

        stateName = relations[0].StateName;
        return relations[0].State;
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
    
    #endregion
}
