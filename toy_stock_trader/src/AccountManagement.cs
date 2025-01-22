class AccountManagement {
    private int AccountCounter = 0;
    private Dictionary<int, Account> Accounts = new Dictionary<int, Account>();
    private DataManager.DataManager Manager;

    public AccountManagement(DataManager.DataManager manager) {
        this.Manager = manager;
    }

    public Account CreateAccount(string name, float funds, string password) {
        Account newAccount = new Account(name, this.AccountCounter, funds, password, this.Manager);
        this.Accounts[this.AccountCounter] = newAccount;

        ++this.AccountCounter;

        Console.WriteLine($"Success new account created! Welcome {newAccount.Name}");
        return newAccount;
    }

    public Account GetAccount(int id) {
        if (this.Accounts.TryGetValue(id, out Account account)) 
        {
            return account;

        } else {
            Console.WriteLine("Account ID not found please try again\n");
        }
        return null;
    }
}