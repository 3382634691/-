using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Camemain : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    //放大
    public void ZoomIn()
    {
        mainCamera.DOOrthoSize(12.4f, 0.5f);
    }
    //缩小
    public void ZoomOut()
    {
        mainCamera.DOOrthoSize(20f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
