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
}
