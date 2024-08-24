using Nethereum.Web3;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string rpcUrl = "https://sepolia.infura.io/v3/0059b68420c34c30aa87d86a7749bb20";
        var web3 = new Web3(rpcUrl);

        string[] rawTransactions = {
            "f86f0f850512dbafea82520894c5b50ca4f07fab6b0575659d377cb68a489372f787038d7ea4c68000808401546d71a09f0f5998efa2ddac45e8027b133dd00f3da561b960d6fa1681f0916f51fb9e20a06f0f67e437992ccfda0bff6a7e5945745283d773641024dbae34c6e652b369e8",
            "f86f10850512dbafea82520894226107e19264bcd3c4c1e9e618e1e80712200cc787038d7ea4c68000808401546d71a08b4058c59b2be44629040bf532a6c794182dbb0b8f1b1e5afc8b06ac83496244a010ed44df1b7954691d94aa107e488c016a7ce392cbc9ff3a14dcb6dfdf9eb00b",
            "f86f11850512dbafea825208946d6b7d9f7a7d319bbe84021730c56d818c839cdc87038d7ea4c68000808401546d72a08fc8322d26aa7709f2102ffd3e2ffc89eb59643ee2c0bc980ae2efbbfc90ef4fa01cea2085b3338a9706b53fe54961ba42fc86939d0fe888ac83c1a6637f036e8a",
            "f86f12850512dbafea825208944d42d55c38c7dbd38cd11eac4bb3006c08be749887038d7ea4c68000808401546d72a0cccda4a8cfda02bcb247491d1eb17290203be52dfc33401975ed95c4cb284794a0622e383ef5fd6db9cbcdbf4e25f983032040e0c3625ac02179618dc07e04154f",
        };

        for (int i = 0; i < rawTransactions.Length; i++)
        {
            try
            {
                var txHash = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(rawTransactions[i]);
                Console.WriteLine($"Transaction {i + 1} Hash: {txHash}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending transaction {i + 1}: {ex.Message}");
            }
        }
    }
}
//Transaction 1 Hash: 0x7c4cd82d30c5c86d9942b6973c735763d0ba24555a40a04783d76e07f00025a7
//Transaction 2 Hash: 0x3f8b7579bb77fb3218bd2acfff12fa92b2bdd719ad85e571bb73a7a93aae1308
//Transaction 3 Hash: 0xc2d5274250e89394753c664b1afbaeb16160a70ba796b35cfbe69ca22c8c7fec
//Transaction 4 Hash: 0x89e8b7456414f8ced39a0898bb8edd8fef788136aa284392203e20582620e03b