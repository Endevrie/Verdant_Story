using UnityEngine;
using System.Collections;

namespace OnLooker
{

    //The textfield is used for getting string user input.
    //You can specify a maximum amount of characters.
    public class TextField : Control
    {

        private string m_Text = string.Empty;
        private uint m_MaxCharacter = 20;
        public TextField(ControlCallback aCallback)
            : base(aCallback)
        {

        }
        public TextField(ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {

        }
        public TextField(string aText, ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {
            m_Text = aText;
        }
        //Properties
        override public EControl type
        {
            get { return EControl.TEXT_FIELD; }
        }
        public string text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        public uint maxCharacter
        {
            get { return m_MaxCharacter; }
            set { m_MaxCharacter = value; }
        }


        //Update implementation
        public override void update()
        {
            //Store the result into the output and do a check to see if its within our character boundary
            string buffer = GUILayout.TextField(m_Text, style);
            if (buffer.Length > m_MaxCharacter)
            {
                return;
            }
            //Then if there was a change in text
            //Make a callback to the callback
            if (buffer != m_Text)
            {
                m_Text = buffer;
                valueChanged.Invoke(this, new ControlArgs(new Value(m_Text)));
            }
        }
    }
}