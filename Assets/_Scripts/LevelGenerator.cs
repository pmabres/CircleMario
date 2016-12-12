using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject prefabDiamantes;
    public GameObject prefabFloor;
    public Transform parentTransform;
    public DifficultyHelper DifficultyHelper;
    private GameManager.GameState _lastGameState;
    private void GenerateLevel()
    {
        GenerateCircle(prefabDiamantes, 2f, DifficultyHelper.GetTotalSpawnedCoins(), 0);
    }

    private void GenerateFloor()
    {
        GenerateCircle(prefabFloor, 1.6f, 36, 90);
    }

    // Use this for initialization
    void Start()
    {
        // Lo hacemos aca despues veo a donde lo mando.
        GameManager.ActiveGameState = GameManager.GameState.Running;
        GameManager.GameStateChanged += OnGameStateChanged;
        GenerateLevel();
        GenerateFloor();
    }

    private void OnDestroy()
    {
        GameManager.GameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Running && _lastGameState == GameManager.GameState.LevelSelection)
        {
            GenerateLevel();
        }
        _lastGameState = state;
    }

    void GenerateCircle(GameObject gameObject, float radio, int amount, int correccionAngulo)
    {
        float separacion = 360f / amount;

        for (int i = 0; i < amount; i++)
        {
            float angulo = i * separacion;
            Vector3 pos = ByCircle(this.transform.position, radio, correccionAngulo - angulo);
            if (gameObject != null)
            {
                Instantiate(gameObject, pos, Quaternion.Euler(0, 0, angulo), parentTransform);
            }
        }
    }

    Vector3 ByCircle(Vector3 centro, float radio, float angulo)
    {
        Vector3 pos;
        pos.x = centro.x + radio * Mathf.Sin(angulo * Mathf.Deg2Rad);
        pos.y = centro.y + radio * Mathf.Cos(angulo * Mathf.Deg2Rad);
        pos.z = centro.z;

        return pos;
    }
   
   
}
