using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUpManager : MonoBehaviour
{
    [SerializeField]
    private LevelUpSO[] levelUpSOs;

    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Image[] icon;
    [SerializeField]
    private TextMeshProUGUI[] titles;
    [SerializeField]
    private TextMeshProUGUI[] desc;

    public void OnSelect(LevelUpSO so)
    {
        so.LevelUp();
        Close();
    }

    public void Open()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);

        // 1️⃣ 후보 필터 (Max 제외)
        List<LevelUpSO> candidates = new();

        foreach (LevelUpSO so in levelUpSOs)
        {
            if (!so.IsMax)
            {
                candidates.Add(so);
            }
        }

        // 후보가 0개면 그냥 종료
        if (candidates.Count == 0)
            return;

        // 2️⃣ 셔플
        for (int i = 0; i < candidates.Count; i++)
        {
            int r = Random.Range(i, candidates.Count);
            (candidates[i], candidates[r]) = (candidates[r], candidates[i]);
        }

        // 3️⃣ 앞에서 최대 3개 세팅
        for (int i = 0; i < 3; i++)
        {
            if (i >= candidates.Count)
            {
                buttons[i].gameObject.SetActive(false);
                continue;
            }

            LevelUpSO so = candidates[i];

            buttons[i].gameObject.SetActive(true);

            icon[i].sprite = so.Icon;
            titles[i].text = so.Title;
            desc[i].text = so.Desc;

            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => OnSelect(so));
        }
    }

    private void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
