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

        /// <summary>
        /// The maximum steering force that can be applied at any framerate.
        /// </summary>
        private const float MAX_STEER_FORCE = 3;

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
        public void Initialize(Flock flock)
        {
            // Give the bird a small push
            Rigidbody.velocity = transform.forward.normalized * MIN_SPEED;

            // Reference the flock this bird belongs to
            Flock = flock;
        }

        #endregion

        #region Fields/Properties

        /// <summary>
        /// References the rigidbody attached to this object.
        /// </summary>
        private Rigidbody Rigidbody;

        /// <summary>
        /// The flock this bird belongs to.
        /// </summary>
        private Flock Flock;

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

        /// <summary>
        /// Normalizes the steering force and clamps it.
        /// </summary>
        private Vector3 NormalizeSteeringForce(Vector3 force)
        {
            return force.normalized * Mathf.Clamp(force.magnitude, 0, MAX_STEER_FORCE);
        }

        #endregion

    }
}
