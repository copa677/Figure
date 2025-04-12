public class Escenario
{
    public List<Objeto> Objetos;
    public float cx,cy,cz;

    public Escenario(List<Objeto> objetos, float x, float y, float z){
        this.Objetos = new List<Objeto>();
        copiar(objetos);
        this.cx = x;
        this.cy = y;
        this.cz = z;
        foreach (var item in this.Objetos)
        {
            item.actualizarCentrosMasas(cx,cy,cz);
        }
    }
    
    private void copiar(List<Objeto> objetos){
        foreach (var item in objetos)
        {
            Objetos.Add(item);
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