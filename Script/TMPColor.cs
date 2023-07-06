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
            //������ �Ͼ������ ����������
            yield return StartCoroutine(ColorLerp(Color.white, Color.red));
            //������ ���������� �Ͼ������
            yield return StartCoroutine(ColorLerp(Color.red, Color.white));
        }
    }

    private IEnumerator ColorLerp(Color start, Color end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            //lerpTime �ð� ���� while �ݺ��� ����
            currentTime += Time.deltaTime;
            percent = currentTime / lerpTime;

            //��Ʈ ������ start���� end�� ����
            textBossWarning.color = Color.Lerp(start, end, percent);

            yield return null;
        }
    }
}
