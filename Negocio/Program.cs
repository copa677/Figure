using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace OpenTKCubo3D
{
    public class Program : GameWindow
    {
        private int _shaderProgram;
        private Matrix4 _view;
        private Matrix4 _projection;
        public Escenario _escenario = new Escenario();
        UIEditor _editor = new UIEditor();
        private ImGuiController _controller;
        private Libreto _libreto;
        private Serializer _serializar = new Serializer();
        private Vector3 _cameraPos = new Vector3(0, 5, 20);
        private Vector3 _cameraTarget = Vector3.Zero;
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
/*
            //rotacion
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: -45f, z: 0f, tiempoInicio: 5f, tiempoFin: 6f,
            tipo: TipoTransformacion.Rotacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 45f, z: 0f, tiempoInicio: 5f, tiempoFin: 6f,
            tipo: TipoTransformacion.Rotacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: -45f, z: 0f, tiempoInicio: 6f, tiempoFin: 7f,
            tipo: TipoTransformacion.Rotacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 45f, z: 0f, tiempoInicio: 6f, tiempoFin: 7f,
            tipo: TipoTransformacion.Rotacion));

            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: -45f, z: 0f, tiempoInicio: 12f, tiempoFin: 13f,
            tipo: TipoTransformacion.Rotacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 45f, z: 0f, tiempoInicio: 12f, tiempoFin: 13f,
            tipo: TipoTransformacion.Rotacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: -45f, z: 0f, tiempoInicio: 13f, tiempoFin: 15f,
            tipo: TipoTransformacion.Rotacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 45f, z: 0f, tiempoInicio: 13f, tiempoFin: 15f,
            tipo: TipoTransformacion.Rotacion));
            
            //traslacion
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: 0f, z: -95f, tiempoInicio: 0f, tiempoFin: 5f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 0f, z: -95f, tiempoInicio: 0f, tiempoFin: 5f,
            tipo: TipoTransformacion.Traslacion));

            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 3f, y: 0f, z: -3f, tiempoInicio: 5f, tiempoFin: 6f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: -6f, y: 0f, z: -6f, tiempoInicio: 5f, tiempoFin: 6f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 3f, y: 0f, z: -4f, tiempoInicio: 6f, tiempoFin: 7f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: -6f, y: 0f, z: -7f, tiempoInicio: 6f, tiempoFin: 7f,
            tipo: TipoTransformacion.Traslacion));

            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 103f, y: 0f, z: 0f, tiempoInicio: 7f, tiempoFin: 12f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: -104f, y: 0f, z: 0f, tiempoInicio: 7f, tiempoFin: 12f,
            tipo: TipoTransformacion.Traslacion));

            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 3f, y: 0f, z: 3f, tiempoInicio: 12f, tiempoFin: 13f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: -7f, y: 0f, z: 6f, tiempoInicio: 12f, tiempoFin: 13f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 4f, y: 0f, z: 4f, tiempoInicio: 13f, tiempoFin: 15f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: -8f, y: 0f, z: 7f, tiempoInicio: 13f, tiempoFin: 15f,
            tipo: TipoTransformacion.Traslacion));

            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto1", x: 0f, y: 0f, z: 95f, tiempoInicio: 15f, tiempoFin: 20f,
            tipo: TipoTransformacion.Traslacion));
            _libreto.AgregarInstruccion(new InstruccionAnimacion(
            nombreObjeto: "Auto2", x: 0f, y: 0f, z: 95f, tiempoInicio: 15f, tiempoFin: 20f,
            tipo: TipoTransformacion.Traslacion));     
*/
            string rutaFija = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "libreto2.json");
            //_serializar.GuardarAJson<Libreto>(_libreto,rutaFija);
            _libreto = _serializar.CargarDesdeJson<Libreto>(rutaFija);
            _libreto.Escenario = _escenario;
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
            if (input.IsKeyDown(Keys.Escape)) Close();

            float moveSpeed = 0.08f;

            // CAMBIO: controles de movimiento con teclas
            if (input.IsKeyDown(Keys.W)) _cameraPos.Z -= moveSpeed;
            if (input.IsKeyDown(Keys.S)) _cameraPos.Z += moveSpeed;
            if (input.IsKeyDown(Keys.A)) _cameraPos.X -= moveSpeed;
            if (input.IsKeyDown(Keys.D)) _cameraPos.X += moveSpeed;
            if (input.IsKeyDown(Keys.Q)) _cameraPos.Y -= moveSpeed;
            if (input.IsKeyDown(Keys.E)) _cameraPos.Y += moveSpeed;

            float targetSpeed = 0.08f;

            // Movimiento del punto al que se mira
            if (input.IsKeyDown(Keys.Left)) _cameraTarget.X -= targetSpeed;
            if (input.IsKeyDown(Keys.Right)) _cameraTarget.X += targetSpeed;
            if (input.IsKeyDown(Keys.Up)) _cameraTarget.Y += targetSpeed;
            if (input.IsKeyDown(Keys.Down)) _cameraTarget.Y -= targetSpeed;
            if (input.IsKeyDown(Keys.KeyPad0)) _cameraTarget.Z -= targetSpeed;
            if (input.IsKeyDown(Keys.KeyPad1)) _cameraTarget.Z += targetSpeed;
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.UseProgram(_shaderProgram);
            // CAMBIO: usa la nueva posición de cámara
            Vector3 cameraPos = _cameraPos;
            _view = Matrix4.LookAt(cameraPos, _cameraTarget, Vector3.UnitY);

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
            "escenariAnimacion.json");

            Escenario E1 = new Escenario();
            /*Objeto auto = LectorModeloObj.ImportarOBJConMaterial("Autos/CarV6/auto1.obj", -3f, 0f, 0f);
            Objeto auto1 = LectorModeloObj.ImportarOBJConMaterial("Autos/CarV6/auto1.obj", 120f, 0f, 0f);
            Objeto carretera = LectorModeloObj.ImportarOBJConMaterial("Autos/3/c3.obj");
            auto.Rotacion(0f, 25f, 0f);
            auto1.Rotacion(0f, 25f, 0f);
            E1.Objetos.Add("Auto1", auto);
            E1.Objetos.Add("Auto2", auto1);
            E1.Objetos.Add("carretera", carretera);*/

            //_serializer.GuardarAJson(E1,rutaFija);
            E1 = _serializer.CargarDesdeJson<Escenario>(rutaFija);
            E1.Objetos["Auto1"].Rotacion(0f, 25f, 0f);
            E1.Objetos["Auto2"].Rotacion(0f, 25f, 0f);
            E1.Objetos["Auto1"].Traslacion(-3f, 0f, 0f);
            E1.Objetos["Auto2"].Traslacion(120f, 0f, 0f);
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