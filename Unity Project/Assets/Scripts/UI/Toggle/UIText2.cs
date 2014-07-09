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

            public override void setBounds(float x, float y, float w, float h)
            {
                if (m_TextComponent == null)
                {
                    Debug.Log("Missing GUIText");
                    return;
                }
                //Assumes GUIText
                //Set the scaled bounds
                m_Bounds.x = x;
                m_Bounds.y = y;
                m_Bounds.width = w;
                m_Bounds.height = h;

                Rect accurateRect = m_TextComponent.GetScreenRect();
                if (uniformScale == true)
                {
                    x = m_Bounds.x - Mathf.Abs(accurateRect.width - accurateRect.width * scale.x) * 0.5f;
                    y = m_Bounds.y - Mathf.Abs(accurateRect.height - accurateRect.height * scale.x) * 0.5f;
                    m_ScaledBounds.Set(x, y, accurateRect.width * scale.x, accurateRect.height * scale.x);
                }
                else
                {
                    x = m_Bounds.x - Mathf.Abs(accurateRect.width - accurateRect.width * scale.x) * 0.5f;
                    y = m_Bounds.y - Mathf.Abs(accurateRect.height - accurateRect.height * scale.y) * 0.5f;
                    m_ScaledBounds.Set(x, y, accurateRect.width * scale.x, accurateRect.height * scale.y);
                }
                Vector2 offset = m_TextComponent.pixelOffset;
                offset.Set(m_ScaledBounds.x, m_ScaledBounds.y);
                m_TextComponent.pixelOffset = offset;
            }


            public override Rect scaledBounds
            {
                get
                {
                    if (m_TextComponent != null)
                    {
                        return m_TextComponent.GetScreenRect();
                    }
                    return base.scaledBounds;
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