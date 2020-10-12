using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization;
using Vuforia;

public class MainScript : MonoBehaviour
{
    private const string defaultLayer = "Skeleton";
    private const string defaultSex = "Male";

    public Text text;

    private GameObject selectedObject;
    private GameObject selectedLayer;
    private GameObject selectedSex;

    public void Awake() {
        LocalizationManager.Read("localization.csv");
        switch (Application.systemLanguage) {
            default:
                LocalizationManager.Language = "English";
                break;
        }
        SelectSex(defaultSex);
    }

    public void SelectObject(GameObject select) {
        ResetSelectedObject();
        selectedObject = select;
        Material[] materials;
        materials = selectedObject.GetComponent<MeshRenderer>().materials;
        foreach (Material material in materials) {
            material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        }
        text.text = LocalizationManager.Localize(selectedObject.name);
    }

    public void ResetSelectedObject() {
        if (selectedObject != null) {
            Material[] materials = selectedObject.GetComponent<MeshRenderer>().materials;
            foreach (Material material in materials) {
                material.shader = Shader.Find("Diffuse");
            }
        }
    }

    public void SelectLayer(string layerName) {
        if(selectedLayer != null) {
            selectedLayer.SetActive(false);
        }
        foreach(Transform childTransform in selectedSex.GetComponentsInChildren<Transform>(true)) {
            if(childTransform.gameObject.name == layerName) {
                selectedLayer = childTransform.gameObject;
                selectedLayer.SetActive(true);
            }
        }
        ResetSelectedObject();
    }

    public void SelectSex(string sexName) {
        if(selectedSex != null) {
            selectedSex.SetActive(false);
        }
        foreach(Transform childTransform in FindObjectOfType<ImageTargetBehaviour>().GetComponentsInChildren<Transform>(true)) {
            if(childTransform.gameObject.name == sexName) {
                selectedSex = childTransform.gameObject;
                selectedSex.SetActive(true);
            }
        }
        SelectLayer(defaultLayer);
    }
}
