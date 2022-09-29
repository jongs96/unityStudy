using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager : MonoBehaviour
{
    public static FileManager Inst = null;
    BinaryFormatter myBf = null;
    private void Awake()
    {
        Inst = this;
        myBf = new BinaryFormatter();
    }

    public void SaveText(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }

    public string LoadText(string filePath)
    {
        //return File.ReadAllLines(filePath);
        return File.ReadAllText(filePath);
    }
    
    public void SaveBinary<T>(string filePath, T data)
    {
        using (FileStream fs = File.Create(filePath))
        {
            myBf.Serialize(fs, data);
            fs.Close();
        }
    }

    public T LoadBinary<T>(string filePath)
    {
        T data = default;
        if(File.Exists(filePath))
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                data = (T)myBf.Deserialize(fs);
                fs.Close();
            }
        }
        else
        {
            Debug.Log(filePath + "파일이 존재 하지 않습니다.");
        }
        return data;
    }
}


