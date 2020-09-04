using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] private float _speed = 1.4f;
    [SerializeField] private GameObject _bullet;
    private Camera _camera;
    private GameObject _player;
    private GamePlay _gamePlay;
    private Rigidbody _rigidbody;
    private Bullets _bullets;
    private int _shootTime;
    private float _shootingTimer = 0;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _gamePlay = FindObjectOfType<GamePlay>();
        _bullets = FindObjectOfType<Bullets>();
        _bullet.GetComponent<UfoBullet>().Init(_bullets.GetTime(), _bullets.GetSpeed());
        _camera = FindObjectOfType<Camera>();
        _rigidbody = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    public void SetLeft(bool val)
    {
        if (val)
        {
            _rigidbody.velocity = Vector2.right * _speed;
        }
        else
        {
            _rigidbody.velocity = Vector2.left * _speed;
        }
    }

    private void Update()
    {
        _shootingTimer += Time.deltaTime;
        float currentX = _camera.WorldToViewportPoint(transform.position).x;
        if (currentX < 0 | currentX > 1)
        {
            Destroy();
        }
        if (_shootingTimer >= _shootTime)
        {
            Shoot();
        }
    }

    public void Destroy()
    {
        _shootingTimer = 0;
        _gamePlay.SetUfo(true);
        gameObject.SetActive(false);
    }

    public void Reload()
    {
        _bullet.GetComponent<UfoBullet>().Destroy();
    }


    private void Shoot()
    {
        _shootingTimer = 0;
        _shootTime = Random.Range(2, 6);
        _bullet.SetActive(true);
        _bullet.transform.position = transform.position;
        _bullet.GetComponent<UfoBullet>().SetDirection(_player);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Asteroid")
        {
            _gamePlay.PlaySound();
            Destroy();
        }
        if (collider.gameObject.tag == "Bullet")
        {
            _gamePlay.PlaySound();
            Destroy();
        }
        if (collider.gameObject == _player)
        {
            _gamePlay.PlaySound();
            Destroy();
        }
    }
}
