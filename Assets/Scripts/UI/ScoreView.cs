using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI killCountText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public static ScoreView instance;

    private void Awake()
    {
        if(instance == null){
            instance = this;
        }
        UpdateKillCount(0);
        UpdateScore(0);
    }
    public void UpdateKillCount(int killCount)
    {
        killCountText.text = FormatNumber(
            "Asesinatos", killCount
        );
    }
    public void UpdateScore(int score)
    {
        scoreText.text = FormatNumber(
            "Puntaje", score
        );
    }

    private string FormatNumber(string text, int number)
    {
        return $"{text}: {number:0000}";
    }
}