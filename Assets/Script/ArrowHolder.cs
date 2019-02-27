using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHolder : MonoBehaviour
{
    [Tooltip("Relativeness to Player: 0 = top, 1 = down, 2 = left, 3 = right")]
    public byte index;
    RectTransform rect;

	// Use this for initialization
	public void UpdateArrows(Vector2 playerSize)
    {
        rect = GetComponent<RectTransform>();

        rect.sizeDelta = playerSize;

        switch(index)
        {
            case 0:
                rect.localPosition = new Vector3(0, playerSize.y, 0);
                break;
            case 1:
                rect.localPosition = new Vector3(0, -playerSize.y, 0);
                break;
            case 2:
                rect.localPosition = new Vector3(-playerSize.x, 0, 0);                
                break;
            case 3:
                rect.localPosition = new Vector3(playerSize.x, 0, 0);
                break;
        }
	}
}
