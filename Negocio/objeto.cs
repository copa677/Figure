using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

public class Objeto
{
    [JsonPropertyName("partes")]
    public List<Parte> Partes { get; set; }
    
    [JsonPropertyName("cx")]
    public float Cx { get; set; }
    
    [JsonPropertyName("cy")]
    public float Cy { get; set; }
    
    [JsonPropertyName("cz")]
    public float Cz { get; set; }

    // Constructor sin parámetros necesario para deserialización
    public Objeto()
    {
        Partes = new List<Parte>();
    }

    public Objeto(List<Parte> partes, float x, float y, float z)
    {
        this.Partes = new List<Parte>();
        copiar(partes);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
        foreach (var item in Partes)
        {
            item.actualizarCentrosMasas(Cx, Cy, Cz);
        }
    }
    private void copiar(List<Parte> _partes){
        foreach (var item in _partes)
        {
            Partes.Add(item);
        }
    }
    public void Rotacion(char eje, float grado){
        foreach (var item in Partes)
        {
            item.Rotacion(eje,grado);
        }
    }
    public void Escalacion(float x, float y, float z){
        foreach (var item in Partes)
        {
            item.Escalacion(x,y,z);
        }
    }   
    public void Traslacion(float x, float y, float z){
        foreach (var item in Partes)
        {
            item.Traslacion(x,y,z);
        }
    }   
    public void actualizarCentrosMasas(float x, float y, float z){
        this.Cx += x;
        this.Cy += y;
        this.Cz += z;
    }

    public void Inicializar()
    {
        foreach (var parte in Partes)
            parte.Inicializar();
    }

    public void Render(int shaderProgram)
    {
        foreach (var parte in Partes)
            parte.Render( shaderProgram);
    }
}