using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] GameObject _arrow;
    
    private Vector3 _mousePos;
    private Vector3 _entityPos;
    private Vector3 _direction;
    private GameObject _arrowInstance;
    
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    private void Start()
    {
        _arrowInstance = Instantiate(_arrow);
        _arrowInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        _entityPos = gameObject.GetComponent<Transform>().position;
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = _mousePos - _entityPos;
        _direction.z = 0;
        _direction.Normalize();
        _arrowInstance.gameObject.GetComponent<Transform>().position = _entityPos + _direction;
        _arrowInstance.gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);
    }
}
