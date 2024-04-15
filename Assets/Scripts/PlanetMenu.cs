using UnityEngine;
using UnityEngine.UI;

public class PlanetMenu : MonoBehaviour
{
    private Planet selectedPlanet;

    public UnityEngine.UI.Slider redSlider;
    public UnityEngine.UI.Slider greenSlider;
    public UnityEngine.UI.Slider blueSlider;

    public Text planetNameText;
    public Text planetColorText;

    public InputField[] positions;
    public InputField[] velocities;
    public InputField mass;

    public UnityEngine.UI.Image theColor;
    public UnityEngine.UI.Button closeButton;
    public UnityEngine.UI.Button applyButton;

    private void Start()
    {
        closeButton.onClick.AddListener(ClosePanel);
        applyButton.onClick.AddListener(ApplyValues);
    }

    public void ShowPanel(Planet planet)
    {
        // Show the panel
        gameObject.SetActive(true);

        // Store a reference to the selected planet's color
        selectedPlanet = planet;
        Color planetColor = selectedPlanet.GetPlanetColor();
        redSlider.value = planetColor.r;
        greenSlider.value = planetColor.g;
        blueSlider.value = planetColor.b;

        float planetMass = selectedPlanet.mass;
        mass.text = planetMass.ToString();

        // Set initial position values in the input fields
        Vector3 position = selectedPlanet.transform.position;
        positions[0].text = position.x.ToString();
        positions[1].text = position.y.ToString();
        positions[2].text = position.z.ToString();

        // Set initial velocity values in the input fields
        Vector3 velocity = selectedPlanet.velocity;
        velocities[0].text = velocity.x.ToString();
        velocities[1].text = velocity.y.ToString();
        velocities[2].text = velocity.z.ToString();

        // Populate the panel with the current settings of the planet
        planetNameText.text = planet.name;
    }

    // Called when the user changes the sliders
    public void OnColorChanged()
    {
        // Update the planet's color based on the slider values
        Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        selectedPlanet.SetPlanetColor(newColor);
        theColor.color = newColor;
    }

    public void ApplyValues()
    {
        // Parse position values from input fields
        float posX, posY, posZ;
        float.TryParse(positions[0].text, out posX);
        float.TryParse(positions[1].text, out posY);
        float.TryParse(positions[2].text, out posZ);

        // Set the new position of the planet
        selectedPlanet.transform.position = new Vector3(posX, posY, posZ);

        // Parse velocity values from input fields
        float velX, velY, velZ;
        float.TryParse(velocities[0].text, out velX);
        float.TryParse(velocities[1].text, out velY);
        float.TryParse(velocities[2].text, out velZ);

        // Set the new velocity of the planet
        selectedPlanet.velocity = new Vector3(velX, velY, velZ);

        // Parse mass value from input field
        float massValue;
        float.TryParse(mass.text, out massValue);
        selectedPlanet.mass = massValue;
        selectedPlanet.UpdateRadius();

    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
