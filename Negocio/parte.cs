using System.Text.Json.Serialization;
public class Parte
{
    [JsonPropertyName("caras")]
    public List<Cara> Caras { get; set; }
    
    [JsonPropertyName("cx")]
    public float Cx { get; set; }
    
    [JsonPropertyName("cy")]
    public float Cy { get; set; }
    
    [JsonPropertyName("cz")]
    public float Cz { get; set; }

    // Constructor sin parámetros necesario para deserialización
    public Parte()
    {
        Caras = new List<Cara>();
    }

    public Parte(List<Cara> caras, float x, float y, float z)
    {
        this.Caras = new List<Cara>();
        copiar(caras);
        this.Cx = x;
        this.Cy = y;
        this.Cz = z;
    }
    private void copiar(List<Cara> _caras){
        foreach (var item in _caras)
        {
            Caras.Add(item);
        }
    }

    public void Rotacion(char eje, float grado){
        foreach (var item in Caras)
        {
            item.Rotacion(eje,grado);
        }
    }
    public void Escalacion(float x, float y, float z){
        foreach (var item in Caras)
        {
            item.Escalacion(x,y,z);
        }
    }   
    public void Traslacion(float x, float y, float z){
        foreach (var item in Caras)
        {
            item.Traslacion(x,y,z);
        }
    }   
    public void actualizarCentrosMasas(float x, float y, float z)
    {
        this.Cx += x;
        this.Cy += y;
        this.Cz += z;
        foreach (var item in Caras)
        {
            item.actualizarCentrosMasas(Cx, Cy, Cz);
        }
    }

    public void Inicializar()
    {
        foreach (var cara in Caras)
            cara.Inicializar();
    }

    public void Render( int shaderProgram)
    {
        foreach (var cara in Caras)
            cara.Render( shaderProgram);
    }
}