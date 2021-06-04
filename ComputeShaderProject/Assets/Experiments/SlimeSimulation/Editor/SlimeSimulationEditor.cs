///Editor class showing inspector buttons on AntSimulation monobehaviour
using UnityEditor;
using UnityEngine;

namespace ComputeShaderExp.AntSimulation
{
    [CustomEditor(typeof(SlimeSimulation))]
    public class SlimeSimulationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (SlimeSimulation)target;
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
