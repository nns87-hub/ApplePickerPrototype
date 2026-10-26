using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public int score = 0;
    Text uiText;
    int displayedScore = int.MinValue;

    void Start()
    {
        uiText = GetComponent<Text>();
        RefreshText();
    }

    void Update()
    {
        if (score == displayedScore)
        {
            return;
        }
        RefreshText();
    }

    void RefreshText()
    {
        displayedScore = score;
        if (uiText != null)
        {
            uiText.text = score.ToString("#,0");
        }
    }
}
