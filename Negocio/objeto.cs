
using System.Text.Json.Serialization;
using OpenTK.Mathematics;

public class Objeto
{
    [JsonPropertyName("partes")]
    public Dictionary<string, Parte> Partes { get; set; }

    [JsonPropertyName("cx")]
    public float Cx { get; set; }

    [JsonPropertyName("cy")]
    public float Cy { get; set; }

    [JsonPropertyName("cz")]
    public float Cz { get; set; }
    public Transformaciones Transform { get; set; } = new Transformaciones();
    [JsonIgnore]
    public Vector3 centroDeMasa { get; set; }
    
    public Objeto()
    {
        Partes = new Dictionary<string, Parte>();
    }

    public Objeto(Dictionary<string, Parte> partes, float x, float y, float z)
    {
        this.Partes = new Dictionary<string, Parte>();
        copiar(partes);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
        centroDeMasa = CalcularCentro();
        Traslacion(Cx,Cy,Cz);
        RecalcularCentroDeMasa();
    }
    public Vector3 CalcularCentro()
    {
        var _vert = Partes.Values
        .SelectMany(fig => fig.Caras.Values
            .SelectMany(c => c._vertices.Values));
        return Transform.CalcularCentro(_vert);
    }

    public void RecalcularCentroDeMasa()
    {
        centroDeMasa = CalcularCentro();
        foreach (var parte in Partes.Values)
            parte.RecalcularCentroDeMasa();
    }
    private void copiar(Dictionary<string, Parte> _partes)
    {
        foreach (var kvp in _partes)
        {
            Partes.Add(kvp.Key, kvp.Value);
        }
    }

    public void Rotacion(float xDeg, float yDeg, float zDeg)
    {
        Transform.RotateA(centroDeMasa, xDeg, yDeg, zDeg);
    }

    public void Escalacion(float f)
    {
        Transform.Position -= centroDeMasa;
        Transform.Escalate(f);
        Transform.Position += centroDeMasa;
    }

    public void Traslacion(float dx, float dy, float dz)
    {
        Transform.Transladate(dx, dy, dz);
    }

    public void Inicializar()
    {
        foreach (var parte in Partes.Values)
            parte.Inicializar();
    }

    public void Render(int shaderProgram, Matrix4 acumulada)
    {
        Matrix4 local = Transform.GetMatrix(centroDeMasa);
        Matrix4 a = local * acumulada;
        foreach (var parte in Partes.Values)
            parte.Render(shaderProgram,a);
    }
}