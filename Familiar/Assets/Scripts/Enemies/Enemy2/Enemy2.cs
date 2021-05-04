using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI; //Navmesh https://docs.unity3d.com/Manual/nav-HowTos.html
using UnityEngine.UI;

public class Enemy2 : MonoBehaviour, IZappable
{
    public float moveSpeed = 10.0f;
    public int health = 4;
    public bool zapped;
    public bool canAttack;
    public GameObject drop;
    public ManaPickup mana;

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
        idlePosition = GetComponentInParent<Transform>().position;
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
        health--;
        slider.value -= 0.25f;
        //Destroy(gameObject);
    }

    public IEnumerator AttackCooldown(float cooldown)
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

}