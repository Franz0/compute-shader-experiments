using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ComputeShaderExp.Utils;

namespace ComputeShaderExp.MeshModifier
{
    public class MeshModifier : MonoBehaviour
    {
        [SerializeField] ComputeShader _shader = null;
        [SerializeField] MeshFilter _filter = null;
        ComputeBuffer _bufferVertices;
        int _updateKernel;

        Vector3[] _vertices;        

        public void Run()
        {
            _vertices = _filter.mesh.vertices;

            int strideVertices = System.Runtime.InteropServices.Marshal.SizeOf(_vertices[0]);

            CSUtils.FillBufferData(ref _bufferVertices, _vertices, _vertices.Length, strideVertices);

            Debug.Log("StrideVertices = " + strideVertices);

            _updateKernel = _shader.FindKernel("CSUpdate");
            _shader.SetBuffer(_updateKernel, "vertices", _bufferVertices);

            _shader.Dispatch(_updateKernel, _vertices.Length, 1, 1);

            var outputVertices = new Vector3[_vertices.Length];
            _bufferVertices.GetData(outputVertices);

            _filter.mesh.SetVertices(outputVertices);
        }
    }
}
