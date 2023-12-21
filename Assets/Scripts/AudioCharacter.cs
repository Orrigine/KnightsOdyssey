using UnityEngine;

public class AudioCharacter : MonoBehaviour
{
    [SerializeField] private AudioCharacterScriptableObject _ACSO;
    [SerializeField] private AudioSource _audioSource;
    
    void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.volume = 0.1f;
        _audioSource.loop = false;
    }
    
    public void HurtSound()
    {
        if (_ACSO.TakeDamage == null)
        {
            Debug.LogError("AudioCharacter: HurtSound: _ACSO.TakeDamage is null");
        }
        else
        {
            _audioSource.clip = _ACSO.TakeDamage;
            _audioSource.Play();
        }
    }
    
    public void HealSound()
    {
        if (_ACSO.Heal == null)
        {
            Debug.LogError("AudioCharacter: HealSound: _ACSO.Heal is null");
        }
        else
        {
            _audioSource.clip = _ACSO.Heal;
            _audioSource.Play();
        }
    }
    
    public void DeathSound()
    {
        if (_ACSO.Death == null)
        {
            Debug.LogError("AudioCharacter: DeathSound: _ACSO.Death is null");
        }
        else
        {
            _audioSource.clip = _ACSO.Death;
            _audioSource.Play();
        }
    }
    
    public void AttackSound()
    {
        if (_ACSO.Attack == null)
        {
            Debug.LogError("AudioCharacter: AttackSound: _ACSO.Attack is null");
        }
        else
        {
            _audioSource.clip = _ACSO.Attack;
            _audioSource.Play();
        }
    }
    
    public void SecondarySound()
    {
        if (_ACSO.Secondary == null)
        {
            Debug.LogError("AudioCharacter: SecondarySound: _ACSO.Attack is null");
        }
        else
        {
            _audioSource.clip = _ACSO.Secondary;
            _audioSource.Play();
        }
    }
    
    public void RunSound()
    {
        if (_ACSO.Run == null)
        {
            Debug.LogError("AudioCharacter: RunSound: _ACSO.Run is null");
        }
        else
        {
            _audioSource.clip = _ACSO.Run;
            _audioSource.Play();
        }
    }
}
