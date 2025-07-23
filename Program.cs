using System.Globalization;
using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

CultureInfo cultura = new CultureInfo("pt-BR");

// Inicializa as listas e os objetos que serão usados
List<Pessoa> hospedes = new List<Pessoa>();
Suite suite = null;
Reserva reserva = null;

bool exibirMenu = true;

// Loop do menu interativo
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Bem-vindo ao sistema de hospedagem!");
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar suíte");
    Console.WriteLine("2 - Cadastrar hóspedes");
    Console.WriteLine("3 - Criar reserva");
    Console.WriteLine("4 - Listar informações da reserva");
    Console.WriteLine("5 - Executar teste predefinido");
    Console.WriteLine("6 - Encerrar");

    string opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Console.WriteLine("Digite o tipo da suíte:");
            string tipoSuite = Console.ReadLine();

            Console.WriteLine("Digite a capacidade da suíte:");
            int capacidade = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor da diária (exemplo: 30.50):");
            decimal valorDiaria = decimal.Parse(Console.ReadLine().Replace(".", ","), cultura);

            suite = new Suite(tipoSuite, capacidade, valorDiaria);

            Console.WriteLine("Suíte cadastrada com sucesso!");
            break;

        case "2":
            Console.WriteLine("Quantos hóspedes você deseja cadastrar?");
            int quantidadeHospedes = int.Parse(Console.ReadLine());

            hospedes.Clear(); // Limpa a lista de hóspedes antes de cadastrar novos

            for (int i = 1; i <= quantidadeHospedes; i++)
            {
                Console.WriteLine($"Digite o nome do hóspede {i}:");
                string nomeHospede = Console.ReadLine();

                hospedes.Add(new Pessoa(nome: nomeHospede));
            }

            Console.WriteLine("Hóspedes cadastrados com sucesso!");
            break;

        case "3":
            if (suite == null)
            {
                Console.WriteLine("Você precisa cadastrar uma suíte antes de criar uma reserva!");
                break;
            }

            Console.WriteLine("Digite a quantidade de dias reservados:");
            int diasReservados = int.Parse(Console.ReadLine());

            reserva = new Reserva(diasReservados);
            reserva.CadastrarSuite(suite);

            try
            {
                reserva.CadastrarHospedes(hospedes);
                Console.WriteLine("Reserva criada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar a reserva: {ex.Message}");
                Console.WriteLine($"Deseja alterar a capacidade da suíte para a quantidade de hospedes ({hospedes.Count})? (S/N)");
                if (Console.ReadLine().ToUpper().StartsWith('S'))
                {
                    suite.Capacidade = hospedes.Count; // Ajusta a capacidade da suíte
                    try
                    {
                        reserva.CadastrarHospedes(hospedes); // Tenta novamente cadastrar os hóspedes
                        Console.WriteLine("Reserva criada com sucesso!");
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine($"Erro ao criar a reserva após ajustar a capacidade: {ex2.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("A reserva não foi criada. Por favor, ajuste os dados e tente novamente.");
                }
            }
            break;

        case "4":
            if (reserva == null)
            {
                Console.WriteLine("Nenhuma reserva foi criada ainda!");
                break;
            }

            Console.WriteLine($"Tipo: {suite.TipoSuite}");
            Console.WriteLine($"Capacidade: {suite.Capacidade}");
            Console.WriteLine($"Valor diária: {suite.ValorDiaria.ToString("C", cultura)}");
            Console.WriteLine($"Dias reservados: {reserva.DiasReservados}");
            Console.WriteLine($"Quantidade de hóspedes: {reserva.ObterQuantidadeHospedes()}");
            Console.WriteLine($"Valor total da diária: {reserva.CalcularValorDiaria().ToString("C", cultura)}");
            break;

        case "5":
            program();
            break;

        case "6":
            exibirMenu = false;
            Console.WriteLine("Obrigado por usar o sistema de hospedagem. Até mais!");
            break;

        default:
            Console.WriteLine("Opção inválida. Tente novamente.");
            break;
    }

    Console.WriteLine("\nPressione qualquer tecla para continuar...");
    Console.ReadLine();


}

void program()
{
    // Cria os modelos de hóspedes e cadastra na lista de hóspedes
    List<Pessoa> hospedes = [];

    Pessoa p1 = new Pessoa(nome: "Hóspede 1");
    Pessoa p2 = new Pessoa(nome: "Hóspede 2");

    hospedes.Add(p1);
    hospedes.Add(p2);

    // Cria a suíte
    Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30);

    // Cria uma nova reserva, passando a suíte e os hóspedes
    Reserva reserva = new Reserva(diasReservados: 5);
    reserva.CadastrarSuite(suite);
    reserva.CadastrarHospedes(hospedes);

    // Exibe a quantidade de hóspedes e o valor da diária
    Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
    Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria().ToString("C", new CultureInfo("pt-BR"))}");
}