using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private bool isReleased;
    private float timer;
    private Vector2 _direction;
    private Rigidbody2D rigid;
    private IObjectPool<Bullet> _pool;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10f)
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

        // z축 회전 각도 구하기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.Euler(0, 0, angle);

        transform.rotation = rotate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
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
