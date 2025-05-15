using System.Numerics;
using ImGuiNET;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Linq;
using System.Collections.Generic;

public class UIEditor
{
    private int selectedObjeto = 0;
    private int selectedParte = 0;
    private int tipoTransformacion = 0; // 0: Rotar, 1: Trasladar, 2: Escalar
    private int ejeSeleccionado = 0;     // 0: X, 1: Y, 2: Z

    private float x = 0f, y = 0f, z = 0f;
    private float anguloRotacion = 0f;
    private float factorEscala = 1.0f;

    private List<string> objetos = new();
    private List<string> partes = new();

    public bool SeleccionaEscenario => selectedObjeto == 0;
    public string? NombreObjetoSeleccionado =>
        selectedObjeto > 0 && selectedObjeto < objetos.Count
            ? objetos[selectedObjeto]
            : null;
    public string? NombreParteSeleccionada =>
        selectedParte >= 0 && selectedParte < partes.Count
            ? partes[selectedParte]
            : null;

    public void Dibujar(Escenario escenario)
    {
        if (escenario == null) return;

        ImGui.Begin("Panel de Transformaciones");

        objetos = new List<string> { "(Escenario)" };
        objetos.AddRange(escenario.Objetos.Keys);
        if (selectedObjeto >= objetos.Count) selectedObjeto = 0;

        if (ImGui.BeginCombo("Objeto", objetos[selectedObjeto]))
        {
            for (int i = 0; i < objetos.Count; i++)
            {
                bool isSel = (selectedObjeto == i);
                if (ImGui.Selectable(objetos[i], isSel))
                    selectedObjeto = i;
                if (isSel) ImGui.SetItemDefaultFocus();
            }
            ImGui.EndCombo();
        }

        // — Si no es el escenario, mostrar selección de parte
        if (!SeleccionaEscenario)
        {
            var objeto = escenario.Objetos[NombreObjetoSeleccionado!];
            partes = objeto.Partes.Keys.ToList();
            partes.Insert(0, "(Todo el objeto)");
            if (selectedParte >= partes.Count) selectedParte = 0;

            if (ImGui.BeginCombo("Parte", partes[selectedParte]))
            {
                for (int i = 0; i < partes.Count; i++)
                {
                    bool isSel = (selectedParte == i);
                    if (ImGui.Selectable(partes[i], isSel))
                        selectedParte = i;
                    if (isSel) ImGui.SetItemDefaultFocus();
                }
                ImGui.EndCombo();
            }
        }

        // — Tipo de transformación
        ImGui.Text("Escoja una transformación:");
        ImGui.RadioButton("Rotar", ref tipoTransformacion, 0); ImGui.SameLine();
        ImGui.RadioButton("Trasladar", ref tipoTransformacion, 1); ImGui.SameLine();
        ImGui.RadioButton("Escalar", ref tipoTransformacion, 2);

        // — Parámetros según tipo
        switch (tipoTransformacion)
        {
            case 0: // Rotar
                ImGui.Text("Selecciona eje:");
                ImGui.RadioButton("X", ref ejeSeleccionado, 0); ImGui.SameLine();
                ImGui.RadioButton("Y", ref ejeSeleccionado, 1); ImGui.SameLine();
                ImGui.RadioButton("Z", ref ejeSeleccionado, 2);
                ImGui.SliderFloat("Ángulo (°)", ref anguloRotacion, -360f, 360f);
                break;

            case 1: // Trasladar
                ImGui.SliderFloat("X", ref x, -50f, 50f);
                ImGui.SliderFloat("Y", ref y, -50f, 50f);
                ImGui.SliderFloat("Z", ref z, -50f, 50f);
                break;

            case 2: // Escalar
                ImGui.SliderFloat("Factor de escala", ref factorEscala, 0.1f, 5f);
                break;
        }

        // — Botón Aplicar
        if (ImGui.Button("Aplicar"))
        {
            object target = SeleccionaEscenario
                ? (object)escenario
                : (
                    selectedParte == 0
                      ? (object)escenario.Objetos[NombreObjetoSeleccionado!]
                      : (object)escenario
                            .Objetos[NombreObjetoSeleccionado!]
                            .Partes[NombreParteSeleccionada!]
                  );

            AplicarTransformacion(target);
        }

        ImGui.End();
    }

    private void AplicarTransformacion(object target)
    {
        // Preparamos los tres ángulos según el eje seleccionado
        float rx = 0, ry = 0, rz = 0;
        if (tipoTransformacion == 0)
        {
            if (ejeSeleccionado == 0) rx = anguloRotacion;
            else if (ejeSeleccionado == 1) ry = anguloRotacion;
            else rz = anguloRotacion;
        }

        switch (target)
        {
            case Escenario e:
                if (tipoTransformacion == 0)
                    e.Rotacion(rx, ry, rz);
                else if (tipoTransformacion == 1)
                    e.Traslacion(x, y, z);
                else
                    e.Escalacion(factorEscala);
                break;

            case Objeto o:
                if (tipoTransformacion == 0)
                    o.Rotacion(rx, ry, rz);
                else if (tipoTransformacion == 1)
                    o.Traslacion(x, y, z);
                else
                    o.Escalacion(factorEscala);
                break;

            case Parte p:
                if (tipoTransformacion == 0)
                    p.Rotacion(rx, ry, rz);
                else if (tipoTransformacion == 1)
                    p.Traslacion(x, y, z);
                else
                    p.Escalacion(factorEscala);
                break;
        }
    }
}
