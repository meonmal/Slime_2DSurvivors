using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    /// <summary>
    /// 총알을 관리하는 오브젝트 풀
    /// </summary>
    private IObjectPool<Bullet> pool;

    /// <summary>
    /// 생성할 프리팹
    /// </summary>
    [SerializeField]
    private Bullet bulletPrefab;

    private void Awake()
    {
        pool = new ObjectPool<Bullet>(
            SpawnBullet,
            OnGet,
            OnRelease,
            DestroyBullet,
            true,
            20,
            maxSize:100);
    }

    public void Spawn(Vector2 direction)
    {
        Bullet bullet = pool.Get();
        bullet.transform.position = SpawnPosition();
        bullet.Initialize(direction);
    }

    private Bullet SpawnBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform);
        bullet.SetPool(pool);

        return bullet;
    }

    private Vector2 SpawnPosition()
    {
        Vector2 playerPosition = transform.position;

        return playerPosition;
    }

    public void OnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    public void OnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
