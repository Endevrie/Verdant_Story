using UnityEngine;
using System.Collections;

namespace OnLooker
{
    public enum MouseButton
    {
        LEFT,
        RIGHT,
        MIDDLE,
        NONE
    }

    [ExecuteInEditMode]
    public class OnLookerUtils : MonoBehaviour
    {
        #region singleton
        private static OnLookerUtils s_Instance = null;
        private static int s_InstanceCount = 0;
        private static void setInstance(OnLookerUtils aManager)
        {
            if (s_Instance == null && aManager != null)
            {
                if (s_InstanceCount == 0)
                {
                    s_Instance = aManager;
                    s_InstanceCount++;
                }
                else
                {
                    Debug.LogWarning("Invalid amount of OnLookerUtils. There must only be one");
                }
            }
            else if (s_Instance != null && aManager == null)
            {
                if ((s_InstanceCount <= 0) == false)
                {
                    s_Instance = null;
                    s_InstanceCount--;
                }
            }
        }
        private static OnLookerUtils instance
        {
            get { return s_Instance; }
        }
        #endregion

        public static bool anyMouseButtonDown()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LEFT))
            {
                return true;
            }
            if (Input.GetMouseButtonDown((int)MouseButton.RIGHT))
            {
                return true;
            }
            if (Input.GetMouseButtonDown((int)MouseButton.MIDDLE))
            {
                return true;
            }
            return false;
        }

        public static bool pointInRect(Vector2 aPoint, Rect aRect)
        {
            return pointInRect(aPoint, aRect, true);
        }
        
        public static bool pointInRect(Vector2 aPoint, Rect aRect, bool yUp)
        {
            //Is the Y Direction Up or down
            if (yUp == true)
            {
                if (aPoint.x < aRect.x || aPoint.y < aRect.y || aPoint.x > aRect.x + aRect.width || aRect.y > aRect.y + aRect.height)
                {
                    return false;
                }
            }
            else
            {
                if (aPoint.x < aRect.x || aPoint.y > aRect.y + aRect.height || aPoint.x > aRect.x + aRect.width || aRect.y < aRect.y)
                {
                    return false;
                }
            }
            return true;
        }

        public static float clampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }

        private static Transform s_WorldPoint;

        public Transform m_WorldPoint;
        public Texture m_DebugTexture;

        void Start()
        {
            setInstance(this);
            if (Application.isPlaying)
            {
                worldPoint = m_WorldPoint;
            }
        }

        void OnDestroy()
        {
            setInstance(null);
        }

        public static Transform worldPoint
        {
            get
            {
                return s_WorldPoint;
            }
            set
            {
                s_WorldPoint = value;
            }

        }
        public static Texture debugTexture
        {
            get
            {
                if (instance == null)
                {
                    return null;
                }
                return instance.m_DebugTexture;
            }
        }

        
        
    }
}