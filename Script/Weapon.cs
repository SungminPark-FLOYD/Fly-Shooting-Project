using System.Collections;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //private float attackRate = 1f;           //°ø°Ý¼Óµµ
    [SerializeField]
    private float maxShotDelay;
    [SerializeField]
    private float curShotDelay;
    
    private int damage = 150;           //ÆøÅº µ¥¹ÌÁö
    public GameObject boomEffect;       //ÆøÅº
    public int boom;                    //ÇöÀç ÆøÅº
    public int maxBoom;                 //ÃÖ´ë ÆøÅº
    public int power = 1;              //°ø°Ý·¹º§
    public int maxPower;               //ÃÖ´ë·¹º§

    public GameObject[] followers;      //º¸Á¶¹«±â
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
            case 1: //¹ß»çÃ¼ 1°³
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;
                    
                break;
            case 2: //¹ß»çÃ¼ 2°³
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.2f;
                
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                
                break;
            default: //Àü¹æ bulletObjB 1°³  ÁÂ¿ì bulletObjA °¢ 1°³
                GameObject bulletC = objectManager.MakeObj("BulletPlayerB");
                bulletC.transform.position = transform.position;

                //¿ÞÂÊ ¹ß»çÃ¼
                cloneBullet = objectManager.MakeObj("BulletPlayerA");
                cloneBullet.transform.position = transform.position + Vector3.left * 0.3f;        
                //¿À¸¥ÂÊ ¹ß»çÃ¼
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

    //ÆøÅº °ø°Ý
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
