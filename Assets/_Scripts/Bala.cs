using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour
{
    public float Speed = 2f;
	void Update () {
	
        this.transform.Translate(Vector3.up * Speed * Time.deltaTime);
	}
}
