using System.Numerics;
using ImGuiNET;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class UIEditor
{
    private int selectedObjeto = 0;
    private int selectedParte = 0;
    private int tipoTransformacion = 0; // 0: Rotar, 1: Trasladar, 2: Escalar
    private int ejeSeleccionado = 3;

    private float x = 0f;
    private float y = 0f;
    private float z = 0f;
    private float anguloRotacion = 0f;
    private float factorEscala = 1.0f;

    private List<string> objetos = new();
    private List<string> partes = new();
    private char[] ejeRotacion = new char[] { 'x', 'y', 'z' };

    public int TipoTransformacion => tipoTransformacion;
    public int EjeSeleccionado => ejeSeleccionado;
    public bool SeleccionaEscenario => selectedObjeto == 0;
    public string? NombreObjetoSeleccionado => selectedObjeto > 0 && selectedObjeto < objetos.Count ? objetos[selectedObjeto] : null;
    public string? NombreParteSeleccionada => selectedParte >= 0 && selectedParte < partes.Count ? partes[selectedParte] : null;


    public void Dibujar(Escenario escenario)
    {
        if (escenario == null) return;

        ImGui.Begin("Panel de Transformaciones");

        objetos = new List<string> { "(Escenario)" };
        objetos.AddRange(escenario.Objetos.Keys);

        if (selectedObjeto >= objetos.Count) selectedObjeto = 0;
        string nombreObjeto = objetos[selectedObjeto];

        if (ImGui.BeginCombo("Objeto", nombreObjeto))
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                bool isSelected = (selectedObjeto == i);
                if (ImGui.Selectable(objetos[i], isSelected))
                    selectedObjeto = i;
                if (isSelected) ImGui.SetItemDefaultFocus();
            }
            ImGui.EndCombo();
        }

        if (!SeleccionaEscenario)
        {
            var objeto = escenario.Objetos[NombreObjetoSeleccionado!];
            partes = objeto?.Partes.Keys.ToList() ?? new();
            partes.Insert(0, "(Todo el objeto)");

            if (selectedParte >= partes.Count) selectedParte = 0;
            string nombreParte = partes[selectedParte];

            if (ImGui.BeginCombo("Parte", nombreParte))
            {
                for (int i = 0; i < partes.Count; i++)
                {
                    bool isSelected = (selectedParte == i);
                    if (ImGui.Selectable(partes[i], isSelected))
                        selectedParte = i;
                    if (isSelected) ImGui.SetItemDefaultFocus();
                }
                ImGui.EndCombo();
            }
        }

        ImGui.Text("Escoja una transformacion:");
        ImGui.RadioButton("Rotar", ref tipoTransformacion, 0); ImGui.SameLine();
        ImGui.RadioButton("Trasladar", ref tipoTransformacion, 1); ImGui.SameLine();
        ImGui.RadioButton("Escalar", ref tipoTransformacion, 2);

        if (tipoTransformacion == 2)
        {
            ImGui.SliderFloat("Factor de Escala", ref factorEscala, 0.1f, 5f);
        }
        else if (tipoTransformacion == 0)
        {
            ImGui.Text("Escoja un eje para aplicar la rotacion");
            ImGui.RadioButton("X", ref ejeSeleccionado, 3); ImGui.SameLine();
            ImGui.RadioButton("Y", ref ejeSeleccionado, 4); ImGui.SameLine();
            ImGui.RadioButton("Z", ref ejeSeleccionado, 5);
            
            ImGui.SliderFloat("Angulo de rotación", ref anguloRotacion, -360f, 360f);
        }
        else
        {
            ImGui.SliderFloat("X", ref x, -50f, 50f);
            ImGui.SliderFloat("Y", ref y, -50f, 50f);
            ImGui.SliderFloat("Z", ref z, -50f, 50f);
        }

        if (ImGui.Button("Aplicar"))
        {
            if (SeleccionaEscenario)
            {
                AplicarTransformacion(escenario);
            }
            else
            {
                var objeto = escenario.Objetos[NombreObjetoSeleccionado!];

                if (selectedParte == 0)
                {
                    if (objeto != null)
                        AplicarTransformacion(objeto);
                }
                else
                {
                    var parte = objeto?.Partes[NombreParteSeleccionada!];
                    if (parte != null)
                        AplicarTransformacion(parte);
                }
            }
        }


        ImGui.End();
    }

    private void AplicarTransformacion(object target)
    {
        switch (target)
        {
            case Escenario escenario:
                if (tipoTransformacion == 0)
                {
                    switch (ejeSeleccionado)
                    {
                        case 3:
                            escenario.Rotacion(ejeRotacion[0], anguloRotacion);
                            break;
                        case 4:
                            escenario.Rotacion(ejeRotacion[1], anguloRotacion);
                            break;
                        case 5:
                            escenario.Rotacion(ejeRotacion[2], anguloRotacion);
                            break;
                    }
                }
                else if (tipoTransformacion == 1)
                    escenario.Traslacion(x, y, z);
                else if (tipoTransformacion == 2)
                    escenario.Escalacion(factorEscala);
                break;

            case Objeto objeto:
                if (tipoTransformacion == 0)
                {
                    switch (ejeSeleccionado)
                    {
                        case 3:
                            objeto.Rotacion(ejeRotacion[0], anguloRotacion);
                            break;
                        case 4:
                            objeto.Rotacion(ejeRotacion[1], anguloRotacion);
                            break;
                        case 5:
                            objeto.Rotacion(ejeRotacion[2], anguloRotacion);
                            break;
                    }
                }
                else if (tipoTransformacion == 1)
                    objeto.Traslacion(x, y, z);
                else if (tipoTransformacion == 2)
                    objeto.Escalacion(factorEscala);
                break;

            case Parte parte:
                if (tipoTransformacion == 0)
                {
                    switch (ejeSeleccionado)
                    {
                        case 3:
                            parte.Rotacion(ejeRotacion[0], anguloRotacion);
                            break;
                        case 4:
                            parte.Rotacion(ejeRotacion[1], anguloRotacion);
                            break;
                        case 5:
                            parte.Rotacion(ejeRotacion[2], anguloRotacion);
                            break;
                    }
                }
                else if (tipoTransformacion == 1)
                    parte.Traslacion(x, y, z);
                else if (tipoTransformacion == 2)
                    parte.Escalacion(factorEscala);
                break;
        }
    }

}
