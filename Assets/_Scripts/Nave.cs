using System;
using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class Nave : MonoBehaviour {


    public float speed = 10f;
    public Transform isla;
    public Vector3 direccion;
    public AnimatorController[] animations;
    public Sprite[] sprites;
    public ParticleSystem PlayerDieParticles;
    private string enemyTag = "enemy";
    private bool _running = true;
   
	void Start ()
	{
	    gameObject.GetComponent<Animator>().runtimeAnimatorController = animations[(int) GameManager.SelectedPlayer];
	    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[(int) GameManager.SelectedPlayer];
	    this.direccion = Vector3.forward;
        GameManager.GameStateChanged += GameStateChanged;
    }

    void GameStateChanged(GameManager.GameState state)
    {
        _running = GameManager.ActiveGameState == GameManager.GameState.Running;
        if (GameManager.ActiveGameState == GameManager.GameState.Dying)
        {
            PlayerDieParticles.Play();
        }
        if (!_running)
        {
            gameObject.GetComponent<Animator>().Stop();
        }
    }

    void Update ()
    {
        if (_running)
        {
            this.transform.RotateAround(isla.position, direccion, speed * Time.deltaTime);
            
        }
    }

    public void Reverse()
    {
        this.direccion *= -1;
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(enemyTag) && GameManager.ActiveGameState == GameManager.GameState.Running)
        {
            LevelManager.Die();
            StartCoroutine(GameOver(2));
        }
    }

    IEnumerator GameOver(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
        LevelManager.GameOver();
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= GameStateChanged;
    }
}
