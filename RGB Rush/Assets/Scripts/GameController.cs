using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameController : MonoBehaviour
{
    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;

    public int Round { get => _round; }

    [SerializeField] private int _nextRoundScore = 0;
    [SerializeField] private TextMeshProUGUI _endScoreText;

    private List<GameObject> _enemies = new List<GameObject>();

    private int _colorRed = 0;
    private int _colorGreen = 0;
    private int _colorBlue = 0;

    private int _currentScore = 0;
    private int _highScore = 0;
    private int _round = 1;

    private void Start()
    {
        OnGameStart.AddListener(ResetColors);
        StartNewGame();
    }

    #region Changing colors
    public void ChangeColor_Red()
    {
        if (_colorRed > 0) _colorRed = 0;
        else _colorRed = 1;
    }

    public void ChangeColor_Green()
    {
        if (_colorGreen > 0) _colorGreen = 0;
        else _colorGreen = 1;
    }

    public void ChangeColor_Blue()
    {
        if (_colorBlue > 0) _colorBlue = 0;
        else _colorBlue = 1;
    }

    private void ResetColors()
    {
        _colorRed = 0;
        _colorGreen = 0;
        _colorBlue = 0;
}
    #endregion

    #region Enemies
    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }

    public void DestroyEnemies()
    {
        Color selectedColor = new Color(_colorRed, _colorGreen, _colorBlue);
        DestroyAllEnemiesWithColor(selectedColor);
        ResetColors();

        // Change round
        if (_currentScore >= _nextRoundScore * _round) 
            _round++;
    }

    public void DestroyAllEnemies()
    {
        // Iterate over enemies and destroy them
        foreach(GameObject enemy in _enemies)
        {
            if(enemy)
            {
                GameObject.Destroy(enemy.gameObject);
            }
        }
        _enemies.Clear();

        Debug.Log("Final score: " + _currentScore);
    }

    private void DestroyAllEnemiesWithColor(Color selectedColor)
    {
        // Iterate over enemies and compare their color with selected color
        List<GameObject> removed = new List<GameObject>();
        foreach(GameObject enemy in _enemies)
        {
            // Destroy enemy if its color matches the selected color
            if(enemy.GetComponent<Enemy>().CompareColor(selectedColor))
            {
                removed.Add(enemy);                     // Add enemy to remove list
                enemy.GetComponent<Enemy>().Destroy();  // Destroy enemy
                _currentScore++;                        // Add score
            }
        }

        // Remove destroyed enemies from the list
        foreach(GameObject removedEnemy in removed)
        {
            _enemies.Remove(removedEnemy);
        }

        return;
    }
    #endregion

    public void EndGame()
    {
        DestroyAllEnemies();    // Destroy all remaining enemies

        // Save score
        if(_currentScore > _highScore)
        {
            SaveScore(_currentScore);
        }
        ChangeEndScore();       // Change score amount on end panel

        OnGameEnd.Invoke();     // Invoke GameEnd event
    }

    public void ChangeEndScore()
    {
        _endScoreText.text = _currentScore.ToString();
    }

    public void StartNewGame()
    {
        _highScore = LoadHighScore();
        _currentScore = 0;
        OnGameStart.Invoke();   // Invoke GameStart event
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveScore(int value)
    {
        // This method is not so safe, but in this type of game it doesn't matter right now
        PlayerPrefs.SetInt("HighScore", value);
    }
}
