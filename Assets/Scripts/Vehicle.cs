using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Vehicle : MonoBehaviour
{
    public ParticleSystem smokeFX;
    public TextMeshPro counterText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameOver += OnGameOver;
        counterText.text = GameManager.Instance.ballsNeeded.ToString();
    }

    private void OnGameOver() => Go();

    // Update is called once per frame
    void Update()
    {

    }

    public void Go()
    {
        smokeFX.Play();
        transform.DOMoveX(10f, 3f).SetEase(Ease.InOutElastic).OnComplete(()=> 
        {
            UIManager.Instance.ToggleGameOverUI(true);
            GameManager.Instance.OnGameOver -= OnGameOver;
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            collision.transform.SetParent(this.transform);
            int balls = GameManager.Instance.UpdateCollectedBallCount();
            if(balls >= 0) counterText.text = balls.ToString();
        }
    }
}
