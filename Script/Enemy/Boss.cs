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
        //열겨형 변수 ToString으로 열겨형에 정의된 변수이름을 string 으로 받아오게된다
       
        StopCoroutine(bossState.ToString());
        //상태 변경
        bossState = newState;

        //새로운 상태 변경
        StartCoroutine(bossState.ToString());
    }

    private IEnumerator MoveToAppearPoint()
    {
        //이동방향 설정
        movement2D.MoveTo(Vector3.down);

        while(true)
        {
            if(transform.position.y <= bossAppearPoint)
            {
                //이동방향을 (0,0,0)으로 설정해 멈추도록 한다.
                movement2D.MoveTo(Vector3.zero);
                //상태 변경
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
        int count = 20;                     //발사체의 생성 개수
        float intervalAngle = 180 / count;  //발사체 사이의 각도
        float weightAngle = 0;              //가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)
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
            //발사체가 생성되는 시작 각도 설정을 위한 변수
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
        //원 형태의 방사 공격 시작
        float attackRate = 0.5f;            //공격 주기
        int count = 40;                     //발사체의 생성 개수
        float intervalAngle = 360 / count;  //발사체 사이의 각도
        float weightAngle = 0;              //가중되는 각도 (항상 같은 위치로 발사하지 않도록 설정)

        //원 형태로 방사하는 발사체 생성 (count 만큼)
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                //발사체 생성
                GameObject clone = objectManager.MakeObj("BulletBossA");
                clone.transform.position = transform.position;
                clone.transform.rotation = transform.rotation;
                //발사체 방향(각)
                float angle = weightAngle + intervalAngle * i;
                //발사체 이동 방향(벡터)
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                //발사체 이동 방향 설정               
                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            //발사체가 생성되는 시작 각도 설정을 위한 변수
            weightAngle += 1;

            //attackRate 시간 만큼 대기
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
        //점수 저장
        PlayerPrefs.SetInt("Score", playerController.Score);
        //다음 씬으로 이동
        SceneManager.LoadScene(sceneName);
    }
}
