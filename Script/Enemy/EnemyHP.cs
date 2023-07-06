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
        curHealth = maxHealth;                              //현재 체력을 최대 체력과 같게 설정
    }

    public void TakeDamage(float damage)
    {
        if (curHealth == 0) return;
        //현재 체력을 damage만큼 감소
        curHealth -= damage;
                
        //피격효과
        spriteRenderer.sprite = sprites[1];
        Invoke("OnHitAnimation", 0.05f);
        
       
        //체력이 0 이하면사망
        if(curHealth <=  0)
        {
            //적 캐릭터 사망
            enemy.OnDie();            
        }

    }

    private void OnHitAnimation()
    {        
        spriteRenderer.sprite = sprites[0];
    }

}
