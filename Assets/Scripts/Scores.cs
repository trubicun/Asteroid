using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    /*
        Очки начисляются так:
    +20 за крупный астероид
    +50 за средний
    +100 за маленький
    +200 за НЛО
    */
    //Тут можно было использовать Enum, но в него нельзя внести изменения из инспектора
    [SerializeField] private int _huge = 20;
    [SerializeField] private int _medium = 50;
    [SerializeField] private int _small = 100;
    [SerializeField] private int _ufo = 200;
    private Text _text;

    private void Start()
    {
        _text = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    public void AddScore(GameObject gameObject)
    {
        if (gameObject.tag == "Asteroid")
        {
            switch(gameObject.GetComponent<Asteroid>().GetSize())
            {
                case 1:
                    {
                        SetScore(_small);
                        break;
                    }
                case 2:
                    {
                        SetScore(_medium);
                        break;
                    }
                case 3:
                    {
                        SetScore(_huge);
                        break;
                    }
            }
        } else
        {
            if (gameObject.tag == "Ufo")
            {
                SetScore(_ufo);
            }
        }
    }

    private void SetScore(int _score)
    {

        _text.text = (int.Parse(_text.text) + _score).ToString();
    }


}
