using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string privateKey = "7bdea1211e15882c9565b5ed7ac544c6ca9f9983a3b749ebc7ff0caee1d60807";
        string rpcUrl = "https://sepolia.infura.io/v3/0059b68420c34c30aa87d86a7749bb20";
        var account = new Account(privateKey);
        var web3 = new Web3(account, rpcUrl);

        string fromAddress = "0x0f268C5244c819Bf737EC0786558f04905F051b9";

        // Получение текущего nonce
        var nonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(fromAddress);

        // Массив адресов для отправки транзакций
        string[] toAddresses = new string[]
        {
            "0xc5B50ca4f07Fab6B0575659d377cB68A489372f7",
            "0x226107E19264BCD3c4C1E9E618e1e80712200cC7",
            "0x6d6B7d9F7A7d319Bbe84021730C56d818C839cDC",
            "0x4D42d55c38c7Dbd38cD11EAC4BB3006C08BE7498"
        };

        decimal amountInEther = 0.001m;
        var amountInWei = Web3.Convert.ToWei(amountInEther);
        var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
        var gasLimit = new HexBigInteger(21000);

        // Отправка каждой транзакции
        foreach (var toAddress in toAddresses)
        {
            var transactionInput = new TransactionInput
            {
                From = fromAddress,
                To = toAddress,
                Value = new HexBigInteger(amountInWei),
                GasPrice = gasPrice,
                Gas = gasLimit,
                Nonce = nonce
            };

            var rawTransaction = await web3.TransactionManager.SignTransactionAsync(transactionInput);
            var txHash = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(rawTransaction);
            Console.WriteLine("Signed Transaction: " + rawTransaction);

            Console.WriteLine($"Transaction to {toAddress} Hash: {txHash}");

            // Увеличиваем nonce для следующей транзакции
            nonce = new HexBigInteger(nonce.Value + 1);
        }
    }
}
