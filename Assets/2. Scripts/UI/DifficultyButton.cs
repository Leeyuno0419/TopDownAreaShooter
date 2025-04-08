using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    public DifficultySettings difficultyData;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UIManager ui = FindObjectOfType<UIManager>();
            ui.OnDifficultySelected(difficultyData);
        });
    }
}
