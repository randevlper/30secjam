using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour, IDamageable {

    public GameObject target;
    public float speed = 3f;
    public float health = 20f;
    public float damage = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 temp = (target.transform.position - transform.position);
        temp.Normalize();
        rb.velocity = temp * speed;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        IDamageable Damageable = collisionInfo.otherCollider.GetComponent<IDamageable>();
        if (collisionInfo.gameObject.tag == "Player")
        {
            Damageable.Damage(new HitData(gameObject, damage));
            Debug.Log("worked");
        }
        else
        {
            Debug.Log("not worked");
        }
    }

    public void Damage(HitData hit)
    {
        health -= hit.damage;
    }
}
