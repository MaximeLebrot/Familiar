using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace AbilitySystem
{
    public class Player : MonoBehaviour, IZappable
    {
        [Tooltip("A counter of how many stones have been picked up")]
        private int stoneCounter;
        [Tooltip("A reference to the instance of the ability system tied to the player")]
        private GameplayAbilitySystem abilitySystem;
        [Tooltip("A reference to the instance of the state machine tied to the player")]
        private StateMachine stateMachine;

        [Header("Booleans")]
        [Tooltip("Checks whether the player is dead")] 
        private bool ded;
        [Tooltip("Checks whether the player can see the code panel")] 
        private bool canSeeCodePanel;
        [Tooltip("Checks whether the player is in the code panel area")] 
        private bool isInCodePanelArea;

        [Header("Inspector input & references")]
        [SerializeField, Tooltip("List of starting abilities the player has access to")]
        private List<GameplayAbility> startingAbilities;
        [SerializeField, Tooltip("List of starting effects the player has access to")]
        private List<GameplayEffect> startingEffects;
        [SerializeField, Tooltip("List of attributes the player has access to")]
        private List<GameplayAttributeSetEntry> attributeSet;
        [SerializeField, Tooltip("Array of all possible states the player can enter")]
        private State[] states;
        [SerializeField, Tooltip("The animator component attached to this game object. Should be inputed manually")]
        private Animator anim;
        [SerializeField, Tooltip("The \"Controller\" component attached to this game object. Should be inputed manually")]
        private Controller playerController;
        [SerializeField, Tooltip("Array of moonstones")]
        private GameObject[] moonstones;
        [SerializeField, Tooltip("The player health UI slider")]
        private Slider healthBar;
        [SerializeField, Tooltip("The animator tied to the F2B object")]
        private Animator fadeToBlack;
        [SerializeField, Tooltip("The image tied to the F2B object")]
        private Image blackImage;

        [Header("Events")]
        [SerializeField, Tooltip("The event in which the player dies")]
        private UnityEvent PlayerDied;


        protected void Awake()
        {
            InitializeSequence();
            Debug.Log(stoneCounter);
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
        private void InitializeSequence()
        {
            InitializeAnimator();
            InitializePlayerController();
            InitializeStateMachine();
            InitializeAbilitySystem();
            InitializeHealthBarValue();
        }
        private void InitializeAnimator()
        {
            if (anim ==null)
                anim = GetComponent<Animator>();
        }
        private void InitializePlayerController()
        {
            if (playerController == null)
                playerController = GetComponent<Controller>();
        }
        private void InitializeStateMachine()
        {
            if (stateMachine == null)
                stateMachine = new StateMachine(this, states);
        }
        private void InitializeAbilitySystem()
        {
            abilitySystem = gameObject.AddComponent<GameplayAbilitySystem>();
            if (abilitySystem != null)
            {
                abilitySystem.RegisterAttributeSet(attributeSet);

                startingAbilities.ForEach(a => abilitySystem.GrantAbility(a));

                abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerHealth, ShockDamageCalculation);
                //abilitySystem.RegisterAttributeCalculation(GameplayAttributes.PlayerStamina, ZappingCalculation);

                startingEffects.ForEach(e => abilitySystem.ApplyEffectToSelf(e)); 
            }
        }
        private void InitializeHealthBarValue()
        {
            healthBar.value = 1;
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
        }
        public void HealthBarUpdate()
        {
            healthBar.value = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth)/10;
        }
        public int GetStones()
        {
            return stoneCounter;
        }
        public void ResetTakeDmgTrigger()
        {
            anim.ResetTrigger("takeDmg");
        }

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
        public Animator Anim
        {
            get => anim;
        }

        public bool Dead
        {
            get => ded;
        }
        public Controller PlayerController
        {
            get => playerController;
        }
        public bool CanSeeCodePanel
        {
            get => canSeeCodePanel;
            set => canSeeCodePanel = value;
        }
        public bool IsInCodePanelArea
        {
            get => isInCodePanelArea;
            set => isInCodePanelArea = value;
        }
    }
}
