using UnityEngine;

public abstract class PickupItem : MonoBehaviour
{
    protected GameObject player;
    protected AbilitySystem.Player playerStats;
    private Vector3 offset = new Vector3(0f, 1.3f, 0f);

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = (AbilitySystem.Player)player.GetComponent("Player");
    }

    public void SetPosition(Vector3 pos)
    {
        gameObject.transform.position = pos + offset;
    }
}
