using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour, IDamageable {

    public GameObject target;
    public float speed = 3f;
    public float maxHealth = 20f;
    public float health = 20f;
    public float damage = 5f;
    public GameObject healthUI;

    private Rigidbody2D rb;
    Slider myHealthUI;

    Canvas canvas;

    void Start()
    {
        //service locator
        canvas = ServiceLocator.instance.canvas;

        //rigidbody
        rb = GetComponent<Rigidbody2D>();

        //health UI 
        GameObject healthUIGameObject = Instantiate(healthUI, canvas.transform);
        myHealthUI = healthUIGameObject.GetComponent<Slider>();
        myHealthUI.GetComponent<EnemyHealth>().target = this.gameObject;
        myHealthUI.interactable = false;
        myHealthUI.value = 1;

    }

    void FixedUpdate()
    {
        Vector2 temp = (target.transform.position - transform.position);
        temp.Normalize();
        rb.velocity = temp * speed;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Player")
        {
            IDamageable Damageable = collisionInfo.gameObject.GetComponent<IDamageable>();
            if (Damageable != null)
            {
                Damageable.Damage(new HitData(gameObject, damage));
            }
        }
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
        SetHealth(maxHealth);
        myHealthUI.gameObject.SetActive(true);
    }

    public void Setup(GameObject target)
    {
        this.target = target;
    }
}
