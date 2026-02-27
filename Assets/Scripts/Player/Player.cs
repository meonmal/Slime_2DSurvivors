using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, XpManager, IDamageable
{
    /// <summary>
    /// 플레이어의 스탯
    /// </summary>
    public PlayerStats playerStats;

    /// <summary>
    /// 경험치 구슬의 레이어
    /// </summary>
    [SerializeField]
    private LayerMask targetLayer;

    /// <summary>
    /// 레벨업 함수를 호출하기 위한 레벨업 매니저
    /// </summary>
    [SerializeField]
    private LevelUpManager levelUPManager;

    /// <summary>
    /// 플레이어 이동속도
    /// </summary>
    [SerializeField]
    private LevelUpSO playerMoveSpeed;
    /// <summary>
    /// 플레이어 데미지
    /// </summary>
    [SerializeField]
    private LevelUpSO playerDamage;
    /// <summary>
    /// 경험치 흡수 범위
    /// </summary>
    [SerializeField]
    private LevelUpSO expRadius;

    /// <summary>
    /// 플레이어의 현재 체력
    /// 확인을 위해 [SerializeField]를 씀
    /// </summary>
    [SerializeField]
    private int currentHP;

    private Collider2D target;
    private int currentXp;

    public int CurrentXp => currentXp;
    public int CurrentHP => currentHP;
    public LevelUpSO PlayerMoveSpeed => playerMoveSpeed;
    public LevelUpSO PlayerDamage => playerDamage;

    private void Start()
    {
        currentHP = playerStats.MaxHP;
        playerStats.Level = 0;
        currentXp = 0;
    }

    public void GetXp(int playerXpAmount)
    {
        int i = playerStats.Level;

        currentXp += playerXpAmount;

        if (currentXp >= playerStats.maxAmount[i])
        {
            playerStats.Level++;
            levelUPManager.Open();
        }
    }

    private void Update()
    {
        AttractNearbyExpOrbs();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            Die();
        }
    }

    private void AttractNearbyExpOrbs()
    {
        target = Physics2D.OverlapCircle(transform.position, expRadius.CurrentValue, targetLayer);


        if (target != null)
        {
            Rigidbody2D targetRigid = target.GetComponent<Rigidbody2D>();

            Vector2 direction = (transform.position - target.transform.position).normalized;
            Vector2 moveDelta = direction * 5f;

            targetRigid.linearVelocity = moveDelta;
        }
    }

    private void Die()
    {

    }
}
