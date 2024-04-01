namespace OOP_ICT.Second.Models;

public class Bank
{
    public void WithdrawFromPlayer(Player player, decimal amount)
    {
        player.Account.Withdraw(amount);
    }

    public bool HasSufficientFunds(Player player, decimal amount)
    {
        return player.Account.HasSufficientFunds(amount);
    }
    
    public void DepositToPlayer(Player player, decimal amount)
    {
        player.Account.Deposit(amount);
    }
}