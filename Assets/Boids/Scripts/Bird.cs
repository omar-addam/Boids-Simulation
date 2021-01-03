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

        /// <summary>
        /// The distance used to find nearby birds that we need to keep distance from.
        /// </summary>
        private const float SEPERATION_RADIUS_THRESHOLD = 1;

        /// <summary>
        /// The distance used to find nearby birds that we need to keep aligned with.
        /// </summary>
        private const float ALIGNMENT_RADIUS_THRESHOLD = 2;

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

            // Compute alignment
            acceleration += NormalizeSteeringForce(ComputeAlignmentForce());

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
        /// Normalizes the steering force and clamps it.
        /// </summary>
        private Vector3 NormalizeSteeringForce(Vector3 force)
        {
            return force.normalized * Mathf.Clamp(force.magnitude, 0, MAX_STEER_FORCE);
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
        /// Computes the seperation force that ensures a safe distance is kept between the birds.
        /// </summary>
        private Vector3 ComputeSeperationForce()
        {
            // Initialize seperation force
            Vector3 force = Vector3.zero;

            // Find nearby birds
            foreach (Bird bird in Flock.Birds)
            {
                if (bird == this
                    || (bird.transform.position - transform.position).magnitude > SEPERATION_RADIUS_THRESHOLD)
                    continue;

                // Repel aaway
                force += transform.position - bird.transform.position;
            }

            return force;
        }

        /// <summary>
        /// Computes the alignment force that aligns this bird with nearby birds.
        /// </summary>
        private Vector3 ComputeAlignmentForce()
        {
            // Initialize alignment force
            Vector3 force = Vector3.zero;

            // Find nearby birds
            foreach (Bird bird in Flock.Birds)
            {
                if (bird == this
                    || (bird.transform.position - transform.position).magnitude > ALIGNMENT_RADIUS_THRESHOLD)
                    continue;

                force += bird.transform.forward;
            }

            return force;
        }

        #endregion

    }
}
