using UnityEngine;

/// <summary>
/// 게임 전체의 흐름을 관리하는 싱글톤 GameManager
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        SelectingDifficulty, // 난이도 선택
        InWave,              // 웨이브 진행 중
        WaveEnded,           // 웨이브 종료
        Shopping,            // 상점 + 능력치 분배
        GameOver             // 게임 종료
    }

    [Header("게임 상태")]
    public GameState currentState;
    public int currentWave = 1;
    public int maxWave = 20;
    public float waveDuration = 30f;
    private float waveTimer;

    [Header("난이도")]
    public DifficultySettings difficultySettings;

    [Header("참조")]
    //public PlayerController player;
    //public EnemySpawner enemySpawner;
    //public ShopManager shopManager;
    public UIManager uiManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        currentState = GameState.SelectingDifficulty;
        //uiManager.ShowDifficultySelectUI();
    }

    private void Update()
    {
        if (currentState == GameState.InWave)
        {
            waveTimer -= Time.deltaTime;
            UIManager.Instance.UpdateWaveTimeUI(waveTimer);

            if (waveTimer <= 0)
            {
                EndWave();
            }
        }
    }

    public void OnDifficultySelected(DifficultySettings selected)
    {
        difficultySettings = selected;
        StartWave();
    }

    public void StartWave()
    {
        currentState = GameState.InWave;
        waveTimer = waveDuration;

        //enemySpawner.BeginWave(currentWave);
        // 웨이브 시작 시 UI 갱신
        UIManager.Instance.UpdateWaveUI(currentWave);
    }

    public void EndWave()
    {
        currentState = GameState.WaveEnded;
        //enemySpawner.StopWave();

        //player.RecoverHP();

        //uiManager.ShowStatUpgradeUI();
        //shopManager.OpenShop();

        currentWave++;

        if (currentWave > maxWave)
        {
            GameOver(true); // 클리어
        }
    }

    public void StartNextWave()
    {
        StartWave();
    }

    public void OnEnemyKilled(int dropAmount)
    {
        //player.AddMaterials(dropAmount);
    }

    public void GameOver(bool win)
    {
        currentState = GameState.GameOver;
        //enemySpawner.StopWave(); 
        //uiManager.ShowGameOverUI(win);
    }
}
