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
    public Slider healthUI;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healthUI.interactable = false;
        healthUI.value = 1;
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
        healthUI.value = (health / maxHealth);
        Debug.Log("did a thing");
    }
}
