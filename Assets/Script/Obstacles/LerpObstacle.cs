using UnityEngine;
using System.Collections;

public class LerpObstacle : MonoBehaviour {

    public RectTransform startPoint;
    public RectTransform endPoint;
    RectTransform trans;

    public float switchDistance;

    Vector3 point1;
    Vector3 point2;

    public float speed;

    bool oneIsEnd;

	// Use this for initialization
	void Start () 
    {
        point1 =    startPoint.localPosition;
        point2 =    endPoint.localPosition;
        trans = GetComponent<RectTransform>();
        trans.localPosition = startPoint.localPosition;
        print("Start Point: " + startPoint.position + " vs. " + startPoint.localPosition);
	}
	
	// Update is called once per frame
	void Update () 
    {
        ///-----------Lerp is Here------------
        if (oneIsEnd)
        {
            trans.localPosition = Vector3.Lerp(trans.localPosition, point1, (speed * Time.deltaTime));
        }
        else
        {
            trans.localPosition = Vector3.Lerp(trans.localPosition, point2, (speed * Time.deltaTime));
        }

        //-----------Detects if it close---------
        if (Vector3.Distance(point1, trans.localPosition) < switchDistance)
        {
            oneIsEnd = false;
        }
        else if (Vector3.Distance(point2, trans.localPosition) < switchDistance)
        {
            oneIsEnd = true;
        }

	}
}
