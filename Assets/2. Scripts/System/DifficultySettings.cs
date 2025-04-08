using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficulty", menuName = "Game/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    [Header("기본 정보")]
    public string difficultyName = "위험 0";
    [TextArea] public string description;

    [Header("적 설정")]
    [Range(0.5f, 3f)]
    public float enemySpeedMultiplier = 1f;

    [Range(0.5f, 3f)]
    public float enemyDamageMultiplier = 1f;

    [Range(0.5f, 3f)]
    public float enemyHealthMultiplier = 1f;

    public bool doubleBossOnFinalWave = false;

    [Header("드롭률 보정")]
    [Range(0f, 2f)]
    public float dropRateMultiplier = 1f;

    [Header("플레이어 피해량 조정")]
    [Range(0.5f, 2f)]
    public float playerDamageMultiplier = 1f;
}
