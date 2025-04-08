using UnityEngine;

/// <summary>
/// ĳ���Ͱ� ������ 16���� �ɷ�ġ�� ������ Ŭ����.
/// �Ϻ� �ɷ�ġ�� ������ �� �� ������, Ư�� �ɷ�ġ�� ���Ƽ�� ����.
/// </summary>
public class CharacterStats : MonoBehaviour
{
    [Header("����")]
    public float maxHP = 10;             // �ִ� ü��. 0 ���Ϸ� �������� ���� ������, ���̺� ���� �� 1�� ������
    public float currentHP;
    public float regen;            // �ʴ� ü�� ȸ����. ���� ����, ������ ü���� ������ ����
    public float lifesteal;        // ���� Ȯ�� (%). ���� �� ���� Ȯ���� 1 HP ȸ�� (�ִ� 10HP/��)

    [Header("���ݷ�")]
    public float damage;           // ���� ���ط� ���� (%). ��ü ���ݷ¿� ������
    public float meleeDamage;      // ���� ���ݷ� ����. ���� ���⿡�� ����
    public float rangedDamage;     // ���Ÿ� ���ݷ� ����. ���Ÿ� ���⿡�� ����
    public float elementalDamage;  // ���� ���ط� ����. ����/�Ҳ� �� ���� ���⿡�� ����

    [Header("���� �ɷ�")]
    public float attackSpeed;      // ���� �ӵ� (%). ���� �ֱ⸦ ����. ���� �� ������
    public float critChance;       // ġ��Ÿ Ȯ�� (%). �� ���� ����� ���� ġ��Ÿ �߻�

    [Header("Ư�� ����")]
    public float engineering;      // ��ž, ��ġ�� ���⿡�� ������ ��ħ
    public float range;            // ���� ��Ÿ� ����. ���� ���⿣ 50%�� �����

    [Header("���/ȸ��")]
    public float armor;            // �޴� ���� ������ ����. ������ ���� ����
    public float dodge;            // ȸ�� Ȯ�� (%). �ǰ� ȸ�� ����. 60% �̻� �Ұ�

    [Header("�̵�/���")]
    public float speed;            // �̵� �ӵ� (%). ������ �̵��� ������
    public float luck;             // ������/����/���� Ȯ�� ����

    [Header("�ڿ�")]
    public float harvest;          // ���̺� ���� �� ȹ�� ��ȭ. ���̺긶�� 5% ����. ������ �ڿ� ����

    // �߰� �޼��峪 �ɷ�ġ �ջ� ������ ���⼭ ���� ����
    public virtual void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
    }
}
