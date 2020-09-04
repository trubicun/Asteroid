using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBullet : MonoBehaviour
{
    //Значения полей беруться из Bullets, в Ufo
    [SerializeField] private float _speed;
    [SerializeField] private float _time;
    private float _timer = 0;
    private GameObject _player;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 5)
        {
            Destroy();
        }
    }

    public void SetDirection(GameObject gameObject)
    {
        _player = gameObject;
        GetComponent<Rigidbody>().velocity = (_player.transform.position - transform.position).normalized * _speed;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        _timer = 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == _player)
        {
            _player.GetComponent<Living>().Die();
            Destroy();
        }
        if (collider.gameObject.tag == "Asteroid")
        {
            Destroy();
        }
    }

    public void Init(float time, float speed)
    {
        _time = time;
        _speed = speed;
    }
}
