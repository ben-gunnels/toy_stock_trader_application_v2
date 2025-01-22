class Account {
    public string Name { get; set; }
    private int ID { get; set; }
    private float Funds{ get; set; } = 0f;
    private float TotalPurchased { get; set; } = 0f;
    private float TotalSold { get; set; } = 0f;

    private bool Lock { get; set; } = true; // Set and unlocked by password
    private string Password { get; set; }

    private Dictionary<string, Stock> portfolio = new Dictionary<string, Stock>();

    private DataManager.DataManager Manager;

    public Account(string name, int id, float fundsAdded, string password, DataManager.DataManager manager) 
    {
        this.Name = name;
        this.ID = id;
        this.Funds = fundsAdded;
        this.Password = password;
        this.Manager = manager;
        Console.WriteLine($"Account successfully created with ID: {this.ID} (PLEASE SAVE THIS FOR FUTURE ACCESS) \n");
    }

    public bool UnlockAccount(string password) {
        if (password != this.Password) 
        { 
            Console.WriteLine("Password must match account\n");
            return false;
        }
        this.Lock = false;
        Console.WriteLine($"Account login successful! Welcome back {this.Name}!\n");
        return true;
    }

    public void LockAccount() { // Unconditionally lock the account
        this.Lock = true;
    }


    async public void PurchaseStock(string symbol, int quantity) {
        if (this.Lock) { return; } // Don't allow a purchase until lock has been released by password
    }

    public void SellStock(string symbol, int quantity) {
        if (this.Lock) { return; }
        if (this.portfolio.TryGetValue(symbol, out Stock stock))
        {


        }
    }

    public void AddFunds(float funds) {
        this.Funds += funds;
        Console.WriteLine($"Funds totaling ${funds} added successfully.");
        this.CheckFunds();
    }

    public void WithdrawFunds(float funds) {
        float starting = this.Funds;
        this.Funds = Math.Max(this.Funds - funds, 0);
        Console.WriteLine($"Funds totaling ${Math.Min(funds, starting)} successfully withdrawn.");
        this.CheckFunds();
    }

    public void CheckFunds() {
        Console.WriteLine($"You have ${this.Funds} in your account\n");
    }
}