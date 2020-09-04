﻿using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField] private GameObject _prefabAsteroid = null;
    [SerializeField] private float _speed;
    [SerializeField] private float _randomSpeed;
    private Living _living;
    private GamePlay _gameplay;
    private int _currentBigMeteors = 2;
    private int _needToDestroy;
    private Pool _asteroids;

    private void Start()
    {
        _gameplay = GetComponent<GamePlay>();
        _prefabAsteroid.GetComponent<Asteroid>().Init(_speed, _randomSpeed);
        _living = FindObjectOfType<Living>();
        //Создаём пул астероидов, 1 для запаса
        _asteroids = new Pool(_prefabAsteroid, (int)Mathf.Pow(_currentBigMeteors, 3) + 1, GameObject.FindGameObjectWithTag("AsteroidsPool"));
    }

    public void AddAsteroid(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _needToDestroy++;
            GameObject asteroid = _asteroids.Take();
            asteroid.GetComponent<Asteroid>().SetSize(3);
            asteroid.SetActive(true);
        }
    }

    public void StartGame()
    {
        _needToDestroy = 0;
        _asteroids.Deactivate();
        _currentBigMeteors = 2;
        AddAsteroid(_currentBigMeteors);
    }

    private void NextLevel()
    {
        _currentBigMeteors++;
        //Один астероид максимум может разлететься на 4 части, значит расширяем на 4
        _asteroids.Expand(_prefabAsteroid, 4, GameObject.FindGameObjectWithTag("AsteroidsPool"));
        AddAsteroid(_currentBigMeteors);
    }

    public void AddDestroyed()
    {
        _gameplay.PlaySound();
        _needToDestroy--;
        Debug.Log("For next level you need to destroy: " + _needToDestroy);
        if (_needToDestroy == 0)
        {
            Invoke("NextLevel", 2f);
        }
    }

    public void Subdivide(Vector3 position, Vector3 velocity, int size)
    {
        _needToDestroy += 2;
        //Находим новое случайное ускорение, и добавляем к имеющемуся
        Vector3 randomSpeed = new Vector3(Random.Range(-_randomSpeed, _randomSpeed), Random.Range(-_randomSpeed, _randomSpeed));
        Vector3 newVelocity = velocity + randomSpeed;
        //Берём первый астероид
        GameObject asteroid = _asteroids.Take();
        asteroid.GetComponent<Asteroid>().SetSize(size - 1);
        asteroid.GetComponent<Rigidbody>().velocity = newVelocity + Vector3.left;
        asteroid.transform.position = position;
        asteroid.SetActive(true);
        //Берём второй астероид
        asteroid = _asteroids.Take();
        asteroid.GetComponent<Asteroid>().SetSize(size - 1);
        asteroid.GetComponent<Rigidbody>().velocity = newVelocity + Vector3.right;
        asteroid.transform.position = position;
        asteroid.SetActive(true);

    }

    public Living GetLiving()
    {
        return _living;
    }
}