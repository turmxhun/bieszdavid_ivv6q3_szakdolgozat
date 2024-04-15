using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphDrawer : MonoBehaviour
{
    public GameObject pointPrefab; // Prefab for the data point
    public Transform graphParent; // Parent object for the graph points
    public float updateInterval = 0.01f; // Time interval between updates
    public int maxDataPoints = 100; // Maximum number of data points to keep
    public Button hideButton;
    
    private int speedScaleFactor = 20;
    private int timeScaleFactor = 5;
    private List<GameObject> dataPoints = new List<GameObject>(); // List to store data points for each planet
    private List<Planet> planets = new List<Planet>(); // List of planets to track
    private bool simulationStarted = false;
    private float startTime;

    private float timer = 0f;

    void Start()
    {
        hideButton.onClick.AddListener(HideGraph);
    }

    void Update()
    {
        if (!simulationStarted)
        {
            return; // If simulation hasn't started, exit Update()
        }

        timer += Time.deltaTime;

        // Update data points at regular intervals
        if (timer >= updateInterval)
        {
            timer = 0f;

            // Update data points for each tracked planet
            foreach (Planet planet in planets)
            {
                if (planet != null) // Ensure planet reference is valid
                {
                    UpdateDataPoint(planet, planet.GetSpeed());
                }
            }
        }
    }

    public void AddPlanet(Planet planet)
    {
        // Add the planet to the list of tracked planets
        planets.Add(planet);

        // Create initial data point for the newly added planet
        CreateDataPoint(planet, planet.GetSpeed());
    }

    void CreateDataPoint(Planet planet, float speed)
    {
        // Instantiate a new data point object
        GameObject dataPoint = Instantiate(pointPrefab, graphParent);

        // Set the parent of the data point to the graph parent
        dataPoint.transform.SetParent(graphParent, false);

        // Calculate the elapsed time since the simulation started
        float elapsedTime = Time.time - startTime;

        // Set the local position of the data point based on elapsed time and speed
        dataPoint.transform.localPosition = new Vector3(elapsedTime * timeScaleFactor, speed * speedScaleFactor, 0f);

        // Get the SpriteRenderer component of the data point prefab
        Image image = dataPoint.GetComponent<Image>();

        // If the prefab uses a SpriteRenderer component
        if (image != null)
        {
            // Set the color of the data point to match the color of the planet
            image.color = planet.GetPlanetColor();
            Debug.Log(image.color);
        }

        // Add the data point to the list
        dataPoints.Add(dataPoint);

        // Remove old data points if necessary
        if (dataPoints.Count > maxDataPoints)
        {
            GameObject oldestDataPoint = dataPoints[0];
            dataPoints.RemoveAt(0);
            Destroy(oldestDataPoint);
        }
    }

    void UpdateDataPoint(Planet planet, float speed)
    {
        // Create a new data point
        CreateDataPoint(planet, speed);
    }

    public void StartSimulation()
    {
        simulationStarted = true;
        startTime = Time.time; // Store the starting time of the simulation
    }

    public void ShowGraph()
    {
        gameObject.SetActive(true);
        
    }
    private void HideGraph()
    {
        gameObject.SetActive(false);

    }
}
