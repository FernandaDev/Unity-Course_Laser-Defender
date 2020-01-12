using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;

    private void OnEnable()
    {
        GameManager.OnScoreChange.AddListener(UpdateScoreDisplay);
        Player.OnHealthChange.AddListener(UpdateHeatlhDisplay);
    }
    private void OnDisable()
    {
        GameManager.OnScoreChange.RemoveListener(UpdateScoreDisplay);
        Player.OnHealthChange.RemoveListener(UpdateHeatlhDisplay);
    }

    private void Start()
    {
        UpdateScoreDisplay();
        UpdateHeatlhDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score "+ GameManager.instance.GetScore().ToString();
    }

    void UpdateHeatlhDisplay()
    {
        if (healthText == null) { return; }

        healthText.text = "HP: " + FindObjectOfType<Player>()?.CurrentHealth.ToString();
    }

}
