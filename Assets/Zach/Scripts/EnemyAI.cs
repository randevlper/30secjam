using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using Gold;


[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour, IDamageable {

    public GameObject target;
    public float speed = 3f;
    public float maxHealth = 20f;
    public float health = 20f;
    public float damage = 5f;
    public float attackSpeed = 2f;
    public float attackRange = 1f;
    public GameObject healthUI;

    private Rigidbody2D rb;
    Slider myHealthUI;
    
    bool isAbleToAttack;

    Canvas canvas;

    Timer attackTimer;

    void Start()
    {
        //service locator
        canvas = ServiceLocator.instance.canvas;

        //rigidbody
        rb = GetComponent<Rigidbody2D>();

        attackTimer = new Timer(SetAttack, attackSpeed, false);
        attackTimer.Start();

        //health UI 
        GameObject healthUIGameObject = Instantiate(healthUI, canvas.transform);
        myHealthUI = healthUIGameObject.GetComponent<Slider>();
        myHealthUI.GetComponent<EnemyHealth>().target = this.gameObject;
        myHealthUI.interactable = false;
        myHealthUI.value = 1;

    }

    void Update()
    {
        if (isAbleToAttack)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < attackRange)
            {
                IDamageable damageable = target.GetComponent<IDamageable>();
                damageable.Damage(new HitData(gameObject, damage));
                Debug.Log("enemy is attacking at a range");
                attackTimer.Reset();
                attackTimer.Start();
                isAbleToAttack = false;
            }
        }
        else
        {
            attackTimer.Tick(Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        Vector2 temp = (target.transform.position - transform.position);
        temp.Normalize();
        rb.velocity = temp * speed;
    }

    void SetAttack()
    {
        isAbleToAttack = true;
    }

    void CheckAttack()
    {
        
    }
    
    public void Damage(HitData hit)
    {
        health -= hit.damage;
        myHealthUI.value = (health / maxHealth);
        if (health <= 0)
        {
            gameObject.SetActive(false);
            myHealthUI.gameObject.SetActive(false);
        }
    }

    public void SetHealth(float value)
    {
        if (value > maxHealth)
        {
            value = maxHealth;
        }

        health = value;
        myHealthUI.value = value / maxHealth;
    }

    public void OnEnable()
    {
        if (myHealthUI != null)
        {
            SetHealth(maxHealth);
            myHealthUI.gameObject.SetActive(true);
        }
    }

    public void Setup(GameObject target)
    {
        this.target = target;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
