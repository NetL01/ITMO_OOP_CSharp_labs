﻿using Lab5.Application.Contracts.Shops;
using Lab5.Application.Models.OperationHistory;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.LoginAccount;

public class LoginAccountScenario : IScenario
{
    private readonly IBankAccountsService _bankAccountsService;

    public LoginAccountScenario(IBankAccountsService bankAccountsService)
    {
        _bankAccountsService = bankAccountsService;
    }

    public string Name => "Login Account";

    public async Task Run()
    {
        string number = AnsiConsole.Ask<string>("Enter account number");
        string password = AnsiConsole.Ask<string>("Enter pincode");

        OperationResult result = await _bankAccountsService.CreateAccount(number, password).ConfigureAwait(false);

        string message = result switch
        {
            OperationResult.Success => "Successful Login",
            OperationResult.Fail => "Bank Account not found",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("ok");
    }
}