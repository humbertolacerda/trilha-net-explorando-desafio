namespace DesafioDoisDIO.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public String HospedeID { get; set; }
        public String SuiteID { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public DateTime? DataAbertura { get; set; }
        public int QuantidadeDeDias { get; set; }
        public String Status { get; set; }
        public Decimal SubTotal { get; set; }
        public Decimal Desconto { get; set; }
        public Decimal ValorTotalPago { get; set; }

        public Reserva()
        {
            
        }
        public Reserva(int id, String hospedeID, String suiteID, DateTime dataCheckIn, DateTime dataCheckOut, DateTime? dataAbertura, int quantidadeDeDias, String status, Decimal subTotal, Decimal deconto, Decimal valorPago ) 
        {
            Id = id;
            HospedeID = hospedeID;
            SuiteID = suiteID;
            DataCheckIn = dataCheckIn;
            DataCheckOut = dataCheckOut;
            DataAbertura = dataAbertura;
            QuantidadeDeDias = quantidadeDeDias;
            Status = status;            
            SubTotal = subTotal;
            Desconto = deconto; 
            ValorTotalPago = valorPago;
        }



    }
}