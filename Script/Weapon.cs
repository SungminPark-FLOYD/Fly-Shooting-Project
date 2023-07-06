using System.Collections;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //private float attackRate = 1f;           //���ݼӵ�
    [SerializeField]
    private float maxShotDelay;
    [SerializeField]
    private float curShotDelay;
    
    private int damage = 150;           //��ź ������
    public GameObject boomEffect;       //��ź
    public int boom;                    //���� ��ź
    public int maxBoom;                 //�ִ� ��ź
    public int power = 1;              //���ݷ���
    public int maxPower;               //�ִ뷹��

    public GameObject[] followers;      //��������
    public ObjectManager objectManager;
   
    public bool isBoomTime;

    public int Boom => boom;

    private void Update()
    {
        TryAttack();
        Reload();
    }
    private void TryAttack()
    {
        if (!Input.GetButton("Fire1")) return;

        if (curShotDelay < maxShotDelay) return;

        AttackByLevel();
        
        curShotDelay = 0;
    }

    private void AttackByLevel()
    {
        GameObject cloneBullet = null;
        switch(power)
        {
            case 1: //�߻�ü 1��
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                    
                break;
            case 2: //�߻�ü 2��
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.2f;
                
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                
                break;
            default: //���� bulletObjB 1��  �¿� bulletObjA �� 1��
                GameObject bulletC = objectManager.MakeObj("BulletPlayerB");
                bulletC.transform.position = transform.position;

                //���� �߻�ü
                cloneBullet = objectManager.MakeObj("BulletPlayerA");
                cloneBullet.transform.position = transform.position + Vector3.left * 0.3f;        
                //������ �߻�ü
                cloneBullet = objectManager.MakeObj("BulletPlayerA");
                cloneBullet.transform.position = transform.position + Vector3.right * 0.3f;
                break;
        }
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void AddFollower()
    {
        if (power == 4)
            followers[0].SetActive(true);
        else if (power == 5)
            followers[1].SetActive(true);
        else if (power == 6)
            followers[2].SetActive(true);
    }

    //��ź ����
    public void OnBoom()
    {
        if (isBoomTime) return;
        if (boom == 0) return;

        boom--;
        isBoomTime = true;
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 2f);

        GameObject[] enemiesL = objectManager.GetPool("EnemyL");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        GameObject[] boss = objectManager.GetPool("EnemyB");

        for (int i = 0; i < enemiesL.Length; ++i)
        {
            if (enemiesL[i].activeSelf)
            {
                enemiesL[i].GetComponent<Enemy>().OnDie();
            }
        }
        for (int i = 0; i < enemiesM.Length; ++i)
        {
            if (enemiesM[i].activeSelf)
            {
                enemiesM[i].GetComponent<Enemy>().OnDie();
            }
        }
        for (int i = 0; i < enemiesS.Length; ++i)
        {
            if (enemiesS[i].activeSelf)
            {
                enemiesS[i].GetComponent<Enemy>().OnDie();
            }
        }
        for(int i=0; i < boss.Length; ++i)
        {
            boss[i].GetComponent<BossHP>().TakeDamage(damage);
        }
    }
    private void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

}
