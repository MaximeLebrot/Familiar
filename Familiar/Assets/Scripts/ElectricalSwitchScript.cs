using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalSwitchScript : MonoBehaviour
{
    public SwitchType switchType;
    public bool IsPowered
    {
        get;
        private set;
    }

    private void Awake()
    {
        
    }

    void Start()
    {
        IsPowered = false;
    }

    void Update()
    {
        if (switchType == SwitchType.ContinuousInput)
        {
            IsPowered = false;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void OnZap()
    {
        Debug.Log("zap");
        if (switchType == SwitchType.Toggled && Input.GetButtonDown("Fire1"))
            IsPowered = !IsPowered;
        else if (switchType == SwitchType.ContinuousInput)
            IsPowered = true;
    }

    public enum SwitchType
    {
        Toggled,
        ContinuousInput
    }
}
