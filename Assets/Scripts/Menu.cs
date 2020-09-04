using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Text _controlButtonText;
    [SerializeField] private Text _HighScore;
    [SerializeField] private Text _Score;
    private GamePlay _gamePlay;
    private Movement _movement;
    private Text _continueButtonText;
    private bool _isPaused;
    private AudioSource[] _audio;


    private void Start()
    {
        _audio = FindObjectsOfType<AudioSource>();
        _movement = FindObjectOfType<Movement>();
        _continueButtonText = _continueButton.transform.GetChild(0).gameObject.GetComponent<Text>();
        _gamePlay = FindObjectOfType<GamePlay>();
        Stop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        SetAudio(true);
        Time.timeScale = 1f;
        _isPaused = false;
        _gamePlay.SetRunning(true);
        _menu.SetActive(false);
    }
    public void Pause()
    {
        _gamePlay.SetRunning(false);
        SetAudio(false);
        Time.timeScale = 0;
        _isPaused = true;
        _menu.SetActive(true);
        _continueButtonText.color = Color.white;
        _continueButton.interactable = true;
    }
    public void Stop()
    {
        _gamePlay.SetRunning(true);
        SetAudio(false);
        Time.timeScale = 0;
        _menu.SetActive(true);
        SaveScore();
        _continueButton.interactable = false;
        _continueButtonText.color = Color.grey;
    }

    public void NewGame()
    {
        SaveScore();
        Resume();
        _gamePlay.NewGame();
        _Score.text = "0";
    }
    public void Quit()
    {
        SaveScore();
        Application.Quit();
    }
    public void SetControl()
    {
        _movement.SetSecondaryControl();
        if (_movement.GetControl())
        {
            _controlButtonText.text = "клавиатура + мышь";
        } else
        {
            _controlButtonText.text = "клавиатура";
        }
    }

    private void SaveScore()
    {
        if (int.Parse(_Score.text) > int.Parse(_HighScore.text))
        {
            PlayerPrefs.SetInt("Score", int.Parse(_Score.text));
        }
        _HighScore.text = PlayerPrefs.GetInt("Score").ToString();
    }

    private void SetAudio(bool val)
    {
        foreach(AudioSource audio in _audio)
        {
            audio.enabled = val;
        }
    }
}
