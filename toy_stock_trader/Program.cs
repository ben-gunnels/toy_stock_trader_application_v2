// See https://aka.ms/new-console-template for more information


class Program {
    static Account CreateAccount(AccountManagement management) {
        bool running = true;
        string name = "UserX";
        string password = "abc123";
        string repeatedPassword;
        string fundInput;
        float funds = 0f;

        while (running) {
            
            Console.WriteLine("Please give your account a name\n");
            name = Console.ReadLine();

            while (true) {
                Console.WriteLine("Please give your account a password\n");
                password = Console.ReadLine();
                Console.WriteLine("Please confirm your password by typing it again\n");
                repeatedPassword = Console.ReadLine();
                if (password == repeatedPassword) {
                    Console.WriteLine("Password input accepted!\n");
                    break;
                } else {
                    Console.WriteLine("Passwords do not match, please try again.");
                }
            }

            while (true) {
                Console.WriteLine("Please add funds to your account or input 0 to bypass\n");
                fundInput = Console.ReadLine();
                if (float.Parse(fundInput) >= 0) {
                    funds = float.Parse(fundInput);
                    Console.WriteLine("Funds successfully added to your account");
                    break;
                } else {
                    Console.WriteLine("Please enter a value >= 0 to your account.\n");
                }
            }
            running = false;
        }
        return management.CreateAccount(name, funds, password);
    }

    static Account AccessAccount(AccountManagement management) {
        bool running = true;
        int id;
        Account account;

        while (running) {
            Console.WriteLine("Please enter your known Account ID: \n");
            id = int.Parse(Console.ReadLine());
            account = management.GetAccount(id);
        
            if (account != null) {
                Console.WriteLine($"Account found. Welcome back {account.Name}. Please enter your password.\n");
                while (true) {
                    string password = Console.ReadLine();
                    if (account.UnlockAccount(password)) {
                        return account;
                    } else {
                        running = false;
                        break;
                    }
                }
            } 
        }
        return null;
    }

    static void AccountFeatures(Account account) {
        bool running = true;
        string selectedFeature;
        string fundInput;
        string symbol;
        string shares;
        string confirm;
        while (running) {
            Console.WriteLine($"What would you like to do with your account {account.Name}? (1 - Add Funds, 2 - Withdraw Funds, 3 - Buy Stock, 4 - Sell Stock, 5 - Check Funds, 6 - Exit and Lock Account)\n");
            selectedFeature = Console.ReadLine();

            switch(selectedFeature) {
                case "1":
                    while (true) {
                        Console.WriteLine("Input the desired number of funds to add: ");
                        fundInput = Console.ReadLine();
                        if (float.Parse(fundInput) >= 0) {
                            account.AddFunds(float.Parse(fundInput));
                            break;
                        } else {
                            Console.WriteLine("Please enter a value >= 0 to add to your account.\n");
                        }
                        break;
                    }
                    break;
                case "2":
                    while (true) {
                        Console.WriteLine("Input the desired number of funds to withdraw: \n");
                        fundInput = Console.ReadLine();
                        if (float.Parse(fundInput) >= 0) {
                            account.WithdrawFunds(float.Parse(fundInput));
                            break;
                        } else {
                            Console.WriteLine("Please enter a value >= 0 to withdraw from your account.\n");
                        }
                        break;
                    }
                    break;
                case "3":
                    while (true) {
                        Console.WriteLine("Please enter the symbol of the stock you would like to purchase followed by the number of desired shares (<= 1000) /n");
                        while (true) {
                            symbol = Console.ReadLine();
                            shares = Console.ReadLine();

                            if (int.Parse(shares) <= 1000 && symbol.Length <= 4) { // set maximum purchase size, check that its a valid symbol length
                                break;
                            } else {
                                Console.WriteLine("Please enter an appropriate ticker symbol and share number\n");
                            }
                        }
                        Console.WriteLine($"Searching for symbol {symbol} and will attempt to purchase {shares} shares. Confirm? (1 - Yes, 2 - No)");
                        confirm = Console.ReadLine();
                        if (confirm == "1") {
                            account.PurchaseStock(symbol, int.Parse(shares));
                        } else {
                            Console.WriteLine("Transaction will not process. Returning to main menu.");
                        }
                        break;
                    }
                    break;
                case "4":
                    while (true) {
                        Console.WriteLine("Please enter the symbol of the stock you would like to sell followed by the number of desired shares (<= 1000) /n");
                        while (true) {
                            symbol = Console.ReadLine();
                            shares = Console.ReadLine();

                            if (int.Parse(shares) <= 1000 && symbol.Length <= 4) { // set maximum purchase size, check that its a valid symbol length
                                break;
                            } else {
                                Console.WriteLine("Please enter an appropriate ticker symbol and share number\n");
                            }
                        }
                        Console.WriteLine($"Searching for symbol {symbol} and will attempt to sell {shares} shares. Confirm? (1 - Yes, 2 - No)");
                        confirm = Console.ReadLine();
                        if (confirm == "1") {
                            account.SellStock(symbol, int.Parse(shares));
                        } else {
                            Console.WriteLine("Transaction will not process. Returning to main menu.\n");
                        }
                        break;
                    }
                    break;
                case "5":
                    account.CheckFunds();
                    break;
                case "6":
                    while (true) {
                        Console.WriteLine("Confirm that you would like to exit your account (1 - Confirm, 2 - Reject)\n");
                        confirm = Console.ReadLine();
                        if (confirm == "1") {
                            Console.WriteLine("Safely exiting and locking account\n");
                            account.LockAccount();
                            running = false;
                            break;
                        } else {
                            Console.WriteLine("Returning to main menu\n");
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    

    static void Main(string[] args) {
        DataManager.DataManager manager = new DataManager.DataManager();
        AccountManagement accountManagement = new AccountManagement(manager);
        bool running = true;
        Account account = null;
        string res;

        Console.WriteLine("Welcome to the BlackBird Investment Platform!\n");
        while (running) {
            Console.WriteLine("Where would you like to start? (create new account [1], access existing account [2])");
            res = Console.ReadLine();
            switch (res) 
            {
                case "1":
                    account = CreateAccount(accountManagement);
                    Console.WriteLine($"Welcome to BlackBird Investment Group\n");
                    break;
                case "2":
                    account = AccessAccount(accountManagement);
                    break;
                default:
                    Console.WriteLine("Please enter a number 1 or 2");
                    break;
            }

            if (account != null) {
                AccountFeatures(account);

            }

        }
    }
}


