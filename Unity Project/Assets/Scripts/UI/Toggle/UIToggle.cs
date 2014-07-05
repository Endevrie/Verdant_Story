using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        [ExecuteInEditMode]
        [Serializable]
        public class UIToggle : MonoBehaviour
        {
            [SerializeField]
            [HideInInspector]
            private bool m_Debug = false;

            [SerializeField]
            [HideInInspector]
            protected UIManager m_Manager = null;

            [SerializeField]
            [HideInInspector]
            private string m_ToggleName = string.Empty;
            protected bool m_MouseInBounds = false;
            private bool m_IsFocused = false;
            [SerializeField]
            [HideInInspector]
            private bool m_Interactive = false;

            [SerializeField]
            [HideInInspector]
            private bool m_TrapDoubleClick = false;
            private float m_LastClick = 0.0f;

            private event UIEvent m_UIEvent;

            void Start()
            {
                if (Application.isPlaying == true)
                {
                    gameStart();
                }
            }
            void OnEnable()
            {
                if (Application.isPlaying == true)
                {
                    gameEnable();
                }
            }
            void OnDisable()
            {
                if (Application.isPlaying == true)
                {
                    gameDisable();
                }
            }
            void OnDestroy()
            {
                if (m_Manager != null)
                {
                    m_Manager.unregisterToggle(this);
                }
                if (Application.isPlaying == true)
                {
                    gameDestroy();
                }
            }
            void Update()
            {
                if (Application.isPlaying == true)
                {
                    gameUpdate();
                }
            }
            void LateUpdate()
            {
                if (Application.isPlaying == true)
                {
                    gameLateUpdate();
                }
            }

            void FixedUpdate()
            {
                if (Application.isPlaying == true)
                {
                    gameFixedUpdate();
                }
            }


            //Use these for in game updates
            protected virtual void gameStart()
            {
                
            }
            protected virtual void gameEnable()
            {

            }
            protected virtual void gameDisable()
            {

            }
            protected virtual void gameDestroy()
            {

            }
            protected virtual void gameUpdate()
            {

            }
            protected virtual void gameFixedUpdate()
            {

            }
            protected virtual void gameLateUpdate()
            {

            }


            

            protected void invokeUIEvent(UIToggle aSender, UIEventArgs aArgs)
            {
                if (m_UIEvent != null)
                {
                    m_UIEvent.Invoke(aSender, aArgs);
                }
            }
            public virtual bool processKeyEvents(KeyCode[] aKeyCodes)
            {
                bool keyPressed = false;
                if (aKeyCodes != null)
                {
                    for (int i = 0; i < aKeyCodes.Length; i++)
                    {
                        if (Input.GetKeyDown(aKeyCodes[i]))
                        {
                            onKeyEvent(aKeyCodes[i]);
                            keyPressed = true;
                        }
                    }
                }
                return keyPressed;
            }
            public virtual void processEvents()
            {
                //Debug.Log(name +  " Events Processed");
                //Mouse Down "Click"
                bool mouseAction = false;
                if (Input.GetMouseButtonDown((int)MouseButton.LEFT))
                {
                    onMouseClick(MouseButton.LEFT);
                    mouseAction = true;
                }
                if (Input.GetMouseButtonDown((int)MouseButton.MIDDLE))
                {
                    onMouseClick(MouseButton.MIDDLE);
                    mouseAction = true;
                }
                if (Input.GetMouseButtonDown((int)MouseButton.RIGHT))
                {
                    onMouseClick(MouseButton.RIGHT);
                    mouseAction = true;
                }
                //Mouse Up
                if (Input.GetMouseButtonUp((int)MouseButton.LEFT))
                {
                    onMouseRelease(MouseButton.LEFT);
                    mouseAction = true;
                }
                if (Input.GetMouseButtonUp((int)MouseButton.MIDDLE))
                {
                    onMouseRelease(MouseButton.MIDDLE);
                    mouseAction = true;
                }
                if (Input.GetMouseButtonUp((int)MouseButton.RIGHT))
                {
                    onMouseRelease(MouseButton.RIGHT);
                    mouseAction = true;
                }
                //Mouse Down "Continous
                if (Input.GetMouseButton((int)MouseButton.LEFT))
                {
                    onMouseDown(MouseButton.LEFT);
                    mouseAction = true;
                }
                if (Input.GetMouseButton((int)MouseButton.MIDDLE))
                {
                    onMouseDown(MouseButton.MIDDLE);
                    mouseAction = true;
                }
                if (Input.GetMouseButton((int)MouseButton.RIGHT))
                {
                    onMouseDown(MouseButton.RIGHT);
                    mouseAction = true;
                }
                if (mouseAction == false)
                {
                    onMouseHover();
                }
                
            }
            public void registerEvent(UIEvent aCallback)
            {
                if (aCallback != null)
                {
                    m_UIEvent += aCallback;
                }
            }
            public void unregisterEvent(UIEvent aCallback)
            {
                if (aCallback != null && m_UIEvent != null)
                {
                    m_UIEvent -= aCallback;
                }
            }

            /// <summary>
            /// Focus Events disregard interactive flag because if they are not interactive they cannot be selected by the UIManager (User).
            /// </summary>
            protected virtual void onFocus()
            {
                invokeUIEvent(this, new UIEventArgs(UIEventType.FOCUS, MouseButton.NONE));
            }
            protected virtual void onUnfocus()
            {
                invokeUIEvent(this, new UIEventArgs(UIEventType.UNFOCUS, MouseButton.NONE));
            }

            ///Event Helper Functions

            protected virtual void onMouseEnter()
            {
                if (m_Interactive == true)
                {
                    invokeUIEvent(this, new UIEventArgs(UIEventType.ENTER, MouseButton.NONE));
                }
            }
            protected virtual void onMouseExit()
            {
                if (m_Interactive == true)
                {
                    invokeUIEvent(this, new UIEventArgs(UIEventType.EXIT, MouseButton.NONE));
                }
            }
            protected virtual void onMouseDown(MouseButton aButton)
            {
                if (m_Interactive == true)
                {
                    invokeUIEvent(this, new UIEventArgs(UIEventType.DOWN, aButton));
                }
            }
            protected virtual void onMouseClick(MouseButton aButton)
            {
                if (m_Interactive == true)
                {
                    bool doubleClick = false;
                    float delta = Time.time - lastClick;
                    if (delta < UIManager.doubleClickTime)
                    {
                        doubleClick = true;
                    }
                    m_LastClick = Time.time;
                    
                    if (doubleClick == true)
                    {
                        invokeUIEvent(this, new UIEventArgs(UIEventType.DOUBLE_CLICK, aButton));
                        if (m_TrapDoubleClick == true)
                        {
                            return;
                        }
                    }
                    invokeUIEvent(this, new UIEventArgs(UIEventType.CLICK, aButton));
                }
            }
            protected virtual void onMouseRelease(MouseButton aButton)
            {
                invokeUIEvent(this, new UIEventArgs(UIEventType.RELEASE, aButton));
            }
            protected virtual void onMouseHover()
            {
                invokeUIEvent(this, new UIEventArgs(UIEventType.HOVER, MouseButton.NONE));
            }

            protected virtual void onKeyEvent(KeyCode aKey)
            {
                invokeUIEvent(this, new UIEventArgs(UIEventType.KEY_PRESS, MouseButton.NONE, aKey));
            }


            /// <summary>
            /// Used by UIManager only
            /// </summary>
            
            public void setFocus(bool aValue)
            {
                if (aValue != m_IsFocused)
                {
                    if (aValue == true)
                    {
                        onFocus();
                        m_IsFocused = aValue;
                    }
                    else
                    {
                        onUnfocus();
                        m_IsFocused = aValue;
                    }
                }
            }
            public void setManager(UIManager aManager)
            {
                if (m_Manager == null && aManager != null)
                {
                    m_Manager = aManager;
                }
            }


            //Accessors
            public bool debug
            {
                get { return m_Debug; }
                set { m_Debug = value; }
            }
            public UIManager manager
            {
                get { return m_Manager; }
            }
            public string toggleName
            {
                get { return m_ToggleName; }
                set { m_ToggleName = value; }
            }
            public bool mouseInBounds
            {
                get { return m_MouseInBounds; }
                protected set { m_MouseInBounds = value; }
            }
            public bool isFocused
            {
                get { return m_IsFocused; }
            }
            public bool isInteractive
            {
                get { return m_Interactive; }
                set { m_Interactive = value; }
            }
            public bool trapDoubleClick
            {
                get { return m_TrapDoubleClick; }
                set { m_TrapDoubleClick = value; }
            }
            protected float lastClick
            {
                get { return m_LastClick; }
                set { m_LastClick = value; }
            }
            
        }
    }
}