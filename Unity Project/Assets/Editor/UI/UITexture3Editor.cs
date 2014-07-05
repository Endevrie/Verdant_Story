using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [CustomEditor(typeof(UITexture3))]
        public class UITexture3Editor : Editor
        {

            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                UITexture3 inspected = (UITexture3)target;

                EditorGUILayout.BeginVertical();

                inspected.debug = EditorGUILayout.Toggle("Debug", inspected.debug);
                inspected.setDefault(true);

                if (OnLookerUtils.worldPoint == null)
                {
                    OnLookerUtils.worldPoint = GameObject.Find("WorldPoint").transform;
                }

                UIEditorUtilities.toggleInspector(inspected);
                UIEditorUtilities.toggle3Inspector(inspected);
                UIEditorUtilities.texture3Inspector(inspected);

                EditorGUILayout.EndVertical();

            }
        }
    }
}