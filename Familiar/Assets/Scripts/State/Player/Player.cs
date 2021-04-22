using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class Player : MonoBehaviour
    {
        //public float moveSpeed;

        public List<GameplayAbility> startingAbilities;
        public List<GameplayEffect> startingEffects;
        public List<GameplayAttributeSetEntry> attributeSet;

        public Controller playerController;
        public State[] states;

        GameplayAbilitySystem abilitySystem;
        StateMachine stateMachine;

        protected void Awake()
        {
            playerController = GetComponent<Controller>();
            stateMachine = new StateMachine(this, states);

            abilitySystem = gameObject.AddComponent<GameplayAbilitySystem>();
            abilitySystem.RegisterAttributeSet(attributeSet);
            startingAbilities.ForEach(a => abilitySystem.GrantAbility(a));
            abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerHealth, FireDamageCalculation);
            startingEffects.ForEach(e => abilitySystem.ApplyEffectToSelf(e));
        }

        private void Update()
        {
            stateMachine.HandleUpdate();

            if (Input.GetButtonDown("Fire1"))
            {
                if (!abilitySystem.TryActivateAbilityByTag(GameplayTags.ZapAbilityTag))
                {
                    Debug.LogWarning("Failed to activate Zap ability");
                }
            }
        }

        public float FireDamageCalculation(float value)
        {
            float? resistance = abilitySystem.GetAttributeValue(GameplayAttributes.FireResistance);

            if (resistance.HasValue)
            {
                value *= (1.0f - resistance.Value);
            }
            return value;
        }
        public float ReloadCalculation(float Value)
        {
            float? Ammo = abilitySystem.GetAttributeValue(GameplayAttributes.FireResistance); // 9mmAmmoAttribute

            if (!Ammo.HasValue)
            {
                return 0.0f;
            }
            Value = Mathf.Min(Value, Ammo.Value);

            return Value;
        }
    }
}
