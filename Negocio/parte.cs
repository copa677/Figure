using System.Text.Json.Serialization;

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
    }
    private void copiar(Dictionary<string, Cara> _caras)
    {
        foreach (var kvp in _caras)
        {
            Caras.Add(kvp.Key, kvp.Value);
        }
    }

    public void Rotacion(char eje, float grado)
    {
        foreach (var cara in Caras.Values)
        {
            cara.Rotacion(eje, grado);
        }
    }

    public void Escalacion(float escala)
    {
        foreach (var cara in Caras.Values)
        {
            cara.Escalacion(escala);
        }
    }

    public void Traslacion(float x, float y, float z)
    {
        foreach (var cara in Caras.Values)
        {
            cara.Traslacion(x, y, z);
        }
    }
    public void actualizarCentrosMasas(float x, float y, float z)
    {
        this.Cx += x;
        this.Cy += y;
        this.Cz += z;
        foreach (var cara in Caras.Values)
        {
            cara.actualizarCentrosMasas(Cx, Cy, Cz);
        }
    }
    public void RecalcularCentrosMasas()
    {
        foreach (var cara in Caras.Values)
        {
            cara.RecalcularCentrosMasas();
        }
    }

    public void Inicializar()
    {
        foreach (var cara in Caras.Values)
            cara.Inicializar();
    }

    public void Render(int shaderProgram)
    {
        foreach (var cara in Caras.Values)
            cara.Render(shaderProgram);
    }
}