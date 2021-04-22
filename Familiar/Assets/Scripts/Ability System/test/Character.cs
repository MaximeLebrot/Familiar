using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem_V3
{
    public class Character : MonoBehaviour
    {
        public float Speed;
        public List<GameplayAbility> StartingAbilities;
        public List<GameplayEffect> StartingEffects;
        public List<GameplayAttributeSetEntry> AttributeSet;

        GameplayAbilitySystem AbilitySystem;

        private void Awake()
        {
            AbilitySystem = gameObject.AddComponent<GameplayAbilitySystem>();
            AbilitySystem.RegisterAttributeSet(AttributeSet);
            StartingAbilities.ForEach(a => AbilitySystem.GrantAbility(a));
            AbilitySystem.RegisterAttributeCalculation(GameplayAttributes.Health, FireDamageCalculation);
            StartingEffects.ForEach(e => AbilitySystem.ApplyEffectToSelf(e));
        }

        private void Update()
        {
            Vector3 Movement = Input.GetAxisRaw("Horizontal") * Vector3.right + Input.GetAxisRaw("Vertical") * Vector3.up;
            Movement.Normalize();

            transform.position += Movement * Speed * Time.deltaTime;
        }

        private void LateUpdate()
        {
        }

        public float FireDamageCalculation(float Value)
        {
            float? Resistance = AbilitySystem.GetAttributeValue(GameplayAttributes.FireResistance);

            if (Resistance.HasValue)
            {
                Value *= (1.0f - Resistance.Value);
            }
            return Value;
        }
        public float ReloadCalculation(float Value)
        {
            float? Ammo = AbilitySystem.GetAttributeValue(GameplayAttributes.FireResistance); // 9mmAmmoAttribute

            if (!Ammo.HasValue)
            {
                return 0.0f;
            }
            Value = Mathf.Min(Value, Ammo.Value);
       
            return Value;
        }
    }
}

