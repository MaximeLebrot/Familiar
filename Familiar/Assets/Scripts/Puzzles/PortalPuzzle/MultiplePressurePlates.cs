using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePressurePlates : MonoBehaviour
{
    public PressurePlatePuzzle parent;
    public bool active;
    private Animator anim;
    public MeshRenderer meshRenderer;
    public Material newMat;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        parent = GetComponentInParent<PressurePlatePuzzle>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetTrigger("Click");
            active = true;
            parent.UpdatePuzzle();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            active = false;
            parent.UpdatePuzzle();
        }
    }

    public void ChangeMaterial()
    {
        //audio feedback
        meshRenderer.material = newMat;
    }
}
