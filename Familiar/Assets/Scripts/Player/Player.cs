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

        [Header("Inspector input")]
        [SerializeField, Tooltip("List of starting abilities the player has access to")]
        private List<GameplayAbility> startingAbilities;
        [SerializeField, Tooltip("List of starting effects the player has access to")]
        private List<GameplayEffect> startingEffects;
        [SerializeField, Tooltip("List of attributes the player has access to")]
        private List<GameplayAttributeSetEntry> attributeSet;
        [SerializeField, Tooltip("Array of all possible states the player can enter")]
        private State[] states;
        [SerializeField, Tooltip("Array of moonstones")]
        private GameObject[] moonstones;

        [Header("References")]
        [SerializeField, Tooltip("The animator component attached to this game object. Should be inputed manually")]
        private Animator anim;
        [SerializeField, Tooltip("The \"Controller\" component attached to this game object. Should be inputed manually")]
        private Controller playerController;
        [SerializeField, Tooltip("The player health UI slider. Should be inputed manually")]
        private Slider healthBar;
        [SerializeField, Tooltip("The animator tied to the F2B object. Should be inputed manually")]
        private Animator fadeToBlack;
        [SerializeField, Tooltip("The image tied to the F2B object. Should be inputed manually")]
        private Image blackImage;

        [Header("Events")]
        [SerializeField, Tooltip("The event in which the player dies")]
        private UnityEvent PlayerDied;

        private void Awake()
        {
            InitializeSequence();
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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log(Stats.Instance.Difficulty);
                    Debug.Log(Stats.Instance.MouseSensitivity);
                    Debug.Log(Stats.Instance.Health);
                }
            }
            HealthBarUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoonStone"))
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
            InitializeStats();
        }
        private void InitializeAnimator()
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
                Debug.LogWarning("Animator value should be set in the inspector");
                if (anim == null)
                    Debug.LogError("Could not find Animator");
            }
        }
        private void InitializePlayerController()
        {
            if (playerController == null)
            {
                playerController = GetComponent<Controller>();
                Debug.LogWarning("Player \"Controller\" value should be set in the inspector");
                if (playerController == null)
                    Debug.LogError("Could not find \"Controller\"");
            }
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
        private float ShockDamageCalculation(float value)
        {
            float? resistance = abilitySystem.GetAttributeValue(GameplayAttributes.ShockResistance);

            if (resistance.HasValue)
            {
                value *= (1.0f - resistance.Value);
            }
            return value;
        }
        private void InitializeStats()
        {
            if (Stats.Instance == null)
                Debug.Log(Stats.Instance.Health);
            float healthDifference = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth) - Stats.Instance.Health;            
            AbilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerHealth, -healthDifference);
            HealthBarUpdate();
            if (Stats.Instance.Position == Vector3.zero)
                gameObject.transform.position = GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position;
            else
                gameObject.transform.position = Stats.Instance.Position;

        }

        public void TakeDamage(float damage)
        {
            AbilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerHealth, -damage);
            Debug.Log("Health = " + AbilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth));
            anim.SetTrigger("takeDmg");
        }

        public void Die()
        {
            ded = true;
            playerController.StopController = true;
            playerController.Camera.CannotMoveCam = true;
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

        private void Respawn(Vector3 target, float delay)
        {
            StartCoroutine(WaitForRespawn(target, delay));
        }

        private IEnumerator WaitForRespawn(Vector3 target, float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.transform.position = target;

            ded = false;
            playerController.StopController = false;
            playerController.Camera.CannotMoveCam = false;
            float? refillHealth;
            refillHealth = -(AbilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth) - 10);
            AbilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerHealth, (float)refillHealth);
            healthBar.value = 1;
        }
        public void OnZap()
        {
            Die();
        }
        private void HealthBarUpdate()
        {
            healthBar.value = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth)/10;
        }
        //used by animator
        private void ResetTakeDmgTrigger()
        {
            anim.ResetTrigger("takeDmg");
        }
        public int StoneCounter
        {
            get => stoneCounter;
        }
        public GameplayAbilitySystem AbilitySystem
        {
            get => abilitySystem;
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
        /* 
         
        private void InitializeValue(var variable, Component<T>)
        {

            if (variable == null)
            {
                Debug.LogWarning(T.name + " value not set in inspector")
                    variable = GetComponent<T>();
                if (variable == null)
                    Debug.LogError("Error fetching value of " + T.name);
            }

        }
              
        */
    }
}
