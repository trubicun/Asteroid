using UnityEngine;

public class Ufo : MonoBehaviour
{
    [SerializeField] private float _speedHorizontal = 2f;
    [SerializeField] private float _amplitude = 3f;
    [SerializeField] private float _frequenz = 2f;
    [SerializeField] private GameObject _bullet;
    private GameObject _player;
    private GamePlay _gamePlay;
    private Rigidbody _rigidbody;
    private Bullets _bullets;
    private int _shootTime;
    private float _shootingTimer = 0;
    float x, y;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _gamePlay = FindObjectOfType<GamePlay>();
        _bullets = FindObjectOfType<Bullets>();
        _bullet.GetComponent<UfoBullet>().Init(_bullets.GetTime(), _bullets.GetSpeed());
        _rigidbody = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    public void SetLeft(bool val)
    {
        if (!val)
        {
            _speedHorizontal *= -1;
        }
    }

    private void Move()
    {
        //y=a+b\sin(cx+d).
        x = x + _speedHorizontal * Time.deltaTime;
        y = Mathf.Sin(x * _frequenz) * _amplitude;
        _rigidbody.velocity = new Vector2(_speedHorizontal, y);
    }

    private void Update()
    {
        Move();
        _shootingTimer += Time.deltaTime;
        //НЛО долетает до края экрана - уничтожается. Если нужно вернуть, то раскоментировать, и удалить скрипт BoundTeleportation
        /* 
        float currentX = _camera.WorldToViewportPoint(transform.position).x;
        if (currentX < 0 | currentX > 1)
        {
            Destroy();
        }
        */
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
            _player.GetComponent<Living>().Die();
            Destroy();
        }
    }
}
