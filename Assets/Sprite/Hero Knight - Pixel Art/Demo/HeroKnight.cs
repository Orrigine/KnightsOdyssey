using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class HeroKnight : MonoBehaviour {

    [FormerlySerializedAs("m_speed")] [SerializeField]
    private float      m_speed = 4.0f;
    [FormerlySerializedAs("m_rollForce")] [SerializeField]
    private float      m_rollForce = 6.0f;
    [FormerlySerializedAs("m_noBlood")] [SerializeField]
    private bool       m_noBlood = false;
    
    [FormerlySerializedAs("_renderModule")] [SerializeField]
    private GameObject m_renderModule;
    [FormerlySerializedAs("_bodyModule")] [SerializeField]
    private GameObject m_bodyModule;
    [FormerlySerializedAs("_actionModule")] [SerializeField]
    private GameObject m_actionModule;
    [FormerlySerializedAs("_structPlayer")] [SerializeField]
    private StructPlayer m_structPlayer;
    
    private LifeSystem          m_lifeSystem;
    private SpriteRenderer      m_spriteRenderer;
    private Aiming              m_aiming;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_rolling = false;
    private bool                m_isBlocking = false;
    private bool                m_isDead = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private readonly float      m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;

    public bool IsBlocking
    {
        get { return m_isBlocking; }
        set { m_isBlocking = value; }
    }
    
    public int FacingDirection
    {
        get { return m_facingDirection; }
        set { m_facingDirection = value; }
    }


    // Use this for initialization
    private void Start ()
    {
        m_structPlayer.MaxLife = m_bodyModule.GetComponent<LifeSystem>().MaxLife;
        m_aiming = m_actionModule.GetComponent<Aiming>();
        m_lifeSystem = m_bodyModule.GetComponent<LifeSystem>();
        m_spriteRenderer = m_renderModule.GetComponent<SpriteRenderer>();
        m_animator = m_renderModule.GetComponent<Animator>();
        m_body2d = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        m_animator.SetBool("Grounded", true);
        
        m_lifeSystem.OnDeath += Die;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (m_lifeSystem.IsDead) 
            return;
        
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;
        
        // -- Handle input and movement --
        Vector2 inputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Swap direction of sprite depending on walk direction
        if (inputMove.x > 0)
        {
            m_spriteRenderer.flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputMove.x < 0)
        {
            m_spriteRenderer.flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling && !m_lifeSystem.IsDead)
        {
            m_body2d.velocity = new Vector2(inputMove.x * m_speed, inputMove.y * m_speed);
        }

        //Attack
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling && !m_lifeSystem.IsDead)
            Attack();

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling && !m_lifeSystem.IsDead)
            Block();

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_lifeSystem.IsDead)
            Roll();

        //Run
        else if (Mathf.Abs(inputMove.x) > Mathf.Epsilon && !m_lifeSystem.IsDead)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else if (!m_lifeSystem.IsDead)
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    private void LateUpdate()
    {
        m_structPlayer.CurrentLife = m_lifeSystem.CurrentLife;
    }

    private void Die()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
    }

    public void Attack()
    {
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 1.0f)
            m_currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_animator.SetTrigger("Attack" + m_currentAttack);
        float x = m_aiming.Direction.x;
        if (x > 0)
        {
            m_spriteRenderer.flipX = false;
            m_facingDirection = 1;
        }
        if (x < 0)
        {
            m_spriteRenderer.flipX = true;
            m_facingDirection = -1;
        }
    
        Vector3 atkPos = m_actionModule.transform.position + m_aiming.Direction;
        atkPos.z = 0;
        Quaternion atkRot = Quaternion.Euler(0, 0, Mathf.Atan2(atkPos.y, atkPos.x) * Mathf.Rad2Deg);
        m_actionModule.GetComponent<PlayerAttack>().Attack(atkPos, atkRot);
        
        // Reset timer
        m_timeSinceAttack = 0.0f;
    }
    
    private void TakeDamage()
    {
        m_animator.SetTrigger("Hurt");
    }
    
    public void Block()
    {
        m_animator.SetTrigger("Block");
        StartCoroutine(Blocking());
        m_animator.SetBool("IdleBlock", true);
    }
    
    private IEnumerator Blocking()
    {
        m_isBlocking = true;
        yield return new WaitForSeconds(0.5f);
        m_isBlocking = false;
        yield break;
    }

    public void Roll()
    {
        m_rolling = true;
        m_animator.SetTrigger("Roll");
        m_lifeSystem.SetInvincible(m_rollDuration);
        m_body2d.velocity = new Vector2(m_facingDirection * m_-+rollForce, m_body2d.velocity.y);
    }
}
