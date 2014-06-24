using UnityEngine;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        public class UIHandler : MonoBehaviour
        {
            private UIToggle2 m_Toggle;
            // Use this for initialization
            void Start()
            {
                
            }

            virtual protected void OnEnable()
            {
                m_Toggle = GetComponent<UIToggle2>();
                if (m_Toggle == null)
                {
                    enabled = false;
                    Debug.Log("Please attach a Toggle to this game object");
                    return;
                }
                m_Toggle.registerEvent(onUIEvent);
            }

            virtual protected void OnDestroy()
            {
                m_Toggle.unregisterEvent(onUIEvent);
            }

            // Update is called once per frame
            void Update()
            {

            }

            virtual protected void onUIEvent(UIToggle2 aSender, UIEventArgs aArgs)
            {
                switch (aArgs.eventType)
                {
                    case UIEventType.DOUBLE_CLICK:
                        Debug.Log("Double Click");
                        break;
                    case UIEventType.CLICK:
                        Debug.Log("Click");
                        break;
                    case UIEventType.ENTER:
                        break;
                    case UIEventType.EXIT:

                        break;
                        
                    case UIEventType.DOWN:

                        break;
                    case UIEventType.RELEASE:
                        Debug.Log("Release");
                        break;

                    case UIEventType.HOVER:

                        break;
                }
            }
        }
    }
}