using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedAssets : Singleton<SharedAssets>
{
    public Material activeBallMaterial;
    public Material inActiveBallMaterial;
    public Color[] ballColors;
    public Collider2D finishCollider;
    public GameObject tutorial;

    public Color GetRandomColor() => ballColors[Random.Range(0, ballColors.Length)];
}
