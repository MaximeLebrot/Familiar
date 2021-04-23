using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : PickupItem
{
    private float? refillMana;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Mana Collected");
            refillMana = -(playerStats.GetAbilitySystem().GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerMana) - 10);
            playerStats.GetAbilitySystem().TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerMana, (float)refillMana);
            Destroy(this.gameObject);
        }
    }
}
