using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    // �� ü��
    int nEnemyHP = 0;
    int nEnemyMaxHP = 300;
    // �÷��̾����� ���ϴ� ���ݷ�
    //int EnemyAttack = 5;



    // �� �ӵ�
    float fSpeed = 1.0f;

    SpriteRenderer sprite;
    Rigidbody2D rigid;
    //PlayerController player;

    public Transform playerTransform;

    void Start()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        else { }
        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
        else { }
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }
        else
        {
            playerTransform = playerObj.transform;
        }
    }
    public void Initalize(float fRatio)
    {
        nEnemyHP = (int)(nEnemyMaxHP * fRatio);
    }
    void Update()
    {
        playerRunAfter();
        if (transform.position.x < playerTransform.position.x)
            FlipByScale(true);
        else
            FlipByScale(false);
        EnemySpeedUp();
    }
    // �÷��̾� ���󰡱�
    void playerRunAfter()
    {
        Vector2 targetPosition = playerTransform.position;
        Vector2 currentPosition = transform.position;

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition,
            fSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
    // ���� �ޱ� ���� ���� �� ���� �ݶ��̴��� ���� ��
    public void Damage(int nHit)
    {
        nEnemyHP -= nHit;
        Debug.Log("���� ������ �޴��� : " + nEnemyHP);
        if (nEnemyHP == 0 || nEnemyHP < 0)
        {
            Destroy(gameObject);
        }
    }
    // �¿� ȸ��
    void FlipByScale(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        // ���� ����(faceLeft == true)�� x�� ����, ������ ����� ���
        scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1f : 1f);
        transform.localScale = scale;
    }
    // �ð� ���������� ���� �ӵ� ����
    void EnemySpeedUp()
    {
        int nTime = (int)Time.deltaTime;
        if (nTime == 4500)
        {
            fSpeed = 1.5f;
        }
        else if (nTime == 9000)
        {
            fSpeed = 2f;
        }
        else { }
    }

    // �ݶ��̴� �浹 ó��
    void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
