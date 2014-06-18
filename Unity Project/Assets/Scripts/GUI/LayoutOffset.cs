using UnityEngine;
using System.Collections;
using System;

namespace OnLooker
{
    [Serializable]
    public class LayoutOffset
    {
        [SerializeField]
        private bool m_UseOffset;
        [SerializeField]
        private Vector2 m_Offset;
        [SerializeField]
        private bool m_Center;
        [SerializeField]
        private bool m_Percentage;

        public bool useOffset
        {
            get { return m_UseOffset; }
            set { m_UseOffset = value; }
        }

        public bool center
        {
            get { return m_Center; }
            set { m_Center = value; }
        }
        public bool percentage
        {
            get { return m_Percentage; }
            set { m_Percentage = value; }
        }

        public Vector2 getOffset(Vector2 aSize)
        {
            if (m_Center == true)
            {
                float x = (float)Screen.width * 0.5f - aSize.x * 0.5f;
                float y = (float)Screen.height * 0.5f - aSize.y * 0.5f;
                if (percentage == true)
                {
                    return new Vector2(x + m_Offset.x * Screen.width, y + m_Offset.y * Screen.height);
                }
                else
                {
                    return new Vector2(x + m_Offset.x, y + m_Offset.y);
                }
            }
            return m_Offset;
        }

       
        
    }
}
