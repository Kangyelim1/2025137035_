using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class StageResult
{
    public string playerName;

    public int stage;

    public int score;
}
[System.Serializable]
public class StageResultist
{
    public List<StageResult> results = new List<StageResult>();
}
public static class StageResultSaver
{
    private const string FILE = "stage_results.json";

    private const string PLAYER_NAME = "playerName";

    private static string filpath = Path.Combine(Application.persistentDataPath, FILE);

    public static void SaveStage(int stage, int score)
    {
        StageResultist list = LoadInternal();
        string playerName = PlayerPrefs.GetString(PLAYER_NAME,"");
        StageResult entry = new StageResult
        {
            playerName = playerName,
            stage = stage,
            score = score
        };
       list.results.Add(entry);
        string json = JsonUtility.ToJson(list, true);
        File.WriteAllText(filpath, json);
    }
    private static StageResultist LoadInternal()
    {
        if(!File.Exists(filpath))
            return new StageResultist();
        string json = File.ReadAllText(filpath);
        StageResultist list = JsonUtility.FromJson<StageResultist>(json);
        if(list == null)
            return new StageResultist();
        else
            return list;
    }

    public static StageResultist LoadRank()
    {
        return LoadInternal();
    }
}
