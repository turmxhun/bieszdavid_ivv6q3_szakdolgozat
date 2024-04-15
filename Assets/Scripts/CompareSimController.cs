using ODESolvers;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using UnityEngine;

public class CompareSimController : MonoBehaviour
{
    public PhysicsEngine pe1;
    public PhysicsEngine pe2;

    public CompCameraController cc;

    public HUDController hud1;
    public HUDController hud2;

    public GraphDrawer gd1;
    public GraphDrawer gd2;

    public GameObject planetPrefab;
    private bool isSimulating = false;

    public int maxPlanets = 6;
    private int activePlanets = 0;
    //private ODESolverBase solver;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var planet in pe1.planets)
        {
            planet.gameObject.SetActive(false);
        }
        if (activePlanets != 6)
        {
            hud1.startSimulationButton.enabled = false;
            hud1.graphButton.enabled = false;
            hud2.graphButton.enabled = false;
        }
        pe1.selectedSolver = PlayerPrefs.GetInt("SelectedMethod1", 0);
        pe2.selectedSolver = PlayerPrefs.GetInt("SelectedMethod2", 0);

        PlayerPrefs.DeleteKey("SelectedMethod1");
        PlayerPrefs.DeleteKey("SelectedMethod2");

        // Initialization
        pe1.time = 0f;
        pe2.time = 0f;


        // Initialize solvers for the first group
        InitializeSolver(pe1.selectedSolver, out pe1.solver);
        

        // Initialize solvers for the second group
        InitializeSolver(pe2.selectedSolver, out pe2.solver);
    }

    public void StartSimulation()
    {
        pe2.dt = pe1.dt;
        pe2.G = pe1.G;
        // Update orbits for the first group of planets using solver1
        foreach (Planet planet in pe1.planets)
        {
            pe1.UpdateOrbit(planet);
        }

        // Update orbits for the second group of planets using solver2
        foreach (Planet planet in pe2.planets)
        {
            pe2.UpdateOrbit(planet);
        }

        pe1.time += pe1.dt;
        pe2.time += pe2.dt;
    }

    public void IsSimulating()
    {
        isSimulating = true;
        gd1.StartSimulation();
        gd2.StartSimulation();
        hud1.graphButton.enabled = true;
        hud2.graphButton.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        hud1.startSimulationButton.onClick.AddListener(IsSimulating);
        hud1.graphButton.onClick.AddListener(ShowGraph);
        hud2.graphButton.onClick.AddListener(ShowGraph);

        if (!isSimulating)
        {

            //Debug.Log("First if");
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Second if");
                if (activePlanets < maxPlanets)
                {
                    //Debug.Log("Third if");
                    // Convert mouse position to world position
                    // Convert mouse position to world position in the left simulation area
                    // Convert mouse position to world position in the left simulation area

                    UnityEngine.Vector3 mousePosition = Input.mousePosition;
                    UnityEngine.Vector3 worldPositionLeft = cc.cameras[0].ScreenToWorldPoint(new UnityEngine.Vector3(mousePosition.x, mousePosition.y, 100));
                    float xDifference = cc.cameras[1].transform.position.x - cc.cameras[0].transform.position.x;
                    if (mousePosition.x <= Screen.width / 2)
                    {
                        UnityEngine.Vector3 worldPositionRight = new UnityEngine.Vector3(worldPositionLeft.x + xDifference, worldPositionLeft.y, worldPositionLeft.z);
                        GameObject newPlanetLeft = Instantiate(planetPrefab, worldPositionLeft, UnityEngine.Quaternion.identity);
                        GameObject newPlanetRight = Instantiate(planetPrefab, worldPositionRight, UnityEngine.Quaternion.identity);
                        AddPlanet(pe1, gd1, newPlanetLeft.GetComponent<Planet>());
                        AddPlanet(pe2, gd2, newPlanetRight.GetComponent<Planet>());
                        activePlanets += 2;
                    }
                    else
                    {
                        Debug.Log("Rossz helyre kattint");
                        hud1.spawnWarningText.enabled = true;
                        new WaitForSeconds(3f);
                        hud1.spawnWarningText.enabled = false;

                    }
                    // Get the difference in x-coordinate between the two simulation areas


                    // Calculate the world position in the right simulation area by adding the xDifference




                }
                if (activePlanets == 6)
                {
                    hud1.startSimulationButton.enabled = true;
                }


            }
            if (Input.GetMouseButtonDown(1) && activePlanets == 6)
            {
                for (int i = 0; i < pe1.planets.Count; i++)
                {
                    pe1.planets[i].EnableCollider(true);
                }
                Debug.Log("Right click");
                // Perform a raycast from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // If the ray hits a planet
                if (Physics.Raycast(ray, out hit))
                {
                    Planet planet = hit.collider.GetComponent<Planet>();
                    if (planet != null)
                    {



                        // Display the modification menu
                        hud1.pm.ShowPanel(planet);

                        // Disable the collider after showing the panel
                        planet.EnableCollider(false);
                    }
                }
                for (int i = 0; i < pe1.planets.Count; i++)
                {
                    pe1.planets[i].EnableCollider(false);
                }
            }
        }
        else
        {
            StartSimulation();
        }
    }



    public void ShowGraph()
    {
        //Debug.Log("Show Graph has been called");
        hud1.graph.ShowGraph();
    }
    void InitializeSolver(int selectedSolver, out ODESolverBase solver)
    {
        switch (selectedSolver)
        {
            case 0:
                solver = new ODESolverEuler();
                break;
            case 1:
                solver = new ODESolverRK4();
                break;
            case 2:
                solver = new ODESolverAMEuler();
                break;
            case 3:
                solver = new ODESolverARK4();
                break;
            // Add cases for other solvers if needed
            default:
                solver = null;
                //Debug.LogError("Invalid solver selection");
                break;
        }
    }
    public void AddPlanet(PhysicsEngine pe, GraphDrawer gd, Planet newPlanet)
    {
        // Add the new planet to the list
        pe.planets.Add(newPlanet);
        gd.AddPlanet(newPlanet);
    }


}