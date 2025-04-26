using System.Text.Json.Serialization;
using System.Collections.Generic;

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

    
    public Escenario()
    {
        Objetos = new Dictionary<string, Objeto>();
        RecalcularCentrosMasas();
    }
    
    public Escenario(Dictionary<string, Objeto> objetos, float x, float y, float z)
    {
        this.Objetos = new Dictionary<string, Objeto>();
        copiar(objetos);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
        foreach (var item in this.Objetos.Values)
        {
            item.actualizarCentrosMasas(Cx, Cy, Cz);
        }
    }
    public void RecalcularCentrosMasas()
    {
        foreach (var cara in Objetos.Values)
        {
            cara.RecalcularCentrosMasas();
        }
    }
    private void copiar(Dictionary<string, Objeto> objetos)
    {
        foreach (var kvp in objetos)
        {
            Objetos.Add(kvp.Key, kvp.Value);
        }
    }
    
    public void Rotacion(char eje, float grado)
    {
        foreach (var item in Objetos.Values)
        {
            item.Rotacion(eje, grado);
        }
    }
    
    public void Escalacion(float escala)
    {
        foreach (var item in Objetos.Values)
        {
            item.Escalacion(escala);
        }
    }
    
    public void Traslacion(float x, float y, float z)
    {
        foreach (var item in Objetos.Values)
        {
            item.Traslacion(x, y, z);
        }
    }
    
    public void Inicializar()
    {
        foreach (var obj in Objetos.Values)
            obj.Inicializar();
    }

    public void Render(int shaderProgram)
    {
        foreach (var obj in Objetos.Values)
            obj.Render(shaderProgram);
    }
}