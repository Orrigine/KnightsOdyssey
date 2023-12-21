using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private uint _damage = 0;
    [SerializeField] public float _lifeTime = 0;
    [SerializeField] private float _size = 1;
    
    public uint Damage
    {
        get => _damage;
        set => _damage = value;
    }

    void Start()
    {
        if (gameObject.GetComponent<CircleCollider2D>() != null)
        {
            gameObject.GetComponent<CircleCollider2D>().radius = _size / 2;
        }    
    }

    private void FixedUpdate()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
            Destroy(gameObject);
    }
}
