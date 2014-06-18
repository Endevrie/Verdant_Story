using UnityEngine;
using System.Collections;

namespace OnLooker
{

    public class Button : Control
    {
        private string m_Text = string.Empty;
        private Texture2D m_Texture = null;
        private bool m_UsingTexture = false;

        public Button(ControlCallback aCallback) : base(aCallback)
        {

        }
        public Button(ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {

        }
        public Button(string aText, ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {
            m_Text = aText;
            m_UsingTexture = false;
        }
        public Button(Texture2D aTexture, ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {
            m_Text = string.Empty;
            m_Texture = aTexture;
            m_UsingTexture = false;
        }
        override public EControl type
        {
            get { return EControl.BUTTON; }
        }
        public string text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }
        public Texture2D texture
        {
            get { return m_Texture; }
            set { m_Texture = value; }
        }
        public bool usingTexture
        {
            get { return m_UsingTexture; }
            set { m_UsingTexture = value; }
        }


        public override void update()
        {
            if (m_UsingTexture == true && m_Texture != null)
            {
                if (GUILayout.Button(m_Texture))
                {
                    valueChanged(this, new ControlArgs(new Value("Click")));
                }
            }
            else
            {
                if (GUILayout.Button(m_Text))
                {
                    valueChanged(this, new ControlArgs(new Value("Click")));
                }
            }

        }

    }
}