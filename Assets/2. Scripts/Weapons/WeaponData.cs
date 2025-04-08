using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public enum WeaponType { Melee, Ranged }
    public enum WeaponTag
    {
        Precise, Primitive, Medical, Unarmed, Blade, Blunt, Heavy,
        Ethereal, Tool, Gun, Explosive, Elemental, Support, Medieval, Legendary
    }
    public enum AttackStyle { Slash, Stab }

    [Header("무기 기본 정보")]
    public string weaponName;
    public Sprite icon;
    public WeaponType type;
    public AttackStyle attackStyle;
    public WeaponTag tag;

    [Header("스탯")]
    public int tier;
    public float baseDamage;
    public float damageMultiplier;
    public float criticalMultiplier;
    public float criticalChance;

    public float cooldown;
    public float knockback;
    public float range;
    public int pierce;

    [Header("설명")]
    [TextArea]
    public string description;
}
