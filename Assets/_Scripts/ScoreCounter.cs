using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public void SetScoreText(int score)
    {
        GetComponent<Text>().text = score.ToString();
    }

    // Use this for initialization
	void Start () {
        GameManager.ScoreChanged += SetScoreText;
	}
    

    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy()
    {
        GameManager.ScoreChanged -= SetScoreText;
    }
}
