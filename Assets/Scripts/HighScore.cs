using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// This script demonstrates that it can display, it can track as well as saves the highest score 
/// that the player will achieve across any gameplay. - [Added by Sharnez Mosby]
/// </summary>

public class HighScore : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI highscoreText;

    private float highscore;

    private void Start()
    {
        highscore = PlayerPrefs.GetFloat("HighScore", 0);
        UpdateHighScoreText();
 
    }

    public void CheckHighScore(float currentScore)
    {
        if (currentScore > highscore) 
        {
            highscore = currentScore;
            PlayerPrefs.SetFloat("HighScore", highscore); 
            PlayerPrefs.Save();

            UpdateHighScoreText();                                                                
        }
    }
    private void UpdateHighScoreText()
    {
        highscoreText.text = $"Highscore: {highscore}";
    }
}
