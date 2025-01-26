using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "Scriptable Objects/PowerUp")]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public PowerUpType powerUpType;
    //public Sprite powerUpSprite;
    public Color powerUpColor;
    public float duration;
    public float speedMultiplier;

}

public enum PowerUpType
{
    Red,
    Blue,
    Green
}
