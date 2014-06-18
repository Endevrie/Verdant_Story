using UnityEngine;
using System.Collections;

public class Profile
{
    private string m_Name;
    private int m_ProgressionLevel;

    public Profile()
    {
        m_Name = string.Empty;
        m_ProgressionLevel = 0;
    }

    public Profile(string aName)
    {
        m_Name = aName;
        m_ProgressionLevel = 0;
    }
    public Profile(string aName, int aProgressionLevel)
    {
        m_Name = aName;
        m_ProgressionLevel = aProgressionLevel;
    }
    public Profile(ProfileSaveData aProfile)
    {
        if (aProfile != null)
        {
            m_Name = aProfile.name;
            m_ProgressionLevel = aProfile.m_ProgressionLevel;
        }
    }
    public string name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }
    public int progressionLevel
    {
        get { return m_ProgressionLevel; }
        set { m_ProgressionLevel = value; }
    }

}
