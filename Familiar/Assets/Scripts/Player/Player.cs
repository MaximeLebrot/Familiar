using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace AbilitySystem
{
    public class Player : MonoBehaviour, IZappable
    {
        private int stoneCounter;
        private GameplayAbilitySystem abilitySystem;
        private StateMachine stateMachine;

        [Header("Public variables")]
        [Tooltip("The animator connected to this game object")] public Animator anim;
        [Tooltip("The controller of the player connected to this game object")] public Controller playerController;

        [Header("Booleans")]
        [Tooltip("Is the player dead?")] public bool ded;
        [Tooltip("Can the player see the code panel")] public bool canSeeCodePanel;
        [Tooltip("Is the player in the code panel area?")] public bool isInCodePanelArea;

        [Header("Hierarky input")]
        [Tooltip("List of starting abilities the player has access to")]
        [SerializeField] private List<GameplayAbility> startingAbilities;
        [Tooltip("List of starting effects the player has access to")]
        [SerializeField] private List<GameplayEffect> startingEffects;
        [Tooltip("List of attributes the player has access to")]
        [SerializeField] private List<GameplayAttributeSetEntry> attributeSet;
        [Tooltip("Array of all possible states the player can enter")]
        [SerializeField] private State[] states;
        [Tooltip("Array of moonstones")]
        [SerializeField] private GameObject[] moonstones;
        [Tooltip("The player health UI slider")]
        [SerializeField] private Slider healthBar;
        [Tooltip("The animator tied to the F2B object")]
        [SerializeField] private Animator fadeToBlack;
        [Tooltip("The image tied to the F2B object")]
        [SerializeField] private Image blackImage;

        [Header("Events")]
        [Tooltip("The event in which the player dies")]
        [SerializeField] private UnityEvent PlayerDied;

        public State CurrentState
        {
            get
            {
                return stateMachine.CurrentState;
            }
        }

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
            anim = GetComponent<Animator>();
            playerController = GetComponent<Controller>();
            stateMachine = new StateMachine(this, states);

            abilitySystem = gameObject.AddComponent<GameplayAbilitySystem>(); //lägg till en instans av ability systemet
            if (abilitySystem != null)
                //Debug.Log("Ability system found");

            abilitySystem.RegisterAttributeSet(attributeSet); //sätter attribut som health och mana med ett float värde
            //Debug.Log("Registered Attribute Set");

            startingAbilities.ForEach(a => abilitySystem.GrantAbility(a)); //adderar abilities som spelaren ska börja med
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

            if (!ded)
            {
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
            }
            HealthBarUpdate();
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MoonStone")
            {
                moonstones[stoneCounter].SetActive(true);
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

        public void TakeDamage(float damage)
        {
            GetAbilitySystem().TryApplyAttributeChange(GameplayAttributes.PlayerHealth, -damage);
            Debug.Log("Health = " + GetAbilitySystem().GetAttributeValue(GameplayAttributes.PlayerHealth));
            anim.SetTrigger("takeDmg");
        }

        public void Die()
        {
            ded = true;
            playerController.StopController = true;
            playerController.Camera.cannotMoveCam = true;
            healthBar.value = 0;

            PlayerDied.Invoke();
            anim.SetTrigger("die");
            Respawn(GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position, 2.5f);
            //Debug.Log("ded");
            //anim.PlayAnim("death");
            //restart / menu
            //Destroy(this.gameObject);
                       
        }

        public void FadeToBlack()
        {
            Debug.Log("FadedToBlack");
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            fadeToBlack.SetBool("Fade", true);
            yield return new WaitUntil(() => blackImage.color.a == 1);
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            fadeToBlack.SetBool("Fade", false);
            yield return new WaitUntil(() => blackImage.color.a == 0);
        }

        public void Respawn(Vector3 target, float delay)
        {
            StartCoroutine(WaitForRespawn(target, delay));
        }

        public IEnumerator WaitForRespawn(Vector3 target, float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.transform.position = target;

            ded = false;
            playerController.StopController = false;
            playerController.Camera.cannotMoveCam = false;
            float? refillHealth;
            refillHealth = -(GetAbilitySystem().GetAttributeValue(GameplayAttributes.PlayerHealth) - 10);
            GetAbilitySystem().TryApplyAttributeChange(GameplayAttributes.PlayerHealth, (float)refillHealth);
            healthBar.value = 1;
        }

        public void OnZap()
        {
            Die();
            //Respawn(GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position, 2.5f);
        }

        public void HealthBarUpdate()
        {
            healthBar.value = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth)/10;
        }

        public void ResetTakeDmgTrigger()
        {
            anim.ResetTrigger("takeDmg");
        }
    }
}
