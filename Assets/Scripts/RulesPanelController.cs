using UnityEngine;
using UnityEngine.UI;

public class RulesPanelController : MonoBehaviour
{
    public GameObject rulesPanel;            // Panel to toggle
    public Image toggleButtonImage;          // Image component on the button
    public Sprite questionIcon;              // Icon to show when panel is closed
    public Sprite closeIcon;                 // Icon to show when panel is open

    private bool isPanelVisible = false;

    public void ToggleRulesPanel()
    {
        isPanelVisible = !isPanelVisible;

        // Show or hide the panel
        rulesPanel.SetActive(isPanelVisible);

        // Update the button icon
        if (isPanelVisible)
        {
            toggleButtonImage.sprite = closeIcon;
        }
        else
        {
            toggleButtonImage.sprite = questionIcon;
        }
    }
}