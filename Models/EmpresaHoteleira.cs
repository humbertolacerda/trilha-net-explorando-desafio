using System;
using DesafioDoisDIO.Models;
using Newtonsoft.Json;
using System.Text;




namespace DesafioDoisDIO.Models
{
    public class EmpresaHoteleira
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NumeroCNPJ { get; set; }
        public String Endereco { get; set; }

        public EmpresaHoteleira(int id, string razaoSocial, string endereco, string numeroCNPJ)
        {
            Id = id;
            RazaoSocial = razaoSocial;
            Endereco = endereco;
            NumeroCNPJ = numeroCNPJ;            
        }


        public void CadastrarPessoa(List<Pessoa> pessoas)
        {
            Console.Clear();
            Console.Write("Nome do Hospede: ");
            string vlNome = Console.ReadLine();

            Console.Write("SobreNome do Hospede: ");
            string vlSobreNome = Console.ReadLine();

            Console.Write("Número do CPF: ");
            string vlCPF = Console.ReadLine();

            Pessoa objPessoa = new Pessoa();
            objPessoa.Nome = vlNome;
            objPessoa.Sobrenome = vlSobreNome;
            objPessoa.NumeroCPF = vlCPF;            

            if (pessoas.Exists(pessoa => pessoa.NumeroCPF == vlCPF) )
            {
                Console.WriteLine("CPF já cadastrado");
            } 
            else 
            {
                objPessoa.Id = pessoas.Count() + 1;                
                pessoas.Add(objPessoa);                

                String vlTexto = JsonConvert.SerializeObject(pessoas, Formatting.Indented);
                File.WriteAllText("BancoDeDadosPessoa.json", vlTexto);
            }            
        }

        public void CadastrarSuite(List<Suite> objListSuites)
        {
            Console.Clear();
            Console.Write("Código Identificador: ");
            string vlCodigo = Console.ReadLine().ToUpper();

            Console.Write("Tipo da Suite: ");
            string vlTipo = Console.ReadLine();

            Console.Write("Capacidade: ");
            int vlCapacidade = Convert.ToInt32( Console.ReadLine() );

            Console.Write("Valor diária: ");
            Decimal vlValorDia = Convert.ToDecimal( Console.ReadLine() );

            if ((objListSuites != null) && objListSuites.Exists(s => s.Codigo == vlCodigo) )
            {
                Console.WriteLine("Atenção Suite já cadastrada!");
            } 
            else 
            {
                List<Suite> objListSuite = new List<Suite>();
                if (objListSuites != null) 
                    objListSuite = objListSuites; 

                int id = (objListSuite != null ? objListSuite.Count() + 1 : 1);                
                objListSuite.Add(new Suite(id, vlCodigo, vlTipo, vlCapacidade, vlValorDia));                

                String vlTexto = JsonConvert.SerializeObject(objListSuite, Formatting.Indented);
                File.WriteAllText("BancoDeDadosSuite.json", vlTexto);

            }            

        }

        public void FazerReserva(List<Suite> objListSuites, List<Pessoa> pessoas)
        {
            Console.Clear();
            if (pessoas.Count == 0) 
            {
                Console.Write("Atenção, Não temos cadastro de hospedes (Tecle Enter para continuar)");
                Console.ReadLine();
                return;
            }
            Console.Clear();
            Console.Write("Número do CPF do Titular: ");
            string vlCPF = Console.ReadLine();

            Pessoa pessoa = pessoas.Find(p => p.NumeroCPF == vlCPF );
            //Pessoa pessoa = pessoas.FindAll(delegate (Pessoa p) {return p.NumeroCPF == vlCPF} );

            if (pessoa.NumeroCPF == vlCPF)
            {
                Console.WriteLine($"Olá {pessoa.NomeCompleto} Bom ter você aqui novamente.".ToUpper() );
                Console.Write("Código Identificador da Suite: ");
                string vlCodigoSuite = Console.ReadLine().ToUpper();

                Suite objSuite = objListSuites.Find(s => s.Codigo == vlCodigoSuite );
                if (objSuite != null && objSuite.Codigo == vlCodigoSuite) 
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Suite {objSuite.TipoSuite} para {objSuite.Capacidade} Pessoas, Valor da diária {objSuite.ValorDiaria.ToString("C")} ");
                    Console.ResetColor();

                    voltarAqui:
                    Console.Clear();
                    Console.WriteLine($"Olá {pessoa.NomeCompleto} Bom ter você aqui novamente.".ToUpper() );
                    Console.WriteLine($"Código Identificador da Suite: {vlCodigoSuite}");

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Suite {objSuite.TipoSuite} para {objSuite.Capacidade} Pessoas, Valor da diária {objSuite.ValorDiaria.ToString("C")} ");
                    Console.ResetColor();
                    Console.WriteLine(" ");

                    Console.Write("Data de Check In: ");                    
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    string vlDataCheckIn = Console.ReadLine();
                    Console.ResetColor();
                    
                    Console.Write("Data de Check Out: ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    string vlDataCheckOut = Console.ReadLine();
                    Console.ResetColor();                    
                    Console.WriteLine(" ");

                    DateTime vlDateCheckIn = Convert.ToDateTime(vlDataCheckIn);
                    DateTime vlDateCheckOut = Convert.ToDateTime(vlDataCheckOut);

                    if (vlDateCheckOut <= vlDateCheckIn)
                    {
                        Console.WriteLine("Data de check Out precisa ser maior que data de Check In");
                        Console.ReadLine();               
                        goto voltarAqui;
                    }
                    var vlDias = (vlDateCheckOut - vlDateCheckIn).Days;
                    Console.WriteLine($"Reserva Confirmada com Sucesso. São {vlDias} dias no valor total de { (objSuite.ValorDiaria * vlDias).ToString("C") }");
                    Console.ReadLine();  

                    List<Reserva> objListReserva = new List<Reserva>();
                    //List <User> usuarios = new List <User> ();

                    String vlTextFile = (File.Exists("BancoDeDadosReserva.json") ? File.ReadAllText("BancoDeDadosReserva.json") : "{}");                    
                    if (vlTextFile != "{}")
                        objListReserva = JsonConvert.DeserializeObject<List<Reserva>>(vlTextFile);
                    else
                        objListReserva = null;

                    int id = (objListReserva != null ? objListReserva.Count() + 1 : 1);                
                    Reserva objReserva = new Reserva(id, vlCPF, vlCodigoSuite, vlDateCheckIn, vlDateCheckOut, null, vlDias, "A", 0, 0, 0);
                    String vlTexto = "";
                    if (vlTextFile == "{}")
                        vlTexto = JsonConvert.SerializeObject(objReserva, Formatting.Indented);
                    else
                    {
                        objListReserva.Add(objReserva); 
                        vlTexto = JsonConvert.SerializeObject(objListReserva, Formatting.Indented);
                    }
                    File.WriteAllText("BancoDeDadosReserva.json", vlTexto);

                }
                else
                {

                }

            } 
            else 
            {
                Console.Write("Pessoa não encontrada: ");
            }            

        }

        public void FazerCheckIn ()
        {
            Console.Clear();
            String vlTextFile = (File.Exists("BancoDeDadosReserva.json") ? File.ReadAllText("BancoDeDadosReserva.json") : "{}");                    
            if (vlTextFile == "{}")
            {
                Console.Write("Atenção, Não temos reservas em aberto (Tecle Enter para continuar)");
                Console.ReadLine();
                return;                
            }
            List<Reserva> objListReserva = JsonConvert.DeserializeObject<List<Reserva>>(vlTextFile);
            Console.Write("Número do CPF do Titular: ");
            string vlCPF = Console.ReadLine();

            Reserva objReserva = objListReserva.Find(r => r.HospedeID == vlCPF && r.DataAbertura == null);
            
            int posicao = objListReserva.IndexOf(objReserva);

            if (objReserva == null)
            {
                Console.Write("Atenção, Não há reservas em aberto para este cliente. (Tecle Enter para continuar)");
                Console.ReadLine();
                return;                                
            }
            else
            {
                objListReserva[posicao].DataAbertura = DateTime.Now;
            }
            String vlTexto = JsonConvert.SerializeObject(objListReserva, Formatting.Indented);
            File.WriteAllText("BancoDeDadosReserva.json", vlTexto);
        }

        public void FazerCheckOut ()
        {
            Console.Clear();
            String vlTextFile = (File.Exists("BancoDeDadosReserva.json") ? File.ReadAllText("BancoDeDadosReserva.json") : "{}");                    
            if (vlTextFile == "{}")
            {
                Console.Write("Atenção, Não temos reservas em aberto (Tecle Enter para continuar)");
                Console.ReadLine();
                return;                
            }
            List<Reserva> objListReserva = JsonConvert.DeserializeObject<List<Reserva>>(vlTextFile);
            Console.Clear();
            Console.Write("Número do CPF do Titular: ");
            string vlCPF = Console.ReadLine();

            Reserva objReserva = objListReserva.Find(r => r.HospedeID == vlCPF && r.DataAbertura != null && r.Status == "A");
            int posicao = objListReserva.IndexOf(objReserva);
            if (posicao < 0)
             {
                Console.Write("Atenção, Não encontramos reservas em aberto para este cliente. (Tecle Enter para continuar)");
                Console.ReadLine();
                return;                
            }

            vlTextFile = (File.Exists("BancoDeDadosPessoa.json") ? File.ReadAllText("BancoDeDadosPessoa.json") : "{}");
            List<Pessoa> objPessoas = JsonConvert.DeserializeObject<List<Pessoa>>(vlTextFile); 

            vlTextFile = (File.Exists("BancoDeDadosSuite.json") ? File.ReadAllText("BancoDeDadosSuite.json") : "{}");                    
            List<Suite> objSuites = JsonConvert.DeserializeObject<List<Suite>>(vlTextFile);
            Suite objSuite = objSuites.Find(s => s.Codigo == objReserva.SuiteID );

            Pessoa objPessoa = objPessoas.Find(p => p.NumeroCPF == vlCPF );
            Console.WriteLine(RazaoSocial.PadLeft(50));
            Console.WriteLine(NumeroCNPJ.PadLeft(50));
            Console.WriteLine( Replicate("*", 80) );
            Console.WriteLine($"FECHAMENTO DA CONTA DA SUITE: {objSuite.TipoSuite} CÓDIGO {objSuite.Codigo}");
            Console.WriteLine(" ");
            Console.WriteLine($"CLIENTE             : {objPessoa.NomeCompleto.ToUpper()}");
            Console.WriteLine($"NÚMERO DOCUMENTO    : {objPessoa.NumeroCPF}");
            Console.WriteLine(" ");
            Console.WriteLine($"Diárias             : {objReserva.QuantidadeDeDias}");
            Console.WriteLine($"Valor da Diárias    : {objSuite.ValorDiaria.ToString("C")}");
            Console.WriteLine($"Data CheckIn        : {objReserva.DataCheckIn.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Data Check Out      : {objReserva.DataCheckOut.ToString("dd/MM/yyyy")}");
            Console.WriteLine($"Sub Total           : {(objReserva.QuantidadeDeDias * objSuite.ValorDiaria).ToString("C").PadLeft(20)} ");

            var vltotal = objReserva.QuantidadeDeDias * objSuite.ValorDiaria;

            var vlDesconto = (vltotal * 10) / 100;
            if (objReserva.QuantidadeDeDias >= 10)
            {
            Console.WriteLine($"Desconto            : {vlDesconto.ToString("C").PadLeft(20)} ");
            }
            else
            {
                vlDesconto = 0;
            }
            Console.WriteLine($"Valor total a pagar : {(vltotal - vlDesconto).ToString("C").PadLeft(20)} ");
            Console.ReadLine();
            Console.Clear();

            objListReserva[posicao].Status = "F";
            objListReserva[posicao].Desconto = vlDesconto;
            objListReserva[posicao].ValorTotalPago = (vltotal - vlDesconto);
            objListReserva[posicao].SubTotal = vltotal;
            String vlTexto = JsonConvert.SerializeObject(objListReserva, Formatting.Indented);
            File.WriteAllText("BancoDeDadosReserva.json", vlTexto);

        }


        public string Replicate(string vlValor, int Quant)
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