using OpenTK.Graphics.OpenGL4;

public class Shader : IDisposable
{
    public int Program { get; private set; }
    
    public Shader(string vertexShader, string fragmentShader)
    {
        int vertex = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertex, vertexShader);
        GL.CompileShader(vertex);
        
        int fragment = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragment, fragmentShader);
        GL.CompileShader(fragment);
        
        Program = GL.CreateProgram();
        GL.AttachShader(Program, vertex);
        GL.AttachShader(Program, fragment);
        GL.LinkProgram(Program);
        
        GL.DetachShader(Program, vertex);
        GL.DetachShader(Program, fragment);
        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);
    }
    
    public void Use()
    {
        GL.UseProgram(Program);
    }
    
    public int GetUniformLocation(string name)
    {
        return GL.GetUniformLocation(Program, name);
    }
    
    public void Dispose()
    {
        GL.DeleteProgram(Program);
    }
}