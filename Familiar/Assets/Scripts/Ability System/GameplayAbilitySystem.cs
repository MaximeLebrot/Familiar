using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AbilitySystem
{
    public class GameplayAbilitySystem : MonoBehaviour
    {
        private Dictionary<Type, float> AttributeSet = new Dictionary<Type, float>();
        private Dictionary<Type, GameplayAbility> GrantedAbilities = new Dictionary<Type, GameplayAbility>();
        
        private Dictionary<Type, Func<float,float>> AttributeSetCalculations = new Dictionary<Type, Func<float,float>>();
        private Dictionary<GameplayEffect, int> ActiveEffects = new Dictionary<GameplayEffect, int>();
        private HashSet<GameplayTag> activeTags = new HashSet<GameplayTag>();

        public void RegisterAttributeSet(List<GameplayAttributeSetEntry> Set)
        {
            Set.ForEach(Entry => AttributeSet.Add(Entry.Attribute.GetType(), Entry.Value));
        }

        public void RegisterAttributeCalculation(Type attribute, Func<float,float> calculation)
        {
            if (!AttributeSetCalculations.ContainsKey(attribute))
            {
                AttributeSetCalculations.Add(attribute, calculation);
            }
            else
            {
                AttributeSetCalculations[attribute] += calculation;
            }
        }

        public float? GetAttributeValue(Type Attribute)
        {
            if (AttributeSet.ContainsKey(Attribute))
            {
                return AttributeSet[Attribute];
            }
            return null;
        }

        public void ApplyEffectToSelf(GameplayEffect effect)
        {
            if (effect.blockedByTags.Any(Tag => activeTags.Contains(Tag)))
            {
                return;
            }

            if (effect.attribute != null)
            {
                TryApplyAttributeChange(effect.attribute.GetType(), effect.value);
            }

            effect.appliedTags.ForEach(Tag => activeTags.Add(Tag));

            //you were here
        }

        public void TryApplyAttributeChange(Type Attribute, float Value)
        {
            if (AttributeSet.ContainsKey(Attribute))
            {
                if (AttributeSetCalculations.ContainsKey(Attribute))
                {
                    var Calculations = AttributeSetCalculations[Attribute];
                    foreach (var Calc in Calculations.GetInvocationList())
                    {
                        //Value((Func<float, float>)Calc)(Value);
                    }
                }
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
                if (!Ability.blockedByTags.Any(Tag => activeTags.Contains(Tag)))
                    Ability.Activate(this);
                return true;
            }
            return false;
        }
        public bool TryActivateAbilityByTag(GameplayTag AbilityTag)
        {
            return TryActivateAbilityByTag(AbilityTag.GetType());
        }
    }
}
