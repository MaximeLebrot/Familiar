using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricitySystemScript : MonoBehaviour
{
    //    public delegate void ZapAction();
    //    public static event ZapAction IsZapping;

    public bool IsActivatedThisCycle
    {
        get
        {
            return GetComponentInChildren<ElectricalSwitchScript>().IsPowered;
        } 
        //set;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Light>(true).gameObject.SetActive(IsActivatedThisCycle);
    }
}
