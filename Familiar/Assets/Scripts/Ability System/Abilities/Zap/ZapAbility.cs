using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/ZapAbility")]
    public class ZapAbility : GameplayAbility
    {
        ShootingScript shoot;
        GameplayAbilitySystem otherAbilitySystem;

        public override void Activate(GameplayAbilitySystem owner)
        {
            //wait for anim
            Commit(owner);
            shoot = player.GetComponent<ShootingScript>();
            shoot.Shoot();
            player.audioHandler.PlayZappingSound();
            //otherAbilitySystem = hit.GetComponent<GameplayAbilitySystem>(); ray hit eller liknande
            //otherAbilitySystem.ApplyEffectToSelf(appliedEffect);
            //Debug.Log(cost.attribute.name + " = " + owner.GetAttributeValue(cost.attribute.GetType()));

            //doZap logik bör vara här
        }
    }
}