
public class Parte
{
    public List<Cara> Caras;
    public float cx,cy,cz;

    public Parte(List<Cara> caras, float x, float y, float z){
        this.Caras = new List<Cara>();
        copiar(caras);
        this.cx = x;
        this.cy = y;
        this.cz = z;
        foreach (var item in Caras)
        {
            item.actualizarCentrosMasas(cx,cy,cz);
        }
    }
    private void copiar(List<Cara> _caras){
        foreach (var item in _caras)
        {
            Caras.Add(item);
        }
    }

    public void actualizarCentrosMasas(float x, float y, float z){
        this.cx = cx + x;
        this.cy = cy + y;
        this.cz = cz + z;
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