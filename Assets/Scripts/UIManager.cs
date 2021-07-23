using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//necesary to comunicate with UI in unity

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField]
    private Text _score;
    
    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _restartLevelText;

    private GameManager _gameManager;

    void Start()
    {        
        _score.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);
        
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _score.text = "Score: " + playerScore;
    }
    public void UpdateLives(int currentLives)
    {
        //display img sprite
        //give it a new one based on the current livex index
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartLevelText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
