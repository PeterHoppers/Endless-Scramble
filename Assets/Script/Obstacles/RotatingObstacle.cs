using UnityEngine;
using System.Collections;

public class RotatingObstacle : MonoBehaviour {

    public float speed;
    RectTransform rectTransfrom;

	// Use this for initialization
	void Start ()
    {
        rectTransfrom = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.rectTransfrom.Rotate(Vector3.forward * Time.deltaTime * speed);
	}
}
