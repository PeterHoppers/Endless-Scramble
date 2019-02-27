using UnityEngine;
using System.Collections;

public class ActivatingCanvas : MonoBehaviour {

    public GameObject panel;

    public void SwitchingCanvas()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
