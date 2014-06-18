using UnityEngine;
using System;
using System.Collections.Generic;

namespace OnLooker
{
    public class GUIManager
    {
        #region SINGLETON
        static private GUIManager s_Instance = null;
        static public GUIManager instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new GUIManager();
                }
                return s_Instance;
            }
        }

        private GUIManager()
        {

        }
        #endregion


        private List<Layout> m_Layouts = new List<Layout>();

        public string newLayout(string aName)
        {
            if (layoutExists(aName) == true || aName == string.Empty)
            {
                return string.Empty;
            }
            m_Layouts.Add(new Layout(aName));
            return aName;
        }
        public string newLayout(string aName, Rect aBounds)
        {
            if (layoutExists(aName) == true || aName == string.Empty)
            {
                return string.Empty;
            }
            m_Layouts.Add(new Layout(aName, aBounds));
            return aName;
        }
        //Helper Method
        public bool layoutExists(string aName)
        {
            for (int i = 0; i < m_Layouts.Count; i++)
            {
                if (m_Layouts[i].name == aName)
                {
                    return true;
                }
            }
            return false;
        }
        //Helper Method
        Layout getLayout(string aName)
        {
            for (int i = 0; i < m_Layouts.Count; i++)
            {
                if (m_Layouts[i].name == aName)
                {
                    return m_Layouts[i];
                }
            }
            return null;
        }

        public Button addButton(string aLayoutName, string aText, string aName)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null && aName != string.Empty)
            {
                Debug.Log("Adding Button: " + aName);
                return layout.addButton(aText, aName);
            }
            else
            {
                Debug.Log("Failure Adding Button: " + aName);
            }
            return null;
        }
        public Button addButton(string aLayoutName, Texture2D aTexture, string aName)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null && aName != string.Empty)
            {
                return layout.addButton(aTexture, aName);
            }
            return null;
        }
        //Labels
        public Label addLabel(string aLayoutName, string aText, string aName)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null && aName != string.Empty)
            {
                return layout.addLabel(aText, aName);
            }
            return null;
        }
        public Label addLabel(string aLayoutName, Color aColor, string aText, string aName)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null && aName != string.Empty)
            {
                layout.addLabel(aColor, aText, aName);
            }
            return null;
        }
        
        public Label addLabel(string aLayoutName, Texture2D aTexture, string aName)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null && aName != string.Empty)
            {
                layout.addLabel(aTexture, aName);
            }
            return null;
        }
        //Text Fields
        public TextField addTextField(string aLayoutName, string aText, string aName)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null && aName != string.Empty)
            {
                layout.addTextField(aText, aName);
            }
            return null;
        }

        public void registerButtonCallback(string aLayoutName, ButtonCallback aCallback)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null)
            {
                layout.registerButtonCallback(aCallback);
            }
        }
        public void registerTextFieldCallback(string aLayoutName,TextFieldCallback aCallback)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null)
            {
                layout.registerTextFieldCallback(aCallback);
            }
        }
        public void unregisterButtonCallback(string aLayoutName, ButtonCallback aCallback)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null)
            {
                layout.unregisterButtonCallback(aCallback);
            }
        }
        public void unregisterTextFieldCallback(string aLayoutName, TextFieldCallback aCallback)
        {
            Layout layout = getLayout(aLayoutName);
            if (layout != null)
            {
                layout.unregisterTextFieldCallback(aCallback);
            }
        }

        public void update()
        {
            for (int i = 0; i < m_Layouts.Count; i++)
            {
                m_Layouts[i].update();
            }
        }

    }
}