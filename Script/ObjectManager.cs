using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPref;
    public GameObject enemyMPref;
    public GameObject enemySPref;
    public GameObject enemyBPref;

    public GameObject alertLinePref;

    public GameObject itemCoinPref;
    public GameObject itemPowerPref;
    public GameObject itemBoomPref;

    public GameObject bulletPlayerAPref;
    public GameObject bulletPlayerBPref;
    public GameObject bulletFollowerPref;

    public GameObject BulletEnemyAPref;
    public GameObject BulletEnemyBPref;
    public GameObject bulletBossAPref;
    public GameObject bulletBossBPref;

    public GameObject deathParticlePref;

    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;
    GameObject[] enemyB;

    GameObject[] alertLine;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletFollower;

    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] deathParticle;

    GameObject[] targetPool;

    void Awake()
    {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[10];
        enemyB = new GameObject[1];

        alertLine = new GameObject[10];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletFollower = new GameObject[100];

        bulletEnemyA = new GameObject[200];
        bulletEnemyB = new GameObject[100];
        bulletBossA = new GameObject[200];
        bulletBossB = new GameObject[100];

        deathParticle = new GameObject[500];

        Generate();
    }

    void Generate()
    {
        //Enemy
        for(int index=0; index < enemyL.Length;index++)
        {
            enemyL[index] = Instantiate(enemyLPref);
            enemyL[index].SetActive(false);
        }
        for (int index = 0; index < enemyM.Length; index++)
        {
            enemyM[index] = Instantiate(enemyMPref);
            enemyM[index].SetActive(false);
        }
        for (int index = 0; index < enemyS.Length; index++)
        {
            enemyS[index] = Instantiate(enemySPref);
            enemyS[index].SetActive(false);
        }
        for(int index= 0; index < alertLine.Length; index++) 
        {
            alertLine[index] = Instantiate(alertLinePref);
            alertLine[index].SetActive(false);
        }
        for(int index=0; index < enemyB.Length; index++)
        {
            enemyB[index] = Instantiate(enemyBPref);
            enemyB[index].SetActive(false);
        }

        //Item
        for (int index = 0; index < itemCoin.Length; index++)
        {
            itemCoin[index] = Instantiate(itemCoinPref);
            itemCoin[index].SetActive(false);
        }
        for (int index = 0; index < itemPower.Length; index++)
        {
            itemPower[index] = Instantiate(itemPowerPref);
            itemPower[index].SetActive(false);
        }
        for (int index = 0; index < itemBoom.Length; index++)
        {
            itemBoom[index] = Instantiate(itemBoomPref);
            itemBoom[index].SetActive(false);
        }

        //Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPref);
            bulletPlayerA[index].SetActive(false);
        }
        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPref);
            bulletPlayerB[index].SetActive(false);
        }
        for(int index=0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPref);
            bulletFollower[index].SetActive(false);
        }

        //EnemyBullet
        for (int index = 0; index < bulletEnemyA.Length; index++)
        {
            bulletEnemyA[index] = Instantiate(BulletEnemyAPref);
            bulletEnemyA[index].SetActive(false);
        }
        for (int index = 0; index < bulletEnemyB.Length; index++)
        {
            bulletEnemyB[index] = Instantiate(BulletEnemyBPref);
            bulletEnemyB[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossA.Length; index++)
        {
            bulletBossA[index] = Instantiate(bulletBossAPref);
            bulletBossA[index].SetActive(false);
        }
        for (int index = 0; index < bulletBossB.Length; index++)
        {
            bulletBossB[index] = Instantiate(bulletBossBPref);
            bulletBossB[index].SetActive(false);
        }

        //Effect
        for(int index = 0; index < deathParticle.Length; index++)
        {
            deathParticle[index] = Instantiate(deathParticlePref);
            deathParticle[index].SetActive(false);
        }

    }

    public GameObject MakeObj(string type)
    {
        
        switch(type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "AlertLine":
                targetPool = alertLine;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletFollwer":
                targetPool = bulletFollower;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "DeathParticle":
                targetPool = deathParticle;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "EnemyB":
                targetPool = enemyB;
                break;
            case "AlertLine":
                targetPool = alertLine;
                break;
            case "ItemCoin":
                targetPool = itemCoin;
                break;
            case "ItemPower":
                targetPool = itemPower;
                break;
            case "ItemBoom":
                targetPool = itemBoom;
                break;
            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "BulletFollwer":
                targetPool = bulletFollower;
                break;
            case "BulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "BulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "BulletBossA":
                targetPool = bulletBossA;
                break;
            case "BulletBossB":
                targetPool = bulletBossB;
                break;
            case "DeathParticle":
                targetPool = deathParticle;
                break;
        }
        return targetPool;
    }
}
