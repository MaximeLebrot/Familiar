using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveGame
{
    private static readonly string persistentPath = Application.persistentDataPath + "/player.saveFile";

    public static void SavePlayer(AbilitySystem.Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = persistentPath;
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(player);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static SaveData LoadPlayer()
    {
        string path = persistentPath;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
