using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private GameObject _ufo;
    [SerializeField] private int _ufoRandomStart = 20;
    [SerializeField] private int _ufoRandomEnd = 40;
    private Camera _camera;
    private Asteroids _asteroids;
    private Living _living;
    private Bullets _bullets;
    private Menu _menu;
    private AudioSource _audio;
    private bool _running;
    private bool _isUfo;
    private int _nextUfoTime;
    private float _timer;


    private void Start()
    {
        _bullets = FindObjectOfType<Bullets>();
        _audio = GetComponent<AudioSource>();
        _nextUfoTime = Random.Range(_ufoRandomStart, _ufoRandomEnd);
        _menu = FindObjectOfType<Menu>();
        _camera = FindObjectOfType<Camera>();
        _living = FindObjectOfType<Living>();
        _asteroids = FindObjectOfType<Asteroids>();
    }

    private void Update()
    {
        if (_isUfo)
        {
            _timer += Time.deltaTime;
            if (_timer >= _nextUfoTime)
            {
                SendUfo();
            }
        }
    }

    public void NewGame()
    {
        _timer = 0;
        SetUfo(true);
        _ufo.GetComponent<Ufo>().Reload();
        _ufo.GetComponent<Ufo>().Destroy();
        _bullets.Reload();
        _asteroids.StartGame();
        _living.Restart();
    }

    public void StopGame()
    {
        _timer = 0;
        _ufo.GetComponent<Ufo>().Reload();
        _ufo.GetComponent<Ufo>().Destroy();
        _menu.Stop();
    }

    private void SendUfo()
    {
        SetUfo(false);
        _timer = 0;
        _ufo.gameObject.SetActive(true);
        float randomY = Random.Range(0.1f,0.9f);
        if (Random.Range(0,2) == 0)
        {
            //Левая сторона
            _ufo.transform.position = _camera.ViewportToWorldPoint(new Vector3(0, randomY,10));
            _ufo.GetComponent<Ufo>().SetLeft(true);
        } else
        {
            //Правая сторона
            _ufo.GetComponent<Ufo>().SetLeft(false);
            _ufo.transform.position = _camera.ViewportToWorldPoint(new Vector3(1, randomY,10));
        }
    }

    public void SetUfo(bool val)
    {
        _isUfo = val;
    }

    public void PlaySound()
    {
        _audio.Play();
    }

    public bool IsRunning()
    {
        return _running;
    }
    public void SetRunning(bool val)
    {
        _running = val;
    }
}
