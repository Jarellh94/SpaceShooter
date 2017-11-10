using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    int score = 0;
    Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateText();
    }


    public void Score(int points)
    {
        score += points;
        UpdateText();
    }

    public void Reset()
    {
        score = 0;
    }

    void UpdateText()
    {
        scoreText.text = score.ToString();
    }
}
