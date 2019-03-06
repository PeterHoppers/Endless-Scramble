using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("GoalCollection")]
public class GoalCollector
{
    [XmlArray("GoalTimes"), XmlArrayItem("GoalTime")]
    public GoalInfo[] goalTimes;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(GoalCollector));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static GoalCollector Load(string path)
    {
        var serializer = new XmlSerializer(typeof(GoalCollector));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as GoalCollector;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static GoalCollector LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(GoalCollector));
        return serializer.Deserialize(new StringReader(text)) as GoalCollector;
    }

    public double GetGoldTime(int levelNum)
    {
        GoalInfo goalInfo = GetGoalInfo(levelNum);
        return goalInfo.GoldTime;
    }

    public double GetSilverTime(int levelNum)
    {
        GoalInfo goalInfo = GetGoalInfo(levelNum);
        return goalInfo.SilverTime;
    }

    public double GetBronzeTime(int levelNum)
    {
        GoalInfo goalInfo = GetGoalInfo(levelNum);
        return goalInfo.BronzeTime;
    }

    GoalInfo GetGoalInfo(int levelNum)
    {
        foreach (GoalInfo goal in goalTimes)
        {
            if (goal.lvlNum == levelNum)
                return goal;
        }

        return null;
    }
}

public class GoalInfo
{
    [XmlAttribute("lvlNum")]
    public int lvlNum;
    public double GoldTime;
    public double SilverTime;
    public double BronzeTime;
}


