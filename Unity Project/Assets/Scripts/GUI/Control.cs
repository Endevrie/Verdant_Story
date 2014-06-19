using UnityEngine;
using System.Collections;

namespace OnLooker
{
    //EControl describes the control types
    public enum EControl
    {
        CONTROL,        //Implemented - Base of all Controls
        BUTTON,         //Implemented
        TOGGLE,         //Not Implemented
        LABEL,          //Implemented
        TEXT_FIELD,     //Implemented
        PASSWORD_FIELD, //Not Implemented
        SLIDER          //Not Implemented
    }

    //This class holds arguments to send over through control callbacks.
    public class ControlArgs
    {
        private Value m_Value;
        public ControlArgs(Value aValue)
        {
            m_Value = aValue;
        }

        public Value value
        {
            get { return m_Value; }
        }
    }

    //The delegate for a control callback
    public delegate void ControlCallback(Control aSender, ControlArgs aArgs);

    //The base class for all controls to come
    //Each control will be given a unique name
    //You can use this name for lookups
    //
    public abstract class Control
    {
        private ControlCallback m_ValueChanged;
        private string m_Name;
        private int m_ID;
        private GUIStyle m_Style;


        public Control(ControlCallback aValueChanged)
        {
            m_ValueChanged = aValueChanged;
            m_Style = new GUIStyle();
            m_Style.alignment = TextAnchor.MiddleCenter;
            m_Style.normal.textColor = Color.white;
        }
        public Control(ControlCallback aValueChanged, string aName, int aID)
        {
            m_ValueChanged = aValueChanged;
            m_Name = aName;
            m_ID = aID;
            m_Style = new GUIStyle();
            m_Style.alignment = TextAnchor.MiddleCenter;
            m_Style.normal.textColor = Color.white;
        }

        //Draws the gui and sends an event based on action
        public abstract void update();

        //Properties
        public ControlCallback valueChanged
        {
            get { return m_ValueChanged; }
            protected set { m_ValueChanged = valueChanged; }
        }
        virtual public EControl type
        {
            get { return EControl.CONTROL; }
        }
        public string name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        public int id
        {
            get { return m_ID; }
        }
        public GUIStyle style
        {
            get { return m_Style; }
            set { m_Style = value; }
        }

        public Color color
        {
            get { return m_Style.normal.textColor; }
            set { m_Style.normal.textColor = value; }
        }
        public Font font
        {
            get { return m_Style.font; }
            set { m_Style.font = value; }
        }


    }
}