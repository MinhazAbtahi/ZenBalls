using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ResultType
{
    Victory,
    Defeat
}

public class GameManager : Singleton<GameManager>
{
    public bool isGameStarted;
    public bool isGameOver;

    public CameraFollow cameraFollow;

    public Action OnGameStart; 
    public Action OnGameOver; 

    public int ballsCount = 0;
    public int ballsNeeded;
    private int collectedBallsCount = 0;
    public List<Transform> balls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateBallCount() => ++ballsCount;

    public int UpdateCollectedBallCount()
    {
        --ballsNeeded;
        if ((ballsNeeded <= 0) && !isGameOver)
        {
            GameOver();
        }

        return ballsNeeded;
    }

    public void GameStarted()
    {
        isGameStarted = true;
        if (OnGameStart != null) OnGameStart();
        //UIManager.Instance.ToggleGameUI(true);
        SharedAssets.Instance.tutorial.SetActive(true);
        HapticController.PlayHaptic(HapticController.HapticType.Success);
    }

    public int IndexOfMin()
    {
        if (balls.Count == 0)
        {
            throw new ArgumentException("List is empty.", "self");
        }

        float min = balls[0].position.y;
        int minIndex = 0;

        for (int i = 1; i < balls.Count; ++i)
        {
            if (balls[i].transform.position.y < min)
            {
                min = balls[i].transform.position.y;
                minIndex = i;
            }
        }

        return minIndex;
    }

    public Transform GetLowestBall()
    {
        return balls[IndexOfMin()];
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameStarted = false;
            isGameOver = true;
            if (OnGameOver != null) OnGameOver();
            SharedAssets.Instance.finishCollider.isTrigger = false;
            SharedAssets.Instance.finishCollider.transform.GetComponent<SpriteRenderer>().enabled = true;
            UIManager.Instance.cameraSlider.SetActive(false);
            HapticController.PlayHaptic(HapticController.HapticType.Medium);
        }
    }

    public void GoToHome() => SceneManager.LoadScene(0);
    
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
