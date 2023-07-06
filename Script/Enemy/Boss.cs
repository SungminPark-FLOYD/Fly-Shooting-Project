using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TreeEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public enum BossState { MoveToAppearPoint = 0, Phase01, Phase02, Phase03, Phase04 }
public class Boss : MonoBehaviour
{
    [SerializeField]
    private float bossAppearPoint = 2.5f;
    [SerializeField]
    private int scorePoint;
    [SerializeField]
    private string bossName;
    [SerializeField]
    private string sceneName;

    private BossState bossState = BossState.MoveToAppearPoint;
    private Movement2D movement2D;

    private PlayerController playerController;
    private BossHP bossHP;


    public GameObject player;
    public ObjectManager objectManager;
    public GameManager gameManager;
    public StageData stageData;

    private void Awake()
    {
        bossHP = GetComponent<BossHP>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        movement2D =GetComponent<Movement2D>();       
    }


    public void ChangeState(BossState newState)
    {
        //������ ���� ToString���� �������� ���ǵ� �����̸��� string ���� �޾ƿ��Եȴ�
       
        StopCoroutine(bossState.ToString());
        //���� ����
        bossState = newState;

        //���ο� ���� ����
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        //�̵����� ����
        movement2D.MoveTo(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPoint)
            {
                //�̵������� (0,0,0)���� ������ ���ߵ��� �Ѵ�.
                movement2D.MoveTo(Vector3.zero);
                //���� ����
                ChangeState(BossState.Phase01);
            }
            yield return null;
        }
    }

    private IEnumerator Phase01()
    {       
        while (true)
        {
            GameObject bulletR = objectManager.MakeObj("BulletBossB");
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletRR = objectManager.MakeObj("BulletBossB");
            bulletRR.transform.position = transform.position + Vector3.right * 0.45f;
            GameObject bulletL = objectManager.MakeObj("BulletBossB");
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            GameObject bulletLL = objectManager.MakeObj("BulletBossB");
            bulletLL.transform.position = transform.position + Vector3.left * 0.45f;

            yield return new WaitForSeconds(0.5f);           

            if (bossHP.CurHP <= bossHP.MaxHP * 0.7f)
            {
                StopCoroutine(Phase01());

                ChangeState(BossState.Phase02);

            }
            yield return null;
        }
    }

    private IEnumerator Phase02()
    {
        while(true)
        {
            float attackRate =  0.5f;
            for(int i=0; i<5; i++)
            {
                GameObject clone = objectManager.MakeObj("BulletEnemyB");
                clone.transform.position = transform.position;

                Vector2 dirVec = player.transform.position - transform.position;
                Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
                dirVec += ranVec;
                clone.GetComponent<Movement2D>().MoveTo(dirVec.normalized);
            }

            yield return new WaitForSeconds(attackRate);

            if (bossHP.CurHP <= bossHP.MaxHP * 0.5f)
            {
                StopCoroutine(Phase02());

                ChangeState(BossState.Phase03);

            }
            yield return null;
        }

    }

    private IEnumerator Phase03()
    {
        int count = 20;                     //�߻�ü�� ���� ����
        float intervalAngle = 180 / count;  //�߻�ü ������ ����
        float weightAngle = 0;              //���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)
        float attackRate = 0.75f;
        while (true)
        {
            for(int i=0; i<count; i++)
            {             
                GameObject clone = objectManager.MakeObj("BulletEnemyA");
                clone.transform.position = transform.position;

                float angle = weightAngle + intervalAngle * i;
                
                Vector3 targetDir = new Vector2(Mathf.Cos(angle * Mathf.PI / 180.0f), -1);
                clone.GetComponent<Movement2D>().MoveTo(targetDir);

            }
            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            yield return new WaitForSeconds(attackRate);


            if (bossHP.CurHP <= bossHP.MaxHP * 0.3f)
            {               
                StopCoroutine(Phase03());

                ChangeState(BossState.Phase04);

            }
            yield return null;
        }
    }

    private IEnumerator Phase04()
    {
        //�� ������ ��� ���� ����
        float attackRate = 0.5f;            //���� �ֱ�
        int count = 40;                     //�߻�ü�� ���� ����
        float intervalAngle = 360 / count;  //�߻�ü ������ ����
        float weightAngle = 0;              //���ߵǴ� ���� (�׻� ���� ��ġ�� �߻����� �ʵ��� ����)

        //�� ���·� ����ϴ� �߻�ü ���� (count ��ŭ)
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                //�߻�ü ����
                GameObject clone = objectManager.MakeObj("BulletBossA");
                clone.transform.position = transform.position;
                clone.transform.rotation = transform.rotation;
                //�߻�ü ����(��)
                float angle = weightAngle + intervalAngle * i;
                //�߻�ü �̵� ����(����)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                //�߻�ü �̵� ���� ����               
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            //�߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
            weightAngle += 1;

            //attackRate �ð� ��ŭ ���
            yield return new WaitForSeconds(attackRate);
            
        }
    }

    public void OnDie()
    {
        playerController.Score += scorePoint;

        gameManager.CallExplosion(transform.position, bossName);

        gameObject.SetActive(false);

        Invoke("Clear", 1f);
    }

    private void Clear()
    {
        //���� ����
        PlayerPrefs.SetInt("Score", playerController.Score);
        //���� ������ �̵�
        SceneManager.LoadScene(sceneName);
    }
}
