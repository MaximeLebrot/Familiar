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

            abilitySystem = gameObject.AddComponent<GameplayAbilitySystem>(); //l�gg till en instans av ability systemet
            if (abilitySystem != null)
                Debug.Log("Ability system found");

            abilitySystem.RegisterAttributeSet(attributeSet); //s�tter attribut som health och mana med ett float v�rde
            Debug.Log("Registered Attribute Set");

            startingAbilities.ForEach(a => abilitySystem.GrantAbility(a)); //adderar abilities som spelaren ska b�rja med
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
            //if (Input.GetButtonDown("Fire2"))
            //{
            //    abilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerMana, 10);
            //}
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
        public void Die()
        {
            Debug.Log("ded");
            //Destroy(this.gameObject);
        }
        public GameplayAbilitySystem GetAbilitySystem()
        {
            return abilitySystem;
        }
    }
}
