using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerData
{
    public string Nick;
    public int Gold;
    public int Exp;
    public List<int> Items;
}

public class StudyFiledata : MonoBehaviour
{
    public TMPro.TMP_Text myLabel = null;
    public TMPro.TMP_Text myNick = null;
    public TMPro.TMP_Text myGold = null;
    public TMPro.TMP_Text myExp = null;
    // Start is called before the first frame update
    void Start()
    {
        //Json, xml
        //string str = "";
        //string[] res = str.Split(',');
        //FileManager.Inst.SaveText( Application.dataPath + "\\Test.txt", "이건테스트입니다.");
        //myLabel.text = FileManager.Inst.LoadText(Application.dataPath + @"\Test.txt");
        /*
        PlayerData data = new PlayerData();
        data.Nick = "NickName";
        data.Gold = 1000;
        data.Exp = 100;
        FileManager.Inst.SaveBinary(Application.dataPath + @"\Test.data", data);
        */
        PlayerData data = FileManager.Inst.LoadBinary<PlayerData>(Application.dataPath + @"\Test.data");
        myNick.text = data.Nick;
        myGold.text = data.Gold.ToString();
        myExp.text = data.Exp.ToString();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
