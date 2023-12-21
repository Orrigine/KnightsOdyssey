using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject _attackHitbox;
    [SerializeField] GameObject _hugeAttack;
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _fire;
    [SerializeField] GameObject _wave;
    [SerializeField] GameObject _spear;

    private GameObject _attackHitboxInstance;

    public void Attack(Vector3 position, Quaternion rotation)
    {
        _attackHitboxInstance = Instantiate(_attackHitbox);
        _attackHitboxInstance.transform.parent = gameObject.transform;
        _attackHitboxInstance.transform.position = position;
        _attackHitboxInstance.transform.rotation = rotation;
    }

    public void HugeAttack(Vector3 position, Quaternion rotation)
    {
        _attackHitboxInstance = Instantiate(_hugeAttack);
        _attackHitboxInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
        _attackHitboxInstance.GetComponent<Transform>().position = position;
        _attackHitboxInstance.GetComponent<Transform>().rotation = rotation;
    }

    public void Arrow(Vector3 position, Quaternion rotation)
    {
        _attackHitboxInstance = Instantiate(_arrow);
        _attackHitboxInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
        _attackHitboxInstance.GetComponent<Transform>().position = position;
        _attackHitboxInstance.GetComponent<Transform>().rotation = rotation;
        _attackHitboxInstance.GetComponent<Rigidbody2D>().velocity = _attackHitboxInstance.transform.up * 30;
        _attackHitboxInstance.transform.SetParent(null);
    }

    public void FirePit(Vector3 position, Quaternion rotation, bool precast)
    {
        _attackHitboxInstance = Instantiate(_fire);
        if (precast)
        {
            _attackHitboxInstance.GetComponent<CircleCollider2D>().enabled = false;
            _attackHitboxInstance.GetComponent<HitBox>()._lifeTime = 1.9f;
            _attackHitboxInstance.GetComponent<SpriteRenderer>().sprite = _attackHitboxInstance.GetComponent<SpriteLibrary>().GetSprite("Main", "Cast");
        }
        else
        {
            _attackHitboxInstance.GetComponent<CircleCollider2D>().enabled = true;
            _attackHitboxInstance.GetComponent<HitBox>()._lifeTime = 3;
            _attackHitboxInstance.GetComponent<SpriteRenderer>().sprite = _attackHitboxInstance.GetComponent<SpriteLibrary>().GetSprite("Main", "Attack");
        }
        _attackHitboxInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
        _attackHitboxInstance.GetComponent<Transform>().position = position;
        _attackHitboxInstance.GetComponent<Transform>().rotation = rotation;
        _attackHitboxInstance.transform.SetParent(null);
    }

    public void Shockwave(Vector3 position, Quaternion rotation)
    {
        _attackHitboxInstance = Instantiate(_wave);
        _attackHitboxInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
        _attackHitboxInstance.GetComponent<Transform>().position = position;
        _attackHitboxInstance.GetComponent<Transform>().rotation = rotation;
        _attackHitboxInstance.transform.SetParent(null);
    }

    public void Spear(Vector3 position, Quaternion rotation)
    {
        _attackHitboxInstance = Instantiate(_spear);
        _attackHitboxInstance.GetComponent<Transform>().parent = gameObject.GetComponent<Transform>();
        _attackHitboxInstance.GetComponent<Transform>().position = position;
        _attackHitboxInstance.GetComponent<Transform>().rotation = rotation;
        _attackHitboxInstance.GetComponent<Rigidbody2D>().velocity = _attackHitboxInstance.transform.up * 30;
        _attackHitboxInstance.transform.SetParent(null);
    }
}
