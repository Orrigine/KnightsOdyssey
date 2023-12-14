using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;
    [SerializeField] GameObject actionModule;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;

    public int FacingDirection
    {
        get { return m_facingDirection; }
        set { m_facingDirection = value; }
    }


    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_animator.SetBool("Grounded", true);
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Swap direction of sprite depending on walk direction
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
            
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }

        // Move
        if (!m_rolling)
        {
            m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);
        }


        // -- Handle Animations --

        //Death
        if (Input.GetKeyDown("e") && !m_rolling)
            Die();
            
        //Hurt
        else if (Input.GetKeyDown("q") && !m_rolling)
            TakeDamage();

        //Attack
        else if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
            Attack();

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
            Block();

        else if (Input.GetMouseButtonUp(1))
            m_animator.SetBool("IdleBlock", false);

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling)
            Roll();

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    private void Die()
    {
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");
    }

    private void Attack()
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
        float x = actionModule.GetComponent<Aiming>().Direction.x;
        if (x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        if (x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }
    
        Vector3 atkPos = actionModule.transform.position + actionModule.GetComponent<Aiming>().Direction;
        atkPos.z = 0;
        Quaternion atkRot = Quaternion.Euler(0, 0, Mathf.Atan2(atkPos.y, atkPos.x) * Mathf.Rad2Deg);
        actionModule.GetComponent<PlayerAttack>().Attack(atkPos, atkRot);
        
        // Reset timer
        m_timeSinceAttack = 0.0f;
    }
    
    private void TakeDamage()
    {
        m_animator.SetTrigger("Hurt");
    }
    
    private void Block()
    {
        m_animator.SetTrigger("Block");
        m_animator.SetBool("IdleBlock", true);
    }

    private void Roll()
    {
        m_rolling = true;
        m_animator.SetTrigger("Roll");
        m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
    }
}
