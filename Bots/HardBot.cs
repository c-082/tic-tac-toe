using TicTacToe.Models;

namespace TicTacToe.Bots;

internal class HardBot(Board board, char playerSymbol, char botSymbol)
{
    public int[] GetMove()
    {
        int bestScore = int.MinValue;
        int[] bestMove = null!;
        foreach (var emptyCell in board.GetEmptyCells())
        {
            board.Move(emptyCell, botSymbol);
            var score = MiniMax(false);
            board.Clear(emptyCell);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = emptyCell;
            }
        }
        return bestMove;
    }
    private int MiniMax(bool isMaximizing)
    {
        if (board.CheckWinner() != GameResult.Ongoing)
        {
            return Evaluate();
        }

        var bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        var symbol = isMaximizing ? botSymbol : playerSymbol;
        var emptyCells = board.GetEmptyCells();
        foreach (var emptyCell in emptyCells)
        {
            board.Move(emptyCell, symbol);
            var recursionScore = MiniMax(!isMaximizing);
            board.Clear(emptyCell);
            bestScore = isMaximizing ? Math.Max(bestScore, recursionScore) : Math.Min(bestScore, recursionScore);
        }
        return bestScore;
    }
    private int Evaluate() => board.CheckWinner() switch
    {
        GameResult.BotWin => 1,
        GameResult.PlayerWin => -1,
        GameResult.Draw => 0,
        GameResult.Ongoing => 0,
        _ => throw new InvalidOperationException("Invalid value for enum TicTacToe.Models.GameResult")
    };
}