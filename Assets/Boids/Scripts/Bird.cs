using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broids
{
    public class Bird : MonoBehaviour
    {

        #region Constants

        /// <summary>
        /// The minimum speed a bird can fly.
        /// </summary>
        private const float MIN_SPEED = 2;

        /// <summary>
        /// The maximum speed a bird can fly.
        /// </summary>
        private const float MAX_SPEED = 5;

        #endregion

        #region Initialization

        /// <summary>
        /// Executes once on start.
        /// </summary>
        private void Awake()
        {
            // Extract rigid body
            Rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Initiaizes the bird.
        /// </summary>
        public void Initialize()
        {
            Rigidbody.velocity = transform.forward.normalized * MIN_SPEED;
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
            // Initialize the new velocity
            Vector3 acceleration = Vector3.zero;

            // TODO: Compute acceleration

            // Compute the new velocity
            Vector3 velocity = Rigidbody.velocity;
            velocity += acceleration * Time.deltaTime;

            // Ensure the velocity remains within the accepted range
            velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude, MIN_SPEED, MAX_SPEED);

            // Apply velocity
            Rigidbody.velocity = velocity;

            // Update rotation
            transform.forward = Rigidbody.velocity.normalized;
        }

        #endregion

    }
}
