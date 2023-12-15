using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandbagHitbox : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private uint _damage = 0;
    [SerializeField] private int _maxLifeTime = 5;
    [SerializeField] private int _lifeTime = 0;
    [SerializeField] private float _size = 1;
    
    private SpriteRenderer _spriteRenderer;
    
    public uint Damage
    {
        get => _damage;
        set => _damage = value;
    }
    
    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().radius = _size / 2; 
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.sprite = _sprites[_lifeTime/2];
        if (_lifeTime >= _maxLifeTime)
            Destroy(gameObject);
        _lifeTime++;
    }

    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Sandbag hit at " + Damage);
        }
    }
}
