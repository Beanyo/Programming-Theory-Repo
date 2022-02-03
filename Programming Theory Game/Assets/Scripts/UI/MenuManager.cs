using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string currentPlayerName;
    public string highPlayerName;
    public int highScore;
    
    private void Awake()
    {
        //Check to see if an existing MainManager exists
        if (Instance != null)
        {
            //If existing already, destroy
            Destroy(gameObject);
            return;
        }
        //Create an Instance of MainManager to be carried to next scene
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highPlayerName;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highPlayerName = currentPlayerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
            highPlayerName = data.highPlayerName;
            Debug.Log(json);
        }
        else
        {
            highPlayerName = "None";
            highScore = 0;
        }
    }
}
