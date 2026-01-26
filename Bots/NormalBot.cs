using TicTacToe.Models;

namespace TicTacToe.Bots;

internal class NormalBot(Board board, char playerSymbol, char botSymbol)
{
    public int[] GetMove()
    {
        var move = Win() ?? Block() ?? GetCenter() ?? GetCorners() ?? RandomMove();
        return move;
    }
    private int[]? Win()
    {
        foreach (var emptyCell in board.GetEmptyCells())
        {
            board.Move(emptyCell, botSymbol);
            if (board.CheckWinner() == GameResult.Draw)
            {
                board.Clear(emptyCell);
                return emptyCell;
            }
            board.Clear(emptyCell);
        }
        return null;
    }
    private int[]? Block()
    {
        foreach (var emptyCell in board.GetEmptyCells())
        {
            board.Move(emptyCell, playerSymbol);
            if (board.CheckWinner() == GameResult.PlayerWin)
            {
                board.Clear(emptyCell);
                return emptyCell;
            }
            board.Clear(emptyCell);
        }
        return null;
    }
    private int[]? GetCenter()
    {
        if (board.GetEmptyCells().Any(c => c[0] == 1 && c[1] == 1))
        {
            return [1, 1];
        }

        return null;
    }
    private int[]? GetCorners() =>
        board.GetEmptyCells()
            .FirstOrDefault(c =>
                c is [0, 0] or [0, 2] or [2, 0] or [2, 2]);
    private int[] RandomMove() => board.GetEmptyCells()[Random.Shared.Next(board.GetEmptyCells().Count)];
}