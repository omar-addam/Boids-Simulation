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

        // Set and display general settings
        DisplayGeneralSettings(true);
    }

    /// <summary>
    /// Continuously updates the settings.
    /// </summary>
    private void Update()
    {
        // General settings
        UpdateGeneralSettings();
        DisplayGeneralSettings();
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

    [Header("General")]

    /// <summary>
    /// Text UI element displaying the minimum speed.
    /// </summary>
    public Text MinimumSpeedTextUI;

    /// <summary>
    /// Slider UI element displaying the minimum speed
    /// </summary>
    public Slider MinimumSpeedSliderUI;

    /// <summary>
    /// Text UI element displaying the maximum speed.
    /// </summary>
    public Text MaximumSpeedTextUI;

    /// <summary>
    /// Slider UI element displaying the minimum speed
    /// </summary>
    public Slider MaximumSpeedSliderUI;

    /// <summary>
    /// Text UI element displaying the maximum steering force.
    /// </summary>
    public Text MaximumSteeringForceTextUI;

    /// <summary>
    /// Slider UI element displaying the maximum steering force.
    /// </summary>
    public Slider MaximumSteeringForceSliderUI;

    /// <summary>
    /// Display the current general settings.
    /// </summary>
    private void DisplayGeneralSettings(bool initialize = false)
    {
        MinimumSpeedTextUI.text = string.Format("Minimum speed ({0:0.00})", Settings.MinSpeed);
        MaximumSpeedTextUI.text = string.Format("Maximum speed ({0:0.00})", Settings.MaxSpeed);
        MaximumSteeringForceTextUI.text = string.Format("Max steering force ({0:0.00})", Settings.MaxSteerForce);

        if (initialize)
        {
            MinimumSpeedSliderUI.value = Settings.MinSpeed;
            MaximumSpeedSliderUI.value = Settings.MaxSpeed;
            MaximumSteeringForceSliderUI.value = Settings.MaxSteerForce;
        }
    }

    /// <summary>
    /// Updates the general settings.
    /// </summary>
    private void UpdateGeneralSettings()
    {
        Settings.MinSpeed = MinimumSpeedSliderUI.value;
        Settings.MaxSpeed = MaximumSpeedSliderUI.value;
        Settings.MaxSteerForce = MaximumSteeringForceSliderUI.value;
    }

    #endregion

}
