using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficulty", menuName = "Game/Difficulty Settings")]
public class DifficultySettings : ScriptableObject
{
    [Header("�⺻ ����")]
    public string difficultyName = "���� 0";
    [TextArea] public string description;

    [Header("�� ����")]
    [Range(0.5f, 3f)]
    public float enemySpeedMultiplier = 1f;

    [Range(0.5f, 3f)]
    public float enemyDamageMultiplier = 1f;

    [Range(0.5f, 3f)]
    public float enemyHealthMultiplier = 1f;

    public bool doubleBossOnFinalWave = false;

    [Header("��ӷ� ����")]
    [Range(0f, 2f)]
    public float dropRateMultiplier = 1f;

    [Header("�÷��̾� ���ط� ����")]
    [Range(0.5f, 2f)]
    public float playerDamageMultiplier = 1f;
}
