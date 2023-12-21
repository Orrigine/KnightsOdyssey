using UnityEngine;

public class LifePointUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _LifePoints;
    [SerializeField] private StructPlayer _structPlayer;
    [SerializeField] private int _currentLifePoints = 0;
    
    void Start()
    {
        _currentLifePoints = _structPlayer.CurrentLife;
    }

    void LateUpdate()
    {
        int currentLife = _structPlayer.CurrentLife;
        if (currentLife != _currentLifePoints)
        {
            _currentLifePoints = currentLife;
            ChangeHeartUI();
        }
    }

    void ChangeHeartUI()
    {
        foreach (GameObject go in _LifePoints)
        {
            go.SetActive(false);
        }
        
        for (int i = 0; i < _currentLifePoints; i++)
        {
            _LifePoints[i].SetActive(true);
        }
    }
}
