using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RuntimeCircleClipper player;
    private Transform camTransform;
    private CameraFollow cameraFollow;
    private Slider slider;
    private bool isInput;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        cameraFollow = camTransform.GetComponent<CameraFollow>();
        slider = GetComponent<Slider>();
        //slider.maxValue = camTransform.position.y;
        slider.value = camTransform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInput) slider.value = camTransform.position.y;
    }

    public void MoveCamnera(float posY)
    {
        if (!isInput) return;

        if (cameraFollow.follow) cameraFollow.follow = false;
        if (!player.lockInput) player.lockInput = true;
        camTransform.position = new Vector3(camTransform.position.x, posY, camTransform.position.z);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!cameraFollow.follow) cameraFollow.follow = true;
        if (player.lockInput) player.lockInput = false;
        if(isInput) isInput = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isInput = true;
    }
}
