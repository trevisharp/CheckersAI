using CheckersAI.View;
using CheckersAI.Model;
using CheckersAI.AI.Majors;
using CheckersAI.AI.Marshals;

var white = new HumanPlayer();
var black = new AIPlayer()
{
    Marshal = new DeepMarshal()
    {
        Major = new ClassicMajor()
    }
};

var game = new CheckerGame(white, black);

game.Open();