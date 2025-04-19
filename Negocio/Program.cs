using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace OpenTKCubo3D
{
    class Program : GameWindow
    {
        private float _cameraAngleY;
        private float _cameraAngleX;
        private float _cameraDistance = 20.0f;
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
            Serializer _serializer = new Serializer();
            string rutaFija = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "escenarioU.json");

            

            List<Objeto> U = new List<Objeto>();

            Escenario E = new Escenario(U, 1f, 1f, 1f);
            //E.Rotacion('x',30.0f);
            //E.Rotacion('x',30.0f);
            //E.Escalacion(0.10f,0.10f,0.10f);
            //E.Traslacion(2f,-1f,-3f);

            //_serializer.GuardarAJson(E,rutaFija);
            E = _serializer.CargarDesdeJson<Escenario>(rutaFija);
            E.Objetos[0].Partes[0].Rotacion('y',30f);

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