using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject _attackHitbox;
    
    private GameObject _attackHitboxInstance;

    public void Attack(Vector3 position, Quaternion rotation)
    {
        _attackHitboxInstance = Instantiate(_attackHitbox);
        _attackHitboxInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
        _attackHitboxInstance.GetComponent<Transform>().position = position;
        _attackHitboxInstance.GetComponent<Transform>().rotation = rotation;
    }

    public void Update()
    {
        if (GetComponent<NavMeshAgent>() != null)
        {
            GetComponent<NavMeshAgent>().updateRotation = false;
            GetComponent<NavMeshAgent>().updateUpAxis = false;  
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
