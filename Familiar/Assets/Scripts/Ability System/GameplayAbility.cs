using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class GameplayAbility : ScriptableObject 
    {
        public Player player;
        public GameplayTag AbilityTag;
        public GameplayAttributeSetEntry Cost;

        public abstract void Activate(GameplayAbilitySystem Owner);
        public void Commit(GameplayAbilitySystem Owner)
        {
            player = FindObjectOfType<Player>();
            if (Cost.Attribute != null)
            {
                Owner.TryApplyAttributeChange(Cost.Attribute.GetType(), -Cost.Value);
            }
        }
        public bool CanActivate(GameplayAbilitySystem Owner)
        {
            if (Cost.Attribute == null)
            {
                return true;
            }

            float? Value = Owner.GetAttributeValue(Cost.Attribute.GetType());
            return Value != null && Value > Cost.Value;
        }
    }
}
