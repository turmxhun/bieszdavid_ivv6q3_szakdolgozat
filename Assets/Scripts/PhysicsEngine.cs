using ODESolvers;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEngine : MonoBehaviour
{
    public float G = 6.674f;
    public List<Planet> planets;
    public float time;
    public float dt = 0.01f;
    public int selectedSolver;
    public ODESolverBase solver;

    private void Start()
    {
        float savedGravity = PlayerPrefs.GetFloat("Gravity", 6.674f);
        G = savedGravity;

        float savedDeltaTime = PlayerPrefs.GetFloat("DeltaTime", 0.01f);
        dt = savedDeltaTime;


        PlayerPrefs.DeleteKey("Gravity");
        PlayerPrefs.DeleteKey("DeltaTime");
    }
    public void UpdateOrbit(Planet planet)
    {
        if (solver != null)
        {
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
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[0], x[2], x[4] }, 0, t),
                (x, t) => x[3],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[0], x[2], x[4] }, 1, t),
                (x, t) => x[5],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[0], x[2], x[4] }, 2, t),
                (x, t) => x[7],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[6], x[8], x[10] }, 0, t),
                (x, t) => x[9],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[6], x[8], x[10] }, 1, t),
                (x, t) => x[11],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[6], x[8], x[10] }, 2, t),
                (x, t) => x[13],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[12], x[14], x[16] }, 0, t),
                (x, t) => x[15],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[12], x[14], x[16] }, 1, t),
                (x, t) => x[17],
                (x, t) => CalculateTotalAcceleration(planet, new float[] { x[12], x[14], x[16] }, 2, t)
            };

            float[] result = solver.Solve(functions, planetData, time, dt);

            if (result != null)
            {
                planet.transform.position = new Vector3(result[0], result[2], result[4]);
                planet.velocity = new Vector3(result[1], result[3], result[5]);
            }
        }
    }

        private float CalculateTotalAcceleration(Planet currentPlanet, float[] currentPosition, int component, float t)
        {
            float totalAcceleration = 0f;
            foreach (Planet otherPlanet in planets)
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
                    currentPlanet.acceleration = acceleration;
                    totalAcceleration += acceleration;
                }
            }

            return totalAcceleration;
        }


}
