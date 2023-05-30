using TMPro;
using UnityEngine;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private RectTransform childImage;
    [SerializeField] private TMP_Text levelText;

    private void Awake()
    {
        levelText.text = $"Lvl 1";
        UpdateBarPercentage(0);
        SystemEventManager.Subscribe(OnGameAction);
    }

    private void OnGameAction(SystemEventManager.ActionType type, object payload)
    {
        switch (type)
        {
            case SystemEventManager.ActionType.ExpGained when payload is float exp:
                UpdateBarPercentage(exp);
                break;
            case SystemEventManager.ActionType.LevelUp when payload is int level:
                levelText.text = $"Lvl {level}";
                break;
            case SystemEventManager.ActionType.GameReset:
                levelText.text = $"Lvl 1";
                UpdateBarPercentage(0);
                break;
        }
    }

    private void UpdateBarPercentage(float percentage)
    {
        childImage.localScale = new Vector3(percentage, childImage.localScale.y);
    }
}
