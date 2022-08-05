using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform ballTransform;
    private float offsetY;
    [SerializeField] private float smoothTime;
    private Vector3 refVelocity;
    public bool follow;


    void Start()
    {
        follow = true;
        offsetY = transform.position.y - ballTransform.position.y;
    }


    public void BallAttach(Transform baTransform)
    {
        this.ballTransform = baTransform.GetChild(0);
        transform.position = new Vector3(0,9.3f,-9);
        offsetY = transform.position.y - ballTransform.position.y;
    }

    void LateUpdate()
    {
        if (!follow || GameManager.Instance.isGameOver) return;

        Vector3 targetPos = new Vector3(transform.position.x, Mathf.Clamp(ballTransform.position.y + offsetY, -32f, 5f) , transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref refVelocity, smoothTime);
        //Vector3 pos = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -32f, 5f), transform.position.z);
        //transform.position = pos;
    }

    public void SetTargetBall(Transform target)
    {
        ballTransform = target;
        offsetY = transform.position.y - ballTransform.position.y;
    }
}
