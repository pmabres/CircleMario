using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public GameObject ship;
    private AudioClip _clip;
    private AudioSource _source;
    private SpriteRenderer _renderer;
    // Use this for initialization
    void Start ()
    {
        _source = GetComponent<AudioSource>();
        _renderer = GetComponent<SpriteRenderer>();
        _clip = _source.clip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_renderer != null && collision.gameObject.name == ship.name && _renderer.enabled)
        {
            _source.PlayOneShot(_clip);
            _renderer.enabled = false;
            GameManager.AddScore();
            GameManager.CheckLevelStatus();
            Destroy(gameObject, _clip.length);
        }
    }
}
