using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    string filepath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        filepath = Application.persistentDataPath + "/savedata.json";
    }

    public void Save(SaveData save)
    {
        string json = JsonUtility.ToJson(save);
        StreamWriter writer = new StreamWriter(filepath);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public SaveData Load()
    {
        string datastr = "";
        StreamReader reader = new StreamReader(filepath);
        datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<SaveData>(datastr);
    }
}
