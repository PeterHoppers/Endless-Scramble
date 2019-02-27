using UnityEngine;

public class ScrambleText {

    public static int degreeOfScramble = 4; //determines how much scrambled the text is

    public static string ScramblingText(string inputString)
    {
        string tempString = "";

        tempString = inputString + " "; // save the original string, edit a copy

        string scrambledString = ""; //the final String
        string tempWord = ""; //funnels everything into a temp string
        int currentIndex = 0; //counts where we are at
        int next = 0; //determines where the next Space is

        while (currentIndex < tempString.Length - 1)
        {
            bool hasPuncuation = false;
            string puncuation = " ";

            //grabs where the space of the string is
            next = tempString.IndexOf(" ", currentIndex);
            //grabs the word by using at the characters until the next space
            tempWord = tempString.Substring(currentIndex, (next - currentIndex)); 

            //checks if the last char is puncuation, and, if so, saves that information for later
            char lastChar = tempWord[tempWord.Length - 1];

            if (lastChar.Equals('.') || lastChar.Equals('!')
                        || lastChar.Equals('?') || lastChar.Equals(','))
            {
                hasPuncuation = true;
                puncuation = lastChar.ToString();
                tempWord = tempWord.Substring(0, tempWord.Length - 1);
            }

            //sends the grabbed word to be shrunk and scrambled
            string scrambleWord = SplitNScrambleWord(tempWord);

            if (currentIndex == 0)//meaning that it is the first word
                scrambledString += scrambleWord;
            else //if not the first word
                scrambledString += " " + scrambleWord; // add a space inbetween words

            currentIndex = next + 1; //moves the currentIndex to the index after the space

            if (hasPuncuation) //if there was some puncuation
                scrambledString += puncuation;
        }

        return scrambledString; //return the scrambled version of the
    }

    static string SplitNScrambleWord(string tempWord)
    {
        string scrambledWord = "";

        if (tempWord.Length < 9) //if it is short, it can be scrambled
        {
            scrambledWord += ScramblingWord(tempWord);
        }
        else // breaks down words greater than 9 into smaller words
        {
            //split the world in half.
            int halfNum = tempWord.Length / 2;
            //recursively call the method by splitting the string into two until it is short enough
            scrambledWord += SplitNScrambleWord(tempWord.Substring(0, halfNum));
            scrambledWord += SplitNScrambleWord(tempWord.Substring(halfNum, (tempWord.Length - halfNum)));
        }

        return scrambledWord;
    }

    static string ScramblingWord(string tempWord)
    {
        string scrambleWord = "";

        if (tempWord.Length <= 3) //no need to scramble if it is short
            scrambleWord = tempWord;
        else if (tempWord.Length < 9) //scrambling algorithm needs the word to be shortened to less than 9
        {
            //get a collection of indexs that are scrambled
            string chars = GetIndexofChars(tempWord.Length, degreeOfScramble); 

            for (int index = 0; index < tempWord.Length; index++)
            {
                //grab the letters based upon the scrambled list of indexs
                string inChar = tempWord[int.Parse(chars.Substring(index, 1))].ToString(); 
                scrambleWord += inChar; //reconstruct the word one char at a time.
            }
        }
        else
            Debug.LogError("Tried to scramble a word that was greater than 9 characters.");

        return scrambleWord;
    }

    //returns a scrambled list of ints converted in string, based upon the length and requested scramble-ness
    static string GetIndexofChars(int index, int amtOfScrambleness) 
    {
        switch (index)
        {
            case 4:
                if (amtOfScrambleness < 6)
                    return "0123";
                else
                    return "0213";
            case 5:
                if (amtOfScrambleness <= 1)
                    return "01234";
                else if (amtOfScrambleness <= 3)
                    return "01324";
                else if (amtOfScrambleness <= 5)
                    return "02134";
                else if (amtOfScrambleness <= 7)
                    return "02314";
                else if (amtOfScrambleness <= 9)
                    return "03124";
                else
                    return "03214";
            case 6:
                if (amtOfScrambleness < 1)
                    return "012345";
                else if (amtOfScrambleness <= 1)
                    return "012435";
                else if (amtOfScrambleness <= 2)
                    return "014235";
                else if (amtOfScrambleness <= 3)
                    return "014325";
                else if (amtOfScrambleness <= 4)
                    return "021435";
                else if (amtOfScrambleness <= 5)
                    return "032145";
                else if (amtOfScrambleness <= 6)
                    return "032415";
                else if (amtOfScrambleness <= 7)
                    return "023415";
                else if (amtOfScrambleness <= 8)
                    return "024315";
                else if (amtOfScrambleness <= 9)
                    return "041325";
                else
                    return "031425";
            case 7:
                if (amtOfScrambleness < 1)
                    return "0123456";
                else if (amtOfScrambleness <= 1)
                    return "0124356";
                else if (amtOfScrambleness <= 2)
                    return "0142356";
                else if (amtOfScrambleness <= 3)
                    return "0143256";
                else if (amtOfScrambleness <= 4)
                    return "0214356";
                else if (amtOfScrambleness <= 5)
                    return "0321546";
                else if (amtOfScrambleness <= 6)
                    return "0324516";
                else if (amtOfScrambleness <= 7)
                    return "0235416";
                else if (amtOfScrambleness <= 8)
                    return "0245316";
                else if (amtOfScrambleness <= 9)
                    return "0415326";
                else
                    return "0531426";
            case 8:
                if (amtOfScrambleness < 1)
                    return "01234567";
                else if (amtOfScrambleness <= 1)
                    return "01243567";
                else if (amtOfScrambleness <= 2)
                    return "01423567";
                else if (amtOfScrambleness <= 3)
                    return "01432567";
                else if (amtOfScrambleness <= 4)
                    return "02154367";
                else if (amtOfScrambleness <= 5)
                    return "03215647";
                else if (amtOfScrambleness <= 6)
                    return "03245617";
                else if (amtOfScrambleness <= 7)
                    return "02356417";
                else if (amtOfScrambleness <= 8)
                    return "02465317";
                else if (amtOfScrambleness <= 9)
                    return "04153267";
                else
                    return "06531427";
            case 9:
                if (amtOfScrambleness < 1)
                    return "012345678";
                else if (amtOfScrambleness <= 1)
                    return "012435678";
                else if (amtOfScrambleness <= 2)
                    return "014235678";
                else if (amtOfScrambleness <= 3)
                    return "014325678";
                else if (amtOfScrambleness <= 4)
                    return "021543768";
                else if (amtOfScrambleness <= 5)
                    return "032156478";
                else if (amtOfScrambleness <= 6)
                    return "032456718";
                else if (amtOfScrambleness <= 7)
                    return "023576418";
                else if (amtOfScrambleness <= 8)
                    return "024675318";
                else if (amtOfScrambleness <= 9)
                    return "041753268";
                else
                    return "076531428";
        }

        return " ";
    }
}