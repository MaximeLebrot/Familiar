using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    public abstract bool IsCarried
    {
        get; set;
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
