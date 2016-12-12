using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public GameObject prefabDiamantes;
    public GameObject prefabFloor;
    public Transform parentTransform;
    public DifficultyHelper DifficultyHelper;
    public GameObject mushRoomPrefab;
    public GameObject powPrefab;
    public GameObject enemyPrefab;
 
    private GameManager.GameState _lastGameState;

    private void GenerateLevel()
    {
        DestroyPowerUpsAndEnemies();
        SpawnPowerUps();
        SpawnEnemy();
        GenerateCircle(prefabDiamantes, 2f, DifficultyHelper.GetTotalSpawnedCoins(), 0);
    }

    private void GenerateFloor()
    {
        GenerateCircle(prefabFloor, 1.6f, 36, 90);
    }

    private void DestroyPowerUpsAndEnemies()
    {
        var enemy = GameObject.Find("enemy(Clone)");
        var hongo = GameObject.Find("hongo(Clone)");
        var pow = GameObject.Find("pow(Clone)");
        if (enemy != null) Destroy(enemy);
        if (hongo != null) Destroy(hongo);
        if (pow != null) Destroy(pow);
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

    void SpawnPowerUps()
    {
        SpawnByChance(mushRoomPrefab, DifficultyHelper.GetMushroomChance());
        SpawnByChance(powPrefab, DifficultyHelper.GetPowChance());
    }

    void SpawnEnemy()
    {
        SpawnByChance(enemyPrefab, DifficultyHelper.GetEnemyChance());
    }

    void SpawnByChance(GameObject prefab, float chance)
    {
        if (Random.value < chance)
        {
            GenerateCircle(prefab, 2f, 36, 0, Random.value);
        }
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

    void GenerateCircle(GameObject gameObject, float radio, int amount, float correccionAngulo)
    {
        GenerateCircle(gameObject, radio, amount, correccionAngulo, 0);
    }

    void GenerateCircle(GameObject gameObject, float radio, int amount, float correccionAngulo, float randomPosition)
    {
        float separacion = 360f / amount;
        int i = 0;
        if (randomPosition > 0)
        {
            i = Mathf.FloorToInt(randomPosition*amount);
            amount = i+1;
        }
        for (; i < amount; i++)
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
