using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LuFSMRelation
{
    [SerializeField] public string StateName;
    [SerializeField] public LuFSMState State;
    [SerializeField] public List<LuFSMStateTransition> transitions;
}

[System.Serializable]
public class LuFSMStateTransition
{
    [SerializeField] public string TransitionName;
    [SerializeField] public string ToStateName;

    public LuFSMStateTransition() { }

    public LuFSMStateTransition(string transitionName)
    {
        TransitionName = transitionName;
    }
}
