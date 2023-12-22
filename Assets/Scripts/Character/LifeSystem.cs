using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private int _maxLife = 3;
    [SerializeField] private int _currentLife = 3;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private bool _isInvincible = false;

    [SerializeField] private Image fadeScreen;

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
            // execute only if it's the player
            if (gameObject.CompareTag("Player"))
            {
                Die();
            }
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

    public void Die()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // Assurez-vous que l'écran de fondu est activé et noir.
        fadeScreen.gameObject.SetActive(true);
        fadeScreen.color = Color.black;

        // Faites un fondu à partir de transparent.
        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            // Utilisez t pour interpoler entre transparent et noir.
            fadeScreen.color = new Color(0, 0, 0, t);
            yield return null;
        }

        // Rechargez la scène.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
