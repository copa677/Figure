using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Text.Json.Serialization;

public class Cara
{
    [JsonPropertyName("vertices")]
    public Dictionary<string, float[]> _vertices { get; set; }

    [JsonPropertyName("cx")]
    public float cx { get; set; }

    [JsonPropertyName("cy")]
    public float cy { get; set; }

    [JsonPropertyName("cz")]
    public float cz { get; set; }

    public Vector3 centroDeMasa { get; set; }
    public Transformaciones Transform { get; } = new Transformaciones();
    // Campos no serializados
    [JsonIgnore]
    private int _vao, _vbo;


    public Cara()
    {
        _vertices = new Dictionary<string, float[]>();
    }


    public Cara(String name, List<Vertice> vertices, float x, float y, float z)
    {
        _vertices = new Dictionary<string, float[]>();
        CargarVertices(vertices, name);
        this.cx = x;
        this.cy = y;
        this.cz = z;

        centroDeMasa = CalcularCentro();
    }
    public Vector3 CalcularCentro() => Transform.CalcularCentro(_vertices.Values);

    public void Traslacion(float x, float y, float z)
    {
        Transform.Transladate(x, y, z);
    }
    public void Rotacion(float xDeg, float yDeg, float zDeg)
    {
        Transform.RotateA(centroDeMasa, xDeg, yDeg, zDeg);
    }
    public void Escalacion(float escala)
    {
        Transform.Position -= centroDeMasa;
        Transform.Escalate(escala);
        Transform.Position += centroDeMasa;
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


    public void RecalcularCentroDeMasa()
    {
        centroDeMasa = CalcularCentro();
    }

    public void Render(int shaderProgram, Matrix4 acumulada)
    {
        Matrix4 local = Transform.GetMatrix(centroDeMasa);
        Matrix4 final = local * acumulada;
        List<float> todosVertices = new List<float>();
        foreach (var verticeArray in _vertices.Values)
        {
            todosVertices.AddRange(verticeArray);
        }
        float[] verticesCombinados = todosVertices.ToArray();

        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(_vao);

        int modelLocation = GL.GetUniformLocation(shaderProgram, "model");
        GL.UniformMatrix4(modelLocation, false, ref final);

        GL.DrawArrays(PrimitiveType.Triangles, 0, verticesCombinados.Length / 6);
        GL.BindVertexArray(0);
    }
}
