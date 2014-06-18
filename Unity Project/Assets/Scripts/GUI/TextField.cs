using UnityEngine;
using System.Collections;

namespace OnLooker
{

    public class TextField : Control
    {
        private string m_Text = string.Empty;
        private string m_Buffer = string.Empty;
        private int m_MaxCharacter = 20;
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
        override public EControl type
        {
            get { return EControl.TEXT_FIELD; }
        }
        public string text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        public int maxCharacter
        {
            get { return m_MaxCharacter; }
            set { m_MaxCharacter = value; }
        }



        public override void update()
        {
            m_Buffer = GUILayout.TextField(m_Text,style);
            if (m_Buffer.Length > m_MaxCharacter)
            {
                return;
            }
            if (m_Buffer != m_Text)
            {
                m_Text = m_Buffer;
                valueChanged(this, new ControlArgs(new Value(m_Text)));
                
            }
        }

    }
}