using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AudioCharacterScriptableObject", order = 1)]
public class AudioCharacterScriptableObject : ScriptableObject
{
    [SerializeField] private AudioClip _takeDamage;
    [SerializeField] private AudioClip _heal;
    [SerializeField] private AudioClip _death;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _secondary;
    [SerializeField] private AudioClip _run;
    
    public AudioClip TakeDamage
    {
        get => _takeDamage;
    }
    
    public AudioClip Heal
    {
        get => _heal;
    }
    
    public AudioClip Death
    {
        get => _death;
    }
    
    public AudioClip Attack
    {
        get => _attack;
    }
    
    public AudioClip Secondary
    {
        get => _secondary;
    }
    
    public AudioClip Run
    {
        get => _run;
    }
}
