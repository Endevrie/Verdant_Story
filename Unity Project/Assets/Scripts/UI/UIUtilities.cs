using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        public enum UIAnchorTarget
        {
            NONE,
            CAMERA,
            OBJECT
        }

        [Serializable]
        public class UIParameters
        {
            [SerializeField]
            private string m_ToggleName;

            //Text
            [SerializeField]
            private string m_Text;
            [SerializeField]
            private Font m_Font;
            [SerializeField]
            private FontStyle m_FontStyle;
            [SerializeField]
            private int m_FontSize;

            //Texture
            [SerializeField]
            private Texture m_Texture;

            //All
            [SerializeField]
            private Color m_Color;
            [SerializeField]
            private bool m_Interactive;

            //2D
            [SerializeField]
            private Rect m_Bounds;
            [SerializeField]
            private float m_Scale;

            //3D
            [SerializeField]
            private Vector3 m_Position;
            [SerializeField]
            private Vector3 m_Rotation;

            public string toggleName
            {
                get { return m_ToggleName; }
                set { m_ToggleName = value; }
            }
            public string text
            {
                get { return m_Text; }
                set { m_Text = value; }
            }
            public Font font
            {
                get { return m_Font; }
                set { m_Font = value; }
            }
            public FontStyle fontStyle
            {
                get { return m_FontStyle; }
                set { m_FontStyle = value; }
            }
            public int fontSize
            {
                get { return m_FontSize; }
                set { m_FontSize = value; }
            }
            public Texture texture
            {
                get { return m_Texture; }
                set { m_Texture = value; }
            }
            public Color color
            {
                get { return m_Color; }
                set { m_Color = value; }
            }
            public bool interactive
            {
                get { return m_Interactive; }
                set { m_Interactive = value; }
            }
            public Rect bounds
            {
                get { return m_Bounds; }
                set { m_Bounds = value; }
            }
            public float scale
            {
                get{return m_Scale;}
                set{m_Scale = value;}
            }
            public Vector3 position
            {
                get { return m_Position; }
                set { m_Position = value; }
            }
            public Vector3 rotation
            {
                get { return m_Rotation; }
                set { m_Rotation = value; }
            }
            

        }

        public class UIUtilities : MonoBehaviour
        {

            //Helpers to convert the 0,0 center to 0,0 bottom left
            static Vector2 convertGUIToScreen(Vector2 aGUI)
            {
                aGUI.x = aGUI.x + Screen.width * 0.5f;
                aGUI.y = aGUI.y + Screen.height * 0.5f;
                return aGUI;
            }
            //Helpers to convert the 0,0 bottom left to the 0,0 center
            static Vector2 convertScreenToGUI(Vector2 aScreen)
            {
                aScreen.x = aScreen.x - Screen.width * 0.5f;
                aScreen.y = aScreen.y - Screen.height * 0.5f;
                return aScreen;
            }
        }
    }
}