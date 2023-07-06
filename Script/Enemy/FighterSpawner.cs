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
            //스테이지 크기 범위내에서 임의의 값 선택
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);
            //경고선 생성
            GameObject alertLineClone = objectManager.MakeObj(alertLinePrefab);
            alertLineClone.transform.position = new Vector3(positionX, 0, 0);
            //Instantiate(alertLinePrefab, new Vector3(positionX, 0, 0), Quaternion.identity);

            //1초 대기
            yield return new WaitForSeconds(1.0f);

            //경고선 삭제
            alertLineClone.SetActive(false);

            //전투기 생성
            Vector3 fighterPosition = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0);
            GameObject fighterClone = objectManager.MakeObj(fighterPrefab);
            fighterClone.transform.position = fighterPosition;
            Enemy enemy = fighterClone.GetComponent<Enemy>();
            enemy.objectManager = objectManager;
            enemy.gameManager = gameManager;

            //대기 시간 설정(minSpawnTime ~ maxSpawnTime)
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            //해당 시간 만큼 대기 후 로직 실행
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
