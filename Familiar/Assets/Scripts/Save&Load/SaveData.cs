using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public string sceneName { get; }
    public float health{ get; }
    public float[] position { get; }

    public SaveData(AbilitySystem.Player player)
    {

        sceneName = SceneManager.GetActiveScene().name;
        health = (float)player.AbilitySystem.GetAttributeValue(AbilitySystem.GameplayAttributes.PlayerHealth);
        position = new float[3];
        position[0] = player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
        position[2] = player.gameObject.transform.position.z;
    }

}
