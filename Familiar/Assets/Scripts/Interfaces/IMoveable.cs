using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    /// <summary>
    /// Due to Unity .NET limitations, no default implementation can be used. Should always be implemented as 
    /// <code>get => Carrier != null</code>
    /// </summary>
    public bool IsCarried
    {
        get;
    }

    public GameObject Carrier
    {
        get; set;
    }

    public abstract Vector3 ResetPoint
    {
        get;
        set;
    }
}