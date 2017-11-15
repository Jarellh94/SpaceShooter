using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    Text myText;

	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myText.text = ScoreKeeper.score.ToString();
        ScoreKeeper.Reset();
	}
	
}
