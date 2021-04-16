using UnityEngine;
using System.Collections.Generic;

namespace AbilitySystem
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;

        public List<GameplayAbility> StartingAbilities;
        public List<GameplayEffect> StartingEffects;
        public List<GameplayAttributeSetEntry> AttributeSet;

        public Controller playerController;
        public State[] states;

        GameplayAbilitySystem AbilitySystem;
        StateMachine stateMachine;

        protected void Awake()
        {
            playerController = GetComponent<Controller>();
            stateMachine = new StateMachine(this, states);

            AbilitySystem = gameObject.AddComponent<GameplayAbilitySystem>();
            AbilitySystem.RegisterAttributeSet(AttributeSet);
            StartingAbilities.ForEach(a => AbilitySystem.GrantAbility(a));
            AbilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerHealth, FireDamageCalculation);
            StartingEffects.ForEach(e => AbilitySystem.ApplyEffectToSelf(e));
        }

        private void Update()
        {
            stateMachine.HandleUpdate();

            if (Input.GetButtonDown("Fire1"))
            {
                if (!AbilitySystem.TryActivateAbilityByTag(GameplayTags.ZapAbilityTag))
                {
                    Debug.LogWarning("Failed to activate Zap ability");
                }
            }
        }

        public float FireDamageCalculation(float value)
        {
            float? resistance = AbilitySystem.GetAttributeValue(GameplayAttributes.FireResistance);

            if (resistance.HasValue)
            {
                value *= (1.0f - resistance.Value);
            }

            return value;
        }
    }
}
