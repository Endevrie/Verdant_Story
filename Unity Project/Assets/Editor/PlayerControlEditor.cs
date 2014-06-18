using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlayerControl))]
public class PlayerControlEditor : Editor 
{

    public override void OnInspectorGUI()
    {
        

        PlayerControl playerControl = (PlayerControl)target;
        if (playerControl == null)
        {
            return;
        }
        UnitMotor unitMotor = playerControl.unitMotor;
        OrbitCamera camera = playerControl.orbitCamera;

        EditorGUILayout.BeginVertical();

            unitMotor = (UnitMotor)EditorGUILayout.ObjectField("Unit Motor",unitMotor, typeof(UnitMotor),true);
            camera = (OrbitCamera)EditorGUILayout.ObjectField("Orbit Camera",camera, typeof(OrbitCamera),true);
            if (unitMotor != null)
            {
                drawUnitMotor(unitMotor);
            }
            if (camera != null)
            {
                drawCamera(camera);
            }

            playerControl.unitMotor = unitMotor;
            playerControl.orbitCamera = camera;

        EditorGUILayout.EndVertical();
    }

    void drawUnitMotor(UnitMotor aUnit)
    {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            GUILayout.Label("Unit Motor Variables");
        EditorGUILayout.EndHorizontal();
        aUnit.unitAnimation = (Animation)EditorGUILayout.ObjectField("Animation", aUnit.unitAnimation, typeof(Animation), true);
        aUnit.movementSpeed = EditorGUILayout.FloatField("Movement Speed", aUnit.movementSpeed);
        aUnit.turnSpeed = EditorGUILayout.FloatField("Turn Speed", aUnit.turnSpeed);
        aUnit.jumpHeight = EditorGUILayout.FloatField("Jump Height", aUnit.jumpHeight);
        aUnit.maxVelocity = EditorGUILayout.FloatField("Max Velocity", aUnit.maxVelocity);
        aUnit.gravity = EditorGUILayout.FloatField("Gravity", aUnit.gravity);
        aUnit.relativeTransform = (Transform)EditorGUILayout.ObjectField("Relative Transform", aUnit.relativeTransform, typeof(Transform), true);
        aUnit.groundedDistanceCheck = EditorGUILayout.FloatField("Grounded Distance Check", aUnit.groundedDistanceCheck);
        aUnit.groundedRadiusSweep = EditorGUILayout.FloatField("Grounded Radius Sweep", aUnit.groundedRadiusSweep);
        aUnit.forwardAxis = EditorGUILayout.TextField("Forward Axis", aUnit.forwardAxis);
        aUnit.sideAxis = EditorGUILayout.TextField("Side Axis", aUnit.sideAxis);
        aUnit.jumpButton = EditorGUILayout.TextField("Jump Button", aUnit.jumpButton);
        aUnit.stanceKey = (KeyCode)EditorGUILayout.EnumPopup("Stance Key", aUnit.stanceKey);
        aUnit.useKey = (KeyCode)EditorGUILayout.EnumPopup("Use Key", aUnit.useKey);



    }
    void drawCamera(OrbitCamera aCamera)
    {
        EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            GUILayout.Label("Unit Camera Variables");
        EditorGUILayout.EndHorizontal();
        aCamera.target = (Transform)EditorGUILayout.ObjectField("Target", aCamera.target, typeof(Transform), true);
        //Y Min max Limits
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Y Limits:",GUILayout.MinWidth(150.0f));
        GUILayout.Label("Min");
        aCamera.yMinLimit = EditorGUILayout.FloatField(aCamera.yMinLimit);
        GUILayout.Label("Max");
        aCamera.yMaxLimit = EditorGUILayout.FloatField(aCamera.yMaxLimit);
        EditorGUILayout.EndHorizontal();
        aCamera.maxDistance = EditorGUILayout.FloatField("Max Distance", aCamera.maxDistance);
        aCamera.zoomSpeed = EditorGUILayout.FloatField("Zoom Speed", aCamera.zoomSpeed);
        aCamera.xRotationInputName = EditorGUILayout.TextField("X Rotation Input", aCamera.xRotationInputName);
        aCamera.yRotationInputName = EditorGUILayout.TextField("Y Rotation Input", aCamera.yRotationInputName);
        aCamera.zoomInputName = EditorGUILayout.TextField("Zoom Input", aCamera.zoomInputName);
    }
}
