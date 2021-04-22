using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AbilitySystem_V3
{
    public class GameplayAbilitySystem : MonoBehaviour
    {
        private Dictionary<Type, float> AttributeSet = new Dictionary<Type, float>();
        private Dictionary<Type, GameplayAbility> GrantedAbilities = new Dictionary<Type, GameplayAbility>();
        private Dictionary<Type, Func<float, float>> AttributeSetCalculations = new Dictionary<Type, Func<float, float>>();
        private Dictionary<Type, Action<float>> OnAttributeChanged = new Dictionary<Type, Action<float>>();
        private Dictionary<GameplayEffect, int> ActiveEffects = new Dictionary<GameplayEffect, int>();
        private HashSet<GameplayTag> ActiveTags = new HashSet<GameplayTag>();
       
        public void RegisterAttributeSet(List<GameplayAttributeSetEntry> Set)
        {
            Set.ForEach(Entry => AttributeSet.Add(Entry.Attribute.GetType(), Entry.Value));
        }

        public void RegisterAttributeCalculation(Type Attribute, Func<float, float> Calculation)
        {
            if (!AttributeSetCalculations.ContainsKey(Attribute))
            {
                AttributeSetCalculations.Add(Attribute, Calculation);
            }
            else
            {
                AttributeSetCalculations[Attribute] += Calculation;
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

        public void ApplyEffectToSelf(GameplayEffect Effect)
        {
            if (Effect.BlockedByTags.Any(Tag => ActiveTags.Contains(Tag)))
            {
                return;
            }

            if (Effect.Attribute != null)
            {
                TryApplyAttributeChange(Effect.Attribute.GetType(), Effect.Value);
            }

            Effect.AppliedTags.ForEach(Tag => ActiveTags.Add(Tag));

            switch (Effect.EffectType)
            {
                case EffectDurationType.Instant: break;
                case EffectDurationType.Duration:
                {
                    StartCoroutine(RemoveAfterTime(Effect));
                    if (!ActiveEffects.ContainsKey(Effect))
                    {
                        ActiveEffects.Add(Effect, 1);
                    }
                    else
                    {
                        ActiveEffects[Effect]++;
                    }
                } break;
                case EffectDurationType.Infinite:
                {
                    if (!ActiveEffects.ContainsKey(Effect))
                    {
                        ActiveEffects.Add(Effect, 1);
                    }
                    else
                    {
                        ActiveEffects[Effect]++;
                    }
                } break;
            }
        }

        public void TryApplyAttributeChange(Type Attribute, float Value)
        {
            if (AttributeSet.ContainsKey(Attribute))
            {
                // Hijack
                if (AttributeSetCalculations.ContainsKey(Attribute))
                {
                    var Calculations = AttributeSetCalculations[Attribute];
                    foreach (var Calc in Calculations.GetInvocationList())
                    {
                        Value = ((Func<float, float>)Calc)(Value);
                    }
                }
                AttributeSet[Attribute] += Value;
                OnAttributeChanged[Attribute]?.Invoke(AttributeSet[Attribute]);
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
                if (!Ability.BlockedByTags.Any(Tag => ActiveTags.Contains(Tag)))
                {
                    Ability.Activate(this);
                    return true;
                }
            }
            return false;
        }
        public bool TryActivateAbilityByTag(GameplayTag AbilityTag)
        {
            return TryActivateAbilityByTag(AbilityTag.GetType());
        }

        public IEnumerator RemoveAfterTime(GameplayEffect Effect)
        {
            yield return new WaitForSeconds(Effect.Duration);
            // TODO: Remove gameplay tags added by this effect
            ActiveEffects[Effect]--;
            if (ActiveEffects[Effect] <= 0)
            {
                ActiveEffects.Remove(Effect);
            }
        }
    }
}
