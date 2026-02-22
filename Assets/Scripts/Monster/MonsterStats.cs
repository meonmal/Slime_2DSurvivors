using UnityEngine;

[CreateAssetMenu]
public class MonsterStats : ScriptableObject
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int maxHp;

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

}
