using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODESolvers
{
    public class ODESolverEuler : ODESolverBase
    {
        public new delegate float Function(float[] x, float t);

        public override float[] Solve(ODESolverBase.Function[] f, float[] x0, float t0, float dt)
        {
            int n = x0.Length;
            float[] x = new float[n];

            for (int i = 0; i < n; i++)
            {
                x[i] = x0[i] + dt * f[i](x0, t0);
            }

            return x;
        }
    }
}
