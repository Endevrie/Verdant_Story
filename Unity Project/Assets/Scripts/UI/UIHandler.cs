using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        public class UIHandler : MonoBehaviour
        {
            private void Start()
            {
                UIToggle toggle = GetComponent<UIToggle>();
                if (toggle == null)
                {
                    Debug.Log("Missing Toggle");
                    return;
                }
                toggle.registerEvent(onUIEvent);
            }
            private void OnDestroy()
            {
                UIToggle toggle = GetComponent<UIToggle>();
                if (toggle == null)
                {
                    Debug.Log("Missing Toggle");
                    return;
                }
                toggle.unregisterEvent(onUIEvent);
            }

            protected virtual void onUIEvent(UIToggle aSender, UIEventArgs aArgs)
            {

                switch (aArgs.eventType)
                {
                    case UIEventType.CLICK:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                    case UIEventType.DOUBLE_CLICK:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                        
                    case UIEventType.DOWN:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                    case UIEventType.ENTER:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                        
                    case UIEventType.EXIT:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                    case UIEventType.RELEASE:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                    case UIEventType.HOVER:

                        break;
                    case UIEventType.FOCUS:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                    case UIEventType.UNFOCUS:
                        Debug.Log(aArgs.eventType.ToString());
                        break;
                    case UIEventType.KEY_PRESS:
                        Debug.Log(aArgs.eventType.ToString());
                        break;

                }
            }
        }
    }
}