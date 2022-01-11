using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraSetting : MonoBehaviour
{
    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneWidth = 10;

    Camera _camera;
    public GameObject InputFieldsLabels;

    void Start()
    {
        _camera = GetComponent<Camera>();
        Debug.Log("sdadas");
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
    }
}
