using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textResultScore;

    private void Awake()
    {
        textResultScore = GetComponent<TextMeshProUGUI>();
        //저장한 점수 불러와서 score에 저장
        int score = PlayerPrefs.GetInt("Score");
        //textResultScore UI에 점수 갱신
        textResultScore.text = "최종 점수 : " + score;
    }
}
