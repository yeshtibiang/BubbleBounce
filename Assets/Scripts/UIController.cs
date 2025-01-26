using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject victoryMenu;

    public Transform scoreBox;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ringText;

    public Transform nextLevelButton;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        nextLevelButton.gameObject.SetActive(GameManager.Instance.currentLevel != LEVELS.LEVEL3);
        InitScoreText();
    }

    private void InitScoreText()
    {
        ringText.text = "X" + GameManager.Instance.AmountOfRingsToCollectOnLevel.ToString();
        scoreText.text = "X" + 3;
    }

    public void LoadNextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void PauseGame()
    {
        scoreBox.GetComponent<RectTransform>().DOMoveY(scoreBox.position.y + 400, 0.4f).OnComplete(() =>
        {
            pauseMenu.SetActive(true);
            pauseMenu.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.4f).SetEase(Ease.OutBack);
        });
    }

    public void ResumeGame()
    {
        pauseMenu.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -1500), 0.4f).OnComplete(() =>
        {
            pauseMenu.SetActive(false);
            scoreBox.GetComponent<RectTransform>().DOMoveY(scoreBox.position.y - 400, 0.4f).SetEase(Ease.OutBack);
        });
    }

    public void goHome()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void UpdateRingText(int ringCount)
    {
        ringText.text = "X" + ringCount.ToString();
    }

    public void UpdateBubbleText(int bubbleCount)
    {
        scoreText.text = "X" + bubbleCount.ToString();
    }

    public void SetLevelComplete()
    {
        victoryMenu.SetActive(true);
        victoryMenu.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.4f).SetEase(Ease.OutBack).OnComplete(() =>
        {

            //victoryMenu.GetComponent<RectTransform>().DOShakeScale(0.5f, 0.2f, 10, 90);
        });
    }

    public void SetGameOver()
    {
        gameOverMenu.SetActive(true);
        gameOverMenu.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.4f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            //gameOverMenu.GetComponent<RectTransform>().DOShakeScale(0.5f, 0.2f, 10, 90);
        });
    }

    public void ReplayGame()
    {
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
    }
}
