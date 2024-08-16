using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TestEnemy))]
public class TestEnemyEditor : Editor
{
    private TestEnemy.EnemyType previousEnemyType;
    private void OnEnable()
    {
        TestEnemy testEnemy = (TestEnemy)target;
        previousEnemyType = testEnemy.enemyType;
    }
    public override void OnInspectorGUI()
    {
        TestEnemy testEnemy = (TestEnemy)target;
        // Draw the default inspector for all properties
        //DrawDefaultInspector();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("enemyType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("hp"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("attack"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("defense"));

        // Check if enemyType has changed
        if (testEnemy.enemyType != previousEnemyType)
        {
            OnEnemyTypeChanged(testEnemy.enemyType);
            previousEnemyType = testEnemy.enemyType;
        }

        // EnemyTypeごとのフィールド名を配列にいれる
        string[] specialFieldNames = new string[]
        {
            "poison",
            "split",
            "fireBreath",
            "berserk",
        };

        // EnemyTypeごとのフィールドを表示する
        // それ以外のフィールドは表示しないし、0で初期化する
        for (int i = 0; i < specialFieldNames.Length; i++)
        {
            string fieldName = specialFieldNames[i];
            if (i == (int)testEnemy.enemyType)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldName));
            }
            else
            {
                serializedObject.FindProperty(fieldName).intValue = 0;
            }
        }

        /*
        switch (testEnemy.enemyType)
        {
            case TestEnemy.EnemyType.ゴブリン:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("poison"));
                break;
            case TestEnemy.EnemyType.ドラゴン:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("fireBreath"));
                break;
            case TestEnemy.EnemyType.スライム:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("split"));
                break;
            case TestEnemy.EnemyType.オーク:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("berserk"));
                break;
        }
        */

        // リセットボタンを追加する
        if (GUILayout.Button("Reset"))
        {
            testEnemy.hp = 0;
            testEnemy.attack = 0;
            testEnemy.defense = 0;
            testEnemy.fireBreath = 0;
            testEnemy.poison = 0;
            testEnemy.berserk = 0;
            testEnemy.split = 0;
        }
        serializedObject.ApplyModifiedProperties();
    }

    private void OnEnemyTypeChanged(TestEnemy.EnemyType newEnemyType)
    {
        // enemyTypeが変更されたときの処理をここに記述します
        Debug.Log($"EnemyTypeが変更されました({(int)newEnemyType}): " + newEnemyType);
    }

}
