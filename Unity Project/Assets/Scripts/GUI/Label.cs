using UnityEngine;
using System.Collections;
using System;

namespace OnLooker
{
    //Much like the button a label can use a text or a texture to display itself
    public class Label : Control
    {
        private string m_Text = string.Empty;
        private Texture2D m_Texture = null;
        private bool m_UsingTexture = false;

        public Label(ControlCallback aCallback)
            : base(aCallback)
        {


        }

        public Label(ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {

        }
        public Label(string aText, ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {
            m_Text = aText;
            m_UsingTexture = false;
        }

        public Label(Color aColor, string aText, ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {
            m_Text = aText;
            m_UsingTexture = false;

        }
        public Label(Texture2D aTexture, ControlCallback aCallback, string aName, int aID)
            : base(aCallback, aName, aID)
        {
            m_Text = string.Empty;
            m_Texture = aTexture;
            m_UsingTexture = false;
        }
        override public EControl type
        {
            get { return EControl.LABEL; }
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
                GUILayout.Label(m_Texture, style);
            }
            else
            {
                GUILayout.Label(m_Text, style);
            }
        }
    }
}