using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int questid;
    public int questactionindex;
    public GameObject[] questobj;
    Dictionary<int, QuestData> questdic;

    // Start is called before the first frame update
    void Awake()
    {
        questdic = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        questdic.Add(10, new QuestData(("��ȭ"), new int[] { 1000, 2000 }));

        questdic.Add(20, new QuestData(("���� ã���ֱ�"), new int[] { 5000, 2000 }));

        questdic.Add(30, new QuestData(("����Ʈ Ŭ����"), new int[] { 0 }));

    }

    public int GetQuestTalkIndex(int id)
	{
        return questid + questactionindex;
    }

    public string CheckQuest(int id)
    { 
        //����Ʈ ����Ǿ��� ��
        if (id == questdic[questid].npcid[questactionindex])
            questactionindex++;

        //��Ʈ�� ����Ʈ ������Ʈ
        ControlObject();

        //����Ʈ �Ϸ�
        if (questactionindex == questdic[questid].npcid.Length)
            NextQuest();

        return questdic[questid].questname;

    }

    public string CheckQuest()
    {
        return questdic[questid].questname;
    }

    void NextQuest()
	{
        questid += 10;
        questactionindex = 0;
	}

    public void ControlObject()
    { 
        switch(questid)
		{
            case 10:
                if (questactionindex == 2)
                    questobj[0].SetActive(true); 
                break;
            case 20:
                if (questactionindex == 0)
                    questobj[0].SetActive(true);
                else if (questactionindex == 1)
                    questobj[0].SetActive(false);
                break;
		}
    }

}
