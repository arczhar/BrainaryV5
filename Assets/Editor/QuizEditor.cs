using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Quiz))]
public class QuizEditor : Editor
{
    private Quiz t;
    private SerializedObject getTarget;
    private SerializedProperty thisList;

    private int listSize;
    private int answerSize = 4;
    private string[] Alphabet = new string[]{"A", "B", "C", "D", "E", "F"};


    private Color defaultColor;
    private Color defaultTextColor;
    void OnEnable()
    {
        t = (Quiz)target;
        getTarget = new SerializedObject(t);
        thisList = getTarget.FindProperty("Questions");

        defaultColor = GUI.backgroundColor;
        defaultTextColor = GUI.color;
    }

    public override void OnInspectorGUI()
    {
        GUIStyle guiSty = new GUIStyle();
        getTarget.Update();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Settings:");
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Icon");
        SerializedProperty iconQuiz = getTarget.FindProperty("Icon");
        iconQuiz.objectReferenceValue = EditorGUILayout.ObjectField("", iconQuiz.objectReferenceValue, typeof(Sprite), true, GUILayout.Width(64), GUILayout.Height(64));
        EditorGUILayout.LabelField("Topic");
        SerializedProperty topic = getTarget.FindProperty("Topic");
        topic.stringValue = EditorGUILayout.TextField("", topic.stringValue, GUILayout.ExpandWidth(true), GUILayout.Width(200));
        EditorGUILayout.LabelField("Total Choices");
        answerSize = EditorGUILayout.IntSlider(answerSize, 3, 6, GUILayout.Width(200));
        SerializedProperty answerTime = getTarget.FindProperty("AnswerTime");
        SerializedProperty waitNext = getTarget.FindProperty("WaitNextQuestion");

        EditorGUILayout.LabelField("Answer Time (Second)");
        answerTime.intValue = EditorGUILayout.IntField("", answerTime.intValue, GUILayout.Width(200));
        EditorGUILayout.LabelField("Wait Next Time (Second)");
        waitNext.intValue = EditorGUILayout.IntField("", waitNext.intValue, GUILayout.Width(200));

        //EditorGUILayout.LabelField("Total Questions");
        listSize = thisList.arraySize;
        //listSize = EditorGUILayout.IntField("", listSize, GUILayout.ExpandWidth(true));



        if (listSize != thisList.arraySize)
        {
            while (listSize > thisList.arraySize)
            {
                thisList.InsertArrayElementAtIndex(thisList.arraySize);
            }
            while (listSize < thisList.arraySize)
            {
                thisList.DeleteArrayElementAtIndex(thisList.arraySize - 1);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        //EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.LabelField("Questions:");
        EditorGUILayout.BeginVertical("Box");

        if (thisList.arraySize <= 0)
        {
            GUI.color = Color.yellow;
            EditorGUILayout.LabelField("No Questions Found!");
            GUI.color = defaultTextColor;
        }

        for (int i = 0; i < thisList.arraySize; i++)
        {

            EditorGUILayout.BeginVertical("Box");
            SerializedProperty listRef = thisList.GetArrayElementAtIndex(i);
            SerializedProperty questionVal = listRef.FindPropertyRelative("Text");
            SerializedProperty expandVal = listRef.FindPropertyRelative("Expanding");
            SerializedProperty choices = listRef.FindPropertyRelative("Choices");
            choices.arraySize = answerSize;
            choices.serializedObject.ApplyModifiedProperties();

            int number = i + 1;
            
            expandVal.boolValue = EditorGUILayout.Foldout(expandVal.boolValue, "Question " + number);

            if (expandVal.boolValue)
            {
                
                EditorStyles.textArea.wordWrap = true;
                questionVal.stringValue = EditorGUILayout.TextArea(questionVal.stringValue, EditorStyles.textArea, GUILayout.Height(50));

               

                for (int x = 0; x < choices.arraySize; x++)
                {
                   
                    SerializedProperty listChoice = choices.GetArrayElementAtIndex(x);
                    SerializedProperty choiceVal = listChoice.FindPropertyRelative("Text");
                    SerializedProperty answerVal = listChoice.FindPropertyRelative("Answer");
                    EditorGUILayout.BeginHorizontal();
                        GUILayout.Label(Alphabet[x] + ".", GUILayout.ExpandWidth(false));
                        choiceVal.stringValue = EditorGUILayout.TextField("", choiceVal.stringValue, GUILayout.ExpandWidth(true), GUILayout.Height(20));
                        answerVal.boolValue = EditorGUILayout.Toggle("", answerVal.boolValue, GUILayout.Width(20));

                   
                    EditorGUILayout.EndHorizontal();

                    
                }

               

                EditorGUILayout.Space();

                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove", GUILayout.Height(30)))
                {
                    t.Questions.RemoveAt(i);
                }
                GUI.backgroundColor = defaultColor;

                //choices.arraySize = answerSize;
            }
            EditorGUILayout.EndVertical();
        }


        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add New Question", GUILayout.Height(30)))
        {
            t.Questions.Add(new Question());
        }
        GUI.backgroundColor = defaultColor;
        
        getTarget.ApplyModifiedProperties();
    }
}
