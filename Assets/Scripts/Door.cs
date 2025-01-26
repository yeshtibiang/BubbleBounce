using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;


public enum LEVELS
{
    LEVEL1,
    LEVEL2,
    LEVEL3,
    MAINMENU,
}

public class Door : MonoBehaviour
{
    public LEVELS level;
    public List<KeySO> requiredKeys;
    public bool isLocked = true;
    public SpriteRenderer doorSprite;
    public Color lockedColor;
    public Color unlockedColor;

    public Transform door;

    private void Start()
    {
        if (requiredKeys.Count == 0)
        {
            Debug.LogWarning("Door is missing a required key.");
        }
    }

    public void TryOpenWithGameManager()
    {
        if (GameManager.Instance.CanOpenLevelDoor())
        {
            OpenDoor();
        }
    }

    public void TryOpen(PlayerInventory playerInventory)
    {
        // verify if player has all the keys to open the door
        int requiredKeyCount = requiredKeys.Count;
        requiredKeys.ForEach(key =>
        {
            if (playerInventory.HasKey(key))
            {
                requiredKeyCount--;
            }
        });

        if (isLocked && requiredKeyCount == 0)
        {
            isLocked = false;
            OpenDoor();
        }
        else if (isLocked)
        {
            Debug.Log("Door is locked.");
        }
        else
        {
            Debug.Log("Door is already unlocked.");
        }
    }

    private void OpenDoor()
    {

        OpenDoorSprite();
    }

    private void UnlockDoor()
    {
        isLocked = false;
        UpdateDoorVisual();
    }

    private void UpdateDoorVisual()
    {
        doorSprite.color = isLocked ? lockedColor : unlockedColor;
    }

    public void OpenDoorSprite()
    {
        door.DOMoveY(door.position.y + 70, 3f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
