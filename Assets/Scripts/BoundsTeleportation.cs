using UnityEngine;

public class BoundsTeleportation : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _currentPosition;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        _currentPosition = _camera.WorldToViewportPoint(transform.position);
        //Умножение на 0.99 - чтобы небыло застреваний
        if (_currentPosition.x > 1 | _currentPosition.x < 0)
        {
            transform.position = new Vector3(-transform.position.x * 0.99f, transform.position.y, transform.position.z);
        }
        if (_currentPosition.y > 1 | _currentPosition.y < 0)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y * 0.99f, transform.position.z);
        }
    }
}
