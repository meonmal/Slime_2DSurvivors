using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class LevelUpSO : ScriptableObject
{
    [Header("UI")]
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string title;
    [SerializeField]
    private string desc;

    public Sprite Icon => icon;
    public string Title => title;
    public string Desc => desc;

    public enum AddStat
    {
        PlayerMoveSpeed,
        PlayerDamage,
        PlayerExpMagnetRadius,
        WeaponDamage,
        WeaponCoolTime,
    }

    [Header("업그레이드할 스탯 타입")]
    [SerializeField]
    private AddStat stat;
    public AddStat Stat => stat;

    [Header("레벨마다 값")]
    [SerializeField]
    private List<float> values = new();

    private int currentLevel;

    public float CurrentValue =>
        values[Mathf.Clamp(currentLevel, 0, values.Count - 1)];

    public bool IsMax =>
        currentLevel >= values.Count - 1;

    private void OnEnable()
    {
        currentLevel = 0;
    }

    public float LevelUp()
    {
        if (!IsMax)
            currentLevel++;

        return CurrentValue;
    }
}
