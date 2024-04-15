using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODESolvers
{
    public class ODESolverAMEuler : ODESolverBase
    {
        public new delegate float Function(float[] x, float t);

        public override float[] Solve(ODESolverBase.Function[] f, float[] x0, float t0, float dt)
        {
            int n = x0.Length;
            float tolerance = 0.1f;
            float[] x1 = new float[n];
            float[] x2 = new float[n];

            // Apply the functions to get the first estimate
            for (int i = 0; i < n; i++)
            {
                x1[i] = x0[i] + dt * f[i](x0, t0);
            }

            // Refine the estimate
            for (int i = 0; i < n; i++)
            {
                x2[i] = x0[i] + dt * f[i](x1, t0 + dt);
            }

            // Calculate the error to decide if the step needs refinement
            float error = 0.0f;
            for (int i = 0; i < n; i++)
            {
                error += Mathf.Abs(x2[i] - x1[i]);
            }

            if (error > tolerance)
            {
                dt /= 2.0f; // Halve the timestep if the error is too large
                return Solve(f, x0, t0, dt); // Recursive call with refined timestep
            }

            return x2; // Return the refined state
        }
    }
}
