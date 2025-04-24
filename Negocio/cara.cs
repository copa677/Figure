using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

public class Cara
{
    [JsonPropertyName("vertices")]
    public float[] _vertices { get; set; }

    [JsonPropertyName("cx")]
    public float cx { get; set; }

    [JsonPropertyName("cy")]
    public float cy { get; set; }

    [JsonPropertyName("cz")]
    public float cz { get; set; }

    // Campos no serializados
    [JsonIgnore]
    private int _vao, _vbo;
    [JsonIgnore]
    private Matrix4 _modelo;


    public Cara()
    {
        _modelo = Matrix4.Identity;
    }


    public Cara(List<Vertice> vertices, float x, float y, float z)
    {
        this._vertices = new float[vertices.Count * 6];
        CargarVertices(vertices);
        this.cx = x;
        this.cy = y;
        this.cz = z;
        _modelo = Matrix4.Identity;
    }

    public void Traslacion(float x, float y, float z)
    {
        _modelo = _modelo * Matrix4.CreateTranslation(x, y, z);
    }
    public void Rotacion(char eje, float grado)
    {
        
        Matrix4 rotacion = Matrix4.Identity;

        switch (eje)
        {
            case 'x':
                rotacion = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(grado));
                break;
            case 'y':
                rotacion = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(grado));
                break;
            case 'z':
                rotacion = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(grado));
                break;
        }

        // Orden correcto: traslacionVuelta * rotacion * traslacionOrigen
        _modelo =  rotacion * _modelo;
    }
    public void Escalacion(float x, float y, float z)
    {
        _modelo = Matrix4.CreateScale(x, y, z) * _modelo;
    }
    private void CargarVertices(List<Vertice> vertices)
    {
        for (int i = 0; i < vertices.Count; i++)
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

    public void actualizarCentrosMasas(float x, float y, float z)
    {
        this.cx += x;
        this.cy += y;
        this.cz += z;
        _modelo = Matrix4.CreateTranslation(cx, cy, cz);
    }

    public void actualizarVertices()
    {
        for (int i = 0; i < _vertices.Length; i += 6)
        {
            _vertices[i] += cx;  // x
            _vertices[i + 1] += cy;  // y
            _vertices[i + 2] += cz;  // z
        }

    }

    public void Render(int shaderProgram)
    {
        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(_vao);

        int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
        GL.UniformMatrix4(modelLocation, false, ref _modelo);

        GL.DrawArrays(PrimitiveType.Lines, 0, _vertices.Length/6);
        GL.BindVertexArray(0);
    }
}

