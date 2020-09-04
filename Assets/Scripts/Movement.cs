using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _force = 300;
    [SerializeField] private float _maxSpeed = 4;
    [SerializeField] private float _turn = 150;
    private AudioSource _audio;
    private Camera _camera;
    private bool _isSecondary;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _camera = FindObjectOfType<Camera>();
        _isSecondary = false;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Secondary - Управление с мышью
        if (_isSecondary)
        {
            //Получаем направление к мыши
            Vector2 direction = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
            //Высчитываем угол к полученному вектору
            Vector3 angle = new Vector3(0,0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
            //Делаем плавный разворот, с помощью линейной интерполяции
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(angle), _turn/50 * Time.deltaTime);
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow) | Input.GetKey(KeyCode.Mouse1))
            {
                Boost();
            }
        } else
        {
            if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow))
            {
                Boost();
            }
            if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, _turn * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, -_turn * Time.deltaTime);
            }
        }
    }

    private void Boost()
    {
        if (_rigidbody.velocity.magnitude < _maxSpeed)
        {
            _rigidbody.AddForce(transform.up * _force * Time.deltaTime);
        }
        if (!_audio.isPlaying)
        {
            _audio.Play();
        }
    }

    public void SetSecondaryControl()
    {
        _isSecondary = !_isSecondary;
    }
    public bool GetControl()
    {
        return _isSecondary;
    }
}
