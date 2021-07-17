///Editor class showing inspector buttons on MeshModifier MonoBehaviour
using UnityEditor;
using UnityEngine;

namespace ComputeShaderExp.MeshModifier
{
    [CustomEditor(typeof(MeshModifier))]
    public class MeshModifierEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (MeshModifier)target;
            if (GUILayout.Button("Run"))
            {
                script.Run();
            }            
        }
    }
}
