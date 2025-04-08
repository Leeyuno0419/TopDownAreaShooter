using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }// 싱글톤 추가

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
        // 싱글톤 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);  // 씬이 바뀌어도 유지할 경우
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
        Debug.Log("게임 시작 요청됨");
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
        Debug.Log("게임 방법 창 열기");
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
        Debug.Log("게임 종료 요청됨");
        Application.Quit();
    }

    public void OnDifficultySelected(DifficultySettings selected)
    {
        GameManager.Instance.OnDifficultySelected(selected);
        difficultyUI.SetActive(false);
        ShowInGameUI(); // 인게임 UI로 전환 (웨이브 준비)
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
        waveText.text = $"현재 웨이브\n{wave}";
    }

    public void UpdateMoneyUI(int money)
    {
        moneyText.text = $"${money}";
    }

    public void UpdateWaveTimeUI(float timeLeft)
    {
        int displayTime = Mathf.CeilToInt(timeLeft);
        timeText.text = $"{displayTime}";

        // 10초 이하일 때 텍스트 빨간색
        if (displayTime <= 10)
            timeText.color = Color.red;
        else
            timeText.color = Color.white; // 기본 색상
    }
}
