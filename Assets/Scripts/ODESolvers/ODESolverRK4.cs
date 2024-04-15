using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODESolvers
{
    public class ODESolverRK4 : ODESolverBase
    {
        public new delegate float Function(float[] x, float t);

        public override float[] Solve(ODESolverBase.Function[] f, float[] x0, float t0, float dt)
        {
            int n = x0.Length;
            float[] k1 = new float[n];
            float[] k2 = new float[n];
            float[] k3 = new float[n];
            float[] k4 = new float[n];
            float[] x = new float[n];
            float[] xTemp = new float[n];

            // Compute k1
            for (int i = 0; i < n; i++)
            {
                k1[i] = dt * f[i](x0, t0);
            }

            // Compute k2
            for (int i = 0; i < n; i++)
            {
                xTemp[i] = x0[i] + k1[i] / 2;
            }
            for (int i = 0; i < n; i++)
            {
                k2[i] = dt * f[i](xTemp, t0 + dt / 2);
            }

            // Compute k3
            for (int i = 0; i < n; i++)
            {
                xTemp[i] = x0[i] + k2[i] / 2;
            }
            for (int i = 0; i < n; i++)
            {
                k3[i] = dt * f[i](xTemp, t0 + dt / 2);
            }

            // Compute k4
            for (int i = 0; i < n; i++)
            {
                xTemp[i] = x0[i] + k3[i];
            }
            for (int i = 0; i < n; i++)
            {
                k4[i] = dt * f[i](xTemp, t0 + dt);
            }

            // Aggregate results
            for (int i = 0; i < n; i++)
            {
                x[i] = x0[i] + (k1[i] + 2 * k2[i] + 2 * k3[i] + k4[i]) / 6;
            }

            return x;
        }
    }
}
