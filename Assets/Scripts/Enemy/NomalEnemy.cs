using UnityEngine;

public class NomalEnemy : MonoBehaviour
{
    float fSpeed = 2.5f;                // 적 속도

    int nEnemyHP = 0;
    int nEnemyMaxHP = 100;

    SpriteRenderer sprite;              //스프라이트렌더러
    Rigidbody2D rigid;                  //Rigidbody2D
    PlayerController player;            //플레이어컨트롤러 스크립트

    public Transform playerTransform;   // 플레이어 위치
    private bool isDead = false;        // 죽음 여부
    public GameObject expOrbPrefab;     // 인스펙터에 프리팹 연결

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
            Debug.LogError("플레이어를 찾을 수 없습니다.");
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

    // 플레이어 따라가기
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
        // 어떠한 무기에 닿았을때 데미지 입기
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

    // 좌우 회전
    void FlipByScale(bool faceLeft)
    {
        Vector3 scale = transform.localScale;
        // 왼쪽 보기(faceLeft == true)면 x는 음수, 오른쪽 보기면 양수
        scale.x = Mathf.Abs(scale.x) * (faceLeft ? -1f : 1f);
        transform.localScale = scale;
    }

    // 시간 지날때마다 적의 속도 증가
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
