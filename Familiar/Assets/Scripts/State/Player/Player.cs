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

            abilitySystem = gameObject.AddComponent<GameplayAbilitySystem>(); //lägg till en instans av ability systemet
            if (abilitySystem != null)
                Debug.Log("Ability system found");

            abilitySystem.RegisterAttributeSet(attributeSet); //sätter attribut som health och mana med ett float värde
            Debug.Log("Registered Attribute Set");

            startingAbilities.ForEach(a => abilitySystem.GrantAbility(a)); //adderar abilities som spelaren ska börja med
            Debug.Log("Starting Abilities granted");

            abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerHealth, ShockDamageCalculation); 
            //abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerStamina, ZappingCalculation);
            Debug.Log("Registered Attribute Calculations");

            startingEffects.ForEach(e => abilitySystem.ApplyEffectToSelf(e));
            Debug.Log("Applied Starting Effects To Self");
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
            Debug.Log(abilitySystem.GetAttributeValue(GameplayAttributes.PlayerMana));
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
        public float ShockDamageCalculation(float value)
        {
            float? resistance = abilitySystem.GetAttributeValue(GameplayAttributes.ShockResistance);

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
