using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : PickupItem
{
    private float? refillMana;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mana Collected");
            refillMana = -(playerStats.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerMana) - 10);
            playerStats.AbilitySystem.TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerMana, (float)refillMana);
            Destroy(this.gameObject);
        }
    }
}
