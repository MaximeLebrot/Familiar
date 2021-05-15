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
            //playerStats.attributeSet.Find(AbilitySystem.GameplayAttributes.PlayerHealth);
            refillHealth = -(playerStats.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth) - 10);
            playerStats.AbilitySystem.TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerHealth, (float)refillHealth);
            Destroy(this.gameObject);
        }
    }
}