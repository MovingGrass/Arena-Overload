using UnityEngine;
using Cinemachine;

public class CameraZoomController : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup; // Reference to the Cinemachine Target Group
    public CinemachineVirtualCamera virtualCamera; // Reference to the Virtual Camera

    [Header("Zoom Limits")]
    public float minFOV = 30f; // Minimum Field of View (for perspective cameras)
    public float maxFOV = 60f; // Maximum Field of View

    public float minOrthographicSize = 5f; // Minimum size (for orthographic cameras)
    public float maxOrthographicSize = 10f;

    private void LateUpdate()
    {
        // Check if the camera is orthographic
        if (virtualCamera.m_Lens.Orthographic)
        {
            // Clamp Orthographic Size
            virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(
                virtualCamera.m_Lens.OrthographicSize, 
                minOrthographicSize, 
                maxOrthographicSize
            );
        }
        else
        {
            // Clamp Field of View
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(
                virtualCamera.m_Lens.FieldOfView, 
                minFOV, 
                maxFOV
            );
        }
    }
}

