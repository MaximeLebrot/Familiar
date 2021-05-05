using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour, IZappable
{
    public float moveSpeed = 10.0f;
    private static float maxHealth = 4.0f;
    public float health = 4;
    public bool zapped;
    public bool canAttack;
    public GameObject drop;
    public ManaPickup mana;

    public Animator anim;
    public NavMeshAgent navAgent;
    public LayerMask collisionMask;
    public GameObject player;
    public Vector3 idlePosition;
    public State[] states;

    public Vector3 vecToPlayer;

    private StateMachine stateMachine;

    public Slider slider;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        idlePosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine(this, states);

        slider.value = 1.0f;
    }

    private void Update()
    {
        vecToPlayer = player.transform.position;
        stateMachine.HandleUpdate();
        Debug.DrawLine(transform.position, vecToPlayer, Color.red);
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

    public void OnZap()
    {
        TakeDamage(1.0f);
        //Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Spider took " + damage + " damage");

        slider.value -= (damage / maxHealth);

        health -= damage;
        anim.SetTrigger("spiderDmg");
    }

    public IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    public IEnumerator KillAfterAnim()
    {
        yield return new WaitForSeconds(1.0f);
        drop.SetActive(true);
        mana.SetPosition(transform.position);
        Destroy(gameObject);
    }
}