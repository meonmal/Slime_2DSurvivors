using UnityEngine;
using UnityEngine.Pool;

public class ExperienceOrbPool : MonoBehaviour
{
    private IObjectPool<ExperienceOrb> pool;

    [SerializeField]
    private ExperienceOrb expOrbPrefab;

    private void Awake()
    {
        pool = new ObjectPool<ExperienceOrb>(
            SpawnExpOrb,
            OnGet,
            OnRelease,
            DestroyExpOrb,
            true,
            20,
            maxSize: 100);
    }

    public void Spawn(int amount, Vector2 position)
    {
        ExperienceOrb expOrb = pool.Get();
        expOrb.transform.position = position;
        expOrb.Initialize(amount);
    }

    private ExperienceOrb SpawnExpOrb()
    {
        ExperienceOrb expOrb = Instantiate(expOrbPrefab);
        expOrb.SetPool(pool);

        return expOrb;
    }

    public void OnGet(ExperienceOrb expOrb)
    {
        expOrb.gameObject.SetActive(true);
    }

    public void OnRelease(ExperienceOrb expOrb)
    {
        expOrb.gameObject.SetActive(false);
    }

    public void DestroyExpOrb(ExperienceOrb expOrb)
    {
        Destroy(expOrb.gameObject);
    }
}
