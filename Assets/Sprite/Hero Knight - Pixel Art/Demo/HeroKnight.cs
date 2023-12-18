using System;
using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    
    [SerializeField] GameObject _renderModule;
    [SerializeField] GameObject _bodyModule;
    [SerializeField] GameObject _actionModule;
    [SerializeField] StructPlayer _structPlayer;
    
    private LifeSystem          _lifeSystem;
    private SpriteRenderer      _spriteRenderer;
    private Aiming              _aiming;

    private Animator            _animator;
    private Rigidbody2D         _body2d;
    private bool                _rolling = false;
    private bool                _isBlocking = false;
    private bool                _isDead = false;
    private int                 _facingDirection = 1;
    private int                 _currentAttack = 0;
    private float               _timeSinceAttack = 0.0f;
    private float               _delayToIdle = 0.0f;
    private float               _rollDuration = 8.0f / 14.0f;
    private float               _rollCurrentTime;

    public bool IsBlocking
    {
        get { return _isBlocking; }
        set { _isBlocking = value; }
    }
    
    public int FacingDirection
    {
        get { return _facingDirection; }
        set { _facingDirection = value; }
    }


    // Use this for initialization
    void Start ()
    {
        _structPlayer.MaxLife = _bodyModule.GetComponent<LifeSystem>().MaxLife;
        _aiming = _actionModule.GetComponent<Aiming>();
        _lifeSystem = _bodyModule.GetComponent<LifeSystem>();
        _spriteRenderer = _renderModule.GetComponent<SpriteRenderer>();
        _animator = _renderModule.GetComponent<Animator>();
        _body2d = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        _animator.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        _timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(_rolling)
            _rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(_rollCurrentTime > _rollDuration)
            _rolling = false;
        
        // -- Handle input and movement --
        Vector2 inputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Swap direction of sprite depending on walk direction
        if (inputMove.x > 0)
        {
            _spriteRenderer.flipX = false;
            _facingDirection = 1;
        }
            
        else if (inputMove.x < 0)
        {
            _spriteRenderer.flipX = true;
            _facingDirection = -1;
        }

        // Move
        if (!_rolling)
        {
            _body2d.velocity = new Vector2(inputMove.x * m_speed, inputMove.y * m_speed);
        }


        // -- Handle Animations -- //
        //Death
        if (Input.GetKeyDown("e") && !_rolling)
            Die();
            
        //Hurt
        else if (Input.GetKeyDown("q") && !_rolling)
            TakeDamage();

        //Attack
        else if(Input.GetMouseButtonDown(0) && _timeSinceAttack > 0.25f && !_rolling)
            Attack();

        // Block
        else if (Input.GetMouseButtonDown(1) && !_rolling)
            Block();

        else if (Input.GetMouseButtonUp(1))
            _animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !_rolling)
            Roll();

        //Run
        else if (Mathf.Abs(inputMove.x) > Mathf.Epsilon)
        {
            // Reset timer
            _delayToIdle = 0.05f;
            _animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            _delayToIdle -= Time.deltaTime;
                if(_delayToIdle < 0)
                    _animator.SetInteger("AnimState", 0);
        }
    }

    private void LateUpdate()
    {
        _structPlayer.CurrentLife = _lifeSystem.CurrentLife;
    }

    private void Die()
    {
        _animator.SetBool("noBlood", m_noBlood);
        _animator.SetTrigger("Death");
    }

    public void Attack()
    {
        _currentAttack++;

        // Loop back to one after third attack
        if (_currentAttack > 3)
            _currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (_timeSinceAttack > 1.0f)
            _currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        _animator.SetTrigger("Attack" + _currentAttack);
        float x = _aiming.Direction.x;
        if (x > 0)
        {
            _spriteRenderer.flipX = false;
            _facingDirection = 1;
        }
        if (x < 0)
        {
            _spriteRenderer.flipX = true;
            _facingDirection = -1;
        }
    
        Vector3 atkPos = _actionModule.transform.position + _aiming.Direction;
        atkPos.z = 0;
        Quaternion atkRot = Quaternion.Euler(0, 0, Mathf.Atan2(atkPos.y, atkPos.x) * Mathf.Rad2Deg);
        _actionModule.GetComponent<PlayerAttack>().Attack(atkPos, atkRot);
        
        // Reset timer
        _timeSinceAttack = 0.0f;
    }
    
    private void TakeDamage()
    {
        _animator.SetTrigger("Hurt");
    }
    
    public void Block()
    {
        _animator.SetTrigger("Block");
        StartCoroutine(Blocking());
        _animator.SetBool("IdleBlock", true);
    }
    
    private IEnumerator Blocking()
    {
        _isBlocking = true;
        yield return new WaitForSeconds(0.5f);
        _isBlocking = false;
        yield break;
    }

    public void Roll()
    {
        _rolling = true;
        _animator.SetTrigger("Roll");
        _lifeSystem.SetInvincible(_rollDuration);
        _body2d.velocity = new Vector2(_facingDirection * m_rollForce, _body2d.velocity.y);
    }
}
