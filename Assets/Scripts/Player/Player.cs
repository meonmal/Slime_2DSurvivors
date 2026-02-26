using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, XpManager
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
    [SerializeField]
    private float absorptionTime;
    [SerializeField]
    private LevelUpManager levelUPManager;

    private Collider2D target;
    private int currentXp;

    private void Start()
    {
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

    private void AttractNearbyExpOrbs()
    {
        target = Physics2D.OverlapCircle(transform.position, playerStats.ExpMagnetRadius, targetLayer);


        if (target != null)
        {
            Rigidbody2D targetRigid = target.GetComponent<Rigidbody2D>();

            Vector2 direction = (transform.position - target.transform.position).normalized;
            Vector2 moveDelta = direction * 5f;

            targetRigid.linearVelocity = moveDelta;
        }
    }
}
