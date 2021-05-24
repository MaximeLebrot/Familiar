using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    private float? refillHealth;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Health Collected");
            //playerStats.attributeSet.Find(AbilitySystem.GameplayAttributes.PlayerHealth);
            refillHealth = -(playerStats.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth) - 10);
            playerStats.AbilitySystem.TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerHealth, (float)refillHealth);
            anim.SetTrigger("isPickedUp"); // Animates the pickup so its smaller + disabled the light component.
            StartCoroutine(WaitAndDisable());
            //Destroy(this.gameObject);
        }
    }

    // Waits so the pick up animation finishes playing
    private IEnumerator WaitAndDisable()
    {
        yield return new WaitForSeconds(1.1f); // OBS!!! The number match animation length
        this.gameObject.SetActive(false);
    }
}