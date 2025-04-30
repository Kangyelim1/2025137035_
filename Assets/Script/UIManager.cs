using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Level_1;
    public TextMeshProUGUI Level_2;
    public TextMeshProUGUI Level_3;
    public TextMeshProUGUI Level_4;
    public TextMeshProUGUI Level_5;

    public void GameStartButtonAction()
    {
        SceneManager.LoadScene("Level_1");
    }

    private void Start()
    {
        Level_1.text = "LEVE 1 : " + HighScore.Load(1).ToString();
        Level_2.text = "LEVE 1 : " + HighScore.Load(1).ToString();
        Level_3.text = "LEVE 1 : " + HighScore.Load(1).ToString();
        Level_4.text = "LEVE 1 : " + HighScore.Load(1).ToString();
        Level_5.text = "LEVE 1 : " + HighScore.Load(1).ToString();
    }
}


