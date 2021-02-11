using Boids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{

    #region Initialization and Updates

    /// <summary>
    /// Flock settings.
    /// </summary>
    public FlockSettingScriptable Settings;

    /// <summary>
    /// Executes once on start.
    /// </summary>
    private void Start()
    {
        // Display the app version
        DisplayVersion();
    }

    #endregion

    #region Version

    /// <summary>
    /// Text UI element displaying the app version.
    /// </summary>
    [SerializeField]
    [Tooltip("Text UI element displaying the app version.")]
    private Text Version;

    /// <summary>
    /// Displays current project's version.
    /// </summary>
    private void DisplayVersion()
    {
        Version.text = string.Format("Version: {0}", Application.version);
    }

    #endregion

    #region General Settings



    #endregion

}
