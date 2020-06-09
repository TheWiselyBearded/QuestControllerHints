using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Connector : MonoBehaviour {
    public GameObject root;
    public GameObject canvas;
    public TextMeshProUGUI ui_text;
    [SerializeField]
    public Material lineRendererMaterial;
    [SerializeField]
    public LineRenderer lineRenderer;


    void Awake() {
        lineRenderer = root.AddComponent<LineRenderer>();
        lineRenderer.material = lineRendererMaterial;// new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.00075f;
    }

    private void OnEnable() {
        lineRenderer.enabled = true;
    }

    private void OnDisable() {
        lineRenderer.enabled = false;
    }

    public void ToggleLineRenderer(bool state) {
        lineRenderer.enabled = state;  
    }

    // Update is called once per frame
    void Update() {
        //draw line starting at cube and go until you reach panel
        //Debug.Log(string.Format("Root:  {0},\tCanvas:", root.transform.position, canvas.transform.position));
        lineRenderer.SetPosition(0, root.transform.position);
        lineRenderer.SetPosition(1, canvas.transform.position);
    }
}
