using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuFSMTransition : IComparable<LuFSMTransition>
{
    public string Label { get; private set; }
    public string Message { get; private set; }
    private int Priority { get; set; }
    

    public LuFSMTransition(string label, string message = null, int priority = 0)
    {
        Label = label;
        Message = message;
        Priority = priority;
    }

    public int CompareTo(LuFSMTransition other)
    {
        return other == null ? 1 : Priority.CompareTo(other.Priority);
    }
}
