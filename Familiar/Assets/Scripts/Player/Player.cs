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
        [Tooltip("Spawn position information")]
        private int spawnPosition;
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
        [Tooltip("Checks whether the player can zap or not")]
        private bool canZap;

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
        [SerializeField, Tooltip("The Audio Handler component attached to this game object. Should be inputed manually")]
        private AudioHandler audioHandler;
        [SerializeField, Tooltip("The \"Controller\" component attached to this game object. Should be inputed manually")]
        private Controller playerController;
        [SerializeField, Tooltip("The player health UI slider. Should be inputed manually")]
        private Slider healthBar;
        [SerializeField, Tooltip("The player health UI. Should be inputed manually")]
        private Image healthBarNew;
        [SerializeField, Tooltip("The player health UI Animator. Should be inputed manually")]
        private Animator healthBarAnim;
        [SerializeField, Tooltip("The animator tied to the F2B object. Should be inputed manually")]
        private Animator fadeToBlack;
        [SerializeField, Tooltip("The image tied to the F2B object. Should be inputed manually")]
        private Image blackImage;
        [SerializeField] private ParticleSystem dustVFX;
        [SerializeField, Tooltip("The MoonstoneDoorCol script tied to the MoonstoneDoor game object. Must be inputed manually")]
        private MoonstoneDoorCol moonstoneDoorCol;


        [Header("Events")]
        [SerializeField, Tooltip("The event in which the player dies")]
        private UnityEvent PlayerDied;
        [SerializeField, Tooltip("The event in which the player respawns")]
        private UnityEvent PlayerRespawned;

        public ParticleSystem DustVFX => dustVFX;

        private void Awake()
        {
            InitializeSequence();
        }

        private void Update()
        {
            stateMachine.HandleUpdate();

            if (!ded)
            {
                if (Input.GetButtonDown("Fire1") && canZap)
                {
                    if (!abilitySystem.TryActivateAbilityByTag(GameplayTags.ZapAbilityTag))
                    {
                        Debug.LogWarning("Failed to activate Zap ability");
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MoonStone"))
            {
                moonstones[stoneCounter].SetActive(true);
                stoneCounter++;
                moonstoneDoorCol.CheckStoneCounter();
                audioHandler.PlayMoonstonePickupSound();
            }
        }

        private void InitializeSequence()
        {
            InitializeAnimator();
            InitializeAudioHandler();
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

        private void InitializeAudioHandler()
        {
            if (audioHandler == null)
            {
                audioHandler = GetComponent<AudioHandler>();
                Debug.LogWarning("Audio Handler value should be set in the inspector");
                if (audioHandler == null)
                    Debug.LogError("Could not find Audio Handler");

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
            healthBarNew.fillAmount = 1;
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
            if (Stats.Instance != null)
            {
                float healthDifference = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth) - Stats.Instance.Health;
                AbilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerHealth, -healthDifference);
                HealthBarUpdate();
                if (Stats.Instance.Position == Vector3.zero)
                    gameObject.transform.position = GameObject.FindGameObjectsWithTag("Respawn")[0].transform.position;
                else
                    gameObject.transform.position = Stats.Instance.Position;
                Debug.Log(Stats.Instance.Rotation);
                transform.rotation = Quaternion.Euler(0f, Stats.Instance.Rotation.y, 0f);
                canZap = true;
                spawnPosition = 0;
            }
        }

        public void TakeDamage(float damage)
        {
            AbilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerHealth, -damage);
            HealthBarUpdate();
            if (abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth) <= 0)
            {
                Die();
            }
            else
            {
                anim.SetTrigger("takeDmg");
                healthBarAnim.SetTrigger("Damage");
                audioHandler.PlayDamageSound();
            }
                
        }

        public void Die()
        {
            ded = true;
            playerController.StopController = true;
            playerController.Camera.CannotMoveCam = true;
            healthBar.value = 0;
            healthBarNew.fillAmount = 0;

            PlayerDied.Invoke();
            anim.SetTrigger("die");
            Respawn(GameObject.FindGameObjectsWithTag("Respawn")[spawnPosition].transform.position, 2.5f);
        }

        public void FadeToBlack()
        {
            StartCoroutine(FadeOut());
        }

        IEnumerator FadeOut()
        {
            fadeToBlack.SetBool("Fade", true);
            yield return new WaitUntil(() => blackImage.color.a == 1);
            StartCoroutine(ChillInDarkness());
        }
        IEnumerator ChillInDarkness()
        {
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(FadeIn());
        }
        IEnumerator FadeIn()
        {
            fadeToBlack.SetBool("Fade", false);
            PlayerRespawned.Invoke();
            ded = false;
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
            playerController.StopController = false;
            playerController.Camera.CannotMoveCam = false;
            float? refillHealth;
            refillHealth = -(AbilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth) - 10);
            AbilitySystem.TryApplyAttributeChange(GameplayAttributes.PlayerHealth, (float)refillHealth);
            healthBar.value = 1;
            healthBarNew.fillAmount = 1;
        }
        public void OnZap()
        {
            Die();
        }
        private void HealthBarUpdate()
        {
            healthBar.value = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth)/10;
            healthBarNew.fillAmount = (float)abilitySystem.GetAttributeValue(GameplayAttributes.PlayerHealth) / 10;
        }
        //used by animator
        private void ResetTakeDmgTrigger()
        {
            anim.ResetTrigger("takeDmg");
        }

        public AudioHandler AudioHandler
        {
            get => audioHandler;
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
        public Animator Anim => anim;

        public bool Dead => ded;

        public Controller PlayerController
        {
            get => playerController;
        }

        public int SpawnPosition
        {
            get => spawnPosition;
            set => spawnPosition = value;
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
        public bool CanZap
        {
            get => canZap;
            set => canZap = value;
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
