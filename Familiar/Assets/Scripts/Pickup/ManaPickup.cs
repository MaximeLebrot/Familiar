using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : PickupItem
{
    private float? refillMana;

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Mana Collected");
            refillMana = -(playerStats.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerMana) - 10);
            playerStats.AbilitySystem.TryApplyAttributeChange(AbilitySystem.GameplayAttributes.PlayerMana, (float)refillMana);
            Destroy(this.gameObject);
        }
    }
}
