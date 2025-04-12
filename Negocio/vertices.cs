using OpenTK.Mathematics;
public class Vertice
{
    public Vector3 Posicion { get; set; }
    public Vector3 Color { get; set; }

    public Vertice(float x, float y, float z, float r, float g, float b)
    {
        Posicion = new Vector3(x, y, z);
        Color = new Vector3(r, g, b);
        
    }
}