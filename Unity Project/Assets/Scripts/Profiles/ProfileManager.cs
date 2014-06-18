using UnityEngine;
using System.Collections.Generic;
using OnLooker.Framework;

public class ProfileManager
{
    static private ProfileManager s_Instance = null;
    
    static public ProfileManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new ProfileManager();
            }
            return s_Instance;
        }
    }
    private ProfileManager()
    {
        m_Profiles = new Profile[MAX_PROFILES];
        m_File = new FileData("Profiles.bin");
    }
    private const int MAX_PROFILES = 3;
    private Profile[] m_Profiles = null;
    private Profile m_CurrentProfile = null;
    private FileData m_File;


    public void setProfle(int aIndex, Profile aProfile)
    {
        m_Profiles[aIndex] = aProfile;
    }
    public void removeProfile(string aName)
    {
        for (int i = 0; i < m_Profiles.Length; i++)
        {
            if (m_Profiles[i].name == aName)
            {
                m_Profiles[i] = null;
            }
        }
    }
    public void removeProfile(int aIndex)
    {
        if (aIndex >= 0 && aIndex < m_Profiles.Length)
        {
            m_Profiles[aIndex] = null;
        }
    }
    public Profile getProfile(string aName)
    {
        for (int i = 0; i < m_Profiles.Length; i++)
        {
            if (m_Profiles[i].name == aName)
            {
                return m_Profiles[i];
            }
        }
        return null;
    }
    public Profile getProfile(int aIndex)
    {
        if (aIndex >= 0 && aIndex < m_Profiles.Length)
        {
            return m_Profiles[aIndex];
        }
        return null;
    }
    public void renameProfile(string aName, string aNewName)
    {
        for (int i = 0; i < m_Profiles.Length; i++)
        {
            if (m_Profiles[i].name == aName)
            {
                m_Profiles[i].name = aNewName;
            }
        }
    }


    public void setCurrentProfile(int aIndex)
    {
        if (aIndex >= 0 && aIndex <= m_Profiles.Length)
        {
            m_CurrentProfile = m_Profiles[aIndex];
        }
    }

    public void saveProfiles()
    {
        m_File.clear();
        for (int i = 0; i < m_Profiles.Length; i++)
        {
            if (m_Profiles[i] != null)
            {
                m_File.add(new ProfileSaveData(m_Profiles[i]));
            }
        }
        m_File.save();
    }
    public void loadProfiles()
    {
        for (int i = 0; i < m_Profiles.Length; i++)
        {
            m_Profiles[i] = null;
        }
        m_File.load();
        ProfileSaveData[] profiles = m_File.get<ProfileSaveData>();
        for (int i = 0; i < m_Profiles.Length && i < profiles.Length; i++)
        {
            m_Profiles[i] = new Profile(profiles[i]);
        }
    }

    


    
}
