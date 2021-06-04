using UnityEngine;

namespace ComputeShaderExp.Utils
{
    public static class CSUtils
    {
        public static int CalculateStride(object obj)
        {
            object value = null;
            int size = 0;
            System.Type type = obj.GetType();
            System.Reflection.FieldInfo[] info = type.GetFields();
            foreach (System.Reflection.FieldInfo field in info)
            {
                value = field.GetValue(obj);
                size += System.Runtime.InteropServices.Marshal.SizeOf(value);
            }
            return size;
        }

        public static void FillBufferData(ref ComputeBuffer buffer, System.Array array, int numAgents, int stride)
        {
            buffer?.Dispose();
            buffer = new ComputeBuffer(numAgents, stride);
            buffer.SetData(array);
        }

        public static RenderTexture CreateRenderTexture(int width, int height, int depth)
        {
            var tex = new RenderTexture(width, height, 24);
            tex.enableRandomWrite = true;
            tex.Create();
            return tex;
        }
    }
}
