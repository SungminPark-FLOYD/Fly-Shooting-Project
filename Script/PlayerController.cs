using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private StageData stageData;
    //[SerializeField]
    //private KeyCode keyCodeAttack = KeyCode.Space;
    [SerializeField]
    private KeyCode keyCodeBoom = KeyCode.E;

    private Movement2D movement2D;
    private Weapon weapon;
   
    Animator anim;

    private bool isDie;
    private int score;
    public int Score { set => score = Mathf.Max(0, value); get => score; }
    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Boom();       
    }

    private void LateUpdate()
    {
        //�÷��̾� ĳ���Ͱ� ȭ�� ���� �ٱ����� ������ ���ϵ��� ��
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch(item.type)
            {
                case "Coin":
                    score += 1000;
                    break;
                case "Power":
                    if (weapon.power == weapon.maxPower)
                        score += 500;
                    else
                    {
                        weapon.power++;
                        weapon.AddFollower();
                    }
                        
                    break;
                case "Boom":
                    if (weapon.boom == weapon.maxBoom)
                        score += 500;
                    else
                        weapon.boom++;
                    break;
            }

            collision.gameObject.SetActive(false);
        }
    }
    

    private void Move()
    {
        if (isDie == true) return;

        //����Ű�� ���� �̵����� ����
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        //������ �ִϸ��̼�
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)x);
        }
    }  

    private void Boom()
    {
        if(Input.GetKeyDown(keyCodeBoom)) 
        {
            weapon.OnBoom();
        }
    }

    public void OnDie()
    {
        //�̵� ���� �ʱ�ȭ
        movement2D.MoveTo(Vector3.zero);
        //�浹 �ڽ� ����
        Destroy(GetComponent<BoxCollider2D>());
        isDie = true;

        //���� ����
        PlayerPrefs.SetInt("Score", score);
        //���� ������ �̵�
        SceneManager.LoadScene(nextSceneName);

    }

}
