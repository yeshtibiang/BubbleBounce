using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public Transform PlayPanel;
    public Transform SelectLevelPanel;

    public GameObject PlayButton;

    public void ShowPlayPanel()
    {
        PlayPanel.DOMoveX(PlayPanel.position.x - 1920, 0.5f).OnComplete(() =>
        {
            PlayPanel.gameObject.SetActive(false);
            SelectLevelPanel.gameObject.SetActive(true);
            SelectLevelPanel.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutBack);
        });
    }

    public void HidePanel()
    {
        SelectLevelPanel.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1920, 0), 0.5f).OnComplete(() =>
        {
            SelectLevelPanel.gameObject.SetActive(false);
            PlayPanel.gameObject.SetActive(true);
            PlayPanel.DOMoveX(PlayPanel.position.x + 1920, 0.5f).SetEase(Ease.InOutBack);
        });
    }

    private void Start()
    {
        AnimPlayButton();
    }

    public void LoadLevel1() { 
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    private void AnimPlayButton()
    {
        PlayButton.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}
