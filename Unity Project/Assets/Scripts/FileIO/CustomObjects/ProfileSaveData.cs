using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using OnLooker.Framework;

[Serializable]
public class ProfileSaveData : CustomSaveData
{
    public string m_ProfileName;
    public int m_ProgressionLevel;

    public ProfileSaveData() : base()
    {
        m_ProfileName = string.Empty;
        m_ProgressionLevel = 0;
    }

    public ProfileSaveData(string aSaveName, string aProfileName, int aProgressionLevel)
    {
        m_Name = aSaveName;
        m_ProfileName = aProfileName;
        m_ProgressionLevel = aProgressionLevel;
    }

    public ProfileSaveData(Profile aProfile)
    {
        if (aProfile != null)
        {
            m_ProfileName = aProfile.name;
            m_ProgressionLevel = aProfile.progressionLevel;
        }
    }

    public ProfileSaveData(SerializationInfo aInfo, StreamingContext aContext) : base()
    {
        m_Name = (string)aInfo.GetValue("Name", typeof(string));
        m_ProfileName = (string)aInfo.GetValue("ProfileName", typeof(string));
        m_ProgressionLevel = (int)aInfo.GetValue("ProgressionLevel", typeof(int));
    }

    public override void GetObjectData(SerializationInfo aInfo, StreamingContext aContext)
    {
        base.GetObjectData(aInfo, aContext);
        aInfo.AddValue("ProfileName", m_ProfileName);
        aInfo.AddValue("ProgressionLevel", m_ProgressionLevel);
    }
}
