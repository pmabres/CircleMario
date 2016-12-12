using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScores : MonoBehaviour
{
    public int size;
    public GameObject score;
    public GameObject parent;
    private List<GameObject> scores = new List<GameObject>();
	// Use this for initialization
	void Start ()
	{
	    var scoreList = GameManager.GetAllPlayerPrefScores();
        for (int i = 1; i < size + 1; i++)
	    {
            var gameObj = Instantiate(score,
           new Vector3(score.transform.position.x,
               score.transform.position.y - 40 * i,
               score.transform.position.z), Quaternion.identity, parent.transform);
            gameObj.SetActive(true);
            scores.Add(gameObj);
	        if (i - 1 < scoreList.Count)
	        {
	            gameObj.GetComponent<Text>().text = "Coins: " + scoreList[i - 1].ScoreValue + "    -      " +
	                                                scoreList[i - 1].ScoreDate.ToString();
	        }
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
