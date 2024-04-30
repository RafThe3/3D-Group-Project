using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveScript
{
    public static void SavePlayer(Object player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + player.name + ".foo";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        PlayerData data = new PlayerData(player);
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(Object player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + player.name + ".foo";
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("no such file exists at " + path);
            return null;
        }
    }
}
