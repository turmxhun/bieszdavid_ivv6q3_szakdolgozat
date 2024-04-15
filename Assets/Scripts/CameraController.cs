using UnityEngine;
using UnityEngine.UI;

public class CameraController : CameraControllerBase
{
    public Camera camera;
    private int activeView;

    public Button[] cameraButtons;



    void Start()
    {
        cameraButtons[0].onClick.AddListener(SwitchX);
        cameraButtons[1].onClick.AddListener(SwitchY);
        cameraButtons[2].onClick.AddListener(SwitchZ);
    }

    //void Update()
    //{
    //    // Handle camera switching (you can use the Input.GetKey or any other input method)
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        Transform newCameraTransform = camera.transform;
    //        Vector3 newPosition = new Vector3(100.0f, 0.0f, 0.0f);
    //        Quaternion newRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
    //        SwitchCamera(newCameraTransform, newPosition, newRotation);
    //        Debug.Log(camera.transform.position);
    //        activeView = 0;

    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha2))
    //    {
    //        Transform newCameraTransform = camera.transform;
    //        Vector3 newPosition = new Vector3(0.0f, 100.0f, 0.0f);
    //        Quaternion newRotation = Quaternion.Euler(90.0f, -90.0f, 0.0f);
    //        SwitchCamera(newCameraTransform, newPosition, newRotation);
    //        Debug.Log(camera.transform.position);
    //        activeView = 1;
    //    }
    //    else if (Input.GetKeyDown(KeyCode.Alpha3))
    //    {
    //        Transform newCameraTransform = camera.transform;
    //        Vector3 newPosition = new Vector3(0.0f, 0.0f, 100.0f);
    //        Quaternion newRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    //        SwitchCamera(newCameraTransform, newPosition, newRotation);
    //        Debug.Log(camera.transform.position);
    //        activeView = 2;
    //    }
    //}

    public void SwitchCamera(Transform cameraTransform, Vector3 position, Quaternion rotation)
    {
        // Set the position and rotation of the camera
        cameraTransform.position = position;
        cameraTransform.rotation = rotation;
    }


    

    public void SwitchX()
    {
        Transform newCameraTransform = camera.transform;
        Vector3 newPosition = new Vector3(100.0f, 0.0f, 0.0f);
        Quaternion newRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        SwitchCamera(newCameraTransform, newPosition, newRotation);
        Debug.Log(camera.transform.position);
        SetActiveView(0);
    }
    public void SwitchY()
    {
        Transform newCameraTransform = camera.transform;
        Vector3 newPosition = new Vector3(0.0f, 100.0f, 0.0f);
        Quaternion newRotation = Quaternion.Euler(90.0f, -90.0f, 0.0f);
        SwitchCamera(newCameraTransform, newPosition, newRotation);
        Debug.Log(camera.transform.position);
        SetActiveView(1);
    }
    public void SwitchZ()
    {
        Transform newCameraTransform = camera.transform;
        Vector3 newPosition = new Vector3(0.0f, 0.0f, 100.0f);
        Quaternion newRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        SwitchCamera(newCameraTransform, newPosition, newRotation);
        Debug.Log(camera.transform.position);
        SetActiveView(2);
    }
    public override int GetActiveView()
    {
        return activeView;
    }
}
