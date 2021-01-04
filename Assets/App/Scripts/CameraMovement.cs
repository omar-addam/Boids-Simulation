using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on start.
    /// </summary>
    private void Start()
    {
        RotateCamera();
    }

    #endregion

    #region Fields/Properties

    [Header("General")]

    /// <summary>
    /// The target which the camera rotates around.
    /// </summary>
    [SerializeField]
    [Tooltip("The target which the camera rotates around.")]
    private GameObject Target;

    /// <summary>
    /// The position offset applied to the target's position
    /// </summary>
    [SerializeField]
    [Tooltip("The position offset applied to the target's position.")]
    private Vector3 TragetOffset = Vector3.zero;



    [Header("Zoom")]

    /// <summary>
    /// The minimum distance a camera can keep with the target when zooming in.
    /// </summary>
    [SerializeField]
    [Tooltip("The minimum distance a camera can keep with the target when zooming in.")]
    private float MinZoom = 5f;

    /// <summary>
    /// The maximum distance a camera can keep with the target when zooming out.
    /// </summary>
    [SerializeField]
    [Tooltip("The maximum distance a camera can keep with the target when zooming out.")]
    private float MaxZoom = 10f;

    /// <summary>
    /// The current distance the camera is keeping with the target.
    /// </summary>
    [SerializeField]
    [Tooltip("The current distance the camera is keeping with the target.")]
    private float Zoom = 10f;



    [Header("Rotation")]

    /// <summary>
    /// The latitude used to compute the rotation of the camera.
    /// </summary>
    [SerializeField]
    [Tooltip("The latitude used to compute the rotation of the camera.")]
    private float Latitude = 0f;

    /// <summary>
    /// The longitude used to compute the rotation of the camera.
    /// </summary>
    [SerializeField]
    [Tooltip("The longitude used to compute the rotation of the camera.")]
    private float Longitude = 0f;

    /// <summary>
    /// The speed at which the camera rotates around the target.
    /// </summary>
    [SerializeField]
    [Tooltip("The speed at which the camera rotates around the target.")]
    private float RotationSpeed = 10f;

    #endregion

    #region Methods

    /// <summary>
    /// Continuously check if the user is trying to rotate the camera using a mouse.
    /// </summary>
    private void Update()
    {
        // Check if user is trying to drag the camera
        if (Input.GetMouseButton(0) || Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // Check if rotating
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                Longitude -= Input.GetAxis("Mouse X") * RotationSpeed * Zoom / MaxZoom;
                Latitude -= Input.GetAxis("Mouse Y") * RotationSpeed * Zoom / MaxZoom;
            }

            // Check if zooming
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float smoothedTime = Mathf.Sqrt(Time.deltaTime / 0.02f);
                Zoom *= 1f - Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * smoothedTime * 1f, -.8f, .4f);
                Zoom = Mathf.Clamp(Zoom, MinZoom, MaxZoom);
            }

            // Apply rotation
            RotateCamera();
        }
    }

    /// <summary>
    /// Rotates the camera around the target object to fit the provided latitude and longitude.
    /// </summary>
    private void RotateCamera()
    {
        // Get target's center
        Vector3 center = Vector3.zero;
        if (Target != null)
            center = Target.transform.position;

        // Compute rotation
        Quaternion rotation = Quaternion.Euler(Latitude, -Longitude, 0);

        // Compute position
        Vector3 position = center - (Quaternion.Euler(Latitude, -Longitude, 0) * Vector3.forward * Zoom);
        
        // Set position and rotation
        transform.rotation = rotation;
        transform.position = position;

        // Apply offset
        transform.position += TragetOffset;
    }

    #endregion

}
