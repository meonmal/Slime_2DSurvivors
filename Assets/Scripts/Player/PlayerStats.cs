using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    /// <summary>
    /// 플레이어의 레벨
    /// </summary>
    [SerializeField]
    private int level;

    /// <summary>
    /// 플레이어의 최대 HP
    /// </summary>
    [SerializeField]
    private int maxHP;

    /// <summary>
    /// 플레이어의 현재 레벨의 최대 경험치량
    /// </summary>
    public int[] maxAmount = new int[] { };

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

    public int MaxHP
    {
        get
        {
            return maxHP;
        }
        set
        {
            maxHP = value;
        }
    }
}
