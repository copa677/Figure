using System.Text.Json.Serialization;
using System.Collections.Generic;
using OpenTK.Mathematics;

public class Escenario
{
    [JsonPropertyName("objetos")]
    public Dictionary<string, Objeto> Objetos { get; set; }

    [JsonPropertyName("cx")]
    public float Cx { get; set; }

    [JsonPropertyName("cy")]
    public float Cy { get; set; }

    [JsonPropertyName("cz")]
    public float Cz { get; set; }
    public Transformaciones Transform { get; } = new Transformaciones();
    [JsonIgnore]
    public Vector3 centroDeMasa { get; set; }

    public Escenario()
    {
        Objetos = new Dictionary<string, Objeto>();
    }

    public Escenario(Dictionary<string, Objeto> objetos, float x, float y, float z)
    {
        this.Objetos = new Dictionary<string, Objeto>();
        copiar(objetos);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
        Traslacion(Cx,Cy,Cz);
        RecalcularCentroDeMasa();
    }
    public Vector3 CalcularCentro()
    {
        var _vert = Objetos.Values
            .SelectMany(obj => obj.Partes.Values
                .SelectMany(fig => fig.Caras.Values
                    .SelectMany(c => c._vertices.Values)));
        return Transform.CalcularCentro(_vert);
    }
    public void RecalcularCentroDeMasa()
    {
        centroDeMasa = CalcularCentro();
        foreach (var obj in Objetos.Values)
            obj.RecalcularCentroDeMasa();
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
    private void copiar(Dictionary<string, Objeto> objetos)
    {
        foreach (var kvp in objetos)
        {
            Objetos.Add(kvp.Key, kvp.Value);
        }
    }



    public void Inicializar()
    {
        foreach (var obj in Objetos.Values)
            obj.Inicializar();
    }

    public void Render(int shaderProgram, Matrix4 acumulada)
    {
        Matrix4 local = Transform.GetMatrix(centroDeMasa);
        Matrix4 a = local * acumulada;
        foreach (var obj in Objetos.Values)
            obj.Render(shaderProgram,a);
    }
}