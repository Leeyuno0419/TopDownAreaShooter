using UnityEngine;

/// <summary>
/// 캐릭터가 가지는 16개의 능력치를 정의한 클래스.
/// 일부 능력치는 음수가 될 수 있으며, 특정 능력치는 페널티가 존재.
/// </summary>
public class CharacterStats : MonoBehaviour
{
    [Header("생존")]
    public float maxHP = 10;             // 최대 체력. 0 이하로 내려가도 죽지 않으며, 웨이브 시작 시 1로 보정됨
    public float currentHP;
    public float regen;            // 초당 체력 회복량. 공식 있음, 음수라도 체력이 깎이지 않음
    public float lifesteal;        // 흡혈 확률 (%). 공격 시 일정 확률로 1 HP 회복 (최대 10HP/초)

    [Header("공격력")]
    public float damage;           // 최종 피해량 보정 (%). 전체 공격력에 곱해짐
    public float meleeDamage;      // 근접 공격력 보정. 근접 무기에만 적용
    public float rangedDamage;     // 원거리 공격력 보정. 원거리 무기에만 적용
    public float elementalDamage;  // 원소 피해량 보정. 마법/불꽃 등 원소 무기에만 적용

    [Header("전투 능력")]
    public float attackSpeed;      // 공격 속도 (%). 공격 주기를 줄임. 음수 시 느려짐
    public float critChance;       // 치명타 확률 (%). 각 무기 계수에 따라 치명타 발생

    [Header("특수 공격")]
    public float engineering;      // 포탑, 설치류 무기에만 영향을 미침
    public float range;            // 무기 사거리 보정. 근접 무기엔 50%만 적용됨

    [Header("방어/회피")]
    public float armor;            // 받는 피해 비율을 줄임. 음수면 피해 증폭
    public float dodge;            // 회피 확률 (%). 피격 회피 가능. 60% 이상 불가

    [Header("이동/행운")]
    public float speed;            // 이동 속도 (%). 음수면 이동이 느려짐
    public float luck;             // 아이템/상점/보상 확률 증가

    [Header("자원")]
    public float harvest;          // 웨이브 시작 시 획득 재화. 웨이브마다 5% 증가. 음수면 자원 감소

    // 추가 메서드나 능력치 합산 로직은 여기서 구현 가능
    public virtual void TakeDamage(int damage)
    {
        currentHP = Mathf.Max(currentHP - damage, 0);
    }
}
