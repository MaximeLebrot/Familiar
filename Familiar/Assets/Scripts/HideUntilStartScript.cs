using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUntilStartScript : MonoBehaviour
{
    public Behaviour component;

    private void Awake()
    {
        try
        {
            component.enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        } catch (Exception) { }
    }
}
