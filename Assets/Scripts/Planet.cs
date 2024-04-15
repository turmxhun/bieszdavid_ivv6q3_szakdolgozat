using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float mass;
    public Vector3 velocity;
    public float radius;
    public Color planetColor;
    private Collider planetCollider;
    private Renderer planetRenderer;
    private LineRenderer lineRenderer;
    public float acceleration;
    
    private List<float> speedHistory = new List<float>();
    private const int MaxSpeedHistoryLength = 100;

    // Use this for initialization
    void Start()
    {
        UpdateScale();
        planetRenderer = GetComponent<Renderer>();

        planetCollider = GetComponent<Collider>();
        lineRenderer = GetComponent<LineRenderer>();
        UpdateSpeedHistory();
    }

    void UpdateScale()
    {
        float scaleSize = radius * 2; 
        transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);
    }

    public void SetPlanetColor(Color newColor)
    {
        planetColor = newColor;
        planetRenderer.material.color = newColor;
    }

    public Color GetPlanetColor()
    {
        return planetColor;
    }

    public void EnableCollider(bool isEnabled)
    {
        if (planetCollider != null)
        {
            planetCollider.enabled = isEnabled;
        }
    }

    // Get the speed of the planet
    public float GetSpeed()
    {
        // Calculate speed magnitude
        float speed = velocity.magnitude;
        return speed;
    }

    private void UpdateSpeedHistory()
    {
        float speed = velocity.magnitude;

        speedHistory.Add(speed);

        if (speedHistory.Count > MaxSpeedHistoryLength)
        {
            speedHistory.RemoveAt(0);
        }
    }

    public List<float> GetSpeedHistory()
    {
        return speedHistory;
    }

    // Clear the speed history
    public void ClearSpeedHistory()
    {
        speedHistory.Clear();
    }

    void Update()
    {
        UpdateSpeedHistory();
    }
    public void UpdateRadius()
    {
        float density = 1.0f;

        radius = Mathf.Pow((3.0f * mass) / (4.0f * Mathf.PI * density), 1.0f / 3.0f);

        UpdateScale();
    }

}
