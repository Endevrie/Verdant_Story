using UnityEngine;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        public class UILabel : UIHandler
        {
            [SerializeField]
            [HideInInspector]
            private UIToggle m_Text = null;
            [SerializeField]
            [HideInInspector]
            private UIToggle m_Texture = null;


            [SerializeField]
            [HideInInspector]
            private Texture m_Normal = null;
            [SerializeField]
            [HideInInspector]
            private Texture m_Hover = null;
            [SerializeField]
            [HideInInspector]
            private Texture m_Focus = null;
            
            // Use this for initialization
            void Start()
            {
                
            }

            // Update is called once per frame
            void Update()
            {
                if (m_Texture == null || m_Text == null)
                {
                    return;
                }

                //3D
                if (m_Texture.GetType() == typeof(UITexture3))
                {
                    UITexture3 uiTexture = (UITexture3)m_Texture;
                    if (m_Text.isFocused == true)
                    {
                        uiTexture.texture = m_Focus;
                    }
                    else if (m_Text.mouseInBounds == true)
                    {
                        uiTexture.texture = m_Hover;
                    }
                    else
                    {
                        uiTexture.texture = m_Normal;
                    }
                }//2D
                else if (m_Texture.GetType() == typeof(UITexture2))
                {
                    UITexture2 uiTexture = (UITexture2)m_Texture;
                    if (m_Text.isFocused == true)
                    {
                        uiTexture.texture = m_Focus;
                    }
                    else if (m_Text.mouseInBounds == true)
                    {
                        uiTexture.texture = m_Hover;
                    }
                    else
                    {
                        uiTexture.texture = m_Normal;
                    }
                }

                


            }

            protected override void onUIEvent(UIToggle aSender, UIEventArgs aArgs)
            {
                switch (aArgs.eventType)
                {
                    case UIEventType.ENTER:

                        break;

                    case UIEventType.EXIT:

                        break;
                    case UIEventType.FOCUS:

                        break;

                        
                }
                
            }

            public string text
            {
                get
                {
                    if (m_Text.GetType() == typeof(UIText2))
                    {
                        return ((UIText2)m_Text).text;
                    }
                    else if (m_Text.GetType() == typeof(UIText3))
                    {
                        return ((UIText3)m_Text).text;
                    }
                    return string.Empty;
                }
                set
                {
                    if (m_Text.GetType() == typeof(UIText2))
                    {
                        ((UIText2)m_Text).text = value;
                    }
                    else if (m_Text.GetType() == typeof(UIText3))
                    {
                        ((UIText3)m_Text).text = value;
                    }
                }
            }
            public Texture normal
            {
                get { return m_Normal; }
                set { m_Normal = value; }
            }
            public Texture hover
            {
                get { return m_Hover; }
                set { m_Hover = value; }
            }
            public Texture focus
            {
                get { return m_Focus; }
                set { m_Focus = value; }
            }

            public UIToggle uiText
            {
                get { return m_Text; }
                set { m_Text = value; }
            }
            public UIToggle uiTexture
            {
                get { return m_Texture; }
                set { m_Texture = value; }
            }
            
        }
    }
}