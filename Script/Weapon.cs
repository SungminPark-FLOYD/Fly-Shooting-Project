using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletObjA;
    [SerializeField]
    private GameObject bulletObjB;
    [SerializeField]
    private float attackRate;           //���ݼӵ�
    [SerializeField]
    private int power = 1;              //���ݷ���
    
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
            //�߻�ü ����
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
            case 1: //�߻�ü 1��
                GameObject bullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                break;
            case 2: //�߻�ü 2��
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.2f, Quaternion.identity);
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.2f, Quaternion.identity);
                break;
            case 3: //���� bulletObjB 1��  �¿� bulletObjA �� 1��
                GameObject bulletC = Instantiate(bulletObjB, transform.position, Quaternion.identity);
                //���� �߻�ü
                clonebullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                clonebullet.GetComponent<Movement2D>().MoveTo(new Vector3(-0.2f, 1, 0));
                //������ �߻�ü
                clonebullet = Instantiate(bulletObjA, transform.position, Quaternion.identity);
                clonebullet.GetComponent<Movement2D>().MoveTo(new Vector3(0.2f, 1, 0));
                break;
        }
    }
}
