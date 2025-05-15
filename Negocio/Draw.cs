using OpenTK.Mathematics;

class Draw
{
    public Draw()
    {

    }
    public Objeto CrearFiguraU(float x, float y, float z)
    {
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
        //----------------------------------------------------
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
        //-----------------------------------------------------------
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

        Dictionary<string, Cara> Ucaras_parte1 = new Dictionary<string, Cara>{
    {"cara trasera", new Cara("Uvertice_cara1_parte1", Uvertice_cara1_parte1, 0f, 0f, 0f)},
    {"cara delantera", new Cara("Uvertice_cara2_parte1", Uvertice_cara2_parte1, 0f, 0f, 0f)},
    {"cara de arriba", new Cara("Uvertice_cara3_parte1", Uvertice_cara3_parte1, 0f, 0f, 0f)},
    {"cara de abajo", new Cara("Uvertice_cara4_parte1", Uvertice_cara4_parte1, 0f, 0f, 0f)},
    {"cara izquierda", new Cara("Uvertice_cara5_parte1", Uvertice_cara5_parte1, 0f, 0f, 0f)},
    {"cara derecha", new Cara("Uvertice_cara6_parte1", Uvertice_cara6_parte1, 0f, 0f, 0f)}
};

        Dictionary<string, Cara> Ucaras_parte2 = new Dictionary<string, Cara>{
    {"cara delantera", new Cara("Uvertice_cara1_parte2", Uvertice_cara1_parte2, 0f, 0f, 0f)},
    {"cara trasera", new Cara("Uvertice_cara2_parte2", Uvertice_cara2_parte2, 0f, 0f, 0f)},
    {"cara arriba", new Cara("Uvertice_cara3_parte2", Uvertice_cara3_parte2, 0f, 0f, 0f)},
    {"cara abajo", new Cara("Uvertice_cara4_parte2", Uvertice_cara4_parte2, 0f, 0f, 0f)},
    {"cara derecha", new Cara("Uvertice_cara5_parte2", Uvertice_cara5_parte2, 0f, 0f, 0f)},
    {"cara izquierda", new Cara("Uvertice_cara6_parte2", Uvertice_cara6_parte2, 0f, 0f, 0f)}
};

        Dictionary<string, Cara> Ucaras_parte3 = new Dictionary<string, Cara>{
    {"cara delantera", new Cara("Uvertice_cara1_parte3", Uvertice_cara1_parte3, 0f, 0f, 0f)},
    {"cara trasera", new Cara("Uvertice_cara2_parte3", Uvertice_cara2_parte3, 0f, 0f, 0f)},
    {"cara de arriba", new Cara("Uvertice_cara3_parte3", Uvertice_cara3_parte3, 0f, 0f, 0f)},
    {"cara de abajo", new Cara("Uvertice_cara4_parte3", Uvertice_cara4_parte3, 0f, 0f, 0f)},
    {"cara derecha", new Cara("Uvertice_cara5_parte3", Uvertice_cara5_parte3, 0f, 0f, 0f)},
    {"cara izquierda", new Cara("Uvertice_cara6_parte3", Uvertice_cara6_parte3, 0f, 0f, 0f)}
};

        Dictionary<string, Parte> Upartes = new Dictionary<string, Parte>{
    {"izquierda", new Parte(Ucaras_parte1, 0f, 0f, 0f)},
    {"derecha", new Parte(Ucaras_parte2, 0f, 0f, 0f)},
    {"abajo", new Parte(Ucaras_parte3, 0f, 0f, 0f)}
};


        Objeto U = new Objeto(Upartes, x, y, z);

        return U;
    }
    public Objeto crearEjes(float x, float y, float z)
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

            Dictionary<string, Cara> C1 = new Dictionary<string, Cara>();
            C1.Add("ejeX", new Cara("ejeX", v1, 0f, 0f, 0f));

            Dictionary<string, Cara> C2 = new Dictionary<string, Cara>();
            C2.Add("ejeY", new Cara("ejeY", v2, 0f, 0f, 0f));

            Dictionary<string, Cara> C3 = new Dictionary<string, Cara>();
            C3.Add("ejeZ", new Cara("ejeZ", v3, 0f, 0f, 0f));

            // Creación de diccionario para las partes
            Dictionary<string, Parte> P1 = new Dictionary<string, Parte>();
            P1.Add("parteEjeX", new Parte(C1, 0f, 0f, 0f));
            P1.Add("parteEjeY", new Parte(C2, 0f, 0f, 0f));
            P1.Add("parteEjeZ", new Parte(C3, 0f, 0f, 0f));

            Objeto o2 = new Objeto(P1, x, y, z);
            return o2;
    }
    public Objeto CrearCarretera(float x, float y, float z, float width, float length)
    {
        // Definir color gris asfalto
        Vector3 roadColor = new Vector3(0.1f, 0.6f, 0.1f);

        float halfW = width / 2f;
        float halfL = length / 2f;

        // Definir vértices (dos triángulos para formar el rectángulo)
        List<Vertice> roadVerts = new List<Vertice>
        {
            // Primer triángulo
            new Vertice(-halfW, 0f, -halfL, roadColor.X, roadColor.Y, roadColor.Z),
            new Vertice( halfW, 0f, -halfL, roadColor.X, roadColor.Y, roadColor.Z),
            new Vertice( halfW, 0f,  halfL, roadColor.X, roadColor.Y, roadColor.Z),

            // Segundo triángulo
            new Vertice(-halfW, 0f, -halfL, roadColor.X, roadColor.Y, roadColor.Z),
            new Vertice( halfW, 0f,  halfL, roadColor.X, roadColor.Y, roadColor.Z),
            new Vertice(-halfW, 0f,  halfL, roadColor.X, roadColor.Y, roadColor.Z)
        };

        // Crear la cara única de la carretera
        var caraDict = new Dictionary<string, Cara>
        {
            { "carretera", new Cara("carretera_road", roadVerts, 0f, 0f, 0f) }
        };

        // Crear la parte que contendrá la cara
        var parteDict = new Dictionary<string, Parte>
        {
            { "superficie", new Parte(caraDict, 0f, 0f, 0f) }
        };
    
        // Crear el objeto carretera en la posición deseada
        Objeto carretera = new Objeto(parteDict, x, y, z);
        return carretera;
    }
}