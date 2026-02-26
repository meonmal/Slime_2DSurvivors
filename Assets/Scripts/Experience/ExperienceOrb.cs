using UnityEngine;
using UnityEngine.Pool;

public class ExperienceOrb : MonoBehaviour
{
    private IObjectPool<ExperienceOrb> _pool;

    private int _amount;
    private bool isReleased;

    public void SetPool(IObjectPool<ExperienceOrb> pool)
    {
        _pool = pool;
    }

    public void Initialize(int amount)
    {
        isReleased = false;
        _amount = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        XpManager xpManager = collision.GetComponent<XpManager>();

        if(xpManager != null)
        {
            xpManager.GetXp(_amount);
            ThisDestroy();
        }
    }

    private void ThisDestroy()
    {
        if (isReleased)
        {
            return;
        }

        isReleased = true;
        this._pool.Release(this);
    }
}
