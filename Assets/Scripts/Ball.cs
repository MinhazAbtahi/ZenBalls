using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private MeshRenderer _renderer;
    private Rigidbody2D rb;

    [SerializeField] private bool isActivated;
    [SerializeField] private bool isFollowBall;

    [SerializeField] private float scaleMax;
    [SerializeField] private float scaleMin;

    private bool isFinished;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponentInChildren<MeshRenderer>();
        CheckStatus();
        SetScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckStatus()
    {
        if (isActivated)
        {
            Activate();
        }
        else
        {
            _renderer.material = SharedAssets.Instance.inActiveBallMaterial;
            Invoke(nameof(SetStatic), 2f);
        }
    }

    private void SetKinematic()=> rb.isKinematic = true;
    private void SetStatic()=> rb.bodyType = RigidbodyType2D.Static;

    private void Activate()
    {
        isActivated = true;
        _renderer.material = SharedAssets.Instance.activeBallMaterial;
        _renderer.material.color = SharedAssets.Instance.GetRandomColor();
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Deactivate()
    {
        isFinished = true;
        isActivated = false;
        SetStatic();
    }

    private void SetScale()
    {
        float scale = Random.Range(scaleMin, scaleMax);
        transform.localScale = Vector3.one * scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.transform.CompareTag("Finish"))
        //{
            
        //}

        if (collision.transform.CompareTag("SliderTrigger"))
        {
            collision.transform.gameObject.SetActive(true);
            UIManager.Instance.cameraSlider.SetActive(true);
        }

        if (collision.transform.CompareTag("Box"))
        {
            if (isFollowBall) GameManager.Instance.cameraFollow.follow = false;
            else
            {
                GameManager.Instance.cameraFollow.SetTargetBall(transform);
                isFollowBall = true;
            }
            Invoke(nameof(Deactivate), 1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("Ball"))
        {
            if (!isActivated && !isFinished)
            {
                if (collision.transform.GetComponent<Ball>().isActivated)
                {
                    GameManager.Instance.UpdateBallCount();
                    Activate();
                    GameManager.Instance.balls.Add(collision.transform);
                }
            }

            if (GameManager.Instance.isGameOver && isActivated)
            {
                if (!collision.transform.GetComponent<Ball>().isActivated)
                {
                    Deactivate();
                }
            }
        }

        if (collision.transform.CompareTag("Platform"))
        {
            SoundManager.Instance.PlayObjectBumpSFX();
        }

        if (collision.transform.CompareTag("BoxBase"))
        {
            Deactivate();
        }
    }


}
