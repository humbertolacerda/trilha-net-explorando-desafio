using System.Text;
using DesafioDoisDIO.Models;
using System.Globalization;
using Newtonsoft.Json;
Console.OutputEncoding = Encoding.UTF8;

bool mostrarMenu = true;
EmpresaHoteleira objHotel = new EmpresaHoteleira(1, "Hotel Fazendo Bom Viver", "Estrada da Santa Fé KM125. New Paradise", "12345678000199");
String vlTextFile = "";
List<Pessoa> objPessoas = new List<Pessoa>();
List<Suite> objSuite = new List<Suite>();

ConsoleKeyInfo key;
int optionKey = 1;
Console.Clear();

while (mostrarMenu)
{
    Console.SetCursorPosition(0, 0);

    Console.WriteLine("╔" + objHotel.Replicate("═", 80) + "╗" );    
    Console.WriteLine("║".PadRight(30) + "\u001b[32mDigite a sua opção:\u001b[0m" + "║".Position(32) );    
    Console.WriteLine($"╠{objHotel.Replicate("═", 80)}╣");

    if (optionKey == 1)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    Console.WriteLine($"║ 1 - Cadastrar Cliente" + "║".Position(58));    
    Console.ResetColor();

    if (optionKey == 2)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    Console.WriteLine("║ 2 - Cadastrar Suites" + "║".Position(59));    
    Console.ResetColor();

    if (optionKey == 3)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    Console.WriteLine("║ 3 - Reserva" + "║".Position(68));    
    Console.ResetColor();

    if (optionKey == 4)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    Console.WriteLine("║ 4 - Check In" + "║".Position(67));    
    Console.ResetColor();

    if (optionKey == 5)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    Console.WriteLine("║ 5 - Check Out" + "║".Position(66));    
    Console.ResetColor();

    if (optionKey == 6)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
    }
    Console.WriteLine("║ 6 - Encerrar" + "║".Position(67));
    Console.ResetColor();

    Console.WriteLine("║" + objHotel.Replicate(" ", 80) + "║");    
    Console.WriteLine("╚" + objHotel.Replicate("═", 80) + "╝");

    key = Console.ReadKey(true);
    switch ( key.Key )
    {
        case ConsoleKey.DownArrow:
            optionKey++;
            break; 
             
        case ConsoleKey.UpArrow:
            optionKey--;
            break;  

        case ConsoleKey.Enter:
        {
            switch (optionKey)  
            {
                case 1:                    
                    vlTextFile = (File.Exists("BancoDeDadosPessoa.json") ? File.ReadAllText("BancoDeDadosPessoa.json") : "{}");
                    objPessoas = JsonConvert.DeserializeObject<List<Pessoa>>(vlTextFile);                    
                    objHotel.CadastrarPessoa(objPessoas);
                    break;

                case 2:
                    vlTextFile = (File.Exists("BancoDeDadosSuite.json") ? File.ReadAllText("BancoDeDadosSuite.json") : "{}");                    
                    objSuite = JsonConvert.DeserializeObject<List<Suite>>(vlTextFile);
                    objHotel.CadastrarSuite(objSuite);
                break;
                                
                case 3:                    
                    vlTextFile = (File.Exists("BancoDeDadosPessoa.json") ? File.ReadAllText("BancoDeDadosPessoa.json") : "{}");
                    objPessoas = JsonConvert.DeserializeObject<List<Pessoa>>(vlTextFile); 

                    vlTextFile = (File.Exists("BancoDeDadosSuite.json") ? File.ReadAllText("BancoDeDadosSuite.json") : "{}");                    
                    objSuite = JsonConvert.DeserializeObject<List<Suite>>(vlTextFile);
                    objHotel.FazerReserva(objSuite, objPessoas);
                    break;
                                
                case 4:
                    objHotel.FazerCheckIn();
                    break;
                                
                case 5:
                    objHotel.FazerCheckOut();
                    break;
                                
                case 6:
                    mostrarMenu = false;
                    Console.Clear();
                break;
            }
            break;
        }    
        default:
            Console.WriteLine("Opção inválida");
            break;        
    }

    if (optionKey < 1)
        optionKey = 6;

    if (optionKey > 6)
        optionKey = 1;    
}