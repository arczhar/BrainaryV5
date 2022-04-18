using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GlobalVariable))]
public class GlobalVariableEditor : Editor
{
    private GlobalVariable t;
    private SerializedObject getTarget;

    private Color defaultColor;
    private Color defaultTextColor;

    void OnEnable()
    {
        t = (GlobalVariable)target;
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

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("Box");

        SerializedProperty expandValAva = getTarget.FindProperty("ExpandingAvatar");
        SerializedProperty avatars = getTarget.FindProperty("Avatars");
        expandValAva.boolValue = EditorGUILayout.Foldout(expandValAva.boolValue, "AVATARS (" + avatars.arraySize + ")");
        if (expandValAva.boolValue)
        {
            EditorGUILayout.BeginVertical("Box");
            
            for (int i = 0; i < avatars.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("Box");
                SerializedProperty avaRef = avatars.GetArrayElementAtIndex(i);
                SerializedProperty expandVal = avaRef.FindPropertyRelative("Expanding");
                SerializedProperty avaId = avaRef.FindPropertyRelative("AvatarId");
                SerializedProperty avaSprite = avaRef.FindPropertyRelative("AvatarImage");

                expandVal.boolValue = EditorGUILayout.Foldout(expandVal.boolValue, "Avatar " + avaId.intValue);

                if (expandVal.boolValue)
                {
                    EditorGUILayout.LabelField("Id");
                    avaId.intValue = EditorGUILayout.IntField("", avaId.intValue);
                    EditorGUILayout.LabelField("Icon");
                    avaSprite.objectReferenceValue = EditorGUILayout.ObjectField("", avaSprite.objectReferenceValue, typeof(Sprite), true, GUILayout.Width(64), GUILayout.Height(64));

                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Remove", GUILayout.Height(30)))
                    {
                        t.Avatars.RemoveAt(i);
                    }
                    GUI.backgroundColor = defaultColor;


                }

                EditorGUILayout.EndVertical();
            }



            EditorGUILayout.EndVertical();

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add New Avatar", GUILayout.Height(30)))
            {
                t.Avatars.Add(new Avatar());
            }
            GUI.backgroundColor = defaultColor;

            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();


        EditorGUILayout.BeginVertical("Box");

        SerializedProperty expandValQuiz = getTarget.FindProperty("ExpandingQuiz");
        SerializedProperty quizList = getTarget.FindProperty("Quizzes");

        expandValQuiz.boolValue = EditorGUILayout.Foldout(expandValQuiz.boolValue, "ALL QUIZ (" + quizList.arraySize + ")");
        if (expandValQuiz.boolValue)
        {
            EditorGUILayout.BeginVertical("Box");
            
            for (int i = 0; i < quizList.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.BeginVertical("Box");
                SerializedProperty quizRef = quizList.GetArrayElementAtIndex(i);
                if(quizRef.objectReferenceValue != null)
                {
                    Quiz qz = quizRef.objectReferenceValue as Quiz;
                    EditorGUILayout.LabelField("Topic: " + qz.Topic);
                }
                else
                {
                    EditorGUILayout.LabelField("Topic: No topic found");
                }

                
                EditorGUILayout.PropertyField(quizRef, new GUIContent(""), true, GUILayout.Height(30));
                EditorGUILayout.EndVertical();
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("x", GUILayout.Height(55), GUILayout.Width(30)))
                {
                    t.Quizzes.RemoveAt(i);
                }
                GUI.backgroundColor = defaultColor;
                EditorGUILayout.EndHorizontal();

                
                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add Quiz", GUILayout.Height(30)))
            {
                //Quiz newQuiz = ScriptableObject.CreateInstance<Quiz>();
                t.Quizzes.Add(null);
            }
            GUI.backgroundColor = defaultColor;
        }
        EditorGUILayout.EndVertical();

        getTarget.ApplyModifiedProperties();
    }

}
