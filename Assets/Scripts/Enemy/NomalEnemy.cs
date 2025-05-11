using UnityEngine;

public class NomalEnemy : MonoBehaviour
{
    float fSpeed = 2.5f;                // �� �ӵ�

    int nEnemyHP = 0;
    int nEnemyMaxHP = 100;

    SpriteRenderer sprite;              //��������Ʈ������
    Rigidbody2D rigid;                  //Rigidbody2D
    PlayerController player;            //�÷��̾���Ʈ�ѷ� ��ũ��Ʈ

    public Transform playerTransform;   // �÷��̾� ��ġ
    private bool isDead = false;        // ���� ����
    public GameObject expOrbPrefab;     // �ν����Ϳ� ������ ����

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
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("�÷��̾ ã�� �� �����ϴ�.");
        }
        else { }
        if (player == null)
        {
            player = GetComponent<PlayerController>();
        }
        else { }





        nEnemyHP = nEnemyMaxHP;
    }
    void Update()
    {
        if (transform.position.x < playerTransform.position.x)
            FlipByScale(true);
        else
            FlipByScale(false);
        EnemySpeedUp();
        playerRunAfter();
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

    public void minuHp()
    {
        // ��� ���⿡ ������� ������ �Ա�
        if (true)
        {
            
        }
        else { }
        if(nEnemyHP == 0 || nEnemyHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

        rigid.linearVelocity = Vector2.zero;

        Instantiate(expOrbPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
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
        if (nTime == 1125)
        {
            fSpeed = 3.0f;
        }
        else if (nTime == 2250)
        {
            fSpeed = 4.0f;
        }
        else { }
    }


}
