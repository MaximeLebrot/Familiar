using UnityEngine;

public class MultiplePressurePlates : MonoBehaviour
{
    [Tooltip("The game object that should open in case of puzzle completion")]
    private bool isActive;
    [SerializeField, Tooltip("A reference to the parent (puzzle) tied this pressure plate")]
    private PressurePlatePuzzle parent;
    [SerializeField, Tooltip("The material swapped to after completion")]
    private Material newMat;
    [SerializeField, Tooltip("A reference to the mesh renderer attached to this game object. Should be inputed manually")]
    private MeshRenderer meshRenderer;
    [SerializeField, Tooltip("A reference to the animator attached to this game object. Should be inputed manually")]
    private Animator anim;

    void Start()
    {
        InitializeSequence();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetTrigger("Click");
            isActive = true;
            parent.UpdatePuzzle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Moveable"))
        {
            anim.SetTrigger("Unclick");
            isActive = false;
            parent.UpdatePuzzle();
        }
    }

    private void InitializeSequence()
    {
        InitializeChildren();
        InitializeAnim();
        InitializeMeshRenderer();
        InitializeNewMaterial();
    }

    private void InitializeNewMaterial()
    {
        if (newMat == null)
        {
            Debug.LogWarning("New material value should be set in the inspector");
            //
            //newMat = Material.
        }
    }

    private void InitializeMeshRenderer()
    {
        if (meshRenderer == null)
        {
            Debug.LogWarning("Mesh renderer value should be set in the inspector");
            meshRenderer = GetComponent<MeshRenderer>();

            if (meshRenderer == null)
                Debug.LogError("Cannot find mesh renderer"); //skulle kunna ge mer info -> typ att jag använder GetComponent
        }
    }

    private void InitializeAnim()
    {
        if (anim == null)
        {
            Debug.LogWarning("Anim value should be set in the inspector");
            anim = gameObject.GetComponent<Animator>();

            if (anim == null)
                Debug.LogError("Cannot find anim"); //skulle kunna ge mer info -> typ att jag använder GetComponent
        }
    }

    private void InitializeChildren()
    {
        if (parent == null)
        {
            parent = GetComponentInParent<PressurePlatePuzzle>();
            Debug.LogWarning("Parent of pressure plate value should be set in the inspector");

            if (parent == null)
                Debug.LogError("Cannot find parent of pressure plates"); //skulle kunna ge mer info -> typ att jag använder GetComponentInParent
        }
    }

    public void ChangeMaterial()
    {
        //audio feedback
        meshRenderer.material = newMat;
    }

    public bool IsActive
    {
        get => isActive;
    }
}
