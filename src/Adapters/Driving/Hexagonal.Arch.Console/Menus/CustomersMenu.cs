using Hexagonal.Arch.Domain.Dtos;
using Hexagonal.Arch.Domain.Ports;

namespace Hexagonal.Arch.Console.Menus;

public class CustomersMenu(
    ICreateCustomerService createCustomerService,
    IGetCustomerService getCustomerService)
{
    public async Task Show() 
    {
        bool show = true;
        while (show) 
        {
            System.Console.WriteLine("\n\nPara cadastrar cliente digite 1, para buscar cliente digite 2 e para SAIR digite 0");
            var option = System.Console.ReadLine();

            switch (option) 
            {
                case "0":
                    show = false;
                    System.Console.WriteLine("SAINDO!");
                    break;
                case "1":
                    await CreateCustomer();
                    System.Console.ReadKey();
                    System.Console.Clear();
                    break;
                case "2":
                    await GetCustomer();
                    System.Console.ReadKey();
                    System.Console.Clear();
                    break;
                default:
                    System.Console.WriteLine("Escolha uma opção válida");
                    break;
            }   
        }
    }

    private async Task CreateCustomer()
    {
        try 
        {
            System.Console.Write("Nome do cliente: ");
            var name = System.Console.ReadLine();
            System.Console.Write("Cpf do cliente: ");
            var cpf = System.Console.ReadLine();
            System.Console.Write("Idade do cliente: ");
            var age = Convert.ToInt16(System.Console.ReadLine());
            System.Console.Write("CEP do cliente: ");
            var cep = System.Console.ReadLine();

            var createCustomerDto = new CreateCustomerDto(name!, cpf!, age, cep!);
            await createCustomerService.CreateAsync(createCustomerDto);

            System.Console.WriteLine("Cliente cadastrado com sucesso!");
        }
        catch (Exception ex) 
        {
            System.Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    private async Task GetCustomer()
    {
        try 
        {
            System.Console.Write("Digite o ID do cliente: ");
            var id = Convert.ToInt32(System.Console.ReadLine());

            var customer = await getCustomerService.GetAsync(id);
            System.Console.WriteLine(customer.ToString());
        }
        catch (Exception ex) 
        {
            System.Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}