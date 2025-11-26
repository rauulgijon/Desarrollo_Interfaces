namespace SpiderMan
{
    internal class Jugador
    {
        // Atributos
        public int Vidas;
        public int CivilesSalvados;

        // Constructor
        public Jugador(int vidas)
        {
            this.Vidas = vidas;
            CivilesSalvados = 0; 
        }

        // Getters
        public int getVidas()
        {
            return Vidas;
        }

        public int getCivilesSalvados()
        {
            return CivilesSalvados;
        }

        // Setters
        public void setVidas(int vidas)
        {
            this.Vidas = vidas;
        }

        public void setCivilesSalvados(int civilesSalvados)
        {
            this.CivilesSalvados = civilesSalvados;
        }
    }
}
