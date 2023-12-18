using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructPlayer : MonoBehaviour
{
    [SerializeField] private int maxLife;
    [SerializeField] private int currentLife;
    [SerializeField] private bool isDead;
    [SerializeField] private bool isInvincible;
    
    public int MaxLife
    {
        get => maxLife;
        set => maxLife = value;
    }
    
    public int CurrentLife
    {
        get => currentLife;
        set => currentLife = value;
    }
    
    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }
    
    public bool IsInvincible
    {
        get => isInvincible;
        set => isInvincible = value;
    }
}
