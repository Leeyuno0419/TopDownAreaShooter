using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int level = 1;
    public int money = 0;
    public float currentExp = 0;
    public float maxExp = 100;

    private void Start()
    {
        currentHP = maxHP;
        UpdateUI();
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
        UpdateUI();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateUI();
    }

    public void GainExp(float amount)
    {
        currentExp += amount;

        // 레벨업
        while (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            level++;
            maxExp *= 1.1f; // 점점 레벨업 어려워지게
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.Instance.UpdateHPUI(currentHP, maxHP);
        UIManager.Instance.UpdateMoneyUI(money);
        UIManager.Instance.UpdateEXPUI(currentExp / maxExp, level);
    }
}
