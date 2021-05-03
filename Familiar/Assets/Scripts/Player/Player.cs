using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem
{
    public class Player : MonoBehaviour, IZappable
    {
        public float moveSpeed;
        private int stoneCounter;

        public bool canSeeCodePanel;
        public bool isInCodePanelArea;

        public List<GameplayAbility> startingAbilities;
        public List<GameplayEffect> startingEffects;
        public List<GameplayAttributeSetEntry> attributeSet;

        public Controller playerController;
        public State[] states;
        public AudioHandler audioHandler;

        GameplayAbilitySystem abilitySystem;
        StateMachine stateMachine;

        public Slider healthBar;        

        public bool IsZapped
        {
            get
            {
                return false;
            }

            set
            {
            }
        }

        public int GetStones()
        {
            return stoneCounter;
        }

        protected void Awake()
        {
            playerController = GetComponent<Controller>();
            stateMachine = new StateMachine(this, states);
            audioHandler = GetComponent<AudioHandler>();


            abilitySystem = gameObject.AddComponent<GameplayAbilitySystem>(); //l�gg till en instans av ability systemet
            if (abilitySystem != null)
                //Debug.Log("Ability system found");

            abilitySystem.RegisterAttributeSet(attributeSet); //s�tter attribut som health och mana med ett float v�rde
            //Debug.Log("Registered Attribute Set");

            startingAbilities.ForEach(a => abilitySystem.GrantAbility(a)); //adderar abilities som spelaren ska b�rja med
            //Debug.Log("Starting Abilities granted");

            abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerHealth, ShockDamageCalculation); 
            //abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerStamina, ZappingCalculation);
            //Debug.Log("Registered Attribute Calculations");

            startingEffects.ForEach(e => abilitySystem.ApplyEffectToSelf(e));
            //Debug.Log("Applied Starting Effects To Self");

            healthBar.value = 1;
            stoneCounter = 0;
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

            if (Input.GetButtonDown("Fire2"))
            {
                abilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerMana, 10);
            }

            HealthBarUpdate();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MoonStone")
            {
                stoneCounter++;
            }
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

        public GameplayAbilitySystem GetAbilitySystem()
        {
            return abilitySystem;
        }
        
        public void Die()
        {
            healthBar.value = 0;

            Debug.Log("ded");
            //Destroy(this.gameObject);
                       
        }

        public void Respawn(Vector3 target, float delay)
        {
            StartCoroutine(WaitForRespawn(target, delay));
        }

        public IEnumerator WaitForRespawn(Vector3 target, float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.transform.position = target;
            healthBar.value = 1;
        }

        public void OnZap()
        {
            Die();
            Respawn(GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position, 1.0f);
        }

        public void HealthBarUpdate()
        {
            healthBar.value = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth)/10;

            
        }
    }
}
