using TicTacToe.Models;

namespace TicTacToe.Bots;

internal class EasyBot(Board board)
{
    public int[] GetMove()
    {
        var emptyCells = board.GetEmptyCells();
        int[] move = emptyCells[Random.Shared.Next(emptyCells.Count - 1)];
        return move;
    }
}