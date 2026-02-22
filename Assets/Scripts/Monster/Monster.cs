using UnityEngine;
using UnityEngine.Pool;

public class Monster : MonoBehaviour, IDamageable
{
    public MonsterStats monsterStats;

    /// <summary>
    /// 이 몬스터가 소속된 오브젝트 풀 참조
    /// MonsterSpawner가 몬스터를 생성할 때 SetPool로 주입해줌
    /// </summary>
    private IObjectPool<Monster> _pool;

    private MonsterMovement monsterMovement;

    /// <summary>
    /// 몬스터의 현재 체력
    /// </summary>
    private int currentHp;

    /// <summary>
    /// MonsterSpawner에서 SpawnMonster()를 실행할 때 호출 됨
    /// 방금 생성된 몬스터를 MonsterSpawner가 가진 풀에 소속시키는 함수
    /// </summary>
    /// <param name="pool"></param>
    public void SetPool(IObjectPool<Monster> pool)
    {
        _pool = pool;
    }

    private void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();
        currentHp = monsterStats.MaxHp;
    }

    /// <summary>
    /// 타겟 세팅
    /// </summary>
    /// <param name="targetRigidbody2D"></param>
    public void SetTarget(Rigidbody2D targetRigidbody2D)
    {
        monsterMovement.SetTarget(targetRigidbody2D);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 몬스터를 풀로 되돌리는 함수
    /// </summary>
    public void Despawn()
    {
        // 호오오오오오옥시 몰라서 만든 안전장치
        // 방금 생성한 게임 오브젝트가 풀에 소속되어 있지 않으면 실행
        if(_pool == null)
        {
            Destroy(gameObject);
            return;
        }

        _pool.Release(this);
    }

    /// <summary>
    /// 몬스터 사망 처리를 하는 함수
    /// </summary>
    public void Die()
    {
        Despawn();
    }
}
