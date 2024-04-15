using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void Start()
    {
        // Configure cameras for split-screen view
        camera1.rect = new Rect(0, 0, 0.5f, 1);
        camera2.rect = new Rect(0.5f, 0, 0.5f, 1);
    }
}
