using UnityEngine;

public class StructPlayer : MonoBehaviour
{
    [SerializeField] private int _maxLife;
    [SerializeField] private int _currentLife;
    [SerializeField] private bool _isDead;
    [SerializeField] private bool _isInvincible;
    [SerializeField] private bool _canAttacking = true;
    [SerializeField] private bool _canRoll = true;
    [SerializeField] private bool _canShield = true;
    
    public int MaxLife
    {
        get => _maxLife;
        set => _maxLife = value;
    }
    
    public int CurrentLife
    {
        get => _currentLife;
        set => _currentLife = value;
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
    
    public bool CanAttacking
    {
        get => _canAttacking;
        set => _canAttacking = value;
    }
    
    public bool CanRoll
    {
        get => _canRoll;
        set => _canRoll = value;
    }
    
    public bool CanShield
    {
        get => _canShield;
        set => _canShield = value;
    }
}
