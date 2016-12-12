using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject centerPoint;
    private float speed = 20f;
    private Vector3 _direccion;
	// Use this for initialization
	void Start ()
	{
        _direccion = Vector3.forward;
        _direccion *= Random.Range(0, 2) * 2 - 1;
        Debug.Log(_direccion);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(centerPoint.transform.position, _direccion, speed * Time.deltaTime);
    }
}
