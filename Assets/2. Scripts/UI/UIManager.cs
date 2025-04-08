using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }// �̱��� �߰�

    public GameObject lobbyUI;
    public GameObject difficultyUI;
    public GameObject inGameUI;
    public GameObject howToUI;
    public GameObject resultUI;
    public Slider hpBar;
    public TMP_Text hpText;
    public Slider expBar;
    public TMP_Text expText;
    public TMP_Text moneyText;
    public TMP_Text waveText;
    public TMP_Text timeText;

    private void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);  // ���� �ٲ� ������ ���
    }


    public void ShowLobbyUI()
    {
        lobbyUI.SetActive(true);
        difficultyUI.SetActive(false);
        inGameUI.SetActive(false);
        howToUI.SetActive(false);
        resultUI.SetActive(false);
    }

    public void ShowDifficultyUI()
    {
        Debug.Log("���� ���� ��û��");
        lobbyUI.SetActive(false);
        difficultyUI.SetActive(true);
    }

    public void ShowInGameUI()
    {
        lobbyUI.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void ShowHowToUI()
    {
        Debug.Log("���� ��� â ����");
        howToUI.SetActive(true);
    }

    public void HideHowToUI()
    {
        howToUI.SetActive(false);
    }

    public void ShowResultUI()
    {
        inGameUI.SetActive(false);
        resultUI.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("���� ���� ��û��");
        Application.Quit();
    }

    public void OnDifficultySelected(DifficultySettings selected)
    {
        GameManager.Instance.OnDifficultySelected(selected);
        difficultyUI.SetActive(false);
        ShowInGameUI(); // �ΰ��� UI�� ��ȯ (���̺� �غ�)
    }

    public void UpdateHPUI(float currentHP, float maxHP)
    {
        hpBar.value = currentHP / maxHP;
        hpText.text = $"{(int)currentHP} / {(int)maxHP}";
    }

    public void UpdateEXPUI(float currentExpRatio, int level)
    {
        expBar.value = currentExpRatio;
        expText.text = $"{level}.LV";
    }

    public void UpdateWaveUI(int wave)
    {
        waveText.text = $"���� ���̺�\n{wave}";
    }

    public void UpdateMoneyUI(int money)
    {
        moneyText.text = $"${money}";
    }

    public void UpdateWaveTimeUI(float timeLeft)
    {
        int displayTime = Mathf.CeilToInt(timeLeft);
        timeText.text = $"{displayTime}";

        // 10�� ������ �� �ؽ�Ʈ ������
        if (displayTime <= 10)
            timeText.color = Color.red;
        else
            timeText.color = Color.white; // �⺻ ����
    }
}
