using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    public Text cameraInfo;
    public Text selectedMethodText;
    public Text planetInfoText;
    public Text spawnWarningText;

    public CameraControllerBase cameraController;
    public Image gridImage;
    public Button startSimulationButton;
    public Button graphButton;
    public PhysicsEngine pe;
    public PlanetMenu pm;
    public GraphDrawer graph;

    public int methodHelper;


    void Start()
    {
        cameraInfo.text = "X,Y axis";
        if (methodHelper == 0)
        {
            int selectedMethod = PlayerPrefs.GetInt("SelectedMethod", 0);
            UpdateSelectedMethodText(selectedMethod);
        }
        else if (methodHelper == 1)
        {
            int selectedMethod = PlayerPrefs.GetInt("SelectedMethod1", 0);
            UpdateSelectedMethodText(selectedMethod);
        }
        else if (methodHelper == 2)
        {
            int selectedMethod = PlayerPrefs.GetInt("SelectedMethod2", 0);
            UpdateSelectedMethodText(selectedMethod);
        }

        spawnWarningText.enabled = false;


}

    void Update()
    {

        string positionsText = "Planet Positions:\n";
        foreach (Planet planet in pe.planets)
        {
            Vector3 pos = planet.transform.position;
            float acceleration = planet.acceleration;
            
            positionsText += $"{planet.name}: X={pos.x:F2}, Y={pos.y:F2}, Z={pos.z:F2}, Acceleration: {acceleration}\n";

        }
        planetInfoText.text = positionsText;


        // Handle camera switching (you can use UI buttons or any other method)
        if (cameraController.GetActiveView() == 0)
        {
            cameraInfo.text = "X,Y axis";

        }
        else if (cameraController.GetActiveView() == 1)
        {
            cameraInfo.text = "X,Z axis";

        }
        else if (cameraController.GetActiveView() == 2)
        {
            cameraInfo.text = "Y,Z axis";

        }
    }

    void UpdateSelectedMethodText(int selectedMethod)
    {
        string methodText = "Selected Method: ";
        switch (selectedMethod)
        {
            case 0:
                Debug.Log("Euler");
                methodText += "Euler";
                break;
            case 1:
                Debug.Log("Adaptive Euler");
                methodText += "Adaptive Euler";
                break;
            case 2:
                Debug.Log("RungeKutta4");
                methodText += "Runge-Kutta 4";
                break;
            case 3:
                Debug.Log("Adaptive Runge-Kutta");
                methodText += "Adaptive Runge-Kutta";
                break;
            default:
                Debug.Log("Unknown method");
                methodText += "Unknown Method";
                break;
        }
        selectedMethodText.text = methodText;
    }
    
    


}
