using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class MonsterSpawner : MonoBehaviour
{ 
    /// <summary>
    /// 스폰 영역 최소 x 좌표
    /// </summary>
    [SerializeField]
    private float minXSpawnPosition;
    /// <summary>
    /// 스폰 영역 최대 x 좌표
    /// </summary>
    [SerializeField]
    private float maxXSpawnPosition;
    /// <summary>
    /// 스폰 영역 최소 y 좌표
    /// </summary>
    [SerializeField]
    private float minYSpawnPosition;
    /// <summary>
    /// 스폰 영역 최대 y 좌표
    /// </summary>
    [SerializeField]
    private float maxYSpawnPosition;
    /// <summary>
    /// 소환 대기 시간
    /// </summary>
    [SerializeField]
    private float spawnTime;
    /// <summary>
    /// 소환할 몬스터들
    /// </summary>
    [SerializeField]
    private Monster[] monsterPrefabs;
    /// <summary>
    /// 몬스터가 쫓아다닐 플레이어
    /// 실제로 들고 있는건 얘다.
    /// </summary>
    [SerializeField]
    private Rigidbody2D target;

    /// <summary>
    /// 몬스터를 관리하는 오브젝트 풀
    /// </summary>
    private IObjectPool<Monster> pool;

    private void Awake()
    {
        // ObjectPool 생성
        pool = new ObjectPool<Monster>(
            SpawnMonster, 
            OnGet, 
            OnRelease, 
            OnDestroyMonster, 
            collectionCheck: false,     // 중복 Release 검사
            defaultCapacity: 20,        // 기본 생성 용량
            maxSize: 200);               // 최대 생성 용량
    }

    /// <summary>
    /// 새 객체가 필요할 때 호출할 코루틴 함수
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawn()
    {
        while (true)
        {
            // 풀에서 몬스터 하나를 꺼낸다.
            Monster monsters = pool.Get();
            // 몬스터의 위치는 SpawnPosition()에 따라 랜덤하게 지정
            monsters.transform.position = SpawnPosition();
            // 몬스터가 따라다닐 타겟 지정
            monsters.SetTarget(target);

            // 소환 대기 시간 만큼 대기한다.
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void Start()
    {
        // 소환 코루틴 실행
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// Pool에서 새로운 몬스터가 필요할 때 실행
    /// Instantiate()는 여기에서만 실행한다.
    /// </summary>
    /// <returns>랜덤한 몬스터 반환</returns>
    private Monster SpawnMonster()
    {
        // 랜덤 프리팹 선택
        int index = Random.Range(0, monsterPrefabs.Length);
        // 선택된 프리팹을 생성한다.
        Monster clone = Instantiate(monsterPrefabs[index]);
        // 방금 생성한 몬스터의 풀을 여기로 지정
        clone.SetPool(pool);

        return clone;
    }

    /// <summary>
    /// 소환할 몬스터의 위치를 계산한다.
    /// </summary>
    /// <returns></returns>
    private Vector2 SpawnPosition()
    {
        // x 축의 스폰 위치를 반전시키기 위해 랜덤한 숫자 생성
        float randomXNumber = Random.Range(0, 101);
        // x 축의 스폰 위치
        float randomXPosition = Random.Range(minXSpawnPosition, maxXSpawnPosition);

        // 위에서 생성한 숫자가 51보다 높거나 같으면 실행
        if (randomXNumber >= 51)
        {
            // x 축의 스폰 위치 반전
            randomXPosition *= -1;
        }

        // y 축의 스폰 위치를 반전시키기 위해 랜덤한 숫자 생성
        float randomYNumber = Random.Range(0, 101);
        // y 축의 스폰 위치
        float randomYPosition = Random.Range(minYSpawnPosition, maxYSpawnPosition);

        // 위에서 생성한 숫자가 51보다 높거나 같으면 실행
        if (randomYNumber >= 51)
        {
            // y 축의 스폰 위치 반전
            randomYPosition *= -1;
        }

        // Vector2 타입의 스폰 위치 생성
        Vector2 spawnPosition = new Vector2(randomXPosition, randomYPosition);

        return spawnPosition;
    }

    /// <summary>
    /// 풀에서 꺼낼 때 실행
    /// </summary>
    /// <param name="monster">풀에서 꺼낼 몬스터</param>
    private void OnGet(Monster monster)
    {
        // 몬스터 게임 오브젝트 활성화
        monster.gameObject.SetActive(true);
    }

    /// <summary>
    /// 풀로 되돌릴 때 실행
    /// </summary>
    /// <param name="monster">풀로 되돌릴 몬스터</param>
    private void OnRelease(Monster monster)
    {
        // 몬스터 게임 오브젝트 비활성화
        monster.gameObject.SetActive(false);
    }

    /// <summary>
    /// 풀에서 몬스터를 아예 삭제시킬 때 호출할 함수
    /// </summary>
    /// <param name="monster">삭제시킬 몬스터</param>
    private void OnDestroyMonster(Monster monster)
    {
        // 몬스터 게임 오브젝트 삭제
        Destroy(monster.gameObject);
    }
}
