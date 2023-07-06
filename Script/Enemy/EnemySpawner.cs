using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;            //적 생성을 위한 스테이지 크기 정보
    [SerializeField]
    private string[] enemyPrefabs;          //생성할 적 캐릭터 프리펩
    [SerializeField]
    private string bossPrefab;              //보스 프리팹
    [SerializeField]
    private float spawnTime;                //생성주기
    [SerializeField]
    private int maxEnemyCount = 100;        //최대 적 생성 수
    [SerializeField]
    private GameObject textBossWarning;     //보스 등장 텍스트 오브젝트

    public ObjectManager objectManager;
    public GameManager gameManager;
    public GameObject player;

    private void Awake()
    {
        enemyPrefabs = new string[] { "EnemyS", "EnemyL" };
        bossPrefab = new string("EnemyB");
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        int currentEnemyCount = 0;      //적 생성 숫자 카운트용 변수
        while (true)
        {
            //x 위치는 스테이지 크기 범위 내에서 임의의 값을 선택
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            int selection = Random.Range(0, enemyPrefabs.Length);

            //적 캐릭터 생성
            GameObject selectedPrefab = objectManager.MakeObj(enemyPrefabs[selection]);
            selectedPrefab.transform.position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            Enemy enemy = selectedPrefab.GetComponent<Enemy>();
            enemy.objectManager = objectManager;
            enemy.gameManager = gameManager;

            //적 생성 숫자 증가
            currentEnemyCount++;
            //적을 최대 숫자까지 생서앟면 적 생성 코루틴 중지 , 보스 생성 코루틴 실행
            if(currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            //sapwnTime만큼 대기
            yield return new WaitForSeconds(spawnTime);
            
        }
    }

    private IEnumerator SpawnBoss()
    {
        //보스 등장 텍스트 활성화
        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        //보스 등장 텍스트 비활성화
        textBossWarning.SetActive(false);

        //보스 오브젝트 활성화
        GameObject bossClone = objectManager.MakeObj(bossPrefab);
        Boss boss = bossClone.GetComponent<Boss>();
        boss.player = player;
        boss.objectManager = objectManager;
        boss.gameManager = gameManager;
        boss.stageData = stageData;

        
        //보스의 첫번째 상태인 지정된 위치로 이동 실행
        bossClone.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }

}
