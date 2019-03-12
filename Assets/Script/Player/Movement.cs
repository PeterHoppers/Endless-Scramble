using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {
	
    //Movements attached to each of the keys
	Vector3 VectorW;
	Vector3 VectorS;
	Vector3 VectorA;
	Vector3 VectorD;

    Vector2 resetPosition;
    GameObject start;
    RectTransform rect;

    //The Image component for each of the keys
    public Image up;
	public Image down;
	public Image left;
	public Image right;

    //The actual sprite itself that is displayed for the keys
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;

    //Animator attached to each of the arrows
    Animator upAnim;
    Animator downAnim;
    Animator leftAnim;
    Animator rightAnim;

    int scramble = 0;

    void Start()
    {
        rect = GetComponent<RectTransform>();

        up.GetComponent<ArrowHolder>().UpdateArrows(rect.sizeDelta);
        left.GetComponent<ArrowHolder>().UpdateArrows(rect.sizeDelta);
        right.GetComponent<ArrowHolder>().UpdateArrows(rect.sizeDelta);
        down.GetComponent<ArrowHolder>().UpdateArrows(rect.sizeDelta);

        upAnim = transform.Find("Up").GetComponent<Animator>();
        leftAnim = transform.Find("Left").GetComponent<Animator>();
        rightAnim = transform.Find("Right").GetComponent<Animator>();
        downAnim = transform.Find("Down").GetComponent<Animator>();

        DeathManager.PlayerKilled += ResetPostion;

        ScrambleDirections();
    }

    public void ResetPostion()
    {
        if (start == null)
        {
            start = GameObject.FindGameObjectWithTag("Start");
            RectTransform startTrans = start.GetComponent<RectTransform>();
            resetPosition = startTrans.localPosition;
        }

        rect.localPosition = resetPosition;
    }

    // Update is called once per frame
    void Update () 
	{
        //--------------Detecting the Key and Moving The direction it is assigned--------------------------
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Translate(VectorW);
            ScrambleDirections();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(VectorS);
            ScrambleDirections();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(VectorA);
            ScrambleDirections();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(VectorD);
            ScrambleDirections();
        }		
	}

    //Set the movement of the buttons to a random direction and the sprite that matches that direction
    void ScrambleDirections()
    {
        //if it is the tutorial, don't scramble
        if (GlobalVars.isTutorial)
            scramble = 1;
        else
        {
            //to prevent it from looking awkward, this bit of code prevents the same number from getting pulled twice
            int check = scramble;
            do
            {
                scramble = Random.Range(1, 25);
            } while (check == scramble);

        }            

        //create and then parse a 4 digit string based upon
        //the random number that was generated
        string numInString = CreatePositionString(scramble);

        int upNum = int.Parse(numInString.Substring(0, 1));
        int downNum = int.Parse(numInString.Substring(1, 1));
        int leftNum = int.Parse(numInString.Substring(2, 1));
        int rightNum = int.Parse(numInString.Substring(3, 1));

        //depending on what number each direction got, it is 
        //then assigned a direction and sprite that corresponds
        //with that direction
        VectorW = SetDirection(upNum);
        up.sprite = SetSprite(upNum);

        VectorS = SetDirection(downNum);
        down.sprite = SetSprite(downNum);

        VectorA = SetDirection(leftNum);
        left.sprite = SetSprite(leftNum);

        VectorD = SetDirection(rightNum);
        right.sprite = SetSprite(rightNum);
    }

    //Based upon the input value, it returns a Vector2 that corresponds with a direction
    //allowing movement to be scrambled
	Vector2 SetDirection(int directionNum)
	{
		Vector3 temp;

        Vector2 ownSize = rect.sizeDelta;

		switch (directionNum)
		{
			case 1: //up
                temp = new Vector2(0, ownSize.y);                
				break;
			case 2: //down
                temp = new Vector2(0, -ownSize.y);
                break;
			case 3: //left
                temp = new Vector2(-ownSize.x, 0);
                break;
			case 4: //right
                temp = new Vector2(ownSize.x, 0);
                break;
			default:
				temp = Vector3.zero;
				Debug.LogError("ERRROROROROR");
				break;
		}

		return temp;
	}

    //Based upon the input value, it returns a Sprite that corresponds with a direction
    //allowing the Sprite to match up with the direction it is now set to
    Sprite SetSprite(int directionNum)
	{
		Sprite tempS;
		
		switch (directionNum)
		{
			case 1:
				tempS = upSprite;
				break;
			case 2:
				tempS = downSprite;
				break;
			case 3:
				tempS = leftSprite;
				break;
			case 4:
				tempS = rightSprite;
				break;
			default:
				tempS = upSprite;
				print ("ERRROROROROR");
				break;
		}
		
		return tempS;
	}

    //returns an random Arrow attached to the player
    //Used in the disabler to turn off one at random to make
    //movement even more of a hinderence
    public Image GetRandomArrow()
    {
        int ran = Random.Range(0, 4);

        switch (ran)
        {
            case 0:
                return up;
            case 1:
                return down;
            case 2:
                return left;
            case 3:
                return right;
            default:
                return null;
        }
    }


    //Returns a 4 digit string that has the number 1-4 not repeating
    //Could have been an algorithm. Hard coded to give control if needed
    string CreatePositionString(int randomNum)
	{
		string numInString = " ";
		switch (randomNum)
		{
			case 1:
				numInString = "1234";
				break;
			case 2:
				numInString = "1243";
				break;
			case 3:
				numInString = "1324";
				break;
			case 4:
				numInString = "1342";
				break;
			case 5:
				numInString = "1423";
				break;
			case 6:
				numInString = "1432";
				break;
			case 7:
				numInString = "2134";
				break;
			case 8:
				numInString = "2143";
				break;
			case 9:
				numInString = "2314";
				break;
			case 10:
				numInString = "2341";
				break;
			case 11:
				numInString = "2413";
				break;
			case 12:
				numInString = "2431";
				break;
			case 13:
				numInString = "3124";
				break;
			case 14:
				numInString = "3142";
				break;
			case 15:
				numInString = "3214";
				break;
			case 16:
				numInString = "3241";
				break;
			case 17:
				numInString = "3412";
				break;
			case 18:
				numInString = "3421";
				break;
			case 19:
				numInString = "4123";
				break;
			case 20:
				numInString = "4132";
				break;
			case 21:
				numInString = "4213";
				break;
			case 22:
				numInString = "4231";
				break;
			case 23:
				numInString = "4312";
				break;
			case 24:
				numInString = "4321";
				break;
		}
		
		return numInString;
	}

    //It is needed, whenever you subsribe to an event
    //that you break the connection whenever the object might get destory
    //whether in scene or between scenes
    private void OnDestroy()
    {
        DeathManager.PlayerKilled -= ResetPostion;
    }
}
