using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyHitbox : MonoBehaviour
{
    [SerializeField] private LifeSystem _lifeSystem;
    [SerializeField] private HeroKnight _brain;

    private void Start()
    {
        _lifeSystem = GetComponent<LifeSystem>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_lifeSystem.IsInvincible) return;
        
        if (other.CompareTag("HitboxEnemy"))
        {
            if (_brain.IsBlocking)
                StartCoroutine(PushBack(other.gameObject.transform.parent.gameObject));
            else
                _lifeSystem.TakeDamage(1);
        }
    }
    
    private IEnumerator PushBack(GameObject other)
    {
        Vector2 direction = transform.position - other.transform.position;
        direction.Normalize();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.AddForce(-direction * 100, ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(0.05f);
        
        rb.velocity = Vector2.zero;
        yield break;
    }
}
