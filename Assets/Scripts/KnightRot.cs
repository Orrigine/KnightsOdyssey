using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightRot : MonoBehaviour
{

    [SerializeField] public GameObject knight;
    [SerializeField] public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        knight = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
