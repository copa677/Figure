using System.Security.Cryptography.X509Certificates;

public class Objeto
{
    public List<Parte> Partes ;
    public  float cx,cy,cz;

    public Objeto(List<Parte> partes, float x, float y, float z){
        this.Partes = new List<Parte>();
        copiar(partes);
        this.cx = x;
        this.cy = y;
        this.cz = z;
        foreach (var item in Partes)
        {
            item.actualizarCentrosMasas(cx,cy,cz);
        }
    }
    private void copiar(List<Parte> _partes){
        foreach (var item in _partes)
        {
            Partes.Add(item);
        }
    }

    public void actualizarCentrosMasas(float x, float y, float z){
        this.cx = cx + x;
        this.cy = cy + y;
        this.cz = cz + z;
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