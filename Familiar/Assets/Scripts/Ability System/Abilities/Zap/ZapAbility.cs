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

            if (shoot.canFire)
            {
                //shoot.Shoot();
                shoot.canFire = false;
                shoot.StartCoroutine(shoot.ResetCanFire());
                player.anim.SetTrigger("attack");
                //player.audioHandler.PlayZappingSound();
            }
           
            //otherAbilitySystem = hit.GetComponent<GameplayAbilitySystem>(); ray hit eller liknande
            //otherAbilitySystem.ApplyEffectToSelf(appliedEffect);
            //Debug.Log(cost.attribute.name + " = " + owner.GetAttributeValue(cost.attribute.GetType()));

            //doZap logik b�r vara h�r
        }
    }
}