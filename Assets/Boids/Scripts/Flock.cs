using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Broids
{
    public class Flock : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Executes once on awake.
        /// </summary>
        private void Awake()
        {
            if (_FlockSettings == null)
                _FlockSettings = ScriptableObject.CreateInstance<FlockSettingScriptable>();

            if (_FlockSettings.NumberOfBirdsToGenerateOnAwake > 0)
                Initialize(_FlockSettings.NumberOfBirdsToGenerateOnAwake);
        }

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

        /// <summary>
        /// A scriptable object instance that contains the flock's settings.
        /// </summary>
        [Tooltip("A scriptable object instance that contains the flock's settings.")]
        [SerializeField]
        private FlockSettingScriptable _FlockSettings;

        /// <summary>
        /// A scriptable object instance that contains the flock's settings.
        /// </summary>
        public FlockSettingScriptable FlockSettings { get { return _FlockSettings; } }



        [Header("Center")]

        /// <summary>
        /// The sphere representing the center of the flock.
        /// </summary>
        [SerializeField]
        [Tooltip("The sphere representing the center of the flock.")]
        private GameObject Center;

        /// <summary>
        /// The current center (local position) of the flock.
        /// </summary>
        [SerializeField]
        [Tooltip("The current center (local position) of the flock.")]
        private Vector3 _CenterPosition;

        /// <summary>
        /// The current center (local position) of the flock.
        /// </summary>
        public Vector3 CenterPosition { get { return _CenterPosition; } }



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
        /// Continuously compute the position of the center of the flock.
        /// </summary>
        private void Update()
        {
            // Compute the center
            float centerX = 0, centerY = 0, centerZ = 0;
            foreach (Bird bird in _Birds)
            {
                centerX += bird.transform.localPosition.x;
                centerY += bird.transform.localPosition.y;
                centerZ += bird.transform.localPosition.z;
            }
            _CenterPosition = new Vector3(centerX, centerY, centerZ) / _Birds.Count();

            // Move the sphere to the center
            Center.transform.localPosition = _CenterPosition;

            // Update sphere visibility
            Center.gameObject.SetActive(_FlockSettings.IsCenterVisible);
        }

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
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            // Add a velocity
            birdScript.Initialize(this);
        }

        #endregion

    }
}
