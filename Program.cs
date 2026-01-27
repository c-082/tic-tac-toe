using TicTacToe.Models;
using TicTacToe.Bots;

char playerSymbol = GetSymbols(out char botSymbol);
Board board = new(playerSymbol, botSymbol);
Play();
Console.WriteLine("Thanks for playing! Press any key to exit");
Console.ReadKey();
void Play()
{
    bool isPlaying = true;
    bool isPlayerTurn = Random.Shared.Next(2) == 1;
    char gameDifficulty = GetGameDifficulty();
    Action bot = gameDifficulty == 'H' ? HardBotMove : (gameDifficulty == 'N' ? NormalBotMove : EasyBotMove);
    while (isPlaying)
    {
        board.PrintBoard();
        if (isPlayerTurn)
        {
            while (!PlayerMove())
            {
            }
        }
        else
        {
            bot();
        }
        if (board.CheckWinner() != GameResult.Ongoing)
        {
            break;
        }
        isPlayerTurn = !isPlayerTurn;
        Console.Clear();
    }
    board.PrintBoard();
    Console.WriteLine($"""
    Game over!
    {board.CheckWinner() switch
    {
        GameResult.PlayerWin => "Player wins",
        GameResult.BotWin => "Bot wins",
        GameResult.Draw => "It's a draw",
        _ => throw new InvalidOperationException("Game is still ongoing")
    }}
    """);
    board.ClearBoard();
    if (AskPlayAgain())
    {
        Play();
    }
}
bool PlayerMove()
{
    Console.Write("Enter row: ");
    var playerMoveInputRow = Console.ReadLine() ?? "";
    if (!int.TryParse(playerMoveInputRow, out int playerMoveRow) || playerMoveRow is > 3 or < 1)
    {
        Console.WriteLine("Invalid input");
        return false;
    }
    Console.Write("Enter column: ");
    var playerMoveInputColumn = Console.ReadLine() ?? "";
    if (!int.TryParse(playerMoveInputColumn, out int playerMoveColumn) || playerMoveColumn is > 3 or < 1)
    {
        Console.WriteLine("Invalid input");
        return false;
    }
    playerMoveRow--;
    playerMoveColumn--;
    var emptyCells = board.GetEmptyCells();
    if (!emptyCells.Any(cell => cell[0] == playerMoveRow && cell[1] == playerMoveColumn))
    {
        Console.WriteLine("This cell is already filled");
        return false;
    }
    board.Move([playerMoveRow, playerMoveColumn], playerSymbol);
    return true;
}
void EasyBotMove() => _ = board.Move(new EasyBot(board).GetMove(), botSymbol);
void NormalBotMove() => _ = board.Move(new NormalBot(board, playerSymbol, botSymbol).GetMove(), botSymbol);
void HardBotMove() => _ = board.Move(new HardBot(board, playerSymbol, botSymbol).GetMove(), botSymbol);
char GetSymbols(out char botSymbol)
{
    char playerSymbol;
    do
    {
        Console.WriteLine("Choose your symbol: X/O");
        playerSymbol = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();
    } while (playerSymbol is not ('X' or 'O'));
    botSymbol = playerSymbol switch
    {
        'X' => 'O',
        'O' => 'X',
        _ => throw new InvalidOperationException("Invalid symbol")
    };
    return playerSymbol;
}
char GetGameDifficulty()
{
    char gameDifficulty;
    do
    {
        Console.WriteLine("""
        Enter game difficulty
            E: Easy
            N: Normal
            H: Hard
        """);
        gameDifficulty = char.ToUpper(Console.ReadKey().KeyChar);
        Console.WriteLine();
    } while (gameDifficulty is not ('E' or 'N' or 'H'));
    return gameDifficulty;
}
bool AskPlayAgain()
{
    bool? playAgain = null;
    do
    {
        Console.WriteLine("Do you want to play again? Y/N");
        playAgain = char.ToUpper(Console.ReadKey().KeyChar) switch
        {
            'Y' => true,
            'N' => false,
            _ => null
        };
        Console.WriteLine();
    } while (playAgain == null);
    return (bool)playAgain;
}