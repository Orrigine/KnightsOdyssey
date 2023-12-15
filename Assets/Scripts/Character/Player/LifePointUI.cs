using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePointUI : MonoBehaviour
{
    [SerializeField] private GameObject[] _LifePoints;
    [SerializeField] private StructPlayer _structPlayer;
    [SerializeField] private int _currentLifePoints = 0;
    
    void Start()
    {
        _structPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<StructPlayer>();
        _currentLifePoints = _structPlayer.transform.GetChild(3).GetComponent<LifeSystem>().CurrentLife;
    }

    void LateUpdate()
    {
        int currentLife = _structPlayer.transform.GetChild(3).GetComponent<LifeSystem>().CurrentLife;
        if (currentLife != _currentLifePoints)
        {
            
        }
    }
}
