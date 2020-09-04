using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Эти 2 поля, настраиваются из скрипта Bullets, для удобства
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    private Bullets _bullets;
    private Transform _playerTransform;
    private Rigidbody _rigidbody;
    private AudioSource _audio;
    private float _timer;

    private void Awake()
    {
        _bullets = FindObjectOfType<Bullets>();
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        _playerTransform = _bullets.GetPlayer().transform;
    }

    private void OnEnable()
    {
        Spawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _time)
        {
            Destroy();
        }
    }

    private void Spawn()
    {
        _audio.Play();
        _timer = 0;
        transform.position = _playerTransform.position + _playerTransform.up * 0.5f;
        transform.rotation = _playerTransform.rotation;
        _rigidbody.velocity = transform.up * _speed;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        _bullets.AddScore(collider.gameObject);
        Destroy();
    }

    public void Init(float speed, float time)
    {
        _speed = speed;
        _time = time;
    }
}
