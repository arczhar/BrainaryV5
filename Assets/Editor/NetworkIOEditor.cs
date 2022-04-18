using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NetworkIO))]
public class NetworkIOEditor : Editor
{
    private NetworkIO t;
    private SerializedObject getTarget;

    private Color defaultColor;
    private Color defaultTextColor;

    void OnEnable()
    {
        t = (NetworkIO)target;
        getTarget = new SerializedObject(t);

        defaultColor = GUI.backgroundColor;
        defaultTextColor = GUI.color;
    }

    public override void OnInspectorGUI()
    {
        getTarget.Update();


        EditorGUILayout.BeginVertical("Box");
        t.isPersistant = EditorGUILayout.Toggle("Singleton", t.isPersistant);
        EditorGUILayout.EndVertical();

        //SERVER OPTONS
        SerializedProperty serverOpt = getTarget.FindProperty("ServerOptions");
        EditorGUILayout.Space();

        SerializedProperty version = serverOpt.FindPropertyRelative("Version");
        SerializedProperty gameId = serverOpt.FindPropertyRelative("GameId");
        SerializedProperty roomClass = serverOpt.FindPropertyRelative("RoomClass");

        GUI.backgroundColor = defaultColor;
        if (string.IsNullOrEmpty(version.stringValue) || string.IsNullOrEmpty(gameId.stringValue) || string.IsNullOrEmpty(roomClass.stringValue))
        {
            GUI.backgroundColor = Color.red;
            EditorGUILayout.HelpBox("Field cannot be empty", MessageType.Error);
        }
            


        
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("SERVER OPTIONS");
        EditorGUILayout.BeginVertical("Box");
        

        version.stringValue = EditorGUILayout.TextField("Version", version.stringValue);
        gameId.stringValue = EditorGUILayout.TextField("Game ID", gameId.stringValue);
        roomClass.stringValue = EditorGUILayout.TextField("Room ID", roomClass.stringValue);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        
        EditorGUILayout.Space();
        GUI.backgroundColor = defaultColor;

        //END SERVER OPTIONS

        getTarget.ApplyModifiedProperties();
    }
}
