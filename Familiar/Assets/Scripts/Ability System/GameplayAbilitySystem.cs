using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AbilitySystem
{
    public class GameplayAbilitySystem : MonoBehaviour
    {
        private Dictionary<Type, float> attributeSet = new Dictionary<Type, float>();
        private Dictionary<Type, GameplayAbility> grantedAbilities = new Dictionary<Type, GameplayAbility>();
        private Dictionary<Type, Func<float,float>> attributeSetCalculations = new Dictionary<Type, Func<float,float>>();
        private Dictionary<Type, Action<float>> onAttributeChanged = new Dictionary<Type, Action<float>>();
        private Dictionary<GameplayEffect, int> activeEffects = new Dictionary<GameplayEffect, int>();
        private HashSet<GameplayTag> activeTags = new HashSet<GameplayTag>();

        public void RegisterAttributeSet(List<GameplayAttributeSetEntry> Set)
        {
            Set.ForEach(Entry => attributeSet.Add(Entry.attribute.GetType(), Entry.value));
        }

        public void RegisterAttributeCalculation(Type attribute, Func<float,float> calculation)
        {
            if (!attributeSetCalculations.ContainsKey(attribute))
            {
                attributeSetCalculations.Add(attribute, calculation);
            }
            else
            {
                attributeSetCalculations[attribute] += calculation;
            }
        }

        public float? GetAttributeValue(Type Attribute)
        {
            if (attributeSet.ContainsKey(Attribute))
            {
                return attributeSet[Attribute];
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

            switch (effect.effectType)
            {
                case EffectDurationType.Instant: break;
                case EffectDurationType.Duration:
                {
                    StartCoroutine(RemoveAfterTime(effect));
                    if (!activeEffects.ContainsKey(effect))
                    {
                        activeEffects.Add(effect, 1);
                    }
                    else
                    {
                        activeEffects[effect]++;
                    }
                } break;
                case EffectDurationType.Infinite:
                {
                    if (!activeEffects.ContainsKey(effect))
                    {
                        activeEffects.Add(effect, 1);
                    }
                    else
                    {
                        activeEffects[effect]++;
                    }
                } break;
            }
        }

        public void TryApplyAttributeChange(Type attribute, float value)
        {
            if (attributeSet.ContainsKey(attribute))
            {
                if (attributeSetCalculations.ContainsKey(attribute))
                {
                    var Calculations = attributeSetCalculations[attribute];
                    foreach (var Calc in Calculations.GetInvocationList())
                    {
                        value = ((Func<float, float>)Calc)(value);
                    }
                }
                attributeSet[attribute] += value;
                onAttributeChanged[attribute]?.Invoke(attributeSet[attribute]);
            }
        }

        public void GrantAbility(GameplayAbility ability)
        {
            grantedAbilities[ability.abilityTag.GetType()] = ability;
        }

        public void RevokeAbility(Type abilityTag)
        {
            grantedAbilities.Remove(abilityTag);
        }
        public void RevokeAbility(GameplayTag abilityTag)
        {
            RevokeAbility(abilityTag.GetType());
        }

        public bool TryActivateAbilityByTag(Type abilityTag)
        {
            GameplayAbility ability;
            if (grantedAbilities.TryGetValue(abilityTag, out ability))
            {
                if (!ability.blockedByTags.Any(Tag => activeTags.Contains(Tag)))
                    ability.Activate(this);
                return true;
            }
            return false;
        }
        public bool TryActivateAbilityByTag(GameplayTag abilityTag)
        {
            return TryActivateAbilityByTag(abilityTag.GetType());
        }
        public IEnumerator RemoveAfterTime(GameplayEffect effect)
        {
            yield return new WaitForSeconds(effect.duration);
            // TODO: Remove gameplay tags added by this effect
            activeEffects[effect]--;
            if (activeEffects[effect] <= 0)
            {
                activeEffects.Remove(effect);
            }
        }
    }
}
