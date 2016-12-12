using UnityEngine;
using System.Collections;

public class Isla : MonoBehaviour {
    public GameObject prefabBalas;
    public Transform parentTransform;
    
    public Sprite[] sprites;
    public DifficultyHelper DifficultyHelper;
    private AudioClip _clip;
    private AudioSource _source;
    private Vector3 targetRotation;
    private bool _running = true;

    void Start () {
        _source = GetComponent<AudioSource>();
        _clip = _source.clip;
        GameManager.GameStateChanged += GameStateChanged;
    }

    void GameStateChanged(GameManager.GameState state)
    {
        _running = GameManager.ActiveGameState == GameManager.GameState.Running;
        if (GameManager.ActiveGameState != GameManager.GameState.Running)
        {
            CancelInvoke();
        }
        else
        {
            InvokeRepeating("Disparar", DifficultyHelper.GetFireInterval(), DifficultyHelper.GetFireInterval());
        }
    }

    void Disparar () {
        if(_running)
        {
            Vector3 objective = Vector3.forward * Random.value * 360;
            if (objective.z > 180 && objective.z < 359)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            targetRotation = objective;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
            StartCoroutine(DispararBala(DifficultyHelper.GetFireInterval()/2));
        }
    }

    private void OnEnable()
    {
        InvokeRepeating("Disparar", DifficultyHelper.GetFireInterval(), DifficultyHelper.GetFireInterval());
    }

    IEnumerator DispararBala(float time)
    {
      yield return new WaitForSeconds(time);
        if (_running)
        {
            if (GameManager.ActiveGameState == GameManager.GameState.Running)
            {
                GameObject bala = Instantiate(prefabBalas, parentTransform);
                bala.transform.eulerAngles = targetRotation;
                _source.PlayOneShot(_clip);
                Destroy(bala, 3f);
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
            }
        }
    }

    void Update()
    {
        if (_running)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRotation), 5*Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= GameStateChanged;
    }
}
