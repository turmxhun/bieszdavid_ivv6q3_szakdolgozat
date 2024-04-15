using UnityEngine;
using UnityEngine.UI;

public class CompareHUD : MonoBehaviour
{
    public Text cameraInfo;
    public Text selectedMethodText1;
    public Text selectedMethodText2;
    public Text planetInfoText1;
    public Text planetInfoText2;
    public Planet[] planets1;
    public Planet[] planets2;
    public CameraController cameraController;
    // Start is called before the first frame update
    void Start()
    {
        cameraInfo.text = "X,Y axis";
        int selectedMethod1 = PlayerPrefs.GetInt("SelectedMethod1", 0);
        int selectedMethod2 = PlayerPrefs.GetInt("SelectedMethod2", 0);
        UpdateSelectedMethodText(selectedMethod1, selectedMethodText1);
        UpdateSelectedMethodText(selectedMethod2, selectedMethodText2);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlanetInfo(planets1, planetInfoText1);
        UpdatePlanetInfo(planets2, planetInfoText2);


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraInfo.text = "X,Y axis";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraInfo.text = "X,Z axis";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraInfo.text = "Y,Z axis";
        }
    }

    void UpdateSelectedMethodText(int selectedMethod, Text MethodText)
    {
        string methodText = "Selected Method: ";
        switch (selectedMethod)
        {
            case 0:
                Debug.Log("Euler");
                methodText += "Euler";
                break;
            case 1:
                Debug.Log("RungeKutta4");
                methodText += "Runge-Kutta 4";
                break;
            case 2:
                Debug.Log("Adaptive Euler");
                methodText += "Adaptive Euler";
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
        MethodText.text = methodText;
    }

    void UpdatePlanetInfo(Planet[] planets, Text planetInfoText)
    {
        string positionsText = "Planet Positions:\n";
        foreach (Planet planet in planets)
        {
            Vector3 pos = planet.transform.position;
            positionsText += $"{planet.name}: X={pos.x:F2}, Y={pos.y:F2}, Z={pos.z:F2}\n";
        }
        planetInfoText.text = positionsText;
        
    }
}

