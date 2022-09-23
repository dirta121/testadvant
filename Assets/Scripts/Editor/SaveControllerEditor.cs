namespace Advant.Editor
{
    using UnityEditor;
    using UnityEngine;
    [CustomEditor(typeof(SaveController))]
    public class SaveControllerEditor : Editor
    {
        [SerializeField] private string language;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            GUILayout.Space(20);
            serializedObject.Update();
            SaveController script = (SaveController)target;
            GUILayout.Label("EDITOR");
            GUILayout.Space(20);
            if (GUILayout.Button("DELETE SAVE"))
            {
                Debug.Log("SETTINGS CLEARED");
                script.DeleteSave();
            }          
            serializedObject.ApplyModifiedProperties();
        }
    }
}