using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [CustomEditor(typeof(UITexture2))]
        public class UITexture2Editor : Editor
        {

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                UITexture2 inspected = (UITexture2)target;

                EditorGUILayout.BeginVertical();
                inspected.debug = EditorGUILayout.Toggle("Debug", inspected.debug);
                inspected.setDefault(true);

                UIEditorUtilities.toggleInspector(inspected);
                UIEditorUtilities.toggle2Inspector(inspected);
                UIEditorUtilities.texture2Inspector(inspected);

                inspected.guiTexture.pixelInset = inspected.scaledBounds;

                EditorGUILayout.EndVertical();
            }
        }
    }
}