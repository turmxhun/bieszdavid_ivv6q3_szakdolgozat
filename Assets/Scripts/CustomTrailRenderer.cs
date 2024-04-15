using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CustomTrailRenderer : MonoBehaviour
{
    public int maxPoints = 100; // Maximum number of points in the trail
    private LineRenderer lineRenderer;
    private Vector3[] trailPoints;
    private int currentPointIndex = 0;
    private Planet planet; // Reference to the associated planet

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trailPoints = new Vector3[maxPoints];
        lineRenderer.positionCount = maxPoints;

        // Initialize all points to the starting position
        for (int i = 0; i < maxPoints; i++)
        {
            trailPoints[i] = transform.position;
        }

        // Find and store a reference to the associated planet
        planet = GetComponentInParent<Planet>();

        // Set the initial color of the trail
        SetTrailColor(planet.GetPlanetColor());
    }

    void Update()
    {
        // Add the current position to the trail points and update the LineRenderer
        trailPoints[currentPointIndex] = transform.position;
        currentPointIndex = (currentPointIndex + 1) % maxPoints;
        UpdateLineRenderer();

        // Update the trail color to match the planet color
        SetTrailColor(planet.GetPlanetColor());
    }

    void SetTrailColor(Color color)
    {
        lineRenderer.startColor = color;
    }

    void UpdateLineRenderer()
    {
        for (int i = 0; i < maxPoints; i++)
        {
            int index = (currentPointIndex + i) % maxPoints;
            lineRenderer.SetPosition(i, trailPoints[index]);
        }
    }
}
