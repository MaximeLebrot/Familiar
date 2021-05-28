using UnityEngine;

public class MoonstoneDoorCol : MonoBehaviour
{
    [SerializeField]
    private AbilitySystem.Player player;
    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
    }
    public void CheckStoneCounter()
    {
        if (player.StoneCounter >= 6)
            Destroy(this.gameObject);
    }

}
