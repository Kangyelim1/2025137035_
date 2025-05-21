using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Runtime.CompilerServices;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform comtentRoot;

    [SerializeField] GameObject rowPrefab;

    StageResultist allData;

    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList();
    }

    void RefreshRankList()
    {
        foreach (Transform child in comtentRoot)
        {
            Destroy(child.gameObject);
        }

        var sortedData = allData.results.Where(r => r.stage == 1).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sortedData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, comtentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i = 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
