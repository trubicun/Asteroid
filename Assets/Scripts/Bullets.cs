using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private int _maximumBullets = 10;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _time = 1;
    [SerializeField] private GameObject _prefabBullet = null;
    private GameObject _player;
    private Scores _scores;
    private Movement _movement;
    private Pool _bullets;
    private GamePlay _gameplay;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _scores = FindObjectOfType<Scores>();
        _gameplay = FindObjectOfType<GamePlay>();
        _movement = GetComponent<Movement>();
        _prefabBullet.GetComponent<Bullet>().Init(_speed,_time);
        _bullets = new Pool(_prefabBullet,_maximumBullets,GameObject.FindGameObjectWithTag("BulletsPool"));
    }
    
    private void Update()
    {
        if (_gameplay.IsRunning())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bullets.Activate();
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_movement.GetControl())
                {
                    _bullets.Activate();
                }
            }
        }
    }

    public void AddScore(GameObject gameObject)
    {
        _scores.AddScore(gameObject);
    }

    public void Reload()
    {
        _bullets.Deactivate();
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
    
    public float GetSpeed()
    {
        return _speed;
    }
    public float GetTime()
    {
        return _time;
    }
}
