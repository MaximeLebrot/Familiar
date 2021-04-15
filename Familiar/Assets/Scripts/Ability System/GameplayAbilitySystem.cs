using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class GameplayAbilitySystem : MonoBehaviour
    {
        private Dictionary<Type, float> AttributeSet = new Dictionary<Type, float>();
        private Dictionary<Type, GameplayAbility> GrantedAbilities = new Dictionary<Type, GameplayAbility>();

        public void RegisterAttributeSet(List<GameplayAttributeSetEntry> Set)
        {
            Set.ForEach(Entry => AttributeSet.Add(Entry.Attribute.GetType(), Entry.Value));
        }

        public float? GetAttributeValue(Type Attribute)
        {
            if (AttributeSet.ContainsKey(Attribute))
            {
                return AttributeSet[Attribute];
            }
            return null;
        }

        public void TryApplyAttributeChange(Type Attribute, float Value)
        {
            if (AttributeSet.ContainsKey(Attribute))
            {
                AttributeSet[Attribute] += Value;
            }
        }

        public void GrantAbility(GameplayAbility Ability)
        {
            GrantedAbilities[Ability.AbilityTag.GetType()] = Ability;
        }

        public void RevokeAbility(Type AbilityTag)
        {
            GrantedAbilities.Remove(AbilityTag);
        }
        public void RevokeAbility(GameplayTag AbilityTag)
        {
            RevokeAbility(AbilityTag.GetType());
        }

        public bool TryActivateAbilityByTag(Type AbilityTag)
        {
            GameplayAbility Ability;
            if (GrantedAbilities.TryGetValue(AbilityTag, out Ability))
            {
                if (Ability.CanActivate(this))
                {
                    Ability.Activate(this);
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public bool TryActivateAbilityByTag(GameplayTag AbilityTag)
        {
            return TryActivateAbilityByTag(AbilityTag.GetType());
        }
    }
}
