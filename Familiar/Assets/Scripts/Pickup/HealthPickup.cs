using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickupItem
{
    private float? refillHealth;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Health Collected");
            playerStats.attributeSet.Find(AbilitySystem.GameplayAttributes.PlayerHealth);
            refillHealth = -(playerStats.GetAbilitySystem().GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth) - 10);
            playerStats.GetAbilitySystem().TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerMana, (float)refillHealth);
            Destroy(this.gameObject);
        }
    }
}