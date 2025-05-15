using System.Text.Json.Serialization;
using OpenTK.Mathematics;

public class Transformaciones
{
    [JsonIgnore]
    public Vector3 Position { get; set; } = Vector3.Zero;
    [JsonIgnore]
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    [JsonIgnore]
    public Vector3 Scale { get; set; } = Vector3.One;

    public Matrix4 GetMatrix(Vector3 centro) => Matrix4.CreateTranslation(-centro) *
    Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X)) *
    Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y)) *
    Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z)) *
    Matrix4.CreateScale(Scale) *
    Matrix4.CreateTranslation(centro + Position);
    public void Transladate(float x, float y, float z)
    {
        Position += new Vector3(x, y, z);
    }

    public void Rotate(float xDeg, float yDeg, float zDeg)
    {
        Rotation += new Vector3(xDeg, yDeg, zDeg);
    }

    public void RotateA(Vector3 centro, float xDeg, float yDeg, float zDeg)
    {
        Rotate(xDeg, yDeg, zDeg);
    }

    public void Escalate(float n)
    {
        if (n != 0)
        {
            Scale *= new Vector3(n);
        }
    }
    public  Vector3 CalcularCentro(IEnumerable<float[]> puntos)
    {
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;
        float minZ = float.MaxValue, maxZ = float.MinValue;

        bool hayPuntos = false;

        foreach (var p in puntos)
        {
            if (p.Length < 3) continue; // Ignorar si no hay al menos x, y, z

            float x = p[0];
            float y = p[1];
            float z = p[2];

            if (x < minX) minX = x;
            if (x > maxX) maxX = x;
            if (y < minY) minY = y;
            if (y > maxY) maxY = y;
            if (z < minZ) minZ = z;
            if (z > maxZ) maxZ = z;

            hayPuntos = true;
        }
        if (!hayPuntos) return Vector3.Zero;
        return new Vector3((minX + maxX) / 2, (minY + maxY) / 2, (minZ + maxZ) / 2);
    }
}