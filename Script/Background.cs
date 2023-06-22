using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private int startIndex;
    [SerializeField]
    private int endIndex;
    [SerializeField]
    private Transform[] sprites;
    [SerializeField]
    private Vector3 moveDirection = Vector3.down;

    float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2 + 1;
    }

    private void Update()
    {
        Move();
        Scrolling();
    }

    private void Move()
    {
        //moveDirection �������� speed �ӵ��� �̵�
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void Scrolling()
    {
        if (sprites[endIndex].position.y < viewHeight * (-1))
        {
            //sprite ����
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up * viewHeight;

            //�̵� �Ϸ�Ǹ� endIndex,startIndex ����
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : startIndexSave - 1;
        }
    }
}
