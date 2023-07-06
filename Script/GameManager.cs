using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    
        
    public Image[] lifeImages;

    public GameObject player;
    public ObjectManager objectManager;

    //¸ñ¼û ·ÎÁ÷
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImages[index].color = new Color(1, 1, 1, 0);
        }
        for (int index=0; index<life; index++)
        {
            lifeImages[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void CallExplosion(Vector3 pos, string type)
    {
        GameObject explosion = objectManager.MakeObj("DeathParticle");
        Explosion explosionLogic = explosion.GetComponent<Explosion>();

        explosion.transform.position = pos;
        explosionLogic.StartExplosion(type);
    }
    
}
