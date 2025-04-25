using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing.Drawing2D;
using System.Text.Json.Serialization;

public class Cara
{
    [JsonPropertyName("vertices")]
    //public float[] _vertices { get; set; }
    public Dictionary<string, float[]> _vertices { get; set; }

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
        _vertices = new Dictionary<string, float[]>();
        _modelo = Matrix4.Identity;
    }


    public Cara(String name, List<Vertice> vertices, float x, float y, float z)
    {
        _vertices = new Dictionary<string, float[]>();
        CargarVertices(vertices, name);
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
        _modelo = rotacion * _modelo;
    }
    public void Escalacion(float x, float y, float z)
    {
        _modelo = Matrix4.CreateScale(x, y, z) * _modelo;
    }
    private void CargarVertices(List<Vertice> vertices, string identificador)
    {
        float[] verticesArray = new float[vertices.Count * 6];

        for (int i = 0; i < vertices.Count; i++)
        {
            // Posición
            verticesArray[i * 6] = vertices[i].Posicion.X;
            verticesArray[i * 6 + 1] = vertices[i].Posicion.Y;
            verticesArray[i * 6 + 2] = vertices[i].Posicion.Z;

            // Color
            verticesArray[i * 6 + 3] = vertices[i].Color.X;
            verticesArray[i * 6 + 4] = vertices[i].Color.Y;
            verticesArray[i * 6 + 5] = vertices[i].Color.Z;
        }

        _vertices[identificador] = verticesArray;
    }

    public void Inicializar()
    {
        _vao = GL.GenVertexArray();
        _vbo = GL.GenBuffer();

        // Combinar todos los vértices del diccionario en un solo array
        List<float> todosVertices = new List<float>();
        foreach (var verticeArray in _vertices.Values)
        {
            todosVertices.AddRange(verticeArray);
        }
        float[] verticesCombinados = todosVertices.ToArray();

        GL.BindVertexArray(_vao);
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, verticesCombinados.Length * sizeof(float), verticesCombinados, BufferUsageHint.StaticDraw);

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

    public void Render(int shaderProgram)
    {
        // Combinar todos los vértices del diccionario en un solo array
        List<float> todosVertices = new List<float>();
        foreach (var verticeArray in _vertices.Values)
        {
            todosVertices.AddRange(verticeArray);
        }
        float[] verticesCombinados = todosVertices.ToArray();

        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(_vao);

        int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
        GL.UniformMatrix4(modelLocation, false, ref _modelo);

        GL.DrawArrays(PrimitiveType.Lines, 0, verticesCombinados.Length / 6);
        GL.BindVertexArray(0);
    }
}

