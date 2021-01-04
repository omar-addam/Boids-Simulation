using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Fields/Properties

    [Header("General")]

    /// <summary>
    /// The target which the camera rotates around.
    /// </summary>
    [SerializeField]
    [Tooltip("The target which the camera rotates around.")]
    public GameObject Target;

    /// <summary>
    /// The speed at which the camera rotates around the target.
    /// </summary>
    [SerializeField]
    [Tooltip("The speed at which the camera rotates around the target.")]
    public float RotationSpeed = 10f;



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



    [Header("Rotation Angle")]

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

    #endregion

    #region Methods

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
    }

    #endregion

}
