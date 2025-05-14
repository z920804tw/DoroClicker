using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameData gameData;
    public GameManager gameManager;

    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
        LoadGame();
        InvokeRepeating("SaveGame", 10, 10);
    }

    public void SaveGameData()
    {
        //先抓取數值
        gameData.coinCount = gameManager.CoinCount;
        gameData.idlePerSecondCount = gameManager.idlePerSecondCount;
        gameData.oneClickCount = gameManager.oneClickCount;

        int[] upgradeCountValue = new int[gameManager.upgrades.Length]; //紀錄當前每個Upgrade的等級數量
        for (int i = 0; i < upgradeCountValue.Length; i++)
        {
            upgradeCountValue[i] = gameManager.upgrades[i].currentCount;
        }
        gameData.upgradeCount = upgradeCountValue;



        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, json);
        Debug.Log($"存檔成功! 位置:{path}");
    }
    public void LoadGame()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(json);


            gameManager.CoinCount = gameData.coinCount;
            gameManager.idlePerSecondCount = gameData.idlePerSecondCount;
            gameManager.oneClickCount = gameData.oneClickCount;

            for (int i = 0; i < gameManager.upgrades.Length; i++)
            {
                gameManager.upgrades[i].currentCount = gameData.upgradeCount[i];
            }

            Debug.Log("載入成功！");

        }
        else
        {
            Debug.Log("找不到存檔");
        }
    }

    public void ResetSaveData() //重製存檔
    {
        CancelInvoke("SaveGame");
        //重製gamedata與Upgrade的UI
        gameData.coinCount = 0;
        gameData.oneClickCount = 1;
        gameData.idlePerSecondCount = 0;
        for (int i = 0; i < gameManager.upgrades.Length; i++)
        {
            gameData.upgradeCount[i] = 0;
            gameManager.upgrades[i].GetComponent<Upgrade>().Reset();
        }

        //覆蓋原本的存檔，替換成重製後的存檔
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(path, json);
        Debug.Log($"重製存檔成功! 位置:{path}");
        LoadGame();
        InvokeRepeating("SaveGame", 10, 10);
    }
    public void SaveGame()
    {
        SaveGameData();
    }




    // public static void SaveData(GameManager gameManager, Upgrade upgrade)
    // {
    //     BinaryFormatter formatter = new BinaryFormatter();
    //     string path = Application.persistentDataPath + "/GameDataSave.save";
    //     FileStream fileStream = new FileStream(path, FileMode.Create);

    //     GameData data = new GameData(gameManager, upgrade);

    //     formatter.Serialize(fileStream, data);
    //     fileStream.Close();

    // }

    // public static GameData LoadData()
    // {
    //     string path = Application.persistentDataPath + "/GameDataSave.save";
    //     if (File.Exists(path))
    //     {
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream fileStream = new FileStream(path, FileMode.Open);

    //         GameData gameData = formatter.Deserialize(fileStream) as GameData;
    //         return gameData;
    //     }
    //     else
    //     {
    //         Debug.Log("找不到存檔");
    //         return null;
    //     }
    // }
}
