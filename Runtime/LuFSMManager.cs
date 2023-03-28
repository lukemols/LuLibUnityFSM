using UnityEngine;

public class LuFSMManager : MonoBehaviour
{
    [SerializeField] public LuFSM startingFSM;
    
    private LuFSM currentFSM;
    
    private void Awake()
    {
        currentFSM = startingFSM;
        if (currentFSM == null || !currentFSM.IsValidFSM())
        {
            enabled = false;
            Debug.LogError("LuFSMManager.Awake: Invalid FSM");
        }
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
        if (newFSM != null && newFSM.IsValidFSM())
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
            var entry = entryPoint ?? LuFSM.FSM_DEFAULT_ENTRY_POINT;
            Debug.Log($"LuFSMManager.StartFSM: Starting FSM {currentFSM.name} with entry point: {entry}");
            currentFSM.StartFSM(entry, this);
        }
    }
    
    private void UpdateFSM()
    {
        if (currentFSM != null)
        {
            currentFSM.UpdateFSM();
        }
    }

    public void FireAction(string action, string parameter)
    {
        if (currentFSM != null)
        {
            currentFSM.HandleAction(action, parameter);
        }
    }
}
