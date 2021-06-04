/// 
/// A class designed to initialize and manage CSAntSimulation compute shader
/// 
using UnityEngine;
using UnityEngine.UI;
using ComputeShaderExp.Math;
using ComputeShaderExp.Utils;

namespace ComputeShaderExp.AntSimulation
{
    public class AntSimulation : MonoBehaviour
    {
        [SerializeField] ComputeShader shader;
        [Header("Texture")]
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] RawImage image;
        [Header("Agents")]
        [SerializeField] int numAgents;
        [SerializeField] float moveSpeed;
        [SerializeField] float evaporateSpeed;
        [SerializeField] float diffuseSpeed;
        [SerializeField] float turnSpeed;
        [SerializeField] float senseDistance;
        [SerializeField] float senseAngle;
        [Header("Start Settings")]
        [SerializeField] int startCircleRadius;
        [SerializeField, Range(0, 360)] float startAngleRange;
        
        
        Agent[] agents;
        ComputeBuffer buffer;
        RenderTexture tex;
        RenderTexture diffuseTex;
        bool run;
        int updateKernel;
        int diffuseKernel;

        /// <summary>
        /// Basic agent structure
        /// </summary>
        struct Agent
        {
            public Vector2 position;
            public float angle;
        }

        private void OnEnable()
        {
            run = false;
            Init();
            
        }
        private void OnDisable()
        {
            buffer.Release();
        }

        private void Update()
        {
            if (run)
                RunShader();
        }

        /// <summary>
        /// Initialize data buffer, textures and pass static data to compute shader
        /// </summary>
        public void Init()
        {
            agents = new Agent[numAgents];
            var centerPos = MathLib.GetCenterRect(width, height);
            for (int i = 0; i < numAgents; i++)
            {
                agents[i].position = MathLib.GetCircleRandomPosition(startCircleRadius, centerPos.x, centerPos.y);
                agents[i].angle = Random.Range(0, startAngleRange * Mathf.Deg2Rad);
            }

            int stride = CSUtils.CalculateStride(agents[0]);

            CSUtils.FillBufferData(ref buffer, agents, numAgents, stride);

            tex = CSUtils.CreateRenderTexture(width, height, 24);
            diffuseTex = CSUtils.CreateRenderTexture(width, height, 24);

            updateKernel = shader.FindKernel("Update");
            diffuseKernel = shader.FindKernel("ProcessTrailMap");
            shader.SetBuffer(updateKernel, "agents", buffer);
            shader.SetInt("width", width);
            shader.SetInt("height", height);
            shader.SetInt("numAgents", numAgents);
            shader.SetFloat("PI", Mathf.PI);
            image.texture = tex;
            run = false;
        }

        /// <summary>
        /// Pause / Unpause the shader execution 
        /// </summary>
        public void OnOffShader()
        {
            run = !run;
        }

        /// <summary>
        /// Runtime function to be called for one compute shader iteration
        /// </summary>
        public void RunShader()
        {            
            shader.SetFloat("moveSpeed", moveSpeed);
            shader.SetFloat("deltaTime", Time.deltaTime);
            shader.SetFloat("evaporateSpeed", evaporateSpeed);
            shader.SetFloat("diffuseSpeed", diffuseSpeed);
            shader.SetFloat("turnSpeed", turnSpeed);
            shader.SetFloat("senseAngle", senseAngle);
            shader.SetFloat("senseDistance", senseDistance);

            shader.SetTexture(diffuseKernel, "TrailMap", tex);
            shader.SetTexture(diffuseKernel, "PreviousMap", diffuseTex);
            shader.SetTexture(updateKernel, "TrailMap", tex);

            shader.Dispatch(updateKernel, width / 16, height / 8, 1);
            shader.Dispatch(diffuseKernel, width / 16, height / 8, 1);

            Graphics.Blit(diffuseTex, tex);
        }
    }
}
