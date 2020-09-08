using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _randomSpeed;
    private int _size;
    private Camera _camera;
    private GameObject _player;
    private Rigidbody _rigidbody;
    private Asteroids _asteroids;
    private Vector3 _originalScale;

    private void Awake()
    {
        _asteroids = FindObjectOfType<Asteroids>();
        _originalScale = transform.localScale;
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = FindObjectOfType<Camera>();
        _rigidbody = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Debug.DrawRay(transform.position,_rigidbody.velocity);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            Destroy(false);
            _asteroids.GetLiving().Die();
        }
        if (collider.gameObject.tag == "Ufo")
        {
            Destroy(false);
        }
        if (collider.gameObject.tag == "Bullet" | collider.gameObject.tag == "UfoBullet")
        {
            Destroy(true);
        }
    }
    private void OnEnable()
    {
        //Вычисляем случайное вращение и добавляем к существующему
        _rigidbody.angularVelocity += new Vector3(Random.Range(_speed - 2, _speed + 2), Random.Range(_speed - 2, _speed + 2));
        //Определяем размер астероида
        transform.localScale = _originalScale * _size;
        if (_size == 3)
        {
            //Если это большой астероид, то он появляется с краю экрана, и получает случайное направление
            transform.position = _camera.ViewportToWorldPoint(new Vector3(0, Random.Range(0f, 1.0f), 10));
            _rigidbody.velocity = CalculateRandomDirection();
        } else
        {

        }
    }

    private Vector2 CalculateRandomDirection()
    {
        Vector2 randomSpeed = new Vector2(Random.Range(_speed - _randomSpeed, _speed + _randomSpeed), Random.Range(_speed - _randomSpeed, _speed + _randomSpeed));
        if (Random.Range(0, 2) == 1)
        {
            randomSpeed.x *= -1;
        }
        if (Random.Range(0, 2) == 1)
        {
            randomSpeed.y *= -1;
        }
        return randomSpeed;
    }

    public void Destroy(bool _isSubdivide)
    {
        if (_size != 1 & _isSubdivide) _asteroids.Subdivide(transform.position, _rigidbody.velocity, _size);
        _asteroids.AddDestroyed();
        gameObject.SetActive(false);
    }


    public void Init(float speed, float randomSpeed)
    {
        _speed = speed;
        _randomSpeed = randomSpeed;
    }

    public int GetSize()
    {
        return _size;
    }

    public void SetSize(int size)
    {
        _size = size;
    }
}
