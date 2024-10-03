using UnityEngine;
using UnityEngine.UI;

public class Dashboard : MonoBehaviour
{
    // Inputs
    public uint torsionInput = 0;
    public uint frequencyInput = 0;
    public float speedInput = 0;
    public uint distance = 0;

    // UI Elements
    public Image torsionImage;
    public Image frequencyImage;
    public Image speedImage;

    // Limits
    public float maxTorsion = 150f;
    public float maxFrequency = 60f;
    public float maxSpeed = 20f;

    // Cached References
    private Engine engine;
    private RoundController roundController;
    private AvatarController avatarController;

    // Dropdowns and Values
    private Dropdown dropdownRound2;
    private Dropdown dropdownRound3;
    private Dropdown dropdownRound4;
    private Dropdown dropdownRound5;

    public int dropdownValue2;
    public int dropdownValue3;
    public int dropdownValue4;
    public int dropdownValue5;

    void Start()
    {
        // Cache references
        engine = FindObjectOfType<Engine>();
        roundController = FindObjectOfType<RoundController>();
        avatarController = FindObjectOfType<AvatarController>();

        // Cache dropdowns
        CacheDropdowns();
    }

    void Update()
    {
        UpdateDropdownValues();
    }

    private void CacheDropdowns()
    {
        dropdownRound2 = GetDropdownByTag("Round2Config");
        dropdownRound3 = GetDropdownByTag("Round3Config");
        dropdownRound4 = GetDropdownByTag("Round4Config");
        dropdownRound5 = GetDropdownByTag("Round5Config");

        // Log any missing dropdowns
        if (dropdownRound2 == null || dropdownRound3 == null || dropdownRound4 == null || dropdownRound5 == null)
        {
            Debug.LogError("One or more dropdown objects are missing!");
        }
    }

    private Dropdown GetDropdownByTag(string tag)
    {
        GameObject dropdownObject = GameObject.FindGameObjectWithTag(tag);
        return dropdownObject != null ? dropdownObject.GetComponent<Dropdown>() : null;
    }

    private void UpdateDropdownValues()
    {
        if (dropdownRound2 != null) dropdownValue2 = dropdownRound2.value;
        if (dropdownRound3 != null) dropdownValue3 = dropdownRound3.value;
        if (dropdownRound4 != null) dropdownValue4 = dropdownRound4.value;
        if (dropdownRound5 != null) dropdownValue5 = dropdownRound5.value;
    }

    public void UpdateAvtarBar(PlayerInputStruct recordedInputs)
    {
        int currentRound = engine.currentRound;

        // Convert inputs into UI percentages
        float torsionValue = (float)recordedInputs.rowPowerInput / maxTorsion;
        float frequencyValue = (float)recordedInputs.rowFrequencyInput / maxFrequency;
        float speedValue = avatarController.avatarSpeed / maxSpeed;

        // Apply round-specific adjustments
        ApplyRoundAdjustments(ref torsionValue, ref frequencyValue, currentRound);

        // Update UI elements
        UpdateUI(torsionValue, frequencyValue, speedValue);
    }

    private void ApplyRoundAdjustments(ref float torsionValue, ref float frequencyValue, int currentRound)
    {
        float powerFactor = roundController.PowerFactor;
        float frequencyFactor = roundController.FrequencyFactor;

        int dropdownValue = GetDropdownValueForRound(currentRound);

        if (dropdownValue == (int)RoundController.RoundType.OnlyFrequencyChanged)
        {
            frequencyValue *= frequencyFactor;
        }
        else if (dropdownValue == (int)RoundController.RoundType.OnlyPowerChanged)
        {
            torsionValue *= powerFactor;
        }
        else if (dropdownValue == (int)RoundController.RoundType.AllChanged)
        {
            torsionValue *= powerFactor;
            frequencyValue *= frequencyFactor;
        }
    }

    private int GetDropdownValueForRound(int round)
    {
        switch (round)
        {
            case 2: return dropdownValue2;
            case 3: return dropdownValue3;
            case 4: return dropdownValue4;
            case 5: return dropdownValue5;
            default: return -1;
        }
    }

    private void UpdateUI(float torsionValue, float frequencyValue, float speedValue)
    {
        torsionImage.fillAmount = torsionValue;
        frequencyImage.fillAmount = frequencyValue;
        speedImage.fillAmount = speedValue;
    }
}
