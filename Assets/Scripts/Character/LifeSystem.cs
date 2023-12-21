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
    
    public event Action OnHeal;
    public event Action OnTakeDamage;
    public event Action OnDeath;
    
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
    
    public bool IsInvincible
    {
        get => _isInvincible;
        set => _isInvincible = value;
    }

    void Start()
    {
        CurrentLife = MaxLife;
        OnHeal += () => { Debug.Log("Heal"); };
        OnTakeDamage += () => { Debug.Log("TakeDamage"); };
        OnDeath += () => { Debug.Log("Death"); };
    }

    private void Update()
    {
        if (CurrentLife <= 0 && !IsDead)
        {
            OnDeath?.Invoke();
            IsDead = true;
        }
    }
    
    private void Heal(int amount)
    {
        OnHeal?.Invoke();
        CurrentLife += amount;
        if (CurrentLife > MaxLife)
            CurrentLife = MaxLife;
    }
    
    public void TakeDamage()
    {
        if (!_isInvincible)
        {
            OnTakeDamage?.Invoke();
            CurrentLife--;
            SetInvincible(0.75f);
        }
    }
    
    public void SetInvincible(float duration)
    {
        StartCoroutine(Invincible(duration));
    }
    
    private IEnumerator Invincible(float duration)
    {
        _isInvincible = true;
        yield return new WaitForSeconds(duration);
        _isInvincible = false;
        yield break;
    }
}
