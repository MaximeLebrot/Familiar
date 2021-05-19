using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>(); //vild kod
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Key")
        {
            animator.SetBool("open", true); //vild kod
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
