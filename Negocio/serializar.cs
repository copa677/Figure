using System.Text.Json;
using System.IO;
using System.Linq;

public static class EscenarioSerializer
{
    public class EscenarioData
    {
        public List<ObjetoData> Objetos { get; set; }
        public float CX { get; set; }
        public float CY { get; set; }
        public float CZ { get; set; }
    }

    public class ObjetoData
    {
        public List<ParteData> Partes { get; set; }
        public float CX { get; set; }
        public float CY { get; set; }
        public float CZ { get; set; }
    }

    public class ParteData
    {
        public List<CaraData> Caras { get; set; }
        public float CX { get; set; }
        public float CY { get; set; }
        public float CZ { get; set; }
    }

    public class CaraData
    {
        public List<VerticeData> Vertices { get; set; }
        public float CX { get; set; }
        public float CY { get; set; }
        public float CZ { get; set; }
    }

    public class VerticeData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
    }

    public static void GuardarEscenario(Escenario escenario, string filePath)
    {
        var data = new EscenarioData
        {
            Objetos = escenario.Objetos.Select(o => new ObjetoData
            {
                Partes = o.Partes.Select(p => new ParteData
                {
                    Caras = p.Caras.Select(c => new CaraData
                    {
                        Vertices = GetVerticesFromCara(c).Select(v => new VerticeData
                        {
                            X = v.X,
                            Y = v.Y,
                            Z = v.Z,
                            R = v.R,
                            G = v.G,
                            B = v.B
                        }).ToList(),
                        CX = c.cx,
                        CY = c.cy,
                        CZ = c.cz
                    }).ToList(),
                    CX = p.cx,
                    CY = p.cy,
                    CZ = p.cz
                }).ToList(),
                CX = o.cx,
                CY = o.cy,
                CZ = o.cz
            }).ToList(),
            CX = escenario.cx,
            CY = escenario.cy,
            CZ = escenario.cz
        };

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public static Escenario CargarEscenario(string filePath)
    {
        string json = File.ReadAllText(filePath);
        var data = JsonSerializer.Deserialize<EscenarioData>(json);

        return new Escenario(
            data.Objetos.Select(o => new Objeto(
                o.Partes.Select(p => new Parte(
                    p.Caras.Select(c => new Cara(
                        c.Vertices.Select(v => new Vertice(v.X, v.Y, v.Z, v.R, v.G, v.B)).ToList(),
                        c.CX, c.CY, c.CZ)).ToList(),
                    p.CX, p.CY, p.CZ)).ToList(),
                o.CX, o.CY, o.CZ)).ToList(),
            data.CX, data.CY, data.CZ);
    }

    private static List<(float X, float Y, float Z, float R, float G, float B)> GetVerticesFromCara(Cara cara)
    {
        var vertices = new List<(float, float, float, float, float, float)>();
        for (int i = 0; i < cara._vertices.Length; i += 6)
        {
            vertices.Add((
                cara._vertices[i],     // X
                cara._vertices[i+1],  // Y
                cara._vertices[i+2],  // Z
                cara._vertices[i+3],  // R
                cara._vertices[i+4],  // G
                cara._vertices[i+5]   // B
            ));
        }
        return vertices;
    }
}