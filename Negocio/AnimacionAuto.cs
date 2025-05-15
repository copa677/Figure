using OpenTK.Mathematics;

public class AnimacionAuto
{
    private Objeto _auto;
    private float _radio;
    private float _velocidad;
    private float _distancia;
    private bool _girando;
    private int fases;
    private float _anguloGiro;

    public AnimacionAuto()
    {
        _distancia = 28f;
        _velocidad = 0.1f;
        _radio = 10f;
        _girando = false;
        fases = 1;
    }

    public void CargarObjt(Objeto auto)
    {
        _auto = auto;
    }

    public void Animar()
    {
        if (_auto == null) return;
        Console.Write(fases);
        switch (fases)
        {
            case 1:
                _auto.Traslacion(0, 0, -_velocidad);
                _distancia -= _velocidad;
                if (_distancia <= 0)
                {
                    fases++;
                    _distancia = 26;
                }
                break;

            case 2:
                _distancia -= _velocidad;
                _auto.Traslacion(_velocidad - 0.081f, 0, -_velocidad);
                _auto.Rotacion(0, -_velocidad * 1.25f, 0);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 38f;
                }
                break;

            case 3:
                _distancia -= _velocidad;
                _auto.Traslacion(0.05f, 0, -0.025f);
                _auto.Rotacion(0, -0.01f, 0);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 23f;
                }
                break;
            case 4:
                _distancia -= _velocidad;
                _auto.Traslacion(0.05f, 0, _velocidad * 0.005f);
                _auto.Rotacion(0, _velocidad * -2.5f, 0);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 10f;
                }
                break;
            case 5:
                _distancia -= _velocidad;
                _auto.Traslacion(0.05f, 0, _velocidad * 0.005f);
                _auto.Rotacion(0, _velocidad * -2.0f, 0);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 10f;
                }
                break;
            case 6:
                _distancia -= _velocidad;
                _auto.Traslacion(0.05f, 0, _velocidad * 0.005f);
                _auto.Rotacion(0, _velocidad * -3.0f, 0);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 10f;
                }
                break;
            case 7:
                _distancia -= _velocidad;
                _auto.Traslacion(0.05f, 0, _velocidad * 0.005f);
                _auto.Rotacion(0, _velocidad * -1.5f, 0);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 50f;
                }
                break;
            case 8:
                _distancia -= _velocidad;
                _auto.Traslacion(0f, 0f, _velocidad);
                if (_distancia <= 0)
                {
                    fases++; _distancia = 40f;
                }
                break;
        }
    }
}