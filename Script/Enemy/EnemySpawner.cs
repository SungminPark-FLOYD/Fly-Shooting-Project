using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;            //�� ������ ���� �������� ũ�� ����
    [SerializeField]
    private string[] enemyPrefabs;          //������ �� ĳ���� ������
    [SerializeField]
    private string bossPrefab;              //���� ������
    [SerializeField]
    private float spawnTime;                //�����ֱ�
    [SerializeField]
    private int maxEnemyCount = 100;        //�ִ� �� ���� ��
    [SerializeField]
    private GameObject textBossWarning;     //���� ���� �ؽ�Ʈ ������Ʈ

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
        int currentEnemyCount = 0;      //�� ���� ���� ī��Ʈ�� ����
        while (true)
        {
            //x ��ġ�� �������� ũ�� ���� ������ ������ ���� ����
            float positionX = Random.Range(stageData.LimitMin.x, stageData.LimitMax.x);

            int selection = Random.Range(0, enemyPrefabs.Length);

            //�� ĳ���� ����
            GameObject selectedPrefab = objectManager.MakeObj(enemyPrefabs[selection]);
            selectedPrefab.transform.position = new Vector3(positionX, stageData.LimitMax.y + 1.0f, 0.0f);
            Enemy enemy = selectedPrefab.GetComponent<Enemy>();
            enemy.objectManager = objectManager;
            enemy.gameManager = gameManager;

            //�� ���� ���� ����
            currentEnemyCount++;
            //���� �ִ� ���ڱ��� �����۸� �� ���� �ڷ�ƾ ���� , ���� ���� �ڷ�ƾ ����
            if(currentEnemyCount == maxEnemyCount)
            {
                StartCoroutine("SpawnBoss");
                break;
            }

            //sapwnTime��ŭ ���
            yield return new WaitForSeconds(spawnTime);
            
        }
    }

    private IEnumerator SpawnBoss()
    {
        //���� ���� �ؽ�Ʈ Ȱ��ȭ
        textBossWarning.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        //���� ���� �ؽ�Ʈ ��Ȱ��ȭ
        textBossWarning.SetActive(false);

        //���� ������Ʈ Ȱ��ȭ
        GameObject bossClone = objectManager.MakeObj(bossPrefab);
        Boss boss = bossClone.GetComponent<Boss>();
        boss.player = player;
        boss.objectManager = objectManager;
        boss.gameManager = gameManager;
        boss.stageData = stageData;

        
        //������ ù��° ������ ������ ��ġ�� �̵� ����
        bossClone.GetComponent<Boss>().ChangeState(BossState.MoveToAppearPoint);
    }

}
