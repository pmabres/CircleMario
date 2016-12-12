using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour
{
   
	void Update () {
	
        this.transform.Translate(Vector3.up * GameManager.DifficultyHelper.GetBulletSpeed() * Time.deltaTime);
	}
}
