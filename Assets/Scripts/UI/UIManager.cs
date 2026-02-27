using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// UI 정보를 갖고 오기 위한 플레이어
    /// </summary>
    [SerializeField]
    private Player player;
    /// <summary>
    /// 플레이어의 EXP바
    /// </summary>
    [SerializeField]
    private Slider playerEXPSlider;
    /// <summary>
    /// 플레이어의 체력 바
    /// </summary>
    [SerializeField]
    private Slider playerHPSlider;
    /// <summary>
    /// 플레이어의 레벨 텍스트
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI levelText;

    private void Update()
    {
        SliderValue();
        PlayerText();
    }


    private void SliderValue()
    {
        playerEXPSlider.value = (float)player.CurrentXp / player.playerStats.maxAmount[player.playerStats.Level];
        playerHPSlider.value = (float)player.CurrentHP / player.playerStats.MaxHP;
    }

    private void PlayerText()
    {
        levelText.text = $"Lv : {player.playerStats.Level + 1}";
    }
}
