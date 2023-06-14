using System.Collections;
using System.Collections.Generic;

public class QuestData
{
	public string questname;
	public int[] npcid;

	public QuestData() { }

	public QuestData(string name, int[] id)
	{
		questname = name;
		npcid = id;
	}
}
