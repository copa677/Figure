using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Linq.Expressions;

namespace OpenTKCubo3D
{
    class Program : GameWindow
    {
        private float _cameraAngleY;
        private float _cameraAngleX;
        private float _cameraDistance = 20.0f;
        //private int _vertexBufferObject;
        //private int _vertexArrayObject;
        private int _shaderProgram;
        private Matrix4 _view;
        private Matrix4 _projection;
        private Escenario _escenario = new Escenario(new List<Objeto>(), 0f, 0f, 0f);

        public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            _escenario.Inicializar();

            // Compilar shaders
            string vertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPosition;
                layout(location = 1) in vec3 aColor;
                out vec3 fragColor;
                uniform mat4 model;
                uniform mat4 view;
                uniform mat4 projection;
                void main()
                {
                    gl_Position = projection * view * model * vec4(aPosition, 1.0);
                    fragColor = aColor;
                }
            ";

            string fragmentShaderSource = @"
                #version 330 core
                in vec3 fragColor;
                out vec4 color;
                void main()
                {
                    color = vec4(fragColor, 1.0);
                }
            ";

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);

            GL.DetachShader(_shaderProgram, vertexShader);
            GL.DetachShader(_shaderProgram, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Configurar la vista y la proyección
            _view = Matrix4.LookAt(new Vector3(5, 5, 12), Vector3.Zero, Vector3.UnitY);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Size.X / (float)Size.Y, 0.1f, 100f);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Size.X / (float)Size.Y, 0.1f, 100f);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape)) Close();

            float rotationSpeed = 0.002f;

            // Rotar cámara con flechas
            if (input.IsKeyDown(Keys.Left)) _cameraAngleY += rotationSpeed;
            if (input.IsKeyDown(Keys.Right)) _cameraAngleY -= rotationSpeed;
            if (input.IsKeyDown(Keys.Up)) _cameraAngleX -= rotationSpeed;
            if (input.IsKeyDown(Keys.Down)) _cameraAngleX += rotationSpeed;
            //Cuando quieras guardar el estado actual de tu escenario:
            if (KeyboardState.IsKeyDown(Keys.F5)) // Guardar con F5
            {
                string rutaGuardado = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "escenario.json");
                EscenarioSerializer.GuardarEscenario(_escenario, rutaGuardado);
                Console.WriteLine($"Escenario guardado en: {rutaGuardado}");
            }
            //Cargar el Escenario desde JSON
            if (KeyboardState.IsKeyDown(Keys.F4)) // Cargar con F6
            {
                string rutaCarga = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "escenario.json");

                if (File.Exists(rutaCarga))
                {
                    _escenario = EscenarioSerializer.CargarEscenario(rutaCarga);
                    _escenario.Inicializar(); // Esto es CRUCIAL para recrear los buffers OpenGL
                    Console.WriteLine($"Escenario cargado desde: {rutaCarga}");
                }
                else
                {
                    Console.WriteLine("No se encontró el archivo de escenario guardado");
                }
            }

            // Limitar ángulo vertical para evitar volteretas
            _cameraAngleX = MathHelper.Clamp(_cameraAngleX, -MathHelper.PiOver2 + 0.1f, MathHelper.PiOver2 - 0.1f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(_shaderProgram);

            // Calcular posición de la cámara
            Vector3 cameraPos = new Vector3(
                _cameraDistance * (float)Math.Sin(_cameraAngleY) * (float)Math.Cos(_cameraAngleX),
                _cameraDistance * (float)Math.Sin(_cameraAngleX),
                _cameraDistance * (float)Math.Cos(_cameraAngleY) * (float)Math.Cos(_cameraAngleX)
            );

            _view = Matrix4.LookAt(cameraPos, Vector3.Zero, Vector3.UnitY); // La cámara mira al centro

            // Pasar matrices al shader
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _view);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projection);

            _escenario.Render(_shaderProgram);

            SwapBuffers();
        }

        static void Main(string[] args)
        {
            List<Vertice> v1 = new List<Vertice>();
            List<Vertice> v2 = new List<Vertice>();
            List<Vertice> v3 = new List<Vertice>();

            v1.Add(new Vertice(-5, 0, 0, 1, 1, 1));
            v1.Add(new Vertice(5, 0, 0, 1, 1, 1));
            v2.Add(new Vertice(0, -5, 0, 1, 1, 1));
            v2.Add(new Vertice(0, 5, 0, 1, 1, 1));
            v3.Add(new Vertice(0, 0, -5, 1, 1, 1));
            v3.Add(new Vertice(0, 0, 5, 1, 1, 1));

            List<Cara> C1 = new List<Cara>();
            C1.Add(new Cara(v1, 0f, 0f, 0f));
            List<Cara> C2 = new List<Cara>();
            C2.Add(new Cara(v2, 0f, 0f, 0f));
            List<Cara> C3 = new List<Cara>();
            C3.Add(new Cara(v3, 0f, 0f, 0f));

            List<Parte> P1 = new List<Parte>();
            P1.Add(new Parte(C1, 0f, 0f, 0f));
            P1.Add(new Parte(C2, 0f, 0f, 0f));
            P1.Add(new Parte(C3, 0f, 0f, 0f));

            List<Objeto> O1 = new List<Objeto>();
            O1.Add(new Objeto(P1, 2f, 2f, 2f));
            
            //figura U
            //cara trasera
            List<Vertice> Uvertice_cara1_parte1 = new List<Vertice>{
                new Vertice(-0.8f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara delantera
            List<Vertice> Uvertice_cara2_parte1 = new List<Vertice>{
                new Vertice(-0.8f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara de arriba
            List<Vertice> Uvertice_cara3_parte1 = new List<Vertice>{
                new Vertice(-0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara de abajo
            List<Vertice> Uvertice_cara4_parte1 = new List<Vertice>{
                new Vertice(-0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  -1.0f,  0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara izquierda
            List<Vertice> Uvertice_cara5_parte1 = new List<Vertice>{
                new Vertice(-0.8f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.8f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara derecha
            List<Vertice> Uvertice_cara6_parte1 = new List<Vertice>{
                new Vertice(-0.3f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f)
            };
            
            //cara delantera 
            List<Vertice> Uvertice_cara1_parte2 = new List<Vertice>{
                new Vertice(0.8f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara trasera
            List<Vertice> Uvertice_cara2_parte2 = new List<Vertice>{
                new Vertice(0.8f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara arriba
            List<Vertice> Uvertice_cara3_parte2 = new List<Vertice>{
                new Vertice(0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara abajo
            List<Vertice> Uvertice_cara4_parte2 = new List<Vertice>{
                new Vertice(0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  -1.0f,  0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara derecha
            List<Vertice> Uvertice_cara5_parte2 = new List<Vertice>{
                new Vertice(0.8f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.8f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.8f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara izquierda
            List<Vertice> Uvertice_cara6_parte2 = new List<Vertice>{
                new Vertice(0.3f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  1.0f,  0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f)
            };

            //cara delantera
            List<Vertice> Uvertice_cara1_parte3 = new List<Vertice>{
                new Vertice(-0.3f, -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,   -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara trasera
            List<Vertice> Uvertice_cara2_parte3 = new List<Vertice>{
                new Vertice(-0.3f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,   -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara de arriba
            List<Vertice> Uvertice_cara3_parte3 = new List<Vertice>{
                new Vertice(-0.3f, -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,   -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara de abajo
            List<Vertice> Uvertice_cara4_parte3 = new List<Vertice>{
                new Vertice(-0.3f, -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,   -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara derecha
            List<Vertice> Uvertice_cara5_parte3 = new List<Vertice>{
                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -1.0f,  0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(0.3f,  -0.5f,  0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
            };
            //cara izquierda
            List<Vertice> Uvertice_cara6_parte3 = new List<Vertice>{
                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, 0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -0.5f, 0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -1.0f, -0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -1.0f,  0.5f, 1.0f, 1.0f, 1.0f),

                new Vertice(-0.3f,  -0.5f,  0.5f, 1.0f, 1.0f, 1.0f),
                new Vertice(-0.3f,  -0.5f, -0.5f, 1.0f, 1.0f, 1.0f),
            };

            List<Cara> Ucaras_parte1 = new List<Cara>{
                new Cara(Uvertice_cara1_parte1,0,0,0),
                new Cara(Uvertice_cara2_parte1,0,0,0),
                new Cara(Uvertice_cara3_parte1,0,0,0),
                new Cara(Uvertice_cara4_parte1,0,0,0),
                new Cara(Uvertice_cara5_parte1,0,0,0),
                new Cara(Uvertice_cara6_parte1,0,0,0),
            };
            List<Cara> Ucaras_parte2 = new List<Cara>{
                new Cara(Uvertice_cara1_parte2,0,0,0),
                new Cara(Uvertice_cara2_parte2,0,0,0),
                new Cara(Uvertice_cara3_parte2,0,0,0),
                new Cara(Uvertice_cara4_parte2,0,0,0),
                new Cara(Uvertice_cara5_parte2,0,0,0),
                new Cara(Uvertice_cara6_parte2,0,0,0),
            };
            List<Cara> Ucaras_parte3 = new List<Cara>{
                new Cara(Uvertice_cara1_parte3,0,0,0),
                new Cara(Uvertice_cara2_parte3,0,0,0),
                new Cara(Uvertice_cara3_parte3,0,0,0),
                new Cara(Uvertice_cara4_parte3,0,0,0),
                new Cara(Uvertice_cara5_parte3,0,0,0),
                new Cara(Uvertice_cara6_parte3,0,0,0),
            };

            List<Parte> Uparte = new List<Parte>{
                new Parte(Ucaras_parte1,0,0,0),
                new Parte(Ucaras_parte2,0,0,0),
                new Parte(Ucaras_parte3,0,0,0)
            };

            List<Objeto> U = new List<Objeto>{
                new Objeto(Uparte,0,0,0)
            };


            Escenario E = new Escenario(U, 1f, 1f, 1f);

            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Cubo 3D con OpenTK y Shaders",
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Core,
            };

            using (var window = new Program(GameWindowSettings.Default, nativeWindowSettings))
            {
                window._escenario = E;
                window.Run();
            }
        }
    }
}