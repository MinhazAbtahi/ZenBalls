using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public GameObject gameUI;
    public GameObject gameOverUI;
    public GameObject cameraSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleGameUI(bool activate)
    {
        gameUI.GetComponent<CanvasGroup>().DOFade(activate ? 1f : 0f, .25f).OnComplete(()=>
        {
            if (activate) SoundManager.Instance.PlayStarSFX();
            gameUI.SetActive(activate);
        });
    }

    public void ToggleGameOverUI(bool activate)
    {
        ToggleGameUI(false);
        gameOverUI.SetActive(activate);
        gameOverUI.GetComponent<CanvasGroup>().DOFade(activate ? 1f : 0f, .5f);
        HapticController.PlayHaptic(HapticController.HapticType.Success);
    }

}
