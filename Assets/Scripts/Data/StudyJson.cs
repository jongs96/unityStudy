using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyJson : MonoBehaviour
{
    public PlayerData myData;
    // Start is called before the first frame update
    void Start()
    {
        string data = FileManager.Inst.LoadText(Application.dataPath + @"\json.txt");
        myData = JsonUtility.FromJson<PlayerData>(data);
    }

    void SavePlayerData()
    {
        PlayerData player = new PlayerData();
        player.Nick = "Atents";
        player.Gold = 425;
        player.Exp = 15;
        player.Items = new List<int>();
        player.Items.Add(1);
        player.Items.Add(2);
        player.Items.Add(3);
        player.Items.Add(4);
        string data = JsonUtility.ToJson(player, true);
        FileManager.Inst.SaveText(Application.dataPath + @"\json.txt", data);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
