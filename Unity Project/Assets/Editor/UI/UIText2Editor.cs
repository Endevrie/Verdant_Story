using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [CustomEditor(typeof(UIText2))]
        public class UIText2Editor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                UIText2 inspected = (UIText2)target;

                EditorGUILayout.BeginVertical();
                inspected.debug = EditorGUILayout.Toggle("Debug", inspected.debug);
                inspected.setDefault(true);

                UIEditorUtilities.toggleInspector(inspected);
                UIEditorUtilities.toggle2Inspector(inspected);
                UIEditorUtilities.text2Inspector(inspected);

                GUIText text = inspected.guiText;
                Vector2 position = text.pixelOffset;
                position.x = inspected.scaledBounds.x;
                position.y = inspected.scaledBounds.y;
                text.pixelOffset = position;

                if (inspected.debug == true)
                {
                    inspected.showBounds();
                    GUITexture guiTexture = inspected.guiTexture;
                    guiTexture.pixelInset = inspected.scaledBounds;
                }
                else
                {
                    inspected.hideBounds();
                }

                EditorGUILayout.EndVertical();


            }
            
        }
    }
}