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
        //플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하도록 함
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

        //방향키를 눌러 이동방향 설정
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        movement2D.MoveTo(new Vector3(x, y, 0));

        //움직임 애니메이션
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
        //이동 방향 초기화
        movement2D.MoveTo(Vector3.zero);
        //충돌 박스 삭제
        Destroy(GetComponent<BoxCollider2D>());
        isDie = true;

        //점수 저장
        PlayerPrefs.SetInt("Score", score);
        //다음 씬으로 이동
        SceneManager.LoadScene(nextSceneName);

    }

}
