using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace OpenTKCubo3D
{
    public class Program : GameWindow
    {
        private float _cameraAngleY;
        private float _cameraAngleX;
        private float _cameraDistance = 20.0f;
        private int _shaderProgram;
        private Matrix4 _view;
        private Matrix4 _projection;
        public Escenario _escenario = new Escenario();
        UIEditor _editor = new UIEditor();
        private ImGuiController _controller;
        private Libreto _libreto;
        private Serializer _serializar = new Serializer();
        public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            _escenario.Inicializar();
            this._controller = new ImGuiController(ClientSize.X, ClientSize.Y);
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
            _libreto = new Libreto(_escenario);

            /*_libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: 0f, z: 2f, tiempoInicio: 0f, tiempoFin: 5f,
            tipo: TipoTransformacion.Rotacion));*/
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: 0f, z: -0.5f, tiempoInicio: 0f, tiempoFin: 3f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 0f, z: -0.5f, tiempoInicio:1f, tiempoFin: 4f,
            tipo: TipoTransformacion.Traslacion));
            /*_libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 0f, z: 2f, tiempoInicio: 0f, tiempoFin: 5f,
            tipo: TipoTransformacion.Rotacion));*/

            string rutaFija = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "libreto1.json");
            //_serializar.GuardarAJson<Libreto>(_libreto,rutaFija);
            //_libreto = _serializar.CargarDesdeJson<Libreto>(rutaFija);
            //_libreto.Escenario = _escenario;
            _libreto.Iniciar();
            _view = Matrix4.LookAt(new Vector3(3, 15, 3), Vector3.Zero, Vector3.UnitY);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Size.X / (float)Size.Y, 0.1f, 200f);
        }
        protected override void OnUnload()
        {
            _libreto?.Detener();
            base.OnUnload();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Size.X / (float)Size.Y, 0.1f, 200f);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var input = KeyboardState;
            var mouse = MouseState;
            if (input.IsKeyDown(Keys.Escape)) Close();

            float rotationSpeed = 0.002f;
            // Rotar cámara con flechas
            if (input.IsKeyDown(Keys.Left)) _cameraAngleY += rotationSpeed;
            if (input.IsKeyDown(Keys.Right)) _cameraAngleY -= rotationSpeed;
            if (input.IsKeyDown(Keys.Up)) _cameraAngleX += rotationSpeed;
            if (input.IsKeyDown(Keys.Down)) _cameraAngleX -= rotationSpeed;

            // Limitar ángulo vertical para evitar volteretas
            _cameraAngleX = MathHelper.Clamp(_cameraAngleX, -MathHelper.PiOver2 + 0.1f, MathHelper.PiOver2 - 0.1f);

            // Zoom con la rueda del mouse
            _cameraAngleX = MathHelper.Clamp(_cameraAngleX, -MathHelper.PiOver2 + 0.1f, MathHelper.PiOver2 - 0.1f);
            //Zoom con la rueda del mouse
            float zoomSpeed = 1.0f;
            _cameraDistance -= mouse.ScrollDelta.Y * zoomSpeed;

            // Limitar distancia mínima y máxima
            _cameraDistance = MathHelper.Clamp(_cameraDistance, 20f, 70f);

        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(_shaderProgram);
            // Calcular posición de la cámara
            Vector3 cameraPos = new Vector3(
                _cameraDistance * (float)Math.Sin(_cameraAngleY) * (float)Math.Cos(_cameraAngleX),
                _cameraDistance * (float)Math.Sin(_cameraAngleX),
                _cameraDistance * (float)Math.Cos(_cameraAngleY) * (float)Math.Cos(_cameraAngleX)
            );
            _view = Matrix4.LookAt(cameraPos, Vector3.Zero, Vector3.UnitY); // La cámara mira al centro

            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "view"), false, ref _view);
            GL.UniformMatrix4(GL.GetUniformLocation(_shaderProgram, "projection"), false, ref _projection);

            _escenario.Render(_shaderProgram, Matrix4.Identity);
            _controller.Update(this, (float)args.Time);
            _editor.Dibujar(_escenario);
            _controller.Render();
            SwapBuffers();
        }

        static void Main(string[] args)
        {
            Serializer _serializer = new Serializer();
            string rutaFija = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "escenario4.json");

            Draw d = new Draw();
            Dictionary<String, Objeto> U = new Dictionary<string, Objeto>();

            U.Add("U1", d.CrearFiguraU(5f, 2f, 2f));
            U.Add("ejes", d.crearEjes(5f, 2f, 2f));
            U.Add("U2", d.CrearFiguraU(0f, 0f, 0f));
            U.Add("U3", d.CrearFiguraU(-3f, -3f, -3f));
            Escenario E = new Escenario(U, 0, 0, 0);

            Escenario E1 = new Escenario();
            //_serializer.GuardarAJson(E,rutaFija);
            //E1 = _serializer.CargarDesdeJson<Escenario>(rutaFija);
            Objeto auto = LectorModeloObj.ImportarOBJConMaterial("Autos/CarV6/auto1.obj",-3f,-2f,0f);
            Objeto auto1 = LectorModeloObj.ImportarOBJConMaterial("Autos/CarV6/auto1.obj",3f,-2f,0f);
            //Objeto carretera = d.CrearCarretera(0, 0, 0, 100, 100);
            //Objeto carretera = LectorModeloObj.ImportarOBJConMaterial("Autos/CarV5/Carretera.obj");
            auto.Rotacion(0f, 25f, 0f);
            auto1.Rotacion(0f, 25f, 0f);
            //carretera.Escalacion(3.5f);
            //carretera.Traslacion(-9f, 0f, -60f);
            E1.Objetos.Add("Auto1", auto);
            E1.Objetos.Add("Auto2", auto1);
            //E1.Objetos.Add("carretera", carretera);
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(1000, 700),
                Title = "Cubo 3D con OpenTK y Shaders",
                Flags = ContextFlags.Default,
                Profile = ContextProfile.Core,
            };

            using (var window = new Program(GameWindowSettings.Default, nativeWindowSettings))
            {

                window._escenario = E1;
                window.Run();
            }
        }

    }
}