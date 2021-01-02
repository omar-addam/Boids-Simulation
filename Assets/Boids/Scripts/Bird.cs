using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on start.
    /// </summary>
    private void Awake()
    {
        // Extract rigid body
        Rigidbody = GetComponent<Rigidbody>();
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the rigidbody attached to this object.
    /// </summary>
    private Rigidbody Rigidbody;

    #endregion

    #region Methods

    /// <summary>
    /// Continuous update the speed and rotation of the bird.
    /// </summary>
    private void Update()
    {




        // Update rotation
        transform.forward = Rigidbody.velocity.normalized;
    }

    #endregion

}
