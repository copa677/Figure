using System.Text.Json.Serialization;

public class Escenario
{
    [JsonPropertyName("objetos")]
    public List<Objeto> Objetos { get; set; }
    
    [JsonPropertyName("cx")]
    public float Cx { get; set; }
    
    [JsonPropertyName("cy")]
    public float Cy { get; set; }
    
    [JsonPropertyName("cz")]
    public float Cz { get; set; }

    // Constructor sin parámetros necesario para deserialización
    public Escenario()
    {
        Objetos = new List<Objeto>();
    }

    public Escenario(List<Objeto> objetos, float x, float y, float z)
    {
        this.Objetos = new List<Objeto>();
        copiar(objetos);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
        foreach (var item in this.Objetos)
        {
            item.actualizarCentrosMasas(Cx, Cy, Cz);
        }
    }
    
    private void copiar(List<Objeto> objetos){
        foreach (var item in objetos)
        {
            Objetos.Add(item);
        }
    }
    public void Rotacion(char eje, float grado){
        foreach (var item in Objetos)
        {
            item.Rotacion(eje,grado);
        }
    }
    public void Escalacion(float x, float y, float z){
        foreach (var item in Objetos)
        {
            item.Escalacion(x,y,z);
        }
    }   
    public void Traslacion(float x, float y, float z){
        foreach (var item in Objetos)
        {
            item.Traslacion(x,y,z);
        }
    }   
    public void Inicializar()
    {
        foreach (var obj in Objetos)
            obj.Inicializar();
    }

    public void Render(int shaderProgram)
    {
        foreach (var obj in Objetos)
            obj.Render(shaderProgram);
    }
}