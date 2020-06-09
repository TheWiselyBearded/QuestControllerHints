using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class HintManager : MonoBehaviour {

    [SerializeField]
    public ControllerHintUtilities.ButtonTip[] buttonTips;
    public Material HighlightButton, OriginalMaterial;
    public bool tutorialMode;       // Set to true by calling the ActivateHints() method.
    
    private int tutorialModeIndex;
    private ControllerHintUtilities.ButtonNames currentTutorialButton;
    private List<ControllerHintUtilities.RequestButtonTip> requestedButtons;
    // When assigned to this list, hints automatically get activated.
    public List<ControllerHintUtilities.RequestButtonTip> RequestedButtons {
        get { return requestedButtons; }
        set { requestedButtons = value; }
    }

    // TODO: Explore solution where buttons are assigned dynamically.

    public void Awake() {
        tutorialMode = false;
        tutorialModeIndex = 0;
        for (int i = 0; i < buttonTips.Length; i++) {
            buttonTips[i].b_renderer = buttonTips[i].b_gameObj.GetComponent<Renderer>();
            buttonTips[i].b_originalMaterial = buttonTips[i].b_renderer.material;
            ToggleButtonTip(buttonTips[i], false);
        }
    }

    /// <summary>
    /// Toggle Button objects correctly (GameObject, Material)
    /// </summary>
    /// <param name="bTip"></param>
    /// <param name="state"></param>
    public void ToggleButtonTip(ControllerHintUtilities.ButtonTip bTip, bool state) {
        bTip.b_connector.gameObject.SetActive(state);
        bTip.b_renderer.material = state ? HighlightButton : bTip.b_originalMaterial;
    }

    /// <summary>
    /// User is holding down button to view all controls.
    /// </summary>
    public void ViewHints() {
        if (!tutorialMode) {    // In case user accidently presses 'activate hints' during tutorial.
            foreach (ControllerHintUtilities.ButtonTip bTip in buttonTips) {
                foreach (ControllerHintUtilities.RequestButtonTip hint in requestedButtons) {
                    if (bTip.b_name.Equals(hint.b_name)) {
                        ToggleButtonTip(bTip, true);
                        bTip.b_gameObj.GetComponent<Renderer>().material = HighlightButton;
                    }
                }
            }
        }
    }

    /// <summary>
    /// User released Hint reminder button. So, turn off all controls.
    /// </summary>
    public void TurnOffHints() {
        if (!tutorialMode) {    // In case user accidently presses 'activate hints' during tutorial.
            foreach (ControllerHintUtilities.ButtonTip bTip in buttonTips) {
                ToggleButtonTip(bTip, false);
                bTip.b_gameObj.GetComponent<Renderer>().material = bTip.b_originalMaterial;
                if (bTip.b_renderer.material == null)
                    bTip.b_renderer.material = OriginalMaterial;
            }
        }
    }


    /// <summary>
    /// ButtonController.Pressed (UnityEvent) will invoke this method.
    /// Finds button the user needs to press and transitions to next hint (if any).
    /// </summary>
    /// <param name="btnObject"></param>
    public void CheckButtonTutorial(UnityEngine.Object btnObject) {
        if (!tutorialMode)
            return;
        string selectedObjName = btnObject.name;
        foreach (string btnName in Enum.GetNames(typeof(ControllerHintUtilities.ButtonNames))) {
            Enum.Parse(typeof(ControllerHintUtilities.ButtonNames), btnName);
            //Debug.Log(Enum.Parse(typeof(BSI_Utilities.ButtonNames),btnName));
            //Debug.Log("Button name string" + btnName);
            if (btnName.Equals(selectedObjName) &&
                Enum.GetName(typeof(ControllerHintUtilities.ButtonNames),currentTutorialButton) == selectedObjName)
                {
                Debug.Log("Next Hint");
                if (++tutorialModeIndex < requestedButtons.Count)
                    ActivateButtonTutorial(requestedButtons[tutorialModeIndex]);
                else {
                    tutorialMode = false;
                    TurnOffHints();
                }
            }
        }
    }

    /// <summary>
    /// Toggle on Hint for specific button.
    /// Toggle off Hints for other buttons.
    /// </summary>
    /// <param name="reqBtn"></param>
    public void ActivateButtonTutorial(ControllerHintUtilities.RequestButtonTip reqBtn) {
        if (!tutorialMode)
            return;
        for (int i = 0; i < buttonTips.Length; i++) {
            if (buttonTips[i].b_name.Equals(reqBtn.b_name)) {
                buttonTips[i].b_connector.ui_text.text = reqBtn.b_text;
                ToggleButtonTip(buttonTips[i], true);
                currentTutorialButton = buttonTips[i].b_name;
            }
            else
                ToggleButtonTip(buttonTips[i], false);
        }
    }

    /// <summary>
    /// Turns all buttons off prior to highlighting buttons
    /// Responsible for re-assigning hint references per step.
    /// </summary>
    public void ActivateHints(List<ControllerHintUtilities.RequestButtonTip> requested,
                                float duration = 3.0f,
                                bool tutorial = false) {
        requestedButtons = requested;
        tutorialMode = tutorial;
        if (tutorialMode) {
            ActivateButtonTutorial(requested[0]);
        } else {    // TODO: Look into design pattern for reconstructing object and reduce loops.
            foreach (ControllerHintUtilities.ButtonTip bTip in buttonTips) {
                ToggleButtonTip(bTip, false);
                foreach (ControllerHintUtilities.RequestButtonTip hint in requestedButtons) {
                    if (bTip.b_name.Equals(hint.b_name)) {
                        ToggleButtonTip(bTip, true);
                        bTip.b_connector.ui_text.text = hint.b_text;
                        //Debug.Log("Activating:\t" + bTip.b_name + " with text:\t" + bTip.b_connector.ui_text.text); ;
                        StartCoroutine(PopupHint(bTip, duration));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Coroutine responsible for changing material of buttons to 'highlight' for 
    /// defined time period.
    /// </summary>
    /// <param name="buttonTip"></param>
    /// <returns></returns>
    private IEnumerator PopupHint(ControllerHintUtilities.ButtonTip buttonTip, float secondsActive) {
        ToggleButtonTip(buttonTip, true);
        yield return new WaitForSeconds(secondsActive);
        ToggleButtonTip(buttonTip, false);
        // Safety check
        if (buttonTip.b_renderer.material == null)
            buttonTip.b_renderer.material = OriginalMaterial;
    }

}
