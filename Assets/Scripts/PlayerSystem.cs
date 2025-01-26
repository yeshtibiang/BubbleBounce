using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    [Header("Bubble Life Settings")]
    public int maxLife = 3; 
    public int currentLife;    

    [Header("Protection Settings")]
    public bool isShielded = false; 
    public float shieldStrength = 0; 
    public float resistanceMultiplier = 1f; // Multiplier to reduce incoming damage (e.g., 0.5 for 50% damage)

    [Header("Visual Feedback")]
    public SpriteRenderer bubbleSprite; // Sprite Renderer for color changes
    public Color healthyColor = Color.white; // Color when life is full
    public Color damagedColor = Color.red;  // Color when life is low
    public Color shieldedColor = Color.cyan; // Color when shield is active

    [Header("Death Settings")]
    public GameObject destructionEffect;

    [Header("Player Save points")]
    public Vector3 respawnPoint;

    private bool isAlive = true;

    private void Awake()
    {
        currentLife = maxLife;
        bubbleSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        respawnPoint = transform.position;
    }

    public void TakeDamage()
    {
        if (isShielded)
        {
            //shieldStrength -= damage * resistanceMultiplier;
            //if (shieldStrength <= 0)
            //{
            //    shieldStrength = 0;
            //    isShielded = false;
            //    bubbleSprite.color = healthyColor;
            //}
        }
        else
        {
            Die();
            //bubbleSprite.color = Color.Lerp(damagedColor, healthyColor, currentLife / maxLife);
        }
    }

    public void ActivateShield(float strength)
    {
        isShielded = true;
        shieldStrength = strength;
        bubbleSprite.color = shieldedColor;
    }

    private void Die()
    {
        SoundManager.Instance.PlayDieClip();
        currentLife--;
        currentLife = Mathf.Max(currentLife, 0);
        UIController.Instance.UpdateBubbleText((int)currentLife);
        if (currentLife <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
        
    }

    private void Respawn()
    {
        if (respawnPoint != null)
        {
            transform.position = respawnPoint;
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isAlive = false;
        if (destructionEffect != null)
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        UIController.Instance.SetGameOver();
        Destroy(gameObject);
    }

    public void Heal()
    {
        currentLife++;
        if (currentLife > maxLife)
        {
            currentLife = maxLife;
        }
        UIController.Instance.UpdateBubbleText(currentLife);
        //bubbleSprite.color = Color.Lerp(damagedColor, healthyColor, (float)currentLife / maxLife);
    }

    private void ResetPlayer()
    {
        isAlive = true;
        currentLife = maxLife;
        if (bubbleSprite != null)
            bubbleSprite.color = healthyColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SavePoint"))
        {
            respawnPoint = collision.transform.position;
            collision.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
