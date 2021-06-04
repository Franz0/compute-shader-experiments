///Editor class showing inspector buttons on AntSimulation monobehaviour
using UnityEditor;
using UnityEngine;

namespace ComputeShaderExp.AntSimulation
{
    [CustomEditor(typeof(AntSimulation))]
    public class AntSimulationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (AntSimulation)target;
            if (GUILayout.Button("Reset"))
            {
                script.Init();
            }

            if (GUILayout.Button("Run/Stop"))
            {
                script.OnOffShader();
            }

            if (GUILayout.Button("NextFrame"))
            {
                script.RunShader();
            }
        }
    }
}
