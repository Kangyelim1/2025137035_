using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot_1;
    [SerializeField] Transform contentRoot_2;
    [SerializeField] Transform contentRoot_3;
    [SerializeField] Transform contentRoot_4;
    [SerializeField] Transform contentRoot_5;

    [SerializeField] GameObject rowPrefab;

    StageResultist allData;


    void Awake()
    {
        allData = StageResultSaver.LoadRank();
        RefreshRankList_1();
        RefreshRankList_2();
        RefreshRankList_3();
        RefreshRankList_4();
        RefreshRankList_5();
    }

    private void RefreshRankList_1()
    {
        foreach (Transform child in contentRoot_1)
        {
            Destroy(child.gameObject);
        }

        var sorteaData = allData.results.Where(r => r.stage == 1).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sorteaData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot_1);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}.{sorteaData[i].playerName}-{sorteaData[i].score}";
        }
    }

    private void RefreshRankList_2()
    {
        foreach (Transform child in contentRoot_2)
        {
            Destroy(child.gameObject);
        }

        var sorteaData = allData.results.Where(r => r.stage == 2).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sorteaData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot_2);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}.{sorteaData[i].playerName}-{sorteaData[i].score}";
        }
    }

    private void RefreshRankList_3()
    {
        foreach (Transform child in contentRoot_3)
        {
            Destroy(child.gameObject);
        }

        var sorteaData = allData.results.Where(r => r.stage == 3).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sorteaData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot_3);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}.{sorteaData[i].playerName}-{sorteaData[i].score}";
        }
    }

    private void RefreshRankList_4()
    {
        foreach (Transform child in contentRoot_4)
        {
            Destroy(child.gameObject);
        }

        var sorteaData = allData.results.Where(r => r.stage == 4).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sorteaData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot_4);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}.{sorteaData[i].playerName}-{sorteaData[i].score}";
        }
    }

    private void RefreshRankList_5()
    {
        foreach (Transform child in contentRoot_5)
        {
            Destroy(child.gameObject);
        }

        var sorteaData = allData.results.Where(r => r.stage == 5).OrderByDescending(x => x.score).ToList();

        for (int i = 0; i < sorteaData.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot_5);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}.{sorteaData[i].playerName}-{sorteaData[i].score}";
        }
    }
}
