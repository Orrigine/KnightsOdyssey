using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    float maxLife = 100;
    [SerializeField] private float _currentLife = 100;
    [SerializeField] private float _lifeRegen = 0;
    [SerializeField] private float _lifeRegenDelay = 0;
    [SerializeField] private float _lifeRegenDelayTimer = 0;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private bool _isRegenerating = false;
    
    public float CurrentLife
    {
        get => _currentLife;
        set => _currentLife = value;
    }
    
    public float MaxLife
    {
        get => maxLife;
        set => maxLife = value;
    }
    
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    private void Awake()
    {
        throw new NotImplementedException();
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

        if (_isRegenerating)
        {
            RegenerateLife();
        }
    }
    
    private void RegenerateLife()
    {
        if (_lifeRegenDelayTimer > 0)
            _lifeRegenDelayTimer -= Time.deltaTime;
        else
            _lifeRegenDelayTimer = 0;
        
        if (_lifeRegenDelayTimer == 0)
        {
            if (CurrentLife < MaxLife)
            {
                CurrentLife += _lifeRegen * Time.deltaTime;
                if (CurrentLife > MaxLife)
                    CurrentLife = MaxLife;
            }
        }
    }
    
    public void Heal(float amount)
    {
        CurrentLife += amount;
        if (CurrentLife > MaxLife)
            CurrentLife = MaxLife;
    }
    
    public void TakeDamage(float amount)
    {
        if (!_isInvincible)
        {
            CurrentLife -= amount;
            _lifeRegenDelayTimer = _lifeRegenDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hitbox"))
        {
            float damage = other.gameObject.GetComponent<HitBox>().Damage;
            TakeDamage(damage);
        }
    }
}
