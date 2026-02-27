using System.Collections;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    /// <summary>
    /// 무기의 데이터(스탯)
    /// </summary>
    public LevelUpSO weaponCoolTime;

    /// <summary>
    /// 공격 타이머
    /// </summary>
    private float timer;

    protected virtual void Update()
    {
        timer += Time.deltaTime;

        if(weaponCoolTime.CurrentValue <= timer)
        {
            timer = 0f;
            Attack();
        }
    }

    /// <summary>
    /// 공격 함수
    /// </summary>
    protected abstract IEnumerator Attack();
}
