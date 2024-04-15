using ODESolvers;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class SingleSimController : MonoBehaviour
{
    public CameraController cc;
    public PhysicsEngine pe;
    public HUDController hud;
    public GraphDrawer gd;

    public GameObject planetPrefab;
    private bool isSimulating = false;

    public int maxPlanets = 3;
    private int activePlanets = 0;

    void Start()
    {
        foreach (var planet in pe.planets)
        {
            planet.gameObject.SetActive(false);
        }
        if (activePlanets != 3)
        {
            hud.startSimulationButton.enabled = false;
            hud.graphButton.enabled = false;
        }
        pe.selectedSolver = PlayerPrefs.GetInt("SelectedMethod", 0);
        PlayerPrefs.DeleteKey("SelectedMethod");
        pe.time = 0f;

        switch (pe.selectedSolver)
        {
            case 0:
                pe.solver = new ODESolverEuler();
                break;
            case 1:
                pe.solver = new ODESolverAMEuler();
                break;
            case 2:
                pe.solver = new ODESolverRK4();
                break;
            case 3:
                pe.solver = new ODESolverARK4();
                break;
            default:
                pe.solver = null;
                Debug.LogError("Invalid solver selection");
                break;
        }
    }

    public void StartSimulation()
    {
        foreach (var planet in pe.planets)
        {
            pe.UpdateOrbit(planet);
        }

        pe.time += pe.dt;
    }

    public void IsSimulating()
    {
        isSimulating = true;
        gd.StartSimulation();
        hud.graphButton.enabled = true;
    }

    public void ShowGraph()
    {
        hud.graph.ShowGraph();
    }

    void Update()
    {
        hud.startSimulationButton.onClick.AddListener(IsSimulating);
        hud.graphButton.onClick.AddListener(ShowGraph);

        if (!isSimulating)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (activePlanets < maxPlanets)
                {
                    UnityEngine.Vector3 mousePosition = Input.mousePosition;

                    UnityEngine.Vector3 worldPosition = cc.camera.ScreenToWorldPoint(new UnityEngine.Vector3(mousePosition.x, mousePosition.y, 100));

                    GameObject newPlanet = Instantiate(planetPrefab, worldPosition, UnityEngine.Quaternion.identity);

                    AddPlanet(newPlanet.GetComponent<Planet>());

                    activePlanets++;
                }
                if (activePlanets == 3)
                {
                    hud.startSimulationButton.enabled = true;
                }


            }
            if (Input.GetMouseButtonDown(1) && activePlanets == 3)
            {
                for (int i = 0; i < pe.planets.Count; i++)
                {
                    pe.planets[i].EnableCollider(true);
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Planet planet = hit.collider.GetComponent<Planet>();
                    if (planet != null)
                    {
                        hud.pm.ShowPanel(planet);
                        planet.EnableCollider(false);
                    }
                }
                for (int i = 0; i < pe.planets.Count; i++)
                {
                    pe.planets[i].EnableCollider(false);
                }
            }
        }
        else
        {
            StartSimulation();
        }
    }
    

    public void AddPlanet(Planet newPlanet)
    {
        pe.planets.Add(newPlanet);
        gd.AddPlanet(newPlanet);
    }
   
}