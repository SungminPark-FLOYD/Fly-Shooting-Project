using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float curHealth;

    private Enemy enemy;
    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();


    }

    private void OnEnable()
    {
        curHealth = maxHealth;                              //���� ü���� �ִ� ü�°� ���� ����
    }

    public void TakeDamage(float damage)
    {
        if (curHealth == 0) return;
        //���� ü���� damage��ŭ ����
        curHealth -= damage;
                
        //�ǰ�ȿ��
        spriteRenderer.sprite = sprites[1];
        Invoke("OnHitAnimation", 0.05f);
        
       
        //ü���� 0 ���ϸ���
        if(curHealth <=  0)
        {
            //�� ĳ���� ���
            enemy.OnDie();            
        }

    }

    private void OnHitAnimation()
    {        
        spriteRenderer.sprite = sprites[0];
    }

}
