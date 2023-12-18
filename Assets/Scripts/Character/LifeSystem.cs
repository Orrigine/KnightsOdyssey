using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private int _maxLife = 3;
    [SerializeField] private int _currentLife = 3;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private bool _isInvincible = false;
    
    public int CurrentLife
    {
        get => _currentLife;
        set => _currentLife = value;
    }
    
    public int MaxLife
    {
        get => _maxLife;
        set => _maxLife = value;
    }
    
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    void Start()
    {
        CurrentLife = MaxLife;
    }

    private void Update()
    {
        if (CurrentLife <= 0)
            IsDead = true;
        else
            IsDead = false;
    }
    
    public void Heal(int amount)
    {
        CurrentLife += amount;
        if (CurrentLife > MaxLife)
            CurrentLife = MaxLife;
    }
    
    public void TakeDamage(int amount)
    {
        if (!_isInvincible)
        {
            CurrentLife -= amount;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hitbox"))
        {
            uint damage = other.gameObject.GetComponent<HitBox>().Damage;
            TakeDamage((int)damage);
        }
    }
}
