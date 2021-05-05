using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace AbilitySystem
{
    public class Player : MonoBehaviour, IZappable
    {
        public Animator anim;
        public float moveSpeed;
        private int stoneCounter;

        public bool ded;

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

        public UnityEvent PlayerDied;

        public GameObject[] moonstones;
        public Animator fadeToBlack;
        public Image blackImage;

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
            audioHandler = GetComponent<AudioHandler>();

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
            playerController.dedNowDontMove = true;
            playerController.cam.cannotMoveCam = true;
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
            playerController.dedNowDontMove = false;
            playerController.cam.cannotMoveCam = false;
            float? refillHealth;
            refillHealth = -(GetAbilitySystem().GetAttributeValue(GameplayAttributes.PlayerHealth) - 10);
            GetAbilitySystem().TryApplyAttributeChange(GameplayAttributes.PlayerHealth, (float)refillHealth);
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

        public void ResetTakeDmgTrigger()
        {
            anim.ResetTrigger("takeDmg");
        }
    }
}
