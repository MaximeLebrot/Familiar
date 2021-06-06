using UnityEngine;

public class Enemy1KillZone : MonoBehaviour
{
    [SerializeField]
    private Enemy1 enemy;
    [SerializeField]
    private AbilitySystem.Player player;

    Vector3 rot;
    bool counting;
    static readonly float time = 2.5f;
    float timer = time;

    private void Start()
    {
        rot = new Vector3(0, 90, 0);
    }

    private void Update()
    {
        if (counting)
            Count();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            counting = true;
            enemy.Transform.LookAt(player.gameObject.transform.position);
            enemy.Anim.SetTrigger("roar");
            player.Die();
        }
    }

    void Count()
    {
        if (timer <= 0)
        {
            counting = false;
            timer = time;
            enemy.Transform.rotation = Quaternion.Euler(rot);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
