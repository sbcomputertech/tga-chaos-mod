using System.Linq;
using UnityEngine;

namespace ChaosMod;

public class Level
{
	public static readonly Level LOBBY = new("Lobby", 0);
	public static readonly Level NIGHTMARES = new("Nightmares", 1);
	public static readonly Level TOYS = new("Toys", 2);
	public static readonly Level CURSED = new("Cursed", 3);
	public static readonly Level FUNTIMES = new("Funtimes", 4);
	public static readonly Level ACCIDENT = new("Accident", 5);
	public static readonly Level LOST_MEDIA = new("Lost Media", 6);
	public static readonly Level BACK_ALLEY = new("Back Alley", 7);
	public static readonly Level BASEMENT = new("Basement", 8);
	public static readonly Level MIND = new("Mind", 9);
	public readonly string name;
	public readonly int spawnVar;

	private Level(string name, int spawnVar)
	{
		this.name = name;
		this.spawnVar = spawnVar;
	}

	private static Level GetLevelBySpawnVar(float spawnVar)
	{
		// Plugin.Logging.LogInfo("SpawnVar: " + spawnVar);
		
		// detect mind spawnvar weirdness:
		if (Object.FindObjectsOfType<Mind_MusicManager>().Any() && spawnVar is < 9f and >= 8f)
		{
			Plugin.Logging.LogMessage("Detected mind spawnvar weirdness! Setting spawn to 9");
			PlayerPrefs.SetFloat("spawn", 9f);
		}
		
		return spawnVar switch
		{
			0 => LOBBY,
			1 => NIGHTMARES,
			2 => TOYS,
			3 => CURSED,
			4 => FUNTIMES,
			5 => ACCIDENT,
			6 => LOST_MEDIA,
			7 => BACK_ALLEY,
			8 => BASEMENT,
			_ => MIND
		};
	}

	public static Level GetCurrentLevel() => GetLevelBySpawnVar(PlayerPrefs.GetFloat("spawn"));
}