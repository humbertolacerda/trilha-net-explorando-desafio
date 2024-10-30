namespace DesafioDoisDIO.Models
{
    public static class StringExtension
    {
        public static String Position(this string vlTexto, int vlColuna)
        {
            return Replicate(" ", vlColuna) + vlTexto;
            //, int vlLinha, int vlColuna
        }
        

        private static string Replicate(string vlValor, int Quant)
        {
            String vlRetorno = "";
            for (int i = 0; i < Quant; i++)
            {
                vlRetorno = (vlRetorno + vlValor);
            }

            return vlRetorno;
        }
    }
}