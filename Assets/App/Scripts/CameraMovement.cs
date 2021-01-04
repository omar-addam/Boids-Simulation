using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Fields/Properties

    [Header("Zoom")]

    /// <summary>
    /// The minimum distance a camera can keep with the target when zooming in.
    /// </summary>
    [SerializeField]
    [Tooltip("The minimum distance a camera can keep with the target when zooming in.")]
    private float MinZoom = 0.5f;

    /// <summary>
    /// The maximum distance a camera can keep with the target when zooming out.
    /// </summary>
    [SerializeField]
    [Tooltip("The maximum distance a camera can keep with the target when zooming out.")]
    private float MaxZoom = 1f;

    /// <summary>
    /// The current distance the camera is keeping with the target.
    /// </summary>
    [SerializeField]
    [Tooltip("The current distance the camera is keeping with the target.")]
    private float Zoom = 1f;



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

}
