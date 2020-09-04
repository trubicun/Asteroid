using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private List<GameObject> _content;

    public Pool(GameObject content, int count, GameObject parent)
    {
        _content = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            _content.Add(Object.Instantiate(content,parent.transform));
        }
    }

    public void Activate()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            if (!_content[i].activeInHierarchy)
            {
                _content[i].SetActive(true);
                break;
            }
        }
    }

    public void Deactivate()
    {
        //Deactivates ALL
        for (int i = 0; i < _content.Count; i++)
        {
            if (_content[i].activeInHierarchy)
            {
                _content[i].SetActive(false);
            }
        }
    }

    public GameObject Take()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            if (!_content[i].activeInHierarchy)
            {
                return _content[i];
            }
        }
        return null;
    }

    public void Expand(GameObject content, int count, GameObject parent)
    {
        for (int i = 0; i < count; i++)
        {
            _content.Add(Object.Instantiate(content, parent.transform));
        }
    }
}
