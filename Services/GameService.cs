using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPSLS.Services
{
    public record GameResult(string PlayerMove, string ComputerMove, string Result, string Explanation);
    public class GameService
    {
        private readonly Random _random = new Random();
    private readonly string[] _possibleMoves = { "rock", "paper", "scissors", "lizard", "spock" };

    public GameResult PlayGame(string playerMove)
    {
        // Convert to lowercase for easier comparison
        playerMove = playerMove.ToLower();
        
        // Get computer's random move
        var computerMove = GetComputerMove();
        
        // Figure out who won
        string result;
        string explanation;

        // Check if it's a tie
        if (playerMove == computerMove)
        {
            result = "Tie";
            explanation = $"Both players chose {playerMove}";
        }
        // If not a tie, determine the winner
        else if (IsPlayerWinner(playerMove, computerMove))
        {
            result = "You Win!";
            explanation = GetMoveExplanation(playerMove, computerMove);
        }
        else
        {
            result = "Computer Wins!";
            explanation = GetMoveExplanation(computerMove, playerMove);
        }

        // Create new GameResult record
        return new GameResult(playerMove, computerMove, result, explanation);
    }

    private string GetComputerMove()
    {
        return _possibleMoves[_random.Next(_possibleMoves.Length)];
    }

    private bool IsPlayerWinner(string playerMove, string computerMove)
    {
        // Define all winning combinations
        return (playerMove, computerMove) switch
        {
            ("rock", "scissors") => true,
            ("rock", "lizard") => true,
            ("paper", "rock") => true,
            ("paper", "spock") => true,
            ("scissors", "paper") => true,
            ("scissors", "lizard") => true,
            ("lizard", "paper") => true,
            ("lizard", "spock") => true,
            ("spock", "scissors") => true,
            ("spock", "rock") => true,
            _ => false
        };
    }

    private string GetMoveExplanation(string winner, string loser)
    {
        // All possible winning combinations and their explanations
        if (winner == "rock" && loser == "scissors") return "Rock crushes Scissors";
        if (winner == "rock" && loser == "lizard") return "Rock crushes Lizard";
        if (winner == "paper" && loser == "rock") return "Paper covers Rock";
        if (winner == "paper" && loser == "spock") return "Paper disproves Spock";
        if (winner == "scissors" && loser == "paper") return "Scissors cuts Paper";
        if (winner == "scissors" && loser == "lizard") return "Scissors decapitates Lizard";
        if (winner == "lizard" && loser == "paper") return "Lizard eats Paper";
        if (winner == "lizard" && loser == "spock") return "Lizard poisons Spock";
        if (winner == "spock" && loser == "rock") return "Spock vaporizes Rock";
        if (winner == "spock" && loser == "scissors") return "Spock smashes Scissors";
        
        return "Invalid move combination";
    }
    }
}