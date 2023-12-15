using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyHitbox : MonoBehaviour
{
    [SerializeField] private LifeSystem _lifeSystem;

    private void Start()
    {
        _lifeSystem = GetComponent<LifeSystem>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitboxEnemy"))
        {
            _lifeSystem.TakeDamage(1);
        }
    }
}
