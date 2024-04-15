using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationControl : MonoBehaviour
{
    public Dropdown methodSingleDropdown; // Assign in the inspector
    public Dropdown methodComp1Dropdown; // Assign in the inspector
    public Dropdown methodComp2Dropdown; // Assign in the inspector
    public Button singleButton; // Assign in the inspector
    public Button compareButton;
    public InputField gravity;
    public InputField deltaTime;

    void Start()
    {
        singleButton.onClick.AddListener(SingleSimulation);
        compareButton.onClick.AddListener(CompareSimulation);

        float savedGravity = PlayerPrefs.GetFloat("Gravity", 6.674f); // Default gravity value
        gravity.text = savedGravity.ToString(); // Update the input field with the saved value

        // Add a listener to the gravity input field to handle changes
        gravity.onEndEdit.AddListener(OnGravityValueChanged);

        float savedDeltaTime = PlayerPrefs.GetFloat("DeltaTime", 0.01f); // Default gravity value
        deltaTime.text = savedDeltaTime.ToString(); // Update the input field with the saved value

        // Add a listener to the gravity input field to handle changes
        deltaTime.onEndEdit.AddListener(OnDeltaTimeValueChanged);
    }

    void SingleSimulation()
    {
        int selectedMethod = methodSingleDropdown.value;

        // Store the selected method using PlayerPrefs
        PlayerPrefs.SetInt("SelectedMethod", selectedMethod);
        float newGravity;
        if (float.TryParse(gravity.text, out newGravity))
        {
            PlayerPrefs.SetFloat("Gravity", newGravity);
        }
        float newDeltaTime;
        if (float.TryParse(deltaTime.text, out newDeltaTime))
        {
            PlayerPrefs.SetFloat("DeltaTime", newDeltaTime);
        }

        PlayerPrefs.Save();

        // Load the simulation scene
        SceneManager.LoadScene("Simulation");
    }

    void CompareSimulation()
    {
        int selectedMethod1 = methodComp1Dropdown.value;
        int selectedMethod2 = methodComp2Dropdown.value;
        Debug.Log(methodComp2Dropdown.value);

        // Store the selected method using PlayerPrefs
        PlayerPrefs.SetInt("SelectedMethod1", selectedMethod1);
        PlayerPrefs.SetInt("SelectedMethod2", selectedMethod2);
        
        float newGravity;
        if (float.TryParse(gravity.text, out newGravity))
        {
            PlayerPrefs.SetFloat("Gravity", newGravity);
        }
        float newDeltaTime;
        if (float.TryParse(deltaTime.text, out newDeltaTime))
        {
            PlayerPrefs.SetFloat("DeltaTime", newDeltaTime);
        }
        PlayerPrefs.Save();

        // Load the simulation scene
        SceneManager.LoadScene("Compare");
    }

    void OnGravityValueChanged(string newValue)
    {
        // Ensure the gravity value is valid and update PlayerPrefs
        float gravityValue;
        if (float.TryParse(newValue, out gravityValue))
        {
            PlayerPrefs.SetFloat("Gravity", gravityValue);
        }
    }

    void OnDeltaTimeValueChanged(string newValue)
    {
        // Ensure the gravity value is valid and update PlayerPrefs
        float deltaTimeValue;
        if (float.TryParse(newValue, out deltaTimeValue))
        {
            PlayerPrefs.SetFloat("DeltaTime", deltaTimeValue);
        }
    }
}