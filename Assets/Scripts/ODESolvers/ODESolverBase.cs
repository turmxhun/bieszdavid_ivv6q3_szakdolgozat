using UnityEngine;

namespace ODESolvers
{
    public abstract class ODESolverBase
    {
        public delegate float Function(float[] x, float t);

        public abstract float[] Solve(Function[] f, float[] x0, float t0, float dt);
    }
}
