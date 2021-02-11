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

        // Set and display cohesion settings
        DisplayCohesionSettings(true);

        // Set and display separation settings
        DisplaySeparationSettings(true);

        // Set and display alignment settings
        DisplayAlignmentSettings(true);
    }

    /// <summary>
    /// Continuously updates the settings.
    /// </summary>
    private void Update()
    {
        // General settings
        UpdateGeneralSettings();
        DisplayGeneralSettings();

        // Cohesion settings
        UpdateCohesionSettings();
        DisplayCohesionSettings();

        // Separation settings
        UpdateSeparationSettings();
        DisplaySeparationSettings();

        // Alignment settings
        UpdateAlignmentSettings();
        DisplayAlignmentSettings();
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
    /// Slider UI element displaying the minimum speed.
    /// </summary>
    public Slider MinimumSpeedSliderUI;

    /// <summary>
    /// Text UI element displaying the maximum speed.
    /// </summary>
    public Text MaximumSpeedTextUI;

    /// <summary>
    /// Slider UI element displaying the minimum speed.
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

    #region Cohesion Settings

    [Header("Cohesion")]

    /// <summary>
    /// Text UI element displaying the cohision force weight.
    /// </summary>
    public Text CohesionForceWeightTextUI;

    /// <summary>
    /// Slider UI element displaying the cohision force weight.
    /// </summary>
    public Slider CohesionForceWeightSliderUI;

    /// <summary>
    /// Text UI element displaying the cohision radius.
    /// </summary>
    public Text CohesionRadiusTextUI;

    /// <summary>
    /// Slider UI element displaying the cohision radius.
    /// </summary>
    public Slider CohesionRadiusSliderUI;

    /// <summary>
    /// Toggle UI element displaying the center status.
    /// </summary>
    public Toggle CohesionUseCenterToggleUI;

    /// <summary>
    /// Display the current cohesion settings.
    /// </summary>
    private void DisplayCohesionSettings(bool initialize = false)
    {
        CohesionForceWeightTextUI.text = string.Format("Force weight ({0:0.00})", Settings.CohesionForceWeight);
        CohesionRadiusTextUI.text = string.Format("Radius ({0:0.00})", Settings.CohesionRadiusThreshold);

        if (initialize)
        {
            CohesionForceWeightSliderUI.value = Settings.CohesionForceWeight;
            CohesionRadiusSliderUI.value = Settings.CohesionRadiusThreshold;
            CohesionUseCenterToggleUI.isOn = Settings.UseCenterForCohesion;
        }
    }

    /// <summary>
    /// Updates the cohesion settings.
    /// </summary>
    private void UpdateCohesionSettings()
    {
        Settings.CohesionForceWeight = CohesionForceWeightSliderUI.value;
        Settings.CohesionRadiusThreshold = CohesionRadiusSliderUI.value;
        Settings.UseCenterForCohesion = CohesionUseCenterToggleUI.isOn;
        Settings.IsCenterVisible = CohesionUseCenterToggleUI.isOn;
    }

    #endregion

    #region Separation Settings

    [Header("Separation")]

    /// <summary>
    /// Text UI element displaying the separation force weight.
    /// </summary>
    public Text SeparationForceWeightTextUI;

    /// <summary>
    /// Slider UI element displaying the separation force weight.
    /// </summary>
    public Slider SeparationForceWeightSliderUI;

    /// <summary>
    /// Text UI element displaying the separation radius.
    /// </summary>
    public Text SeparationRadiusTextUI;

    /// <summary>
    /// Slider UI element displaying the separation radius.
    /// </summary>
    public Slider SeparationRadiusSliderUI;

    /// <summary>
    /// Display the current separation settings.
    /// </summary>
    private void DisplaySeparationSettings(bool initialize = false)
    {
        SeparationForceWeightTextUI.text = string.Format("Force weight ({0:0.00})", Settings.SeperationForceWeight);
        SeparationRadiusTextUI.text = string.Format("Radius ({0:0.00})", Settings.SeperationRadiusThreshold);

        if (initialize)
        {
            SeparationForceWeightSliderUI.value = Settings.SeperationForceWeight;
            SeparationRadiusSliderUI.value = Settings.SeperationRadiusThreshold;
        }
    }

    /// <summary>
    /// Updates the separation settings.
    /// </summary>
    private void UpdateSeparationSettings()
    {
        Settings.SeperationForceWeight = SeparationForceWeightSliderUI.value;
        Settings.SeperationRadiusThreshold = SeparationRadiusSliderUI.value;
    }

    #endregion

    #region Alignment Settings

    [Header("Alignment")]

    /// <summary>
    /// Text UI element displaying the alignment force weight.
    /// </summary>
    public Text AlignmentForceWeightTextUI;

    /// <summary>
    /// Slider UI element displaying the alignment force weight.
    /// </summary>
    public Slider AlignmentForceWeightSliderUI;

    /// <summary>
    /// Text UI element displaying the alignment radius.
    /// </summary>
    public Text AlignmentRadiusTextUI;

    /// <summary>
    /// Slider UI element displaying the alignment radius.
    /// </summary>
    public Slider AlignmentRadiusSliderUI;

    /// <summary>
    /// Display the current alignment settings.
    /// </summary>
    private void DisplayAlignmentSettings(bool initialize = false)
    {
        AlignmentForceWeightTextUI.text = string.Format("Force weight ({0:0.00})", Settings.AlignmentForceWeight);
        AlignmentRadiusTextUI.text = string.Format("Radius ({0:0.00})", Settings.AlignmentRadiusThreshold);

        if (initialize)
        {
            AlignmentForceWeightSliderUI.value = Settings.AlignmentForceWeight;
            AlignmentRadiusSliderUI.value = Settings.AlignmentRadiusThreshold;
        }
    }

    /// <summary>
    /// Updates the alignment settings.
    /// </summary>
    private void UpdateAlignmentSettings()
    {
        Settings.AlignmentForceWeight = AlignmentForceWeightSliderUI.value;
        Settings.AlignmentRadiusThreshold = AlignmentRadiusSliderUI.value;
    }

    #endregion

}
