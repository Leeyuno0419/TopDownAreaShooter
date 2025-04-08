using UnityEngine;

/// <summary>
/// ���� ��ü�� �帧�� �����ϴ� �̱��� GameManager
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        SelectingDifficulty, // ���̵� ����
        InWave,              // ���̺� ���� ��
        WaveEnded,           // ���̺� ����
        Shopping,            // ���� + �ɷ�ġ �й�
        GameOver             // ���� ����
    }

    [Header("���� ����")]
    public GameState currentState;
    public int currentWave = 1;
    public int maxWave = 20;
    public float waveDuration = 30f;
    private float waveTimer;

    [Header("���̵�")]
    public DifficultySettings difficultySettings;

    [Header("����")]
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
        // ���̺� ���� �� UI ����
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
            GameOver(true); // Ŭ����
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
