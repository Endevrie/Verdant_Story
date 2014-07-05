using UnityEngine;
using UnityEditor;
using System.Collections;


namespace OnLooker
{
    namespace UI
    {
        [CustomEditor(typeof(UIText3))]
        public class UIText3Editor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                EditorGUILayout.BeginVertical();

                UIText3 inspected = (UIText3)target;
                inspected.debug = EditorGUILayout.Toggle("Debug", inspected.debug);
                inspected.setDefault(true);

                if (OnLookerUtils.worldPoint == null)
                {
                    OnLookerUtils.worldPoint = GameObject.Find("WorldPoint").transform;
                }

                UIEditorUtilities.toggleInspector(inspected);
                UIEditorUtilities.toggle3Inspector(inspected);
                UIEditorUtilities.text3Inspector(inspected);

                EditorGUILayout.EndVertical();
            }
        }
    }
}
