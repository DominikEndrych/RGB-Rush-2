using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreValueText;

    private int _highScore;

    private void Start()
    {
        _highScore = LoadHighScore();
        LoadGUI();
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    private void LoadGUI()
    {
        _scoreValueText.text = _highScore.ToString();
    }
}
