using UnityEngine;

[CreateAssetMenu]
public class MonsterStats : ScriptableObject
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int monsterDamage;
    [SerializeField]
    private int xpAmount;

    /// <summary>
    /// 몬스터의 이동속도
    /// </summary>
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }

    /// <summary>
    /// 몬스터의 최대 HP
    /// </summary>
    public int MaxHp
    {
        get
        {
            return maxHp;
        }
        set
        {
            maxHp = value;
        }
    }

    /// <summary>
    /// 몬스터의 데미지
    /// </summary>
    public int MonsterDamage
    {
        get
        {
            return monsterDamage;
        }
        set
        {
            monsterDamage = value;
        }
    }

    /// <summary>
    /// 몬스터가 주는 경험치량
    /// </summary>
    public int XpAmount
    {
        get
        {
            return xpAmount;
        }
        set
        {
            xpAmount = value;
        }
    }
}
