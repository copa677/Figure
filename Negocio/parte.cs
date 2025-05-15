
using System.Text.Json.Serialization;
using OpenTK.Mathematics;

public class Parte
{
    [JsonPropertyName("caras")]
    public Dictionary<string, Cara> Caras { get; set; }

    [JsonPropertyName("cx")]
    public float Cx { get; set; }

    [JsonPropertyName("cy")]
    public float Cy { get; set; }

    [JsonPropertyName("cz")]
    public float Cz { get; set; }
    public Transformaciones Transform { get; } = new Transformaciones();
    [JsonIgnore]
    public Vector3 centroDeMasa { get; set; }

    // Constructor sin parámetros necesario para deserialización
    public Parte()
    {
        Caras = new Dictionary<string, Cara>();
    }

    public Parte(Dictionary<string, Cara> caras, float x, float y, float z)
    {
        this.Caras = new Dictionary<string, Cara>();
        copiar(caras);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
        centroDeMasa = CalcularCentro();
    }

    public Vector3 CalcularCentro()
    {
        var vert = Caras.Values.SelectMany(c => c._vertices.Values);
        return Transform.CalcularCentro(vert);
    }
    private void copiar(Dictionary<string, Cara> _caras)
    {
        foreach (var kvp in _caras)
        {
            Caras.Add(kvp.Key, kvp.Value);
        }
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

    public void Traslacion(float x, float y, float z)
    {
        Transform.Transladate(x, y, z);
    }
    
    public void RecalcularCentroDeMasa()
    {
        centroDeMasa = CalcularCentro();
        foreach (var cara in Caras.Values)
            cara.RecalcularCentroDeMasa();
    }

    public void Inicializar()
    {
        foreach (var cara in Caras.Values)
            cara.Inicializar();
    }

    public void Render(int shaderProgram, Matrix4 acumulada)
    {
        Matrix4 local = Transform.GetMatrix(centroDeMasa);
        Matrix4 a = local * acumulada;
        foreach (var cara in Caras.Values)
            cara.Render(shaderProgram,a);
    }
}