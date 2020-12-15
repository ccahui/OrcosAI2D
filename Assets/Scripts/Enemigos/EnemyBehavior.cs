using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
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


    public float VIDA_FINAL = 300;
    public int ATAQUE = 50;
    private int vida;
    HealthBar healthBar;

    private GameManager gameManager;
    void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
        distance = float.MaxValue;

        GameObject gameObject = transform.Find("HealthBarEnemy").gameObject;
        healthBar = gameObject.GetComponent<HealthBar>();
    }

    void Start()
    {
        healthBar.SetSize(1);
        vida = (int)VIDA_FINAL;
        gameManager = GameManager.Instance;
    }

    public void enemeyAttack(int daño)
    {

        vida = vida - daño;
        if (vida <= 0)
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

    void Update()
    {
        if (!attackMode)
        {
            Move();
        }
        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("golpear"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position,new Vector2(1,0), rayCastLength, raycastMask);
            RaycastDebugger();
        }
        //When Player is detected
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
           // Debug.Log("Enemy null");
            inRange = false;
        }

        if (inRange == false)
        {
            //anim.SetBool("moving", false);
            StopAttack();
        }

    }

    void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            // ataca al enemigo mas cercano que encuentre
           // float distanceNueva = Vector2.Distance(transform.position, trig.transform.position);
           // if (distanceNueva < distance)
            {
                target = trig.transform;
                inRange = true;
                Flip();
            }
           
         //   Debug.Log("Detecte un enemigo");
        }
    }


    void EnemyLogic()
    {
      //  Debug.Log("Enemy Logic");
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            //Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
            //quitarVida();
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
            //Debug.Log("Moviendome...");
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("moving", false);
        anim.SetBool("golpear", true);
        

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

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, new Vector2(50,50) * rayCastLength , Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
        quitarVida();
    }
    private void quitarVida()
    {

        if(target != null)
        {
            PlayerBehavior player = target.GetComponent<PlayerBehavior>();
            player.enemeyAttack(ATAQUE);
            verificarVida(player);
            gameManager.gameOver();
        }
    }
    void verificarVida(PlayerBehavior player)
    {
        if (player.GetVida() <= 0)
        {
            target.gameObject.SetActive(false);
            SelectTarget(); 
        }
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {

        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
         //   distance = distanceToLeft;
        }
        else
        {
            target = rightLimit;
          //  distance = distanceToRight;
        }

        //Ternary Operator
        //target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;

       Flip();
    }
    void Flip()
    {


        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
   //         Debug.Log("Twist");
            rotation.y = 0;
        }
    

        //Ternary Operator
        //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }
   
}
