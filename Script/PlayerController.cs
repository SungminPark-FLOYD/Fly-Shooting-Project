using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private KeyCode keyCodeAttack = KeyCode.Space;

    private Movement2D movement2D;
    private Weapon weapon;

    Animator anim;
    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon = GetComponent<Weapon>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void LateUpdate()
    {
        //�÷��̾� ĳ���Ͱ� ȭ�� ���� �ٱ����� ������ ���ϵ��� ��
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    private void Move()
    {
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

    private void Fire()
    {
        //���� Ű Down/Up ���� ���� ����
        if (Input.GetKeyDown(keyCodeAttack))
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack))
        {
            weapon.StopFiring();
        }
    }
}
