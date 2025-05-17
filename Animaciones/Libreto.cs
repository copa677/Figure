using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Threading;
using OpenTK.Mathematics;

public class Libreto
{
    [JsonIgnore]
    public Escenario Escenario { get; set; }
    public List<InstruccionAnimacion> Instrucciones { get; set; } = new List<InstruccionAnimacion>();
    [JsonIgnore]
    private Thread hilo;
    [JsonIgnore]
    private Stopwatch reloj = new Stopwatch();
    [JsonIgnore]
    private bool activo = false;

    public Libreto()
    {
        Instrucciones = new List<InstruccionAnimacion>();
    }

    public Libreto(Escenario escenario)
    {
        this.Escenario = escenario;
    }

    public void AgregarInstruccion(InstruccionAnimacion instruccion)
    {
        Instrucciones.Add(instruccion);
    }

    public void Iniciar()
    {
        if (activo) return;

        activo = true;
        reloj.Restart();
        // Reiniciar t previos
        foreach (var instr in Instrucciones)
        {
            instr.UltimoT = 0f;
        }
        hilo = new Thread(() =>
        {
            while (activo)
            {
                float tiempoActual = reloj.ElapsedMilliseconds / 1000f;

                foreach (var instr in Instrucciones)
                {
                    if (tiempoActual >= instr.TiempoInicio && tiempoActual <= instr.TiempoFin)
                    {
                        if (Escenario.Objetos.TryGetValue(instr.NombreObjeto, out var obj))
                        {
                            float duracion = instr.TiempoFin - instr.TiempoInicio;
                            float t = (tiempoActual - instr.TiempoInicio) / duracion;
                            float deltaT = t - instr.UltimoT;
                            instr.UltimoT = t;

                            float valX = instr.X * deltaT;
                            float valY = instr.Y * deltaT;
                            float valZ = instr.Z * deltaT;

                            switch (instr.Tipo)
                            {
                                case TipoTransformacion.Traslacion:
                                    obj.Traslacion(valX, valY, valZ);
                                    break;

                                case TipoTransformacion.Rotacion:
                                    obj.Rotacion(valX, valY, valZ);
                                    break;

                                case TipoTransformacion.Escalacion:
                                    float escala = 1.0f + ((instr.X - 1.0f) * t); // Supone escala relativa
                                    obj.Escalacion(escala);
                                    break;
                            }
                        }
                    }
                }

                Thread.Sleep(16); // ~60 FPS
            }

            reloj.Stop();
        });

        hilo.Start();
    }

    public void Detener()
    {
        activo = false;
        hilo?.Join();
    }
}
