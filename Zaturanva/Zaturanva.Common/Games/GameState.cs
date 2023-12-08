using System.Diagnostics.CodeAnalysis;

using Zaturanva.Common.Armies;
using Zaturanva.Common.ChessBoard;
using Zaturanva.Common.Colors;
using Zaturanva.Common.Contestants.PlayerManagement;
using Zaturanva.Common.Contestants.TeamManagement;

namespace Zaturanva.Common.Games;

public class GameState
{
	public required Players Players { get; init; }
	public GameOptions GameOptions { get; init; } = new();

	public Color? FocusColor { get; set; }

	public Color? ActiveColor { get; set; }

	public TurnPhase TurnPhase { get; set; }

	public required Board Board { get; init; }

	private readonly Dictionary<Color, Army> _armyByColor = new();

	public GameState SetBlackArmy(Army army)
	{
		_armyByColor[Color.Black] = army;
		return this;
	}

	public GameState SetWhiteArmy(Army army)
	{
		_armyByColor[Color.White] = army;
		return this;
	}

	public GameState SetBlueArmy(Army army)
	{
		_armyByColor[Color.Blue] = army;
		return this;
	}

	public GameState SetOrangeArmy(Army army)
	{
		_armyByColor[Color.Orange] = army;
		return this;
	}

	public Army this[Color color]
		=> _armyByColor[color];

	[SuppressMessage(
		"Style",
		"IDE1006:Naming Styles",
		Justification = "<Pending>"
	)]
	// ReSharper disable once InconsistentNaming
	private Dictionary<Team, Alliance>? __allianceByTeam;

	private Dictionary<Team, Alliance> _allianceByTeam
	{
		get
		{
			__allianceByTeam ??= new()
			{
				[Team.Achromatics]
					= new()
					{
						_armyByColor[Color.Black],
						_armyByColor[Color.White],
					},
				[Team.Vivids] = new()
				{
					_armyByColor[Color.Blue], _armyByColor[Color.Orange],
				},
			};

			return __allianceByTeam;
		}
	}

	public Alliance this[Team team]
		=> _allianceByTeam[team];

	public Alliance FindAllianceFor(Color color)
		=> _allianceByTeam.Values.First(
			alliance => alliance.Contains(_armyByColor[color])
		);
}