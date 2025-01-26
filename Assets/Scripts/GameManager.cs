using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LEVELS currentLevel;
    public int AmountOfRingsToCollectOnLevel = 1;
    public int amountOfRingsCollected = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            currentLevel = LEVELS.LEVEL1;
            Instance = this;
        }
    }

    public void LoadNextLevel()
    {
        switch (currentLevel)
        {
            case LEVELS.MAINMENU:
                SceneManager.LoadScene("Level1");
                break;
            case LEVELS.LEVEL1:
                SceneManager.LoadScene("Level2");
                break;
            case LEVELS.LEVEL2:
                SceneManager.LoadScene("Level3");
                break;
            case LEVELS.LEVEL3:
                Debug.Log("No more levels to load.");
                break;
        }
    }

    public void CollectRing()
    {
        amountOfRingsCollected++;
        UIController.Instance.UpdateRingText(Math.Max(AmountOfRingsToCollectOnLevel - amountOfRingsCollected, 0));
    }

    public void LoadLevel(string levelName)
    {

        SceneManager.LoadScene(levelName);
    }

    public bool CanOpenLevelDoor()
    {
        return amountOfRingsCollected >= AmountOfRingsToCollectOnLevel;
    }

    public void LoadMainMenu()
    {
       SceneManager.LoadScene("MainMenu");
    }
}
