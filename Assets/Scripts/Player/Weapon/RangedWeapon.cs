using System.Collections;
using UnityEngine;

public class RangedWeapon : WeaponBase
{
    /// <summary>
    /// 타겟(몬스터)의 Layer(Monster)
    /// </summary>
    [SerializeField]
    private LayerMask targetLayer;
    /// <summary>
    /// 적 탐지 범위
    /// </summary>
    [SerializeField]
    private float findRange = 10f;
    /// <summary>
    /// 적을 찾는 주기
    /// </summary>
    [SerializeField]
    private float findCoolTime = 0.1f;
    /// <summary>
    /// 검색된 타겟들
    /// </summary>
    [SerializeField]
    private Collider2D[] targets;
    [SerializeField]
    private LevelUpSO weaponDamage;
    /// <summary>
    /// 오브젝트 풀로 관리하기 위한 컴포넌트
    /// </summary>
    private BulletSpawner bulletSpawner;

    /// <summary>
    /// 현재 가장 가까운 타겟
    /// </summary>
    public Collider2D currentTarget;

    public LevelUpSO WeaponDamage => weaponDamage;

    private void Awake()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
    }

    private void Start()
    {
        StartCoroutine(FindMonster());
        StartCoroutine(Attack());
    }

    /// <summary>
    /// 공격을 실행하는 함수
    /// 다만 여기에서 실제로 행하는 것은 총알 생성이다.
    /// </summary>
    protected override IEnumerator Attack()
    {
        while(true)
        {
            // 타겟이 없으면 공격은 쉰다.
            if(currentTarget == null)
            {
                yield return null;
                continue;
            }

            // 타겟과의 거리 계산
            Vector2 direction = currentTarget.transform.position - transform.position;

            // 거리가 너무 가깝다면 이번 틱은 실행x
            if (direction.sqrMagnitude < 0.001f)
            {
                yield return null;
                continue;
            }
            // 방향 벡터 정규화
            direction.Normalize();

            bulletSpawner.Spawn(direction);

            // 공격 쿨타임동안 대기
            yield return new WaitForSeconds(weaponCoolTime.CurrentValue);
        }
    }

    /// <summary>
    /// 가장 가까운 몬스터를 찾는 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator FindMonster()
    {
        while (true)
        {
            // 현재 범위 내에 있는 오브젝트의 레이어를 검색한다.
            targets = Physics2D.OverlapCircleAll(transform.position, findRange, targetLayer);

            // 만약 가장 가까운 적이 없다면 현재 타겟 없음, 아래 코드 실행x
            if (targets.Length == 0)
            {
                currentTarget = null;
                yield return null;
                continue;
            }

            // 최소 거리 비교를 위한 기준값 세팅
            currentTarget = targets[0];
            // 위에서 발견한 몬스터의 거리를 최소 거리 기준점으로 설정
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);

            // 감지된 모든 몬스터들 중 가장 가까운 몬스터를 찾는다.
            foreach(Collider2D col in targets)
            {
                // 현재 순회중인 몬스터와의 거리 계산
                float distance2 = Vector2.Distance(transform.position, col.transform.position);

                // 현재 기준보다 더 가까운 몬스터를 찾게 되면 그 몬스터를 기준점으로 변경
                if(distance > distance2)
                {
                    distance = distance2;
                    currentTarget = col;
                }
            }

            yield return new WaitForSeconds(findCoolTime);
        }
    }
}
