namespace TicTacToe.Models;

internal class Board
{
    private readonly char[,] cells = new char[3, 3];
    private readonly char playerSymbol, botSymbol;
    public Board(char playerSymbol, char botSymbol)
    {
        this.playerSymbol = playerSymbol;
        this.botSymbol = botSymbol;
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cells[i, j] = '-';
            }
        }
    }
    public void PrintBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("+---+---+---+");
            Console.Write('|');
            for (int j = 0; j < 3; j++)
            {
                Console.Write($" {cells[i, j]} |");
            }
            Console.WriteLine();
        }
        Console.WriteLine("+---+---+---+");
    }
    public bool Move(int[] cell, char symbol)
    {
        if (cells[cell[0], cell[1]] != '-')
        {
            return false;
        }
        cells[cell[0], cell[1]] = symbol;
        return true;
    }
    public List<int[]> GetEmptyCells()
    {
        var list = new List<int[]>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (cells[i, j] == '-')
                {
                    list.Add([i, j]);
                }
            }
        }
        return list;
    }
    public GameResult CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (IsWinningLine(cells[i, 0], cells[i, 1], cells[i, 2]))
            {
                return cells[i, 0] switch
                {
                    var c when c == playerSymbol => GameResult.PlayerWin,
                    var c when c == botSymbol => GameResult.BotWin,
                    _ => throw new InvalidOperationException("Invalid symbol")
                };
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (IsWinningLine(cells[0, i], cells[0, 1], cells[0, 2]))
            {
                return cells[0, i] switch
                {
                    var c when c == playerSymbol => GameResult.PlayerWin,
                    var c when c == botSymbol => GameResult.BotWin,
                    _ => throw new InvalidOperationException("Invalid symbol")
                };
            }
        }
        if (IsWinningLine(cells[0, 0], cells[1, 1], cells[2, 2]) ||
            IsWinningLine(cells[0, 2], cells[1, 1], cells[2, 0]))
        {
            return cells[1, 1] switch
            {
                var c when c == playerSymbol => GameResult.PlayerWin,
                var c when c == botSymbol => GameResult.BotWin,
                _ => throw new InvalidOperationException("Invalid symbol")
            };
        }
        if (GetEmptyCells().Count == 0)
        {
            return GameResult.Draw;
        }
        return GameResult.Ongoing;
    }
    public void ClearBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Clear([i, j]);
            }
        }
    }
    private static bool IsWinningLine(char a, char b, char c) => a != '-' && a == b && b == c;
    /// <remarks>
    /// Do NOT call this method outside TicTacToe.Bots or TicTacToe.Models.Board as it will overwrite the existing cell
    /// </remarks>
    public void Clear(int[] cell) => cells[cell[0], cell[1]] = '-';
}