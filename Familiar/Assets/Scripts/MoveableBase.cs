using UnityEngine;
using Unity;

public abstract class MoveableBase : MonoBehaviour, IMoveable
{
    public abstract bool IsCarried
    {
        get;
    }

    public abstract GameObject Carrier
    {
        get;
        set;
    }

    public abstract Vector3 ResetPoint
    {
        get;
        set;
    }
}