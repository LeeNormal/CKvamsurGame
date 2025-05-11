using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    // 적 체력
    int nEnemyHP = 0;
    int nEnemyMaxHP = 300;
    // 플레이어한테 가하는 공격력
    //int EnemyAttack = 5;



    // 적 속도
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
            Debug.LogError("플레이어를 찾을 수 없습니다.");
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
    // 플레이어 따라가기
    void playerRunAfter()
    {
        Vector2 targetPosition = playerTransform.position;
        Vector2 currentPosition = transform.position;

        Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition,
            fSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
    // 공격 받기 아직 공격 못 받음 콜라이더랑 같이 함
    public void Damage(int nHit)
    {
        nEnemyHP -= nHit;
        Debug.Log("몬스터 데미지 받는중 : " + nEnemyHP);
        if (nEnemyHP == 0 || nEnemyHP < 0)
        {
            Destroy(gameObject);
        }
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

    // 콜라이더 충돌 처리
    void OnTriggerEnter2D(Collider2D collider)
    {

    }
}
