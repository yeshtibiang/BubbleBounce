using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private PowerUp currentPowerUp;
    private float powerUpTimer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (currentPowerUp != null)
        {
            powerUpTimer -= Time.deltaTime;
            if ( powerUpTimer <= 0)
            {
                EndPowerUp();
            }
        }
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        if (currentPowerUp != null)
        {
            EndPowerUp();
        }

        currentPowerUp = powerUp;
        powerUpTimer = powerUp.duration;

        if (powerUp.powerUpColor != null)
        {
            spriteRenderer.color = powerUp.powerUpColor;
        }

        switch (powerUp.powerUpType)
        {
            case PowerUpType.Red:
                break;
            case PowerUpType.Blue:
                playerMovement.IsDoubleJumpActive = true;
                break;
            case PowerUpType.Green:
                playerMovement.SetSpeedMultiplier(powerUp.speedMultiplier);
                break;
        }
    }

    private void EndPowerUp()
    {
        spriteRenderer.color = Color.white;
        playerMovement.SetSpeedMultiplier(1f);
        currentPowerUp = null;
    }
}
