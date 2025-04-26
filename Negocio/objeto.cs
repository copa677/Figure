
using System.Text.Json.Serialization;

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

    // Constructor sin parámetros necesario para deserialización
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
    }
    public void RecalcularCentrosMasas()
    {
        foreach (var cara in Partes.Values)
        {
            cara.RecalcularCentrosMasas();
        }
    }
    private void copiar(Dictionary<string, Parte> _partes)
    {
        foreach (var kvp in _partes)
        {
            Partes.Add(kvp.Key, kvp.Value);
        }
    }
    
    public void Rotacion(char eje, float grado)
    {
        foreach (var parte in Partes.Values)
        {
            parte.Rotacion(eje, grado);
        }
    }
    
    public void Escalacion(float escala)
    {
        foreach (var parte in Partes.Values)
        {
            parte.Escalacion(escala);
        }
    }
    
    public void Traslacion(float x, float y, float z)
    {
        foreach (var parte in Partes.Values)
        {
            parte.Traslacion(x, y, z);
        }
    }
    
    public void actualizarCentrosMasas(float x, float y, float z)
    {
        this.Cx += x;
        this.Cy += y;
        this.Cz += z;
        foreach (var parte in Partes.Values)
        {
            parte.actualizarCentrosMasas(Cx, Cy, Cz);
        }
    }

    public void Inicializar()
    {
        foreach (var parte in Partes.Values)
            parte.Inicializar();
    }

    public void Render(int shaderProgram)
    {
        foreach (var parte in Partes.Values)
            parte.Render(shaderProgram);
    }
}