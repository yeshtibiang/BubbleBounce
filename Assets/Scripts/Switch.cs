using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool isActivated = false;
    public KeySO associateKey;
    [SerializeField]
    private SpriteRenderer leftRing;
    [SerializeField]
    private SpriteRenderer rightRing;

    [SerializeField]
    private Sprite activeLeftRing;
    [SerializeField]
    private Sprite activeRightRing;
    [SerializeField]
    private Sprite inactiveLeftRing;
    [SerializeField]
    private Sprite inactiveRightRing;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                SoundManager.Instance.PlayPickUpSfx();
                other.GetComponent<PlayerInventory>().CollectKey(associateKey);
                isActivated = true;
                leftRing.sprite = inactiveLeftRing;
                rightRing.sprite = inactiveRightRing;

            }
        }
    }

}
