using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broids
{
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

        /// <summary>
        /// Initiaizes the bird.
        /// </summary>
        public void Initialize(Flock flock)
        {
            // Give the bird a small push
            Rigidbody.velocity = transform.forward.normalized * flock.FlockSettings.MinSpeed;

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
            acceleration += NormalizeSteeringForce(ComputeCohisionForce())
                * Flock.FlockSettings.CohesionForceWeight;

            // Compute seperation
            acceleration += NormalizeSteeringForce(ComputeSeperationForce())
                * Flock.FlockSettings.SeperationForceWeight;

            // Compute alignment
            acceleration += NormalizeSteeringForce(ComputeAlignmentForce())
                * Flock.FlockSettings.AlignmentForceWeight;

            // Compute collision avoidance
            acceleration += NormalizeSteeringForce(ComputeCollisionAvoidanceForce()) 
                * Flock.FlockSettings.CollisionAvoidanceForceWeight;

            // Compute the new velocity
            Vector3 velocity = Rigidbody.velocity;
            velocity += acceleration * Time.deltaTime;

            // Ensure the velocity remains within the accepted range
            velocity = velocity.normalized * Mathf.Clamp(velocity.magnitude,
                Flock.FlockSettings.MinSpeed, Flock.FlockSettings.MaxSpeed);

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
            return force.normalized * Mathf.Clamp(force.magnitude, 0, Flock.FlockSettings.MaxSteerForce);
        }

        /// <summary>
        /// Computes the cohision force that will pull the bird back to the center of the flock.
        /// </summary>
        private Vector3 ComputeCohisionForce()
        {
            // Check if this is the only bird in the flock
            if (Flock.Birds.Count == 1)
                return Vector3.zero;

            // Check if we are using the center of the flock
            if (Flock.FlockSettings.UseCenterForCohesion)
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

            // Else, use the center of the neighbor birds
            float centerX = 0, centerY = 0, centerZ = 0;
            int count = 0;
            foreach (Bird bird in Flock.Birds)
            {
                if (bird == this
                    || (bird.transform.position - transform.position).magnitude > Flock.FlockSettings.CohesionRadiusThreshold)
                    continue;

                centerX += bird.transform.localPosition.x;
                centerY += bird.transform.localPosition.y;
                centerZ += bird.transform.localPosition.z;
                count++;
            }

            // Compute force
            return count == 0 
                ? Vector3.zero 
                : new Vector3(centerX, centerY, centerZ) / count;
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
                    || (bird.transform.position - transform.position).magnitude > Flock.FlockSettings.SeperationRadiusThreshold)
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
                    || (bird.transform.position - transform.position).magnitude > Flock.FlockSettings.AlignmentRadiusThreshold)
                    continue;

                force += bird.transform.forward;
            }

            return force;
        }

        /// <summary>
        /// Computes the force that helps avoid collision.
        /// </summary>
        private Vector3 ComputeCollisionAvoidanceForce()
        {
            // Check if heading to collision
            if (!Physics.SphereCast(transform.position,
                Flock.FlockSettings.CollisionAvoidanceRadiusThreshold, 
                transform.forward, 
                out RaycastHit hitInfo,
                Flock.FlockSettings.CollisionAvoidanceRadiusThreshold))
                return Vector3.zero;

            // Compute force
            return transform.position - hitInfo.point;
        }

        #endregion

    }
}
