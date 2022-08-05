using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameLoader : MonoBehaviour
{
    public bool loadCompleted;

    private byte currentTextIndex;
    private string[] texts = { "Loading", "Loading.", "Loading..", "Loading..." };

    [SerializeField] private float loadingTime = 2f;
    [SerializeField] TextMeshProUGUI loadingText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Image progressBarImg;

    WaitForSeconds time = new WaitForSeconds(0.2f);

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(InitializeStuff());

        if (loadCompleted)
        {
            CloseLoadingPanel();
        }
        maxProgress = 0.9f;
        progressStep = 0.1f;
        elapsedLimit = 0.25f;
    }


    private IEnumerator InitializeStuff()
    {
        yield return (time);
    }

    // Update is called once per frame
    private float totalTimeElapsed;
    private float elapsed;
    private float elapsedLimit;
    private float maxProgress;
    private float progressStep;

    private void Update()
    {
        if (!loadCompleted)
        {
            totalTimeElapsed += Time.deltaTime;
            if (totalTimeElapsed >= loadingTime)
            {
                OnLoadingCompleted();
            }
        }

        elapsed += Time.deltaTime;
        if (elapsed >= elapsedLimit)
        {
            elapsed = 0;
            elapsedLimit += 0.1f;
            UpdateLoadingUI(progressBarImg != null ? (progressBarImg.fillAmount + progressStep) : 0.1f);
        }
    }

    private void UpdateLoadingUI(float progress)
    {
        if (progress > 1) progress = 1.0f;
        if (progressBarImg != null && progressBarImg.fillAmount < maxProgress)
        {
            progressBarImg.fillAmount = progress;

            if (loadingText != null)
            {
                int progressPercentage = (int)(progress * 100);
                loadingText.text = texts[currentTextIndex++] + " " + progressPercentage + "%";
                if (currentTextIndex >= texts.Length) currentTextIndex = 0;
            }
        }
    }

    public void OnLoadingCompleted()
    {
        if (loadCompleted) return;

        loadCompleted = true;
        maxProgress = 0.9f;
        progressStep = 0.1f;
        elapsedLimit = 0.25f;
        UpdateLoadingUI(1);
        CloseLoadingPanel();
    }


    private void CloseLoadingPanel()
    {
        titleText.DOFade(0, .5f);
        GetComponent<CanvasGroup>().DOFade(0, .5f).OnComplete(() =>
        {
            SetGameProperties();
            //Destroy(gameObject.transform.parent.gameObject);
            gameObject.transform.parent.gameObject.SetActive(false);
        });
    }

    private void OpenLoadingPanel()
    {
        gameObject.transform.parent.gameObject.SetActive(true);
    }

    private void SetGameProperties()
    {
        GameManager.Instance.GameStarted();
    }
}
