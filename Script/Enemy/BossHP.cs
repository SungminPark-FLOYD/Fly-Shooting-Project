using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 500;    //�ִ� ü��
    [SerializeField]
    private float curHealth;          //���� ü��

    private Boss boss;

    private Animator anim;

    public float MaxHP => maxHealth;
    public float CurHP => curHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<Boss>();
    }

    private void OnEnable()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (curHealth == 0) return;
        //���� ü���� damage��ŭ ����
        curHealth -= damage;

        anim.SetTrigger("OnHit");

        if(curHealth <= 0)
        {
            boss.OnDie();
        }

    }
}
