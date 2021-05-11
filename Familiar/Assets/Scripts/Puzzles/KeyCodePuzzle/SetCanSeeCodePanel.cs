using UnityEngine;

public class SetCanSeeCodePanel : MonoBehaviour
{
    private AbilitySystem.Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilitySystem.Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.canSeeCodePanel = true;
        }
    }
}
