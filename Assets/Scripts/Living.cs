using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    [SerializeField] private List<GameObject> _lives;
    private int _currentLive;
    private bool _isBlinking = false;
    private float _timer;
    bool _isDead;
    private GamePlay _gamePlay;
    private MeshRenderer _mesh;
    private Rigidbody _rigidbody;
    private BoxCollider _collider;

    private void Start()
    {
        _gamePlay = FindObjectOfType<GamePlay>();
        _currentLive = _lives.Count;
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _mesh = GetComponent<MeshRenderer>();
    }

    public void Die()
    {
        _gamePlay.PlaySound();
        _isDead = true;
        _rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
        if (_currentLive > 0)
        {
            Invoke("Spawn", 5f);
        } else
        {
            _gamePlay.StopGame();
        }
    }

    private void Update()
    {
        if (_isBlinking)
        {
            _timer += Time.deltaTime;
            //Моргаем 2 раза в секунду
            if (_timer < 0.25f)
            {
                _mesh.enabled = true;
            } else
            {
                if (_timer < 0.5f)
                {
                    _mesh.enabled = false;
                } else
                {
                    if (_timer < 0.75f)
                    {
                        _mesh.enabled = true;
                    } else
                    {
                        if (_timer < 1f)
                        {
                            _mesh.enabled = false;
                        } else
                        {
                            _timer = 0;
                        }
                    }
                }
            }
        }
    }

    public void Spawn()
    {
        if (_isDead)
        {
            transform.position = Vector3.zero;
            _lives[_currentLive - 1].SetActive(false);
            _currentLive--;
            gameObject.SetActive(true);
            StartCoroutine(Invincibility());
        }
    }

    public void Restart()
    {
        _isDead = false;
        StopAllCoroutines();
        gameObject.SetActive(true);
        transform.position = Vector3.zero;
        _currentLive = _lives.Count;
        _mesh.enabled = true;
        _collider.enabled = true;
        _isBlinking = false;
        foreach (GameObject i in _lives)
        {
            i.SetActive(true);
        }
    }

    IEnumerator Invincibility()
    {
        _timer = 0;
        _isBlinking = true;
        _collider.enabled = false;
        yield return new WaitForSeconds(3);
        _mesh.enabled = true;
        _collider.enabled = true;
        _isBlinking = false;
    }
}
