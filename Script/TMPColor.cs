using UnityEngine;
using TMPro;
using System.Collections;

public class TMPColor : MonoBehaviour
{
    [SerializeField]
    private float lerpTime = 0.1f;
    private TextMeshProUGUI textBossWarning;

    private void Awake()
    {
        textBossWarning = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine("ColorLerpLoop");
    }

    private IEnumerator ColorLerpLoop()
    {
        while(true)
        {
            //색상을 하얀색에서 빨간색으로
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            //색상을 빨간색에서 하얀색으로
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color start, Color end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            //lerpTime 시간 동안 while 반복문 실행
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            //폰트 색상을 start에서 end로 변경
            textBossWarning.color = Color.Lerp(start, end, percent);

            yield return null;
        }
    }
}
