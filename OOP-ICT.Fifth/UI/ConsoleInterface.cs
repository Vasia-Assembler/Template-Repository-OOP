using OOP_ICT.Fourth.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Fifth.Services;
using Spectre.Console;


namespace OOP_ICT.Fifth.UI;
public class ConsoleInterface
{
    private readonly PokerGameManager pokerGameManager;

    public ConsoleInterface(PokerGameManager pokerGameManager)
    {
        this.pokerGameManager = pokerGameManager;
    }

    public void Start()
    {
        while (true)
        {
            DisplayMainMenu();
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    StartNewGame();
                    break;
                case "2":
                    ViewPlayerStatistics();
                    break;
                case "q":
                case "Q":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    private void DisplayMainMenu()
    {
        AnsiConsole.MarkupLine("[bold underline]Главное меню[/]");
        AnsiConsole.MarkupLine("[yellow]1.[/] Начать игру");
        AnsiConsole.MarkupLine("[yellow]2.[/] Вывести статистику");
        AnsiConsole.MarkupLine("[yellow]Q.[/] Выход");
        Console.Write("Select an option: ");
    }

    private void StartNewGame()
    {
        AnsiConsole.Clear();
        AnsiConsole.WriteLine("Начинаем игру...");

        var players = GetPlayers();
        if (players.Count < 2)
        {
            AnsiConsole.WriteLine("Нужно хотя бы 2 игрока.");
            return;
        }

        var pokerGame = new PokerGame(players);
        pokerGame.StartNewGame();
        pokerGame.DealCards();
        var playerBets = GetPlayerBets(players);
        pokerGame.AcceptBets(playerBets);
        var losers = pokerGame.CompareHands();
        var winners = players.Where(player => !losers.Contains(player));

        AnsiConsole.WriteLine("Победители:");
        foreach (var winner in winners)
        {
            AnsiConsole.WriteLine($"- {winner.Name}");
        }

        AnsiConsole.WriteLine("Проигравшие:");
        foreach (var loser in losers)
        {
            AnsiConsole.WriteLine($"- {loser.Name}");
        }
        AnsiConsole.WriteLine("Баланс игроков:");
        foreach (var player in players)
        {
            AnsiConsole.WriteLine($"- {player.Name}: {player.Account.Balance}");
        }
    }

    private List<decimal> GetPlayerBets(List<Player> players)
    {
        var bets = new List<decimal>();
        foreach (var player in players)
        {
            var bet = AnsiConsole.Ask<decimal>($"Enter bet for {player.Name}: ");
            bets.Add(bet);
        }
        return bets;
    }

    private List<Player> GetPlayers()
    {
        var players = new List<Player>();
        var count = AnsiConsole.Ask<int>("Введите число игроков: ");

        for (int i = 0; i < count; i++)
        {
            var name = AnsiConsole.Ask<string>($"Введите имя для игрока {i + 1}: ");
            var acc = new Account(1000);
            players.Add(new Player(name, acc));
        }

        return players;
    }

    private void ViewPlayerStatistics()
    {
        AnsiConsole.Clear();
        AnsiConsole.WriteLine("Статистика игроков:");
    }

}