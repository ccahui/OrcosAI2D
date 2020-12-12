using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;
    private bool inRange; //Check if Player is in range
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;
    #endregion



    private float VIDA_FINAL = 800;
    private int vida;

    HealthBar healthBar;

    void Awake()
    {
        GameObject gameObject = transform.Find("HealthBar").gameObject;
        healthBar= gameObject.GetComponent<HealthBar>();

        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
        distance = float.MaxValue;

    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetSize(1);
        vida = (int)VIDA_FINAL;
    }


    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
        }

        //When Player is detected
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            anim.SetBool("moving", false);
            StopAttack();
        }

    }


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("golpear", false);
        }
    }

    void Move()
    {
        anim.SetBool("moving", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("golpear"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }

    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("golpear", false);
    }
    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("moving", false);
        anim.SetBool("golpear", true);

    }


    public void enemeyAttack(int daño)
    {
     
        vida = vida - daño;
        if(vida <= 0)
        {
            healthBar.SetSize(0);
            vida = 0;
        }
        else
        {
            float size = (vida * 1f) / VIDA_FINAL;
            healthBar.SetSize(size);
        }
    }
    public int GetVida()
    {
        return vida;
    }

    void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy")
        {
            target = trig.transform;
            inRange = true;
           // quitarVida();
        }
    }

    private void quitarVida()
    {
        if (target != null)
        {
            EnemyBehavior enemy = target.GetComponent<EnemyBehavior>();
            enemy.enemeyAttack(50);
            verificarVida(enemy);
        }
    }

    void verificarVida(EnemyBehavior enemy)
    {
        if (enemy.GetVida() <= 0)
        {
            target.gameObject.SetActive(false);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
        quitarVida();
    }
}
