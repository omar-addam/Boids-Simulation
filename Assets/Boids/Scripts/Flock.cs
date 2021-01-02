using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broids
{
    public class Flock : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Generates the birds in the flock.
        /// </summary>
        /// <param name="numberOfBirds">The number of birds to be generated in this flock.</param>
        public void Initialize(int numberOfBirds)
        {
            // Clear any existing bird
            Clear();

            // Create new birds
            for (int i = 0; i < numberOfBirds; i++)
                CreateBird();
        }

        #endregion

        #region Fields/Properties

        [Header("Birds")]

        /// <summary>
        /// The prefab template used to generate birds in this flock.
        /// </summary>
        [SerializeField]
        [Tooltip("The prefab template used to generate birds in this flock.")]
        private GameObject BirdTemplate;

        /// <summary>
        /// The parent holding all the generated birds.
        /// </summary>
        [SerializeField]
        [Tooltip("The parent holding all the generated birds.")]
        private GameObject BirdsParent;

        /// <summary>
        /// List of all the birds in this flock.
        /// </summary>
        [SerializeField]
        [Tooltip("List of all the birds in this flock.")]
        private List<Bird> _Birds;

        /// <summary>
        /// List of all the birds in this flock.
        /// </summary>
        public List<Bird> Birds { get { return _Birds; } }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes all generated birds.
        /// </summary>
        private void Clear()
        {
            _Birds = new List<Bird>();
            foreach (Transform bird in BirdsParent.transform)
                GameObject.Destroy(bird.transform);
        }

        /// <summary>
        /// Adds a new bird to the flock.
        /// </summary>
        private void CreateBird()
        {
            // Initialize list
            if (_Birds == null)
                _Birds = new List<Bird>();

            // Create new bird
            GameObject bird = GameObject.Instantiate(BirdTemplate, BirdsParent.transform);

            // Extract its script
            Bird birdScript = bird.GetComponent<Bird>();
            _Birds.Add(birdScript);

            // Set random location
            bird.transform.localPosition = new Vector3
            (
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f)
            );

            // Set random rotation
            bird.transform.localEulerAngles = new Vector3
            (
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
            );
        }

        #endregion

    }
}
