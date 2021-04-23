using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class GameplayAbility : ScriptableObject 
    {
        protected Player player;
        public GameplayTag abilityTag;
        public GameplayEffect appliedEffect;
        public GameplayEffect cost;
        public GameplayEffect cooldown;

        public List<GameplayTag> blockedByTags;
        //public List<GameplayTag> requiredTags;

        public abstract void Activate(GameplayAbilitySystem owner);
        public void Commit(GameplayAbilitySystem owner)
        {
            player = FindObjectOfType<Player>();
            if (cost.attribute != null)
            {
                owner.TryApplyAttributeChange(cost.attribute.GetType(), -cost.value);
                Debug.Log("Applied AttributeChange");
            }
            //if (cooldown != null)
                //owner.ApplyEffectToSelf(cooldown);
        }
        public bool CanActivate(GameplayAbilitySystem owner)
        {
            if (cost.attribute == null)
            {
                return true;
            }

            float? value = owner.GetAttributeValue(cost.attribute.GetType());
            return value != null && value > cost.value;
        }
    }
}
