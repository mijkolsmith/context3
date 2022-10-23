using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        QuestManager questManager = (QuestManager)target;

        EditorGUILayout.BeginHorizontal();
        if (questManager.currentQuest == null || questManager.currentQuest.Name == "")
        {
            EditorGUILayout.LabelField("Current quest is either null or doesn't have a name. ");
        }
        else
        {
            EditorGUILayout.LabelField("Current quest: ");
            EditorGUILayout.LabelField(questManager.currentQuest.Name);
        }
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.LabelField("Custom Quest Editor");

        //for (int i = 0; i < questManager.quests.Count; i++)
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    EditorGUILayout.LabelField(questManager.quests[i].Name);
        //    questManager.quests[i].Name = EditorGUILayout.TextField("");
        //    EditorGUILayout.EndHorizontal();
        //}
        base.OnInspectorGUI();
    }
}
