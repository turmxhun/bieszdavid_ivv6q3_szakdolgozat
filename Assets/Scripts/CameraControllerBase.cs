using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerBase : MonoBehaviour
{
    private int activeView;
    public virtual int GetActiveView()
    {
        return activeView;
    }

    protected void SetActiveView(int activeView)
    {
        activeView = this.activeView;
    }
}
