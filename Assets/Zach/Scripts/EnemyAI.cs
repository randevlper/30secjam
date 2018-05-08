using Gold;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour {

    public GameObject target;
    public float speed = 3f;
    public Character character;
    public Animator animator;

    public float damage = 5f;
    public float attackSpeed = 2f;
    public float attackRange = 2f;
    public float attackPauseTime = 0.5f;
    public GameObject healthUI;

    private Rigidbody2D rb;
    Slider myHealthUI;

    Canvas canvas;

    Timer attackTimer;
    public bool isAbleToAttack;
    public bool isAttacking;

    void Start () {
        //character
        character.onHit += OnHit;
        character.onDeath += Death;
        character.onHealthChange += SetHealthUI;

        //service locator
        canvas = ServiceLocator.instance.canvas;

        //rigidbody
        rb = GetComponent<Rigidbody2D> ();

        attackTimer = new Timer (SetAttack, attackSpeed, false);
        attackTimer.Start ();

        //health UI 
        GameObject healthUIGameObject = Instantiate (healthUI, canvas.transform);
        myHealthUI = healthUIGameObject.GetComponent<Slider> ();
        myHealthUI.GetComponent<EnemyHealth> ().target = this.gameObject;
        myHealthUI.interactable = false;
        myHealthUI.value = 1;

    }

    public void StopAttacking () {
        isAttacking = false;
        animator.SetBool ("IsAttacking", isAttacking);
    }

    public void Attack () {
        isAttacking = true;
        animator.SetBool ("IsAttacking", isAttacking);
        IDamageable damageable = target.GetComponent<IDamageable> ();
        damageable.Damage (new HitData (gameObject, damage));
        attackTimer.Start ();
    }

    public void CanAttack () {
        if (Vector3.Distance (target.transform.position, transform.position) < attackRange && !isAttacking) {
            Attack ();
        }
    }

    void Update () {
        attackTimer.Tick (Time.deltaTime);
        CanAttack ();
    }

    void SetRotation () {
        float lookAngle = Vector2.SignedAngle (
            Vector2.right,
            (target.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.Euler (0, 0, lookAngle - 90);
    }

    void FixedUpdate () {
        if (!isAttacking) {
            Vector2 moveDir = (target.transform.position - transform.position);
            moveDir.Normalize ();
            rb.velocity = moveDir * speed;
            SetRotation ();
        } else {
            rb.velocity = Vector2.zero;
        }
    }

    public void SetAttack () {
        isAbleToAttack = true;
    }

    public void OnHit (HitData hit) {
        myHealthUI.value = (character.Health / character.MaxHealth);
    }

    void Death () {
        gameObject.SetActive (false);
        myHealthUI.gameObject.SetActive (false);
    }

    public void SetHealthUI (float value) {
        myHealthUI.value = value;
    }

    public void OnEnable () {
        if (myHealthUI != null) {
            myHealthUI.gameObject.SetActive (true);
        }
        character.Health = character.MaxHealth;
    }

    public void Setup (GameObject target) {
        this.target = target;
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere (transform.position, attackRange);
    }
}