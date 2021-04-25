using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUntilStartScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
