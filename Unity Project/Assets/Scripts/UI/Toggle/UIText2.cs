using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{

    namespace UI
    {

        //Requires GUIText
        [Serializable]
        public class UIText2 : UIToggle2
        {
            [SerializeField]
            [HideInInspector]
            private GUIText m_TextComponent = null;
            [SerializeField]
            [HideInInspector]
            private GUITexture m_DebugBounds = null;
            [SerializeField]
            [HideInInspector]
            private int m_FontSize = 12;
            // Use this for initialization
            void Start()
            {
                
                if (Application.isPlaying)
                {
                    initialize();
                    hideBounds();
                }
            }

            // Update is called once per frame
            protected override void gameUpdate()
            {
                base.gameUpdate();
            }

            public void setDefault(bool aForce)
            {
                bool init = false;
                if (m_TextComponent == null)
                {
                    init = true;
                }


                //If this component needs to initialize or is forced to then do so
                if (init == true || aForce == true)
                {
                    initialize();
                }
            }
            public void initialize()
            {
                m_TextComponent = GetComponent<GUIText>();
            }

            public void showBounds()
            {
                if (m_DebugBounds != null)
                {
                    return;
                }
                m_DebugBounds = gameObject.AddComponent<GUITexture>();
                m_DebugBounds.texture = OnLookerUtils.debugTexture;
                m_DebugBounds.pixelInset = scaledBounds;
            }
            public void hideBounds()
            {
                m_DebugBounds = GetComponent<GUITexture>();
                if (m_DebugBounds != null)
                {
                    DestroyImmediate(m_DebugBounds);
                }
            }
            
           

            public string text
            {
                get { return m_TextComponent.text; }
                set { m_TextComponent.text = value; }
            }
            public Font font
            {
                get { return m_TextComponent.font; }
                set { m_TextComponent.font = value; }

            }
            public int fontSize
            {
                get { return m_FontSize; }
                set {
                    m_FontSize = value;
                    m_TextComponent.fontSize = (int)(value * scale.x); }
            }
            public FontStyle fontStyle
            {
                get { return m_TextComponent.fontStyle; }
                set { m_TextComponent.fontStyle = value; }
            }
            public Color fontColor
            {
                get { return m_TextComponent.color; }
                set { m_TextComponent.color = value; }
            }

        }
    }
}