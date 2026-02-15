using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    /// <summary>
    /// 몬스터의 이동 속도
    /// </summary>
    [SerializeField]
    private float monsterMoveSpeed;
    /// <summary>
    /// 몬스터가 쫓아다닐 타겟(플레이어)
    /// </summary>
    [SerializeField]
    private Rigidbody2D target;

    /// <summary>
    /// 몬스터를 이동시키는데 필요한 Rigidbody2D
    /// </summary>
    private Rigidbody2D monsterRigidbody2D;
    private Monster monster;

    private void Awake()
    {
        monsterRigidbody2D = GetComponent<Rigidbody2D>();
        monster = GetComponent<Monster>();
    }

    /// <summary>
    /// 타겟 세팅
    /// </summary>
    /// <param name="playerRigidbody2D"></param>
    public void SetTarget(Rigidbody2D playerRigidbody2D)
    {
        target = playerRigidbody2D;
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 타겟을 향해 이동시키는 함수
    /// </summary>
    private void Move()
    {
        // 플레이어를 향하는 방향 벡터 계산
        Vector2 directionToTarget = (target.position - monsterRigidbody2D.position).normalized;
        // 실제 이동 거리 계산(속도 x 시간)
        Vector2 movementDelta = directionToTarget * monsterMoveSpeed * Time.fixedDeltaTime;

        // Rigidbody2D를 이용해 위치 이동
        monsterRigidbody2D.MovePosition(monsterRigidbody2D.position + movementDelta);
    }
}
