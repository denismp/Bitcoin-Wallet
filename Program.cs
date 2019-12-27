using System;
using System.Collections;
using System.Globalization;
using System.Text;
using HBitcoin.FullBlockSpv;
using HBitcoin.KeyManagement;
using HBitcoin.Models;
using NBitcoin;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using static System.Console;

namespace Bitcoin_Wallet
{
    class BitcoinWallet
    {
        const string walletFilePath = @"Wallets\"; // Path where you want to store the Wallets

        static void Main(string[] args)
        {
            string[] avaliableOperations =
            {
                "create", "recover", "balance", "history", "receive", "send", "exit" //Allowed functionality
            };
            string input = string.Empty;
            while (!input.ToLower().Equals("exit"))
            {
                do
                {
                    Write(
                        "Enter operation [\"Create\", \"Recover\", \"Balance\", \"History\", \"Receive\", \"Send\", \"Exit\"]: ");
                    input = ReadLine().ToLower().Trim();
                } while (!((IList) avaliableOperations).Contains(input));

                switch (input)
                {
                    case "create":
                        CreateWallet();
                        break;
                    case "recover":
                        WriteLine(
                            "Please note the wallet cannot check if your password is correct or not. " +
                            "If you provide a wrong password a wallet will be recovered with your " +
                            "provided mnemonic AND password pair: ");
                        Write("Enter password: ");
                        string pw = ReadLine();
                        Write("Enter mnemonic phrase: ");
                        string mnemonic = ReadLine();
                        Write("Enter date (yyyy-MM-dd): ");
                        string date = ReadLine();
                        Mnemonic mnem = new Mnemonic(mnemonic);
                        RecoverWallet(pw, mnem, date);
                        break;
                    case "receive":
                        Write("Enter wallet's name: ");
                        String walletName = ReadLine();
                        Write("Enter password: ");
                        pw = ReadLine();
                        Receive(pw, walletName);
                        break;
                    case "balance":
                        Write("Enter wallet's name: ");
                        walletName = ReadLine();
                        Write("Enter password: ");
                        pw = ReadLine();
                        Write("Enter wallet address: ");
                        string wallet = ReadLine();
                        ShowBalance(pw, walletName, wallet);
                        break;
                    case "history":
                        Write("Enter wallet's name: ");
                        walletName = ReadLine();
                        Write("Enter password: ");
                        pw = ReadLine();
                        Write("Enter wallet address: ");
                        wallet = ReadLine();
                        ShowHistory(pw, walletName, wallet);
                        break;
                    case "send":
                        Write("Enter wallet's name: ");
                        walletName = ReadLine();
                        Write("Enter wallet password: ");
                        pw = ReadLine();
                        Write("Enter wallet address: ");
                        wallet = ReadLine();
                        Write("Select outpoint (transaction ID): ");
                        string outPoint = ReadLine();
                        Send(pw, walletName, wallet, outPoint);
                        break;
                }
            }
        }

        private static void Send(string password, string walletName, string wallet, string outPoint)
        {

            NBitcoin.BitcoinExtKey privateKey = null;
            try
            {
                // TODO: Load the Wallet and check the Private Key that the User provides.

            }
            catch
            {
                WriteLine("Wrong wallet or password!");
                return;
            }
                // TODO: Implement the Displaying of Balanace and Sending the Transaction with the correct amount.

            // if (broadcastResponse.Success)
            // {
            //     Console.WriteLine("Transaction send!");
            // }
            // else
            // {
            //     Console.WriteLine("something went worng!:-(");
            // }
        }

        private static void ShowHistory(string password, string walletName, string wallet)
        {
            try
            {
                // TODO: Load the Wallet
            }
            catch
            {
                WriteLine("Wrong wallet or password!");
                return;
            }

                // TODO: Create the Client and get the Received History of the Account

            string header = "-----COINS RECEIVED-----";
            WriteLine(header);

                // TODO: Display the Received History of the Account

            WriteLine(new string('-', header.Length));
    
                 // TODO: Get the Spent History of the Account

            string footer = "-----COINS SPENT-----";
            WriteLine(footer);
                 // TODO: Display the Spent History of the Account
            WriteLine(new string('-', footer.Length));
        }

        private static void ShowBalance(string pw, string walletName, string wallet)
        {
            try
            {
                // TODO: Load the Wallet.
            }
            catch
            {
                WriteLine("Wrong wallet or password!");
                return;
            }

            QBitNinjaClient client = new QBitNinjaClient(Network.TestNet);
            decimal totalBalance = 0;

            // TODO: Calculate the Total Balance.

            WriteLine($"Balance of wallet: {totalBalance}");
        }

        private static void Receive(string password, string walletName)
        {
            try
            {
                // TODO: Load the Wallet and Display all the Addresses in it.
            }
            catch (Exception)
            {
                WriteLine("Wallet with such name does not exist!");
            }
        }

        private static void RecoverWallet(string password, Mnemonic mnemonic, string date)
        {
            // TODO: Implement the logic to Recover and Save the Wallet.
            WriteLine("Wallet successfully recovered");
        }

        private static void CreateWallet()
        {
            Network currentNetwork = Network.TestNet;
            string pw;
            string pwConfirmed;
            do
            {
                Write("Enter password: ");
                pw = ReadLine();
                Write("Confirm password: ");
                pwConfirmed = ReadLine();
                if (pw != pwConfirmed)
                {
                    WriteLine("Passwords did not match!");
                    WriteLine("Try again.");
                }
            } while (pw != pwConfirmed);

            bool failure = true;
            while (failure)
            {
                try
                {
                    Write("Enter wallet name:");
                    string walletName = ReadLine();
                    Mnemonic mnemonic;

                    // TODO: Initialize the Wallet with the Correct Parameters.
                    Safe safe = Safe.Create(out mnemonic, pw, walletFilePath + walletName + ".json", currentNetwork);

                    WriteLine("Wallet created successfully");
                    WriteLine("Write down the following mnemonic words.");
                    WriteLine("With the mnemonic words AND the password you can recover this wallet.");
                    WriteLine();
                    WriteLine("----------");
                    WriteLine(mnemonic);
                    WriteLine("----------");
                    WriteLine(
                        "Write down and keep in SECURE place your private keys. Only through them you can access your coins!");

                    // TODO: Display the Addresses and the Correspondig Private Keys.
                    for (int i = 0; i < 10; i++ )
                    {
                        WriteLine($"Address: {safe.GetAddress(i) } -> Private key: {safe.FindPrivateKey(safe.GetAddress(i))}");
                    }

                    failure = false;
                }
                catch
                {
                    WriteLine("Wallet already exists");
                }
            }
        }
    }
}

