using UnityEngine;
using System.Collections.Generic;
using System;

namespace OnLooker
{

    public delegate void ButtonCallback(Button aSender);
    public delegate void TextFieldCallback(TextField aSender, string aText);

    //Defines an area and has a list of controls
    //Has a event for each control that requires one.
    [Serializable]
    public class Layout
    {
        [SerializeField]
        private Rect m_Bounds;
        private List<Control> m_Controls = new List<Control>();
        private int m_ControlID = 0;
        private string m_Name = string.Empty;

        private event ButtonCallback e_ButtonCallback;
        private event TextFieldCallback e_TextFieldCallback;

        public Rect bounds
        {
            get { return m_Bounds; }
            set { m_Bounds = value; }
        }
        public string name
        {
            get { return m_Name; }
        }

        //Callback Registration functions
        public void registerButtonCallback(ButtonCallback aCallback)
        {
            if (aCallback != null)
            {
                e_ButtonCallback += aCallback;
            }
        }
        public void unregisterButtonCallback(ButtonCallback aCallback)
        {
            if (aCallback != null)
            {
                e_ButtonCallback -= aCallback;
            }
        }
        public void registerTextFieldCallback(TextFieldCallback aCallback)
        {
            if (aCallback != null)
            {
                e_TextFieldCallback += aCallback;
            }
        }
        public void unregisterTextFieldCallback(TextFieldCallback aCallback)
        {
            if (aCallback != null)
            {
                e_TextFieldCallback -= aCallback;
            }
        }

        //
        private int getNextControlID()
        {
            m_ControlID++;
            return m_ControlID - 1;
        }

        //All layouts are to have a unique name
        public Layout(string aName)
        {
            m_Name = aName;
            if (m_Controls == null)
            {
                m_Controls = new List<Control>();
            }
        }
        public Layout(string aName, Rect aBounds)
        {
            m_Name = aName;
            m_Bounds = aBounds;
            if (m_Controls == null)
            {
                m_Controls = new List<Control>();
            }
        }

        //Update all the controls in the given area.
        public void update()
        {
            if (m_Controls == null)
            {
                return;
            }
            GUI.Box(m_Bounds, string.Empty);
            GUILayout.BeginArea(m_Bounds);

            for (int i = 0; i < m_Controls.Count; i++)
            {
                m_Controls[i].update();
            }

            GUILayout.EndArea();
        }


        //Control Functions
        //No control with the same name can be added to the current list of controls
        public Button addButton(string aText, string aControlName)
        {
            if (controlExist(aControlName) == true)
            {
                return null;
            }
            if (m_Controls != null)
            {
                Button button = new Button(aText, valueChanged, aControlName, getNextControlID());
                m_Controls.Add(button);
                return button;
            }
            return null;
        }
        public Button addButton(Texture2D aTexture, string aControlName)
        {
            if (controlExist(aControlName) == true)
            {
                return null;
            }
            if (m_Controls != null && aTexture != null)
            {
                Button button = new Button(aTexture, valueChanged, aControlName, getNextControlID());
                m_Controls.Add(button);
                return button;
            }
            return null;
        }
        public Label addLabel(string aText, string aControlName)
        {
            if (controlExist(aControlName) == true)
            {
                return null;
            }
            if (m_Controls != null)
            {
                Label label = new Label(aText, valueChanged, aControlName, getNextControlID());
                m_Controls.Add(label);
                return label;
            }
            return null;
        }
        public Label addLabel(Color aColor, string aText, string aControlName)
        {
            if (controlExist(aControlName) == true)
            {
                return null;
            }
            if (m_Controls != null)
            {
                Label label = new Label(aColor, aText, valueChanged, aControlName, getNextControlID());
                m_Controls.Add(label);
                return label;
            }
            return null;

        }
        public Label addLabel(Texture2D aTexture, string aControlName)
        {
            if (controlExist(aControlName) == true)
            {
                return null;
            }
            if (m_Controls != null && aTexture != null)
            {
                Label label = new Label(aTexture, valueChanged, aControlName, getNextControlID());
                m_Controls.Add(label);
                return label;
            }
            return null;
        }
        public TextField addTextField(string aText, string aControlName)
        {
            if (controlExist(aControlName) == true)
            {
                return null;
            }
            if (m_Controls != null)
            {
                TextField textfield = new TextField(aText, valueChanged, aControlName, getNextControlID());
                m_Controls.Add(textfield);
                return textfield;
            }
            return null;
        }

        public Control removeControl(Control aControl)
        {
            if (aControl != null && m_Controls != null)
            {
                if (m_Controls.Contains(aControl))
                {
                    m_Controls.Remove(aControl);
                    return null;
                }
            }
            return aControl;
        }
        public bool removeControl(string aControlName)
        {
            Control control = getControl(aControlName);
            m_Controls.Remove(control);
            if (control != null)
            {
                //Removed Successfully
                return true;
            }
            //Unsuccessful removal
            return false;
        }

        private void valueChanged(Control aSender, ControlArgs aArgs)
        {
            switch (aSender.type)
            {
                //Click
                case EControl.BUTTON:
                    handleButton((Button)aSender);
                    break;
                case EControl.TEXT_FIELD:
                    string text = aArgs.value.asString();
                    handleTextField((TextField)aSender, text);
                    if (aArgs.value.getError() != ParseError.NO_ERROR)
                    {
                        Debug.Log("Error in Parsing");
                    }
                    break;
            }
        }
        private void handleButton(Button aSender)
        {
            if (aSender == null)
            {
                return;
            }
            //Invoke Button Callback
            e_ButtonCallback.Invoke(aSender);

        }
        private void handleTextField(TextField aSender, string aText)
        {
            if (aSender == null)
            {
                return;
            }
            //Invoke Textfield Callback
            e_TextFieldCallback.Invoke(aSender, aText);

        }

        //Checks to make sure a control exists
        //Returns true if the control exists or if the string was empty;
        private bool controlExist(string aName)
        {
            if (aName == string.Empty)
            {
                return true;
            }
            for (int i = 0; i < m_Controls.Count; i++)
            {
                if (m_Controls[i].name == aName)
                {
                    return true;
                }
            }
            return false;
        }
        //Searchs for a control and returns it if found, null if not
        private Control getControl(string aName)
        {
            if (aName == string.Empty)
            {
                return null;
            }
            for (int i = 0; i < m_Controls.Count; i++)
            {
                if (m_Controls[i].name == aName)
                {
                    return m_Controls[i];
                }
            }
            return null;
        }
    }

}


