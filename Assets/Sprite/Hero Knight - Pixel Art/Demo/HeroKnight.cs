using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class HeroKnight : MonoBehaviour {

    [FormerlySerializedAs("m_speed")] [SerializeField]
    private float      m_speed = 4.0f;
    [FormerlySerializedAs("m_rollForce")] [SerializeField]
    private float      m_rollForce = 16.0f;
    [FormerlySerializedAs("m_noBlood")] [SerializeField]
    private bool       m_noBlood = false;
    
    [FormerlySerializedAs("_renderModule")] [SerializeField]
    private GameObject m_renderModule;
    [FormerlySerializedAs("_bodyModule")] [SerializeField]
    private GameObject m_bodyModule;
    [FormerlySerializedAs("_actionModule")] [SerializeField]
    private GameObject m_actionModule;
    [FormerlySerializedAs("_audioModule")] [SerializeField]
    private GameObject m_audioModule;
    [FormerlySerializedAs("_structPlayer")] [SerializeField]
    private StructPlayer m_structPlayer;
    
    private LifeSystem          m_lifeSystem;
    private SpriteRenderer      m_spriteRenderer;
    private Aiming              m_aiming;
    private AudioCharacter      m_audioCharacter;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_rolling = false;
    private bool                m_isBlocking = false;
    private float               m_blockCooldown = 1.0f;
    private bool                m_isDead = false;
    private bool                m_isAttacking = false;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private readonly float      m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCooldown = 2;

    public bool IsBlocking
    {
        get { return m_isBlocking; }
        set { m_isBlocking = value; }
    }

    // Use this for initialization
    private void Start ()
    {
        m_structPlayer.MaxLife = m_bodyModule.GetComponent<LifeSystem>().MaxLife;
        m_aiming = m_actionModule.GetComponent<Aiming>();
        m_lifeSystem = m_bodyModule.GetComponent<LifeSystem>();
        m_spriteRenderer = m_renderModule.GetComponent<SpriteRenderer>();
        m_animator = m_renderModule.GetComponent<Animator>();
        m_body2d = transform.parent.GetComponent<Rigidbody2D>();
        m_audioCharacter = m_audioModule.GetComponent<AudioCharacter>();
        m_animator.SetBool("Grounded", true);
        
        m_lifeSystem.OnDeath += Die;
        m_lifeSystem.OnTakeDamage += TakeDamage;
        m_lifeSystem.OnTakeDamage += m_audioCharacter.HurtSound;
        m_lifeSystem.OnHeal += m_audioCharacter.HealSound;
    }

    // Update is called once per frame
    private void Update ()
    {
        if (m_lifeSystem.IsDead) return;
        
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        
        // -- Handle input and movement --
        Vector2 inputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Swap direction of sprite depending on walk direction
        if (m_aiming.Direction.x > 0)
            m_spriteRenderer.flipX = false;
            
        else if (m_aiming.Direction.x < 0)
            m_spriteRenderer.flipX = true;

        // Move
        if (!m_rolling)
        {
            m_body2d.velocity = new Vector2(inputMove.x * m_speed, inputMove.y * m_speed);
        }

        //Attack
        if (Input.GetMouseButtonDown(0) && !m_rolling && !m_isAttacking && m_structPlayer.CanAttacking)
        {
            m_audioCharacter.AttackSound();
            StartCoroutine(Attack());
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling && m_structPlayer.CanShield)
        {
            m_audioCharacter.SecondarySound();
            Block();
        }

        // Roll
        else if (Input.GetKeyDown("left shift") && m_structPlayer.CanRoll && !m_rolling)
        {
            StartCoroutine(Roll());
        }

        //Run
        else if (Mathf.Abs(inputMove.x) > Mathf.Epsilon || Mathf.Abs(inputMove.y) > Mathf.Epsilon)
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
        m_audioCharacter.DeathSound();
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
        m_aiming.enabled = false;
        m_bodyModule.GetComponent<Collider2D>().enabled = false;
    }

    public IEnumerator Attack()
    {
        m_isAttacking = true;
        
        m_currentAttack++;

        // Loop back to one after third attack
        if (m_currentAttack > 3)
            m_currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (m_timeSinceAttack > 2.0f)
            m_currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        m_animator.SetTrigger("Attack" + m_currentAttack);
    
        Vector3 atkPos = m_actionModule.transform.position + m_aiming.Direction;
        atkPos.z = 0;
        Quaternion atkRot = Quaternion.Euler(0, 0, Mathf.Atan2(atkPos.y, atkPos.x) * Mathf.Rad2Deg);
        m_actionModule.GetComponent<PlayerAttack>().Attack(atkPos, atkRot);
        
        yield return new WaitForSeconds(0.25f);
        m_isAttacking = false;
        
        if (m_currentAttack == 3)
        {
            m_structPlayer.CanAttacking = false;
            yield return new WaitForSeconds(0.75f); // Wait 0.5 to prevent spamming combo
            m_structPlayer.CanAttacking = true;
        }
        m_timeSinceAttack = 0.0f;
        yield break;
    }
    
    private void TakeDamage()
    {
        m_animator.SetTrigger("Hurt");
    }
    
    public void Block()
    {
        StartCoroutine(Blocking());
    }
    
    private IEnumerator Blocking()
    {
        m_animator.SetTrigger("Block");
        m_isBlocking = true;
        m_structPlayer.CanShield = false;
        yield return new WaitForSeconds(0.5f);
        m_isBlocking = false;
        yield return new WaitForSeconds(m_blockCooldown);
        m_structPlayer.CanShield = true;
        yield break;
    }

    public IEnumerator Roll()
    {
        m_rolling = true;
        m_structPlayer.CanRoll = false;
        m_audioCharacter.RunSound();
        m_animator.SetTrigger("Roll");
        m_lifeSystem.SetInvincible(m_rollDuration);
        m_body2d.velocity = new Vector2(m_aiming.Direction.x * m_rollForce, m_aiming.Direction.y * m_rollForce);

        yield return new WaitForSeconds(m_rollDuration);
        m_rolling = false;
        yield return new WaitForSeconds(m_rollCooldown);
        m_structPlayer.CanRoll = true;
        yield break;
    }
}
