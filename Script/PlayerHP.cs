using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private int life;
    public GameManager manager;

    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            life--;
            if (life == 0)
            {
               playerController.OnDie();
            }
            else
            {
                manager.UpdateLifeIcon(life);
                StartCoroutine("OnHit");
            }
        }
    }


    //무적 시간 만들기
    private IEnumerator OnHit()
    {
        gameObject.layer = 6; 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        yield return new WaitForSeconds(2f);

        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);

    }
    
}
