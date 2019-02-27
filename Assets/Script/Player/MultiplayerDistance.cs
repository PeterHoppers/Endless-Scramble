using UnityEngine;
using System.Collections;

public class MultiplayerDistance : MonoBehaviour {

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    bool usingP1;
    bool usingP2;
    bool usingP3;
    bool usingP4;

    //------Distance Values Nums--------
    int isP1P2;
    int isP1P3;
    int isP1P4;
    int isP2P3;
    int isP2P4;
    int isP3P4;

    public int chasingDistance = 6;

    KeyCode lastKey;

    //MultiplayerActivtion multiAct;

    // Use this for initialization
    void Start ()
    {
        //multiAct = Camera.main.GetComponent<MultiplayerActivtion>();
        // = multiAct.player1;
        //player2 = multiAct.player2;
        //player3 = multiAct.player3;
        //player4 = multiAct.player4;
    }

    public void GetBools(bool getP1, bool getP2, bool getP3, bool getP4)
    {
        usingP1 = getP1;
        usingP2 = getP2;
        usingP3 = getP3;
        usingP4 = getP4;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingP1 && usingP2)
            isP1P2 = CalcDistance(player1, player2, isP1P2);

        if (usingP1 && usingP3)
            isP1P3 = CalcDistance(player1, player3, isP1P3);

        if (usingP1 && usingP4)
            isP1P4 = CalcDistance(player1, player4, isP1P4);

        if (usingP2 && usingP3)
            isP2P3 = CalcDistance(player2, player3, isP2P3);

        if (usingP2 && usingP4)
            isP2P4 = CalcDistance(player2, player4, isP2P4);

        if (usingP3 && usingP4)
            isP3P4 = CalcDistance(player3, player4, isP3P4);
    }

    int CalcDistance(GameObject firstPlayer, GameObject secondPlayer, int battleNum)
    {
        int distanceTotal = 0;
        GameObject chaser = null;

        distanceTotal = (int)Mathf.Abs(firstPlayer.transform.position.x - secondPlayer.transform.position.x);

        distanceTotal += (int)Mathf.Abs(firstPlayer.transform.position.y - secondPlayer.transform.position.y);

        if (distanceTotal < chasingDistance)
        {
            if (battleNum == 0)
            {
                chaser = DetectWhoseKey(lastKey);

                if (chaser == firstPlayer) //1st Player is Chasing
                {
                    //-----------FirstPlayer Changes------------
                    if (firstPlayer.GetComponent<PlayerStats>().isFleeing)
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().bothMaterial;
                    else
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().chasingMaterial;

                    firstPlayer.GetComponent<PlayerStats>().isChasing = true;
                    firstPlayer.GetComponent<PlayerStats>().amtChasing++;

                    //-----------SecondPlayer Changes------------
                    if (secondPlayer.GetComponent<PlayerStats>().isChasing)
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().bothMaterial;
                    else
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().fleeingMaterial;

                    secondPlayer.GetComponent<PlayerStats>().isFleeing = true;
                    secondPlayer.GetComponent<PlayerStats>().amtFleeing++;
                    return 1;
                }
                else if (chaser == secondPlayer)  //2nd Player is Chasing
                {
                    //-----------FirstPlayer Changes------------
                    if (firstPlayer.GetComponent<PlayerStats>().isChasing)
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().bothMaterial;
                    else
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().fleeingMaterial;

                    firstPlayer.GetComponent<PlayerStats>().isFleeing = true;
                    firstPlayer.GetComponent<PlayerStats>().amtFleeing++;

                    //-----------SecondPlayer Changes------------
                    if (secondPlayer.GetComponent<PlayerStats>().isFleeing)
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().bothMaterial;
                    else
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().chasingMaterial;

                    secondPlayer.GetComponent<PlayerStats>().isChasing = true;
                    secondPlayer.GetComponent<PlayerStats>().amtChasing++;
                    return 2;
                }
                else
                {
                    Debug.LogError("The one who moved into range was neither");
                    return 0;
                }
            }
            else
                return battleNum;
        }
        else
        {
            if (battleNum == 1)
            {
                firstPlayer.GetComponent<PlayerStats>().amtChasing--;
                secondPlayer.GetComponent<PlayerStats>().amtFleeing--;

                //-----------FirstPlayer Changes------------
                if (firstPlayer.GetComponent<PlayerStats>().amtChasing <= 0)
                {
                    if (firstPlayer.GetComponent<PlayerStats>().isFleeing)
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().fleeingMaterial;
                    else
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().normalMaterial;

                    firstPlayer.GetComponent<PlayerStats>().isChasing = false;
                }

                //-----------SecondPlayer Changes------------
                if (secondPlayer.GetComponent<PlayerStats>().amtFleeing <= 0)
                {
                    if (secondPlayer.GetComponent<PlayerStats>().isChasing)
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().chasingMaterial;
                    else
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().normalMaterial;

                    secondPlayer.GetComponent<PlayerStats>().isFleeing = false;
                }
            }
            else if (battleNum == 2)
            {
                firstPlayer.GetComponent<PlayerStats>().amtFleeing--;
                secondPlayer.GetComponent<PlayerStats>().amtChasing--;

                //-----------FirstPlayer Changes------------
                if (firstPlayer.GetComponent<PlayerStats>().amtFleeing <= 0)
                {
                    if (firstPlayer.GetComponent<PlayerStats>().isChasing)
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().chasingMaterial;
                    else
                        firstPlayer.GetComponent<MeshRenderer>().material = firstPlayer.GetComponent<PlayerStats>().normalMaterial;

                    firstPlayer.GetComponent<PlayerStats>().isFleeing = false;
                }
                //-----------SecondPlayer Changes------------
                if (secondPlayer.GetComponent<PlayerStats>().amtChasing <= 0)
                {
                    if (secondPlayer.GetComponent<PlayerStats>().isFleeing)
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().fleeingMaterial;
                    else
                        secondPlayer.GetComponent<MeshRenderer>().material = secondPlayer.GetComponent<PlayerStats>().normalMaterial;

                    secondPlayer.GetComponent<PlayerStats>().isChasing = false;
                }
            }

            return 0;
        }


    }

    GameObject DetectWhoseKey(KeyCode lastPressed)
    {
        if (lastPressed == KeyCode.W || lastPressed == KeyCode.S || lastPressed == KeyCode.A || lastPressed == KeyCode.D)
            return player1;
        else if (lastPressed == KeyCode.U || lastPressed == KeyCode.J || lastPressed == KeyCode.H || lastPressed == KeyCode.K)
            return player2;
        else if (lastPressed == KeyCode.UpArrow || lastPressed == KeyCode.DownArrow || lastPressed == KeyCode.LeftArrow || lastPressed == KeyCode.RightArrow)
            return player3;
        else if (lastPressed == KeyCode.Keypad8 || lastPressed == KeyCode.Keypad5 || lastPressed == KeyCode.Keypad4 || lastPressed == KeyCode.Keypad6)
            return player4;
        else
            return null;
    }

    void OnGUI() //detects last key
    {
        Event e = Event.current;

        if (e.isKey)
        {
            if (e.keyCode != KeyCode.None)
            {
                lastKey = e.keyCode;
            }
        }

    }
}
