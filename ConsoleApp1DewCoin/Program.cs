using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using EllipticCurve;

namespace ConsoleApp1DewCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            PrivateKey key1 = new PrivateKey();
            PublicKey wallet1 = key1.publicKey();

            PrivateKey key2 = new PrivateKey();
            PublicKey wallet2 = key2.publicKey();

            Blockchain dewcoin = new Blockchain(3, 100);

            Console.WriteLine("Start the miner");
            dewcoin.MinePendingTransactions(wallet1);
            Console.WriteLine("\nBalance of wallet is $" + dewcoin.GetBalanceOfWallet(wallet1).ToString());

            Transaction tx1 = new Transaction(wallet1, wallet2, 10);
            tx1.SignTransaction(key1);
            dewcoin.addPendingTransaction(tx1);
            Console.WriteLine("Start the miner");
            dewcoin.MinePendingTransactions(wallet2);
            Console.WriteLine("\nBalance of wallet1 is $" + dewcoin.GetBalanceOfWallet(wallet1).ToString());
            Console.WriteLine("\nBalance of wallet2 is $" + dewcoin.GetBalanceOfWallet(wallet2).ToString());

            string blockJSON = JsonConvert.SerializeObject(dewcoin, Formatting.Indented);
            Console.WriteLine(blockJSON);



            if (dewcoin.IsChainValid())
            {
                Console.WriteLine("Blockchain is valid");
            }
            else
            {
                Console.WriteLine("Blockchain not valid");
            }
                
        }
    }

    

    
}
