using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuFSMManager : MonoBehaviour
{
    [SerializeField] public LuFSM startingFSM;
    
    private LuFSM currentFSM;
    
    private void Awake()
    {
        currentFSM = startingFSM;
    }

    private void Start()
    {
        StartFSM();
    }

    private void Update()
    {
        UpdateFSM();
    }

    public void OpenFSM(LuFSM newFSM, string entryPoint = null)
    {
        if (newFSM != null)
        {
            if (currentFSM != null)
            {
                currentFSM.ExitFSM();
            }

            currentFSM = newFSM;
            StartFSM(entryPoint);
        }
    }

    private void StartFSM(string entryPoint = null)
    {
        if (currentFSM != null)
        {
            currentFSM.StartFSM(entryPoint ?? LuFSM.FSM_DEFAULT_ENTRY_POINT);
        }
    }
    
    private void UpdateFSM()
    {
        if (currentFSM != null)
        {
            currentFSM.UpdateFSM();
        }
    }
}
