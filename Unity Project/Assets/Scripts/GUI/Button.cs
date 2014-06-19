using UnityEngine;
using System.Collections;

namespace OnLooker
{
    //Button implementation can use text or a texture when displayed 
    //however only one can be used at a time.
    public class Button : Control
    {
        private string m_Text = string.Empty;
        private Texture2D m_Texture = null;
        private bool m_UsingTexture = false;

        public Button(ControlCallback aCallback)
            : base(aCallback)
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
        //Properties
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

        //Update implementation
        public override void update()
        {
            //If the button was clicked we invoke the callback method to notify it
            if (m_UsingTexture == true && m_Texture != null)
            {
                if (GUILayout.Button(m_Texture))
                {
                    valueChanged.Invoke(this, new ControlArgs(new Value("Click")));
                }
            }
            else
            {
                if (GUILayout.Button(m_Text))
                {
                    valueChanged.Invoke(this, new ControlArgs(new Value("Click")));
                }
            }

        }

    }
}