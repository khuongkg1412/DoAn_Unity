using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SaveDataPlayer(PlayerStruct player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.dt";
        FileStream stream = new FileStream(path, FileMode.Create);
        if (player != null)
        {
            formatter.Serialize(stream, player);
        }
        else
        {
            Debug.Log("Data Null");
        }

        stream.Close();
    }
    public static PlayerStruct LoadDataPlayer()
    {
        string path = Application.persistentDataPath + "/player.dt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerStruct player = formatter.Deserialize(stream) as PlayerStruct;
            stream.Close();
            return player;
        }
        else
        {
            Debug.Log("Data Null");
            return null;
        }
    }


}
