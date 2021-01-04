using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Fields/Properties

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

    #endregion

}
