using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletObjA;
    [SerializeField]
    private GameObject bulletObjB;
    [SerializeField]
    private float attackRate;           //공격속도
    [SerializeField]
    private int power = 1;              //공격레벨
    
    private bool isAttack;
    
    public void StartFiring()
    {
        StopCoroutine(TryAttack());
        isAttack = true;
        StartCoroutine(TryAttack());
    }
    
    public void StopFiring()
    {   
        isAttack = false;
         StopCoroutine(TryAttack());  
    }

    private IEnumerator TryAttack()
    {     
        while (isAttack)
        {
            //발사체 생성
            //Instantiate(bulletObjA, transform.position, Quaternion.identity);

            AttackByLevel();
            
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void AttackByLevel()
    {
        GameObject clonebullet = null;
        switch(power)
        {
            case 1: //발사체 1개
                GameObject bullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                break;
            case 2: //발사체 2개
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3: //전방 bulletObjB 1개  좌우 bulletObjA 각 1개
                GameObject bulletC = Instantiate(bulletObjB, transform.position, Quaternion.identity);
                //왼쪽 발사체
                clonebullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                clonebullet.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));
                //오른쪽 발사체
                clonebullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                clonebullet.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }
}
