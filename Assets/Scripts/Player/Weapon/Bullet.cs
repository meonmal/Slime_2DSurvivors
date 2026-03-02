using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private int damage;

    private bool isReleased;
    private float timer;
    private Vector2 _direction;

    private Rigidbody2D rigid;
    private RangedWeapon rangedWeapon;
    private Player player;

    private IObjectPool<Bullet> _pool;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rangedWeapon = GetComponentInParent<RangedWeapon>();
        player = GetComponentInParent<Player>();
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 5f)
        {
            ThisDestroy();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigid.linearVelocity = _direction * moveSpeed;
    }

    public void Initialize(Vector2 direction)
    {
        _direction = direction;
        timer = 0f;
        isReleased = false;
        damage = (int)player.PlayerDamage.CurrentValue * (int)rangedWeapon.WeaponDamage.CurrentValue;

        // z축 회전 각도 구하기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.Euler(0, 0, angle);

        transform.rotation = rotate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.GetComponent<IDamageable>();

        if (iDamageable != null && collision.CompareTag("Monster"))
        {
            iDamageable.TakeDamage(damage);
            ThisDestroy();
        }
    }

    private void ThisDestroy()
    {
        if(isReleased)
        {
            return;
        }

        isReleased = true;
        this._pool.Release(this);
    }
}
