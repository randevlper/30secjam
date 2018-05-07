using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour {

    public GameObject target;
    public float speed = 3f;

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
}
