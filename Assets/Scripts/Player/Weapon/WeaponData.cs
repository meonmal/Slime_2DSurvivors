using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    /// <summary>
    /// 무기 자체 데미지
    /// </summary>
    [SerializeField]
    private int weaponDamage;
    /// <summary>
    /// 무기 자체 쿨타임
    /// </summary>
    [SerializeField]
    private float weaponAttackCoolTime;

    /// <summary>
    /// 무기 자체 데미지 프로퍼티
    /// </summary>
    public int WeaponDamage
    {
        get
        {
            return weaponDamage;
        }
        set
        {
            weaponDamage = value;
        }
    }

    /// <summary>
    /// 무기 자체 쿨타임 프로퍼티
    /// </summary>
    public float WeaponAttackCoolTime
    {
        get
        {
            return weaponAttackCoolTime;
        }
        set
        {
            weaponAttackCoolTime = value;
        }
    }
}
