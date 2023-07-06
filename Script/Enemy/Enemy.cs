using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int scorePoint;
    [SerializeField]
    private string[] itemPrefs;
    [SerializeField]
    private string enemyName;
    private PlayerController playerController;

    public GameManager gameManager;
    public ObjectManager objectManager;

    private void Awake()
    {
        itemPrefs = new string[] { "ItemCoin", "ItemPower", "ItemBoom" };
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Àû¿¡°Ô ºÎµúÈù ¿ÀºêÁ§Æ® ÅÂ±×°¡ "Player"ÀÌ¸é
        if(collision.CompareTag("Player"))
        {
            OnDie();
        }
        
    }

    public void OnDie()
    {      
        playerController.Score += scorePoint;
        //Æø¹ß ÀÌÆåÆ®
        gameManager.CallExplosion(transform.position, enemyName);

        SpawnItem();

        //Àû »ç¸Á
        gameObject.SetActive(false);
    }
    private void SpawnItem()
    {
        
        int ran =  Random.Range(0, 10);
       
        if(ran < 4)
        {
            Debug.Log("Not Item");
        }
        else if(ran < 6)
        {
            GameObject itemCoin = objectManager.MakeObj(itemPrefs[0]);
            itemCoin.transform.position = transform.position;
 
        }
       else if(ran < 8)
        {
            GameObject itemPower = objectManager.MakeObj(itemPrefs[1]);
            itemPower.transform.position = transform.position;
        }
        else if (ran < 10)
        {
            GameObject itemBoom = objectManager.MakeObj(itemPrefs[2]);
            itemBoom.transform.position = transform.position;
        }
    }
}
