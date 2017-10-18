using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class IndexFacdtory : MonoBehaviour {
    //path to json data
    string path;
    //jsondata
    string jsonString;

	void Start() {
        //get path to straming assets
        path = Application.streamingAssetsPath + "/Anomaly.json";
        //get data from file
        jsonString = File.ReadAllText(path);

        CodexDataBase database = JsonUtility.FromJson<CodexDataBase>(jsonString);

		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().codexDatabase = database;
    }

}

[System.Serializable]
public class CodexDataBase
{
    public Codex[] dataBase;
}

[System.Serializable]
public class Codex {
	public int codexID;
    public string name;
    public string information;
}