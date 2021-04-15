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
            Commit(Owner);
            Debug.Log(Owner.GetAttributeValue(Cost.Attribute.GetType()));
            shoot = player.GetComponent<ShootingScript>();
            shoot.Shoot();
            //doZap
        }
    }
}