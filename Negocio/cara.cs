using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

public class Cara
{
    private int _vao, _vbo;
    public float[] _vertices;
    public float cx,cy,cz;
    private Matrix4 _modelo;
    public Cara(List<Vertice> vertices, float x, float y, float z)
    {
        this._vertices = new float[vertices.Count * 6];
        CargarVertices(vertices);
        this.cx = x;
        this.cy = y;
        this.cz = z;
        _modelo = Matrix4.Identity;
    }

    private void CargarVertices(List<Vertice> vertices)
    {
        for(int i = 0; i < vertices.Count; i++)
        {
            // Posición
            _vertices[i * 6] = vertices[i].Posicion.X;
            _vertices[i * 6 + 1] = vertices[i].Posicion.Y;
            _vertices[i * 6 + 2] = vertices[i].Posicion.Z;
            
            // Color
            _vertices[i * 6 + 3] = vertices[i].Color.X;
            _vertices[i * 6 + 4] = vertices[i].Color.Y;
            _vertices[i * 6 + 5] = vertices[i].Color.Z;
        }
    }

    public void Inicializar()
    {
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindVertexArray(0);
    }

    public void actualizarCentrosMasas(float x, float y, float z){
        this.cx = cx + x;
        this.cy = cy + y;
        this.cz = cz + z;
        actualizarVertices();
    }

    public void actualizarVertices(){
        for (int i = 0; i < _vertices.Length; i += 6)
        {
            _vertices[i] += cx;  // x
            _vertices[i + 1] += cy;  // y
            _vertices[i + 2] += cz;  // z
        }
    }
    
    public void SetTransform(Matrix4 transform)
    {
        _modelo = transform;
    }

    public void Render(int shaderProgram)
    {
        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(_vao);

        int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
        GL.UniformMatrix4(modelLocation, false, ref _modelo);

        GL.DrawArrays(PrimitiveType.Lines, 0, 72);
        GL.BindVertexArray(0);
    }
}

