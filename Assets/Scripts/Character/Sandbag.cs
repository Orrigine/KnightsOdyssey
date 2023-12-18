using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandbag : MonoBehaviour
{
    [SerializeField] public bool isAttacking = false;
    [SerializeField] private GameObject _hitboxPrefab;
    
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        StartCoroutine(Attack());
    }
    
    private IEnumerator Attack()
    {
        while (true)
        {
            Vector3 origin = transform.position;
            Vector3 position = origin + new Vector3(-1.5f, 0, 0);
            GameObject hitbox = Instantiate(_hitboxPrefab, position, Quaternion.identity);
            hitbox.transform.parent = transform;
            yield return new WaitForSeconds(1.5f);
        }
    }
}
