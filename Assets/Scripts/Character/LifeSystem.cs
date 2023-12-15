using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    private int maxLife = 3;
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
        get => maxLife;
        set => maxLife = value;
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
}
