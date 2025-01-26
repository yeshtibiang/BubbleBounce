using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    [Header("Damage Settings")]
    public float damage = 10f;
    public bool destroysOnContact = false;

    [Header("Patrol settings (for patrol enemy)")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;
    private bool movingToB = true;

    // pour reset la position de l'ennemi au cas où
    private Vector3 initialPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Stationnary:
                // do nothing
                break;
            case EnemyType.Patrolling:
                Patrol();
                break;
        }
    }

    private void Patrol()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Patrol points not set for patrolling enemy");
            return;
        }

        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerSystem>().TakeDamage();
            if (destroysOnContact)
            {
                Destroy(gameObject);
            }
        }
    }
}

public enum EnemyType
{
    Stationnary,
    Patrolling
}
