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
        string privateKey = "------";
        string rpcUrl = "https://sepolia.infura.io/v3/0059b68420c34c30aa87d86a7749bb20";

        var account = new Account(privateKey);
        var web3 = new Web3(account, rpcUrl);

        string fromAddress = "----------";
        decimal amountInEther = 0.001m;
        var amountInWei = Web3.Convert.ToWei(amountInEther);

        var gasPrice = await web3.Eth.GasPrice.SendRequestAsync();
        var gasLimit = new HexBigInteger(21000); // Стандартный лимит для простых транзакций

        // Получение текущего nonce
        var currentNonce = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(fromAddress);

        // Список адресатов
        string[] toAddresses = {
            "0xc5B50ca4f07Fab6B0575659d377cB68A489372f7",
            "0x226107E19264BCD3c4C1E9E618e1e80712200cC7",
            "0x6d6B7d9F7A7d319Bbe84021730C56d818C839cDC",
            "0x4D42d55c38c7Dbd38cD11EAC4BB3006C08BE7498"
        };

        for (int i = 0; i < toAddresses.Length; i++)
        {
            var transactionInput = new TransactionInput
            {
                From = fromAddress,
                To = toAddresses[i],
                Value = new HexBigInteger(amountInWei),
                GasPrice = gasPrice,
                Gas = gasLimit,
                Nonce = new HexBigInteger(currentNonce.Value + i) // Увеличиваем nonce для каждой транзакции
            };

            var rawTransaction = await web3.TransactionManager.SignTransactionAsync(transactionInput);
            Console.WriteLine($"Signed Transaction {i + 1}: {rawTransaction}");
        }
    }
}
//Signed Transaction 1: f86f0f850512dbafea82520894c5b50ca4f07fab6b0575659d377cb68a489372f787038d7ea4c68000808401546d71a09f0f5998efa2ddac45e8027b133dd00f3da561b960d6fa1681f0916f51fb9e20a06f0f67e437992ccfda0bff6a7e5945745283d773641024dbae34c6e652b369e8
//Signed Transaction 2: f86f10850512dbafea82520894226107e19264bcd3c4c1e9e618e1e80712200cc787038d7ea4c68000808401546d71a08b4058c59b2be44629040bf532a6c794182dbb0b8f1b1e5afc8b06ac83496244a010ed44df1b7954691d94aa107e488c016a7ce392cbc9ff3a14dcb6dfdf9eb00b
//Signed Transaction 3: f86f11850512dbafea825208946d6b7d9f7a7d319bbe84021730c56d818c839cdc87038d7ea4c68000808401546d72a08fc8322d26aa7709f2102ffd3e2ffc89eb59643ee2c0bc980ae2efbbfc90ef4fa01cea2085b3338a9706b53fe54961ba42fc86939d0fe888ac83c1a6637f036e8a
//Signed Transaction 4: f86f12850512dbafea825208944d42d55c38c7dbd38cd11eac4bb3006c08be749887038d7ea4c68000808401546d72a0cccda4a8cfda02bcb247491d1eb17290203be52dfc33401975ed95c4cb284794a0622e383ef5fd6db9cbcdbf4e25f983032040e0c3625ac02179618dc07e04154f