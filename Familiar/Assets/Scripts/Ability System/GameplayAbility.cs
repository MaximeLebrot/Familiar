using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class GameplayAbility : ScriptableObject 
    {
        public Player player;
        public GameplayTag AbilityTag;
        public GameplayEffect Effect;
        public /*GameplayAttributeSetEntry*/GameplayEffect Cost;

        public List<GameplayTag> blockedByTags;

        public abstract void Activate(GameplayAbilitySystem Owner);
        public void Commit(GameplayAbilitySystem Owner)
        {
            player = FindObjectOfType<Player>();
            if (Cost.attribute != null)
            {
                Owner.TryApplyAttributeChange(Cost.attribute.GetType(), -Cost.value);
            }
        }
        public bool CanActivate(GameplayAbilitySystem Owner)
        {
            if (Cost.attribute == null)
            {
                return true;
            }

            float? Value = Owner.GetAttributeValue(Cost.attribute.GetType());
            return Value != null && Value > Cost.value;
        }
    }
}
