using UnityEngine;

public class PowerUpObject : MonoBehaviour
{
    public PowerUp powerUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PowerUpManager powerUpManager = collision.GetComponent<PowerUpManager>();
            powerUpManager.ActivatePowerUp(powerUp);
            StartCoroutine(DestroyPowerUp());
        }
    }


    private System.Collections.IEnumerator DestroyPowerUp()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}

