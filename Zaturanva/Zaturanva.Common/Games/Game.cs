using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Contestants.TeamManagement;

namespace Zaturanva.Common.Games;

public class Game
{
	public required Players Players { get; init; }

	public Color? CurrentTurnColor { get; set; }

	public Color? WaitingForColor { get; set; }

	public TurnPhase TurnPhase { get; set; }

	public required Board Board { get; init; }

	private readonly Dictionary<Color, Army> _armyByColor = new();

	public Game SetBlackArmy(Army army)
	{
		_armyByColor[Color.Black] = army;
		return this;
	}

	public Game SetWhiteArmy(Army army)
	{
		_armyByColor[Color.White] = army;
		return this;
	}

	public Game SetBlueArmy(Army army)
	{
		_armyByColor[Color.Blue] = army;
		return this;
	}

	public Game SetOrangeArmy(Army army)
	{
		_armyByColor[Color.Orange] = army;
		return this;
	}

	public Army this[Color color]
		=> _armyByColor[color];

	private readonly Dictionary<Team, Alliance> _allianceByTeam = new();

	public Alliance this[Team team]
	{
		get
		{
			if (_allianceByTeam.Count == 0)
			{
				_allianceByTeam[Team.Achromatics] = new()
				{
					_armyByColor[Color.Black], _armyByColor[Color.White],
				};
				_allianceByTeam[Team.Vivids] = new()
				{
					_armyByColor[Color.Blue], _armyByColor[Color.Orange],
				};
			}

			return _allianceByTeam[team];
		}
	}
}