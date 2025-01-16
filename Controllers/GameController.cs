using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RPSLS.Services;

namespace RPSLS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("play")]
    [Route("FetchRPSLS")]
    public ActionResult<GameResult> Play([FromBody] PlayerMove playerMove)
    {
        // Check if move is valid
        if (string.IsNullOrEmpty(playerMove.Move))
        {
            return BadRequest("Move cannot be empty!");
        }

        // Check if move is one of the valid options
        string[] validMoves = { "rock", "paper", "scissors", "lizard", "spock" };
        if (!validMoves.Contains(playerMove.Move.ToLower()))
        {
            return BadRequest("Invalid move! Please choose rock, paper, scissors, lizard, or spock.");
        }

        var result = _gameService.PlayGame(playerMove.Move);
        return Ok(result);
    }
}

// Record to receive the player's move
public record PlayerMove
{
    public string Move { get; init; } = string.Empty;
}

// Record for the game result
public record GameResult(string PlayerMove, string ComputerMove, string Result, string Explanation);
    }
