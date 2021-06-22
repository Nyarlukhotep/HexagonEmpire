namespace Client.Scripts
{
	public enum InputPhase
	{
		UNDEFINED = 0,
		DOWN = 1,
		PRESS = 2,
		UP = 3
	}

	public enum TileContentType
	{
		Default,
		PlayerBase,
		Forest,
		Stone
	}

	public enum TileState
	{
		Closed,
		Open
	}
	
	public enum RewardType
	{
		Gold,
		Wood,
		Stone
	}
}