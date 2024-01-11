using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int hightScore;
    public string playerName;
    public string newPlayerName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        //Load Score
        LoadData();
    }

    [System.Serializable]
    class PlayerInfo
    {
        public string name;
        public int hightScore;
    }

    public void SaveData(int score)
    {
        string path = Application.persistentDataPath + "/playerData.json";

        //Nothing will happen if no hight score is made
        if (hightScore > score)
            return;

        //Save Data
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.name = newPlayerName;
        playerInfo.hightScore = score;

        //Pass the score
        hightScore = score;
        playerName = newPlayerName;

        //Save File
        string json = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(path, json);
    }

    //I'm gonna use this at the start of the game
    void LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        //Check if file exist
        if (!File.Exists(path))
        {
            hightScore = 0;
            return;
        }

        //Read the file
        string json = File.ReadAllText(path);
        PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(json);

        //Update the values
        playerName = playerInfo.name;
        hightScore = playerInfo.hightScore;
    }
}
