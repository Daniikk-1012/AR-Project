using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization;

public class RaycastModel : MonoBehaviour{
    public MainScript mainScript;

    void Update() {
        foreach(Touch touch in Input.touches) {
            if (touch.fingerId == 0) {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (touch.phase == TouchPhase.Began) {
                    RaycastHit raycastHit = new RaycastHit();
                    Physics.Raycast(ray, out raycastHit);
                    if (raycastHit.collider.gameObject == gameObject) {
                        mainScript.SelectObject(gameObject);
                    }
                }
            }
        }
    }
}
