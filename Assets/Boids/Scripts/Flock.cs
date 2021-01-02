using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Broids
{
    public class Flock : MonoBehaviour
    {

        #region Fields/Properties

        /// <summary>
        /// The prefab template used to generate birds in this flock.
        /// </summary>
        [SerializeField]
        [Tooltip("The prefab template used to generate birds in this flock")]
        private GameObject BirdTemplate;

        #endregion

    }
}
