using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Vector3 _mousePos;
    private Vector3 _entityPos;
    private Vector3 _direction;
    
    public Vector3 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    void Update()
    {
        _entityPos = gameObject.GetComponent<Transform>().position;
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //_mousePos.position = _mousePos;
        _direction = _mousePos - _entityPos;
        _direction.z = 0;
        _direction.Normalize();
    }
}
