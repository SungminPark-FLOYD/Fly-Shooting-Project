using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class FighterSpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private string alertLinePrefab;
    [SerializeField]
    private string fighterPrefab;
    [SerializeField]
    private float minSpawnTime = 1.0f;
    [SerializeField]
    private float maxSpawnTime = 4.0f;

    public ObjectManager objectManager;
    public GameManager gameManager;
    private void Awake()
    {
        alertLinePrefab = new string("AlertLine");
        fighterPrefab = new string("EnemyM");
        StartCoroutine(SpawnFighter());
    }

    private IEnumerator SpawnFighter()
    {
        while(true)
        {
            //�������� ũ�� ���������� ������ �� ����
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //��� ����
            GameObject alertLineClone = objectManager.MakeObj(alertLinePrefab);
            alertLineClone.transform.position = new Vector3(positionX, 0, 0);
            //Instantiate(alertLinePrefab, new Vector3(positionX, 0, 0), Quaternion.identity);

            //1�� ���
            yield return new WaitForSeconds(1.0f);

            //��� ����
            alertLineClone.SetActive(false);

            //������ ����
            Vector3 fighterPosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);
            GameObject fighterClone = objectManager.MakeObj(fighterPrefab);
            fighterClone.transform.position = fighterPosition;
            Enemy enemy = fighterClone.GetComponent<Enemy>();
            enemy.objectManager = objectManager;
            enemy.gameManager = gameManager;

            //��� �ð� ����(minSpawnTime ~ maxSpawnTime)
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            //�ش� �ð� ��ŭ ��� �� ���� ����
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
