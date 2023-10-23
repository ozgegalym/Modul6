using System;
using System.Collections.Generic;

namespace Bankomat
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            Client client = new Client("1111222233334444", "1234", 5000);
            bank.AddClient(client);

            int attempts = 3;
            while (attempts > 0)
            {
                Console.Write("Введите пароль кредитной карты: ");
                string inputPin = Console.ReadLine();
                if (inputPin == client.Pin)
                {
                    break;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Неверный пароль. Осталось попыток: {attempts}");
                }
            }

            if (attempts == 0)
            {
                Console.WriteLine("Превышено количество попыток. Попробуйте позже.");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine("Меню:");
                    Console.WriteLine("1. Вывести баланс");
                    Console.WriteLine("2. Пополнить счет");
                    Console.WriteLine("3. Снять деньги со счета");
                    Console.WriteLine("4. Выход");

                    Console.Write("Выберите действие: ");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        Console.WriteLine($"Баланс на вашем счете: {client.AccountBalance}");
                        Console.Write("Введите 'M' для возврата в меню, 'E' для выхода: ");
                        string returnMenu = Console.ReadLine();
                        if (returnMenu.ToUpper() == "E")
                        {
                            break;
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Введите сумму для пополнения: ");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        client.Deposit(amount);
                        Console.WriteLine("Счет успешно пополнен.");
                        Console.Write("Введите 'M' для возврата в меню, 'E' для выхода: ");
                        string returnMenu = Console.ReadLine();
                        if (returnMenu.ToUpper() == "E")
                        {
                            break;
                        }
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите сумму для снятия: ");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        if (client.Withdraw(amount))
                        {
                            Console.WriteLine("Сумма успешно снята со счета.");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно средств на счете.");
                        }
                        Console.Write("Введите 'M' для возврата в меню, 'E' для выхода: ");
                        string returnMenu = Console.ReadLine();
                        if (returnMenu.ToUpper() == "E")
                        {
                            break;
                        }
                    }
                    else if (choice == "4")
                    {
                        break;
                    }
                }
            }
        }
    }

    class Bank
    {
        private Dictionary<string, Client> clients = new Dictionary<string, Client>();

        public void AddClient(Client client)
        {
            clients.Add(client.CardNumber, client);
        }

        public Client GetClient(string cardNumber)
        {
            if (clients.ContainsKey(cardNumber))
            {
                return clients[cardNumber];
            }
            else
            {
                return null;
            }
        }
    }

    class Client
    {
        public string CardNumber { get; }
        public string Pin { get; }
        public double AccountBalance { get; private set; }

        public Client(string cardNumber, string pin, double initialBalance)
        {
            CardNumber = cardNumber;
            Pin = pin;
            AccountBalance = initialBalance;
        }

        public void Deposit(double amount)
        {
            AccountBalance += amount;
        }

        public bool Withdraw(double amount)
        {
            if (AccountBalance >= amount)
            {
                AccountBalance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
