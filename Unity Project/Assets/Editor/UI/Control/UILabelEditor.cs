using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [CustomEditor(typeof(UILabel))]
        public class UILabelEditor : Editor
        {

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                UILabel inspected = (UILabel)target;

                inspected.uiText = UIEditorUtilities.toggleField("UIText", inspected.uiText);
                inspected.uiTexture = UIEditorUtilities.toggleField("UITexture", inspected.uiTexture);
                inspected.normal = OLEditorUtilities.textureField("Normal", inspected.normal);
                inspected.hover = OLEditorUtilities.textureField("Hover", inspected.hover);
                inspected.focus = OLEditorUtilities.textureField("Focus", inspected.focus);

                //2D Mode
                if (inspected.uiText.GetType() == typeof(UIText2) && inspected.uiTexture.GetType() == typeof(UITexture2))
                {

                }
                //3D Mode
                else if (inspected.uiText.GetType() == typeof(UIText3) && inspected.uiTexture.GetType() == typeof(UITexture3))
                {

                }
            }
        }
    }
}