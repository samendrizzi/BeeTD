using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement main;

    [SerializeField] private Camera cam;

    private float zoomStep = 1f;
    private float minCamSize = 1f;
    private float maxCamSize = 10f;
    private float moveStep = 1f;
    private float keystrokesPerSecond = 20f;

    private Vector3 dragOrigin;
    private float keystrokeTimer;

    private void Start()
    {
        zoomStep = GlobalValues.main.zoomStep;
        minCamSize = GlobalValues.main.minCamSize;
        maxCamSize = GlobalValues.main.maxCamSize;
        moveStep = GlobalValues.main.moveStep;
        keystrokesPerSecond = GlobalValues.main.keystrokesPerSecond;
    }

    private void Awake()
    {
        main = this;
    }

    private void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ZoomIn();
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ZoomOut();
        }
       
        //Disabled due to click interface logic
        //PanCamera();
        if (keystrokeTimer > 1 / keystrokesPerSecond)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                ZoomIn();
                keystrokeTimer = 0f;
            }
            if (Input.GetKey(KeyCode.X))
            {
                ZoomOut();
                keystrokeTimer = 0f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                PanCameraLeft();
                keystrokeTimer = 0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                PanCameraRight();
                keystrokeTimer = 0f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                PanCameraUp();
                keystrokeTimer = 0f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                PanCameraDown();
                keystrokeTimer = 0f;
            }
        }
        keystrokeTimer += Time.deltaTime; 
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += difference;
        }
    }
    
    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;
        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    public void PanCameraLeft()
    {
        cam.transform.position += Vector3.left * moveStep;
    }

    public void PanCameraRight()
    {
        cam.transform.position += Vector3.right * moveStep;
    }

    public void PanCameraUp()
    {
        cam.transform.position += Vector3.up * moveStep;
    }

    public void PanCameraDown()
    {
        cam.transform.position += Vector3.down * moveStep;
    }
}
