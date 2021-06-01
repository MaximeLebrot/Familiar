using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonstonePickup : PickupItem
{
    private Animator anim;
    //[SerializeField] private GameObject child;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
                ps.Stop();
  

            anim.SetTrigger("isPickedUp"); // Animates the pickup so its smaller + disabled the light component.
            StartCoroutine(WaitAndDisable());
            //Destroy(this.gameObject);
        }
    }

    // Waits so the pick up animation finishes playing
    private IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(2.0f); // OBS!!! The number match animation length
        this.gameObject.SetActive(false);
    }
}