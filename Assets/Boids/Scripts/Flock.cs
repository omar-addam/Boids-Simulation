using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broids
{
    public class Flock : MonoBehaviour
    {

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

    }
}
