using ODESolvers;
using UnityEngine;

public class PhysicsEngineComp : MonoBehaviour
{
    public float G = 6.674f;
    public Planet[] planetsGroup1; // Array of planets for the first group
    public Planet[] planetsGroup2; // Array of planets for the second group
    public float time;
    public float dt;
    public int selectedSolverGroup1; // Selected solver for the first group
    public int selectedSolverGroup2; // Selected solver for the second group
    private ODESolverBase solver1;
    private ODESolverBase solver2;

    void Start()
    {
        selectedSolverGroup1 = PlayerPrefs.GetInt("SelectedMethod1", 0);
        selectedSolverGroup2 = PlayerPrefs.GetInt("SelectedMethod2", 0);
        PlayerPrefs.DeleteKey("SelectedMethod1");
        PlayerPrefs.DeleteKey("SelectedMethod2");

        // Initialization
        time = 0f;
        dt = 0.01f; // Example value

        // Initialize solvers for the first group
        InitializeSolver(selectedSolverGroup1, out solver1);

        // Initialize solvers for the second group
        InitializeSolver(selectedSolverGroup2, out solver2);
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

    public void StartSimulation()
    {
        // Update orbits for the first group of planets using solver1
        foreach (Planet planet in planetsGroup1)
        {
            UpdateOrbit(planet, solver1, planetsGroup1);
        }

        // Update orbits for the second group of planets using solver2
        foreach (Planet planet in planetsGroup2)
        {
            UpdateOrbit(planet, solver2, planetsGroup2);
        }

        time += dt;
    }

    void Update()
    {
        StartSimulation(); // You may want to call StartSimulation in the Update method for testing
    }
    private void UpdateOrbit(Planet planet, ODESolverBase solver, Planet[] allPlanets)
    {
        if (solver != null)
        {
            // Collect data for the specified planet
            float[] planetData = new float[6];

            planetData[0] = planet.transform.position.x;
            planetData[1] = planet.velocity.x;
            planetData[2] = planet.transform.position.y;
            planetData[3] = planet.velocity.y;
            planetData[4] = planet.transform.position.z;
            planetData[5] = planet.velocity.z;

            ODESolverBase.Function[] functions = new ODESolverBase.Function[]
            {
            (x, t) => x[1],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[0], x[2], x[4] }, 0, t,allPlanets),
                (x, t) => x[3],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[0], x[2], x[4] }, 1, t,allPlanets),
                (x, t) => x[5],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[0], x[2], x[4] }, 2, t, allPlanets),
                // Additional equations for the second planet
                (x, t) => x[7],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[6], x[8], x[10] }, 0, t, allPlanets),
                (x, t) => x[9],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[6], x[8], x[10] }, 1, t, allPlanets),
                (x, t) => x[11],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[6], x[8], x[10] }, 2, t, allPlanets),
                // Additional equations for the third planet
                (x, t) => x[13],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[12], x[14], x[16] }, 0, t, allPlanets),
                (x, t) => x[15],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[12], x[14], x[16] }, 1, t, allPlanets),
                (x, t) => x[17],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[12], x[14], x[16] }, 2, t, allPlanets)
            };

            float[] result = solver.Solve(functions, planetData, time, dt);

            if (result != null)
            {
                //Debug.Log($"Planet {planet.name} - Position: {result[0]}, {result[2]}, {result[4]}");
                //Debug.Log($"Planet {planet.name} - Velocity: {result[1]}, {result[3]}, {result[5]}");

                planet.transform.position = new Vector3(result[0], result[2], result[4]);
                planet.velocity = new Vector3(result[1], result[3], result[5]);
            }
        }
    }







    // Calculate total acceleration due to gravity from all other planets
    private float CalculateTotalAcceleration(Planet currentPlanet, float[] currentPosition, int component, float t, Planet[] allPlanets)
    {
        float totalAcceleration = 0f;
        foreach (Planet otherPlanet in allPlanets)
        {
            if (otherPlanet != currentPlanet)
            {
                float[] direction = new float[]
                {
                otherPlanet.transform.position.x - currentPosition[0],
                otherPlanet.transform.position.y - currentPosition[1],
                otherPlanet.transform.position.z - currentPosition[2]
                };
                float distance = Mathf.Sqrt(direction[0] * direction[0] + direction[1] * direction[1] + direction[2] * direction[2]);
                float forceMagnitude = (G * currentPlanet.mass * otherPlanet.mass) / Mathf.Pow(distance, 2);
                float[] force = new float[]
                {
                direction[0] * forceMagnitude / distance,
                direction[1] * forceMagnitude / distance,
                direction[2] * forceMagnitude / distance
                };
                float acceleration = force[component] / currentPlanet.mass;
                totalAcceleration += acceleration;
            }
        }

        return totalAcceleration;
    }


    // Other necessary methods...
}
