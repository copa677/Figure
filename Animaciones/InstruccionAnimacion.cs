public class InstruccionAnimacion
{
    public string NombreObjeto { get; set;}
    public float X { get; set;}
    public float Y { get; set;}
    public float Z { get; set;}
    public float TiempoInicio { get; set;}
    public float TiempoFin { get; set;}
    public TipoTransformacion Tipo { get; set;}
    public InstruccionAnimacion() {}
    public InstruccionAnimacion(string nombreObjeto, float x, float y, float z, float tiempoInicio, float tiempoFin, TipoTransformacion tipo)
    {
        NombreObjeto = nombreObjeto;
        X = x;
        Y = y;
        Z = z;
        TiempoInicio = tiempoInicio;
        TiempoFin = tiempoFin;
        Tipo = tipo;
    }
}
