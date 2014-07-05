using UnityEngine;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        public delegate void UIEvent(UIToggle aSender, UIEventArgs aArgs);
        public enum UIEventType
        {
            ENTER,
            EXIT,
            CLICK,
            DOUBLE_CLICK,
            DOWN,
            RELEASE,
            HOVER,
            FOCUS,
            UNFOCUS,
            KEY_PRESS
        }

        public class UIEventArgs
        {
            private UIEventType m_EventType;
            private MouseButton m_MouseButton;
            private KeyCode m_KeyCode;

            public UIEventArgs(UIEventType aEventType, MouseButton aMouseButton)
            {
                m_EventType = aEventType;
                m_MouseButton = aMouseButton;
                m_KeyCode = KeyCode.None;
            }
            public UIEventArgs(UIEventType aEventType, MouseButton aMouseButton, KeyCode aKeyCode)
            {
                m_EventType = aEventType;
                m_MouseButton = aMouseButton;
                m_KeyCode = aKeyCode;
            }
            public UIEventType eventType { get { return m_EventType; } }
            public MouseButton mouseButton { get { return m_MouseButton; } }
            public KeyCode keyCode { get { return m_KeyCode; } }
        }
    }
}