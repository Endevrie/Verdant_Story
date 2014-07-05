using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        public class UIHandler : MonoBehaviour
        {
            private UIToggle m_Toggle;
            // Use this for initialization
            void Start()
            {

            }

            void OnEnable()
            {
                m_Toggle = GetComponent<UIToggle>();
                if (m_Toggle == null)
                {
                    Debug.Log("Attach this to a proper UI Gameobjec");
                    return;
                }
                m_Toggle.registerEvent(onUIEvent);
            }
            void OnDisable()
            {
                if (m_Toggle != null)
                {
                    m_Toggle.unregisterEvent(onUIEvent);
                }
            }

            // Update is called once per frame
            void Update()
            {

            }
            protected virtual void onUIEvent(UIToggle aSender, UIEventArgs aArgs)
            {

                switch (aArgs.eventType)
                {
                    case UIEventType.CLICK:
                        break;
                    case UIEventType.DOUBLE_CLICK:

                        break;
                        
                    case UIEventType.DOWN:
                        break;
                    case UIEventType.ENTER:
                        break;
                        
                    case UIEventType.EXIT:
                        break;
                    case UIEventType.RELEASE:

                        break;
                    case UIEventType.HOVER:

                        break;
                    case UIEventType.FOCUS:

                        break;
                    case UIEventType.UNFOCUS:

                        break;
                    case UIEventType.KEY_PRESS:
                        Debug.Log("Key Press");
                        break;

                }
            }
        }
    }
}