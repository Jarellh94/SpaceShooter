using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    public int extraLifePoint = 1000;
    Text scoreText;
    PlayerController player;

    void Start()
    {
        Reset();
        scoreText = GetComponent<Text>();
        UpdateText();

        player = FindObjectOfType<PlayerController>();
    }

    public void Score(int points)
    {
        score += points;

        if(score >= (score + extraLifePoint))
        {
            player.AddLife();
        }

        UpdateText();
    }

    public static void Reset()
    {
        score = 0;
    }

    void UpdateText()
    {
        scoreText.text = score.ToString();
    }
}
