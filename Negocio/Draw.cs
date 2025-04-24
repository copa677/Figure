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
                new Cara(Uvertice_cara1_parte1,0f,0f,0f),
                new Cara(Uvertice_cara2_parte1,0f,0f,0f),
                new Cara(Uvertice_cara3_parte1,0f,0f,0f),
                new Cara(Uvertice_cara4_parte1,0f,0f,0f),
                new Cara(Uvertice_cara5_parte1,0f,0f,0f),
                new Cara(Uvertice_cara6_parte1,0f,0f,0f),
            };
        List<Cara> Ucaras_parte2 = new List<Cara>{
                new Cara(Uvertice_cara1_parte2,0f,0f,0f),
                new Cara(Uvertice_cara2_parte2,0f,0f,0f),
                new Cara(Uvertice_cara3_parte2,0f,0f,0f),
                new Cara(Uvertice_cara4_parte2,0f,0f,0f),
                new Cara(Uvertice_cara5_parte2,0f,0f,0f),
                new Cara(Uvertice_cara6_parte2,0f,0f,0f),
            };
        List<Cara> Ucaras_parte3 = new List<Cara>{
                new Cara(Uvertice_cara1_parte3,0f,0f,0f),
                new Cara(Uvertice_cara2_parte3,0f,0f,0f),
                new Cara(Uvertice_cara3_parte3,0f,0f,0f),
                new Cara(Uvertice_cara4_parte3,0f,0f,0f),
                new Cara(Uvertice_cara5_parte3,0f,0f,0f),
                new Cara(Uvertice_cara6_parte3,0f,0f,0f),
            };

        List<Parte> Uparte = new List<Parte>{
                new Parte(Ucaras_parte1,0f,0f,0f),
                new Parte(Ucaras_parte2,0f,0f,0f),
                new Parte(Ucaras_parte3,0f,0f,0f)
            };

        
        Objeto U = new Objeto(Uparte,x,y,z);
            
        return U;
    }
    
}