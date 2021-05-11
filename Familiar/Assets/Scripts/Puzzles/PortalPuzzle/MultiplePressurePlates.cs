using UnityEngine;

public class MultiplePressurePlates : MonoBehaviour
{
    public bool active;
    private PressurePlatePuzzle parent;
    private Material newMat;
    private MeshRenderer meshRenderer;
    private Animator anim;

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
            anim.SetTrigger("Unclick");
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
