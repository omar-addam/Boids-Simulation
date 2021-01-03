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

            // Compute cohesion
            acceleration += NormalizeSteeringForce(ComputeCohisionForce());

            // Compute seperation
            acceleration += NormalizeSteeringForce(ComputeSeperationForce());

            // TODO: Compute alignment

            // TODO: Compute collision avoidance

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
        /// Computes the cohision force that will pull the bird back to the center of the flock.
        /// </summary>
        private Vector3 ComputeCohisionForce()
        {
            // Get current center of the flock
            Vector3 center = Flock.CenterPosition;

            // Get rid of this bird's position from the center
            float newCenterX = center.x * Flock.Birds.Count - transform.localPosition.x;
            float newCenterY = center.y * Flock.Birds.Count - transform.localPosition.y;
            float newCenterZ = center.z * Flock.Birds.Count - transform.localPosition.z;
            Vector3 newCenter = new Vector3(newCenterX, newCenterY, newCenterZ) / (Flock.Birds.Count - 1);

            // Compute force
            return newCenter - transform.localPosition;
        }

        /// <summary>
        /// Computes the seperation force that will ensure a safe distance is kept between the birds.
        /// </summary>
        private Vector3 ComputeSeperationForce()
        {
            // TODO: Implement
            return Vector3.zero;
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
