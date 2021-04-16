using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/ZapAbility")]
    public class ZapAbility : GameplayAbility
    {
        ShootingScript shoot;
        public override void Activate(GameplayAbilitySystem Owner)
        {
            //if (CanActivate(Owner))
            Commit(Owner); 
            shoot = player.GetComponent<ShootingScript>();
            shoot.Shoot();
            Debug.Log(Owner.GetAttributeValue(Cost.attribute.GetType()));
           
            //doZap
        }
    }
}