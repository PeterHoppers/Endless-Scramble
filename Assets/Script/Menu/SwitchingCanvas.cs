using UnityEngine;
using System.Collections;

public class SwitchingCanvas : MonoBehaviour {

    public GameObject panelTurnOn;
    public GameObject panelTurnOff;

    public void FlippingCanvas()
    {
        panelTurnOn.SetActive(true);
        panelTurnOff.SetActive(false);
    }
}
