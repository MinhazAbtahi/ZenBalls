using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public RectTransform hand;
    public Image fillImg;

    // Start is called before the first frame update
    void Start()
    {
        StartTutorial();
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameStarted) return;

        if (Input.GetMouseButtonDown(0))
        {
            this.enabled = false;
            gameObject.SetActive(false);
        }
    }

    private void StartTutorial()
    {
        hand.DOAnchorPosY(-250f, 1.5f).SetLoops(-1, LoopType.Restart);
        fillImg.DOFillAmount(1f, 1.5f).SetLoops(-1, LoopType.Restart);
    }
}
