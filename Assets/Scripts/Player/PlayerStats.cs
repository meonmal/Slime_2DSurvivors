using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// 플레이어의 이동속도
    /// </summary>
    [SerializeField]
    private float moveSpeed;
    /// <summary>
    /// 플레이어의 데미지
    /// </summary>
    [SerializeField]
    private int damage;
    /// <summary>
    /// 플레이어의 레벨
    /// </summary>
    [SerializeField]
    private int level;
    [SerializeField]
    private float expMagnetRadius;

    public int[] maxAmount = new int[] { };

    /// <summary>
    /// 플레이어의 이동속도 프로퍼티
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
    /// 플레이어의 데미지
    /// </summary>
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    /// <summary>
    /// 플레이어의 레벨
    /// </summary>
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    /// <summary>
    /// 경험치 구슬 흡수 범위
    /// </summary>
    public float ExpMagnetRadius
    {
        get
        {
            return expMagnetRadius;
        }
        set
        {
            expMagnetRadius = value;
        }
    }
}
