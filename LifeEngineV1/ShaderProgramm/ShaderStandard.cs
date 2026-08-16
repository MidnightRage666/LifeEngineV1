using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using System.IO;

namespace LifeEngineV1.ShaderProgramm
{
    public static class ShaderStandard
    {
        private static int _shaderId = -1;

        private static int _vertexShaderId = -1;
        private static int _fragmentShaderId = -1;

        public static void Init()
        {
            _shaderId = GL.CreateProgram();

            Assembly a = Assembly.GetExecutingAssembly();

            // Vertex Shader auslesen
            Stream sVertex = a.GetManifestResourceStream("LifeEngineV1.ShaderProgramm.shaderStandard_vertex.glsl");
            StreamReader sReaderVertex = new StreamReader(sVertex);
            string sVertexCode = sReaderVertex.ReadToEnd();
            sReaderVertex.Dispose();
            sVertex.Close();

            // Fragment Shader auslesen
            Stream sFragment = a.GetManifestResourceStream("LifeEngineV1.ShaderProgramm.shaderStandard_fragment.glsl");
            StreamReader sReaderFragment = new StreamReader(sFragment);
            string sFragmentCode = sReaderFragment.ReadToEnd();
            sReaderFragment.Dispose();
            sFragment.Close();

            _vertexShaderId = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(_vertexShaderId, sVertexCode);

            _fragmentShaderId = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(_fragmentShaderId, sFragmentCode);

            GL.CompileShader(_vertexShaderId);
            GL.AttachShader(_shaderId, _vertexShaderId);

            GL.CompileShader(_fragmentShaderId);
            GL.AttachShader(_shaderId, _fragmentShaderId);

            GL.LinkProgram(_shaderId);


            CheckShaderStatus(_shaderId, _vertexShaderId, _fragmentShaderId);
        }

        private static void CheckShaderStatus(int programId, int vertexShaderId, int fragmentShaderId, int geometryShaderId = -1, int tessControlShaderId = -1, int tessEvalShaderId = -1)
        {
            GL.GetProgram(programId, GetProgramParameterName.LinkStatus, out int linkStatus);
            if (linkStatus != 1)
            {
                GL.GetProgram(programId, GetProgramParameterName.InfoLogLength, out int logLength);
                if (logLength > 0)
                {
                    string msg = GL.GetProgramInfoLog(programId);
                    Console.WriteLine("[ProgramLog] " + msg);
                }
            }


            string vMsg = "";
            string fMsg = "";
            GL.GetShader(vertexShaderId, ShaderParameter.CompileStatus, out int vertexStatus);
            GL.GetShader(fragmentShaderId, ShaderParameter.CompileStatus, out int fragmentStatus);
            if (vertexStatus == 0 || fragmentStatus == 0)
            {
                if (vertexStatus == 0)
                {
                    vMsg = GL.GetShaderInfoLog(vertexShaderId);
                    Console.WriteLine("[ShaderVertex] " + vMsg);
                }
                if (fragmentStatus == 0)
                {
                    fMsg = GL.GetShaderInfoLog(fragmentShaderId);
                    Console.WriteLine("[ShaderFragment] " + fMsg);
                }
            }

            if (geometryShaderId > 0)
            {
                string gMsg = "";
                GL.GetShader(geometryShaderId, ShaderParameter.CompileStatus, out int geometryStatus);
                if (geometryStatus == 0)
                {
                    gMsg = GL.GetShaderInfoLog(geometryShaderId);
                    Console.WriteLine("[ShaderGeometry] " + gMsg);
                }
            }

            if (tessControlShaderId > 0)
            {
                string gMsg = "";
                GL.GetShader(tessControlShaderId, ShaderParameter.CompileStatus, out int tcStatus);
                if (tcStatus == 0)
                {
                    gMsg = GL.GetShaderInfoLog(tessControlShaderId);
                    Console.WriteLine("[ShaderTessC] " + gMsg);
                }
            }

            if (tessEvalShaderId > 0)
            {
                string gMsg = "";
                GL.GetShader(tessControlShaderId, ShaderParameter.CompileStatus, out int teStatus);
                if (teStatus == 0)
                {
                    gMsg = GL.GetShaderInfoLog(tessEvalShaderId);
                    Console.WriteLine("[ShaderTessE] " + gMsg);
                }
            }
        }

        public static int GetShaderId()
        {
            return _shaderId;
        }
    }
}
