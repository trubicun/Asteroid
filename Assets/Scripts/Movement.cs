using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _force = 300;
    [SerializeField] private float _maxSpeed = 4;
    [SerializeField] private float _turn = 150;
    private AudioSource _audio;
    private Camera _camera;
    private Rigidbody _rigidbody;
    private int _control;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _camera = FindObjectOfType<Camera>();
        _rigidbody = GetComponent<Rigidbody>();
        _control = GetControl();
    }

    private void Update()
    {
        if (IsMouse())
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

    public void ChangeControl()
    {
        _control *= -1;
        PlayerPrefs.SetInt("Control", _control);
    }
    public int GetControl()
    {
        //При самом первом запуске игры, всегда будет стоять клавиатура
        if (PlayerPrefs.GetInt("Control") == 0)
        {
            PlayerPrefs.SetInt("Control", -1);
        }
        return PlayerPrefs.GetInt("Control");
    }
    public bool IsMouse()
    {
        //1 - Управление с мышью
        //-1 - Управление без мыши
        if (_control == 1) return true;
        else return false;
    }
}
