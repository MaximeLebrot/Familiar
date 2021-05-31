using UnityEngine;

public class MoveableScript : MoveableBase
{
    [SerializeField] private Transform resetPoint;

    public override bool IsCarried => Carrier != null;

    public override GameObject Carrier
    {
        get;
        set;
    }

    public override Vector3 ResetPoint
    {
        get => resetPoint.position;
        set => resetPoint.position = value;
    }

    // Start is called before the first frame update
    void Awake()
    {
        resetPoint.parent = null;
    }
}
