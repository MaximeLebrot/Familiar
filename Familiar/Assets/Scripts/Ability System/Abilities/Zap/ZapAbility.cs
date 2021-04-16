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
        public override void Activate(GameplayAbilitySystem Owner)
        {
            //if (CanActivate(Owner))
            //wait for anim
            Commit(Owner); 
            shoot = player.GetComponent<ShootingScript>();
            shoot.Shoot();
            //otherAbilitySystem = hit.GetComponent<GameplayAbilitySystem>(); ray hit eller liknande
            otherAbilitySystem.ApplyEffectToSelf(appliedEffect);
            Debug.Log(Owner.GetAttributeValue(cost.attribute.GetType()));
           
            //doZap
        }
    }
}