using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialExample : MonoBehaviour {
    [Tooltip("Assign Left Hand Controls"), SerializeField]
    public List<ControllerHintUtilities.RequestButtonTip> leftHandTips;
    [Tooltip("Assign Right Hand Controls"), SerializeField]
    public List<ControllerHintUtilities.RequestButtonTip> rightHandTips;
    [Tooltip("Set tutorial mode for the left handed controller.")]
    public bool leftHandTutorialMode;
    [Tooltip("Set tutorial mode for the right handed controller.")]
    public bool rightHandTutorialMode;
    public HintManager leftHandHints, rightHandHints;
    // Start is called before the first frame update
    void Start()    {
        if (leftHandHints == null)
            leftHandHints = GameObject.Find("Left Quest Controller").GetComponent<HintManager>();
        if (rightHandHints == null)
            rightHandHints = GameObject.Find("Right Quest Controller").GetComponent<HintManager>();
        // ActivateHints() is the method we use to assign Hint instances to buttons.
        leftHandHints.ActivateHints(leftHandTips, 1.5f, leftHandTutorialMode); 
        rightHandHints.ActivateHints(rightHandTips, 1.5f, rightHandTutorialMode);
    }

    
}
