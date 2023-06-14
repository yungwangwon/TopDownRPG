using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkdata;
    Dictionary<int, Sprite> portrait;
    public Sprite[] portraitarr;


    // Start is called before the first frame update
    void Awake()
    {
        talkdata = new Dictionary<int, string[]>();
        portrait = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
	{
        //��ũ ������
        // npcA : 1000, npcB : 2000
        // frointobj : 100
        talkdata.Add(1000, new string[] { "�ȳ�:0", "Hi22222:1" });
        talkdata.Add(2000, new string[] { "�� ȣ���� ���� �Ƹ�����?:0", "��� �� ȣ������ ����� �������־�:1" });
        talkdata.Add(100, new string[] { "This is front onject" });
        talkdata.Add(5000, new string[] { "��ó���� ������ ã�Ҵ�." });


        //����Ʈ ������
        talkdata.Add(1000 + 10, new string[] 
        {"���:0",
        "�� ������ ���� ������ �ִٴµ� : 1",
        "������ ȣ�� �� �絵�� �˷��ٰž�.:3"});

        talkdata.Add(2000 + 11, new string[]
        {"����:0",
        "�ֿ�:1 ",
        "�Ӹ�:2"});
        talkdata.Add(2000 + 21, new string[]
        {"��, ã���༭ ������:0"});

        //Npc_A
        //0 : �⺻, 1 : ȭ,
        //2 : ���¸��, 3 : ��ȭ
        portrait.Add(1000, portraitarr[0]);
        portrait.Add(1001, portraitarr[1]);
        portrait.Add(1002, portraitarr[2]);
        portrait.Add(1003, portraitarr[3]); 

        
        //npc_B
        portrait.Add(2000, portraitarr[4]);
        portrait.Add(2001, portraitarr[5]);
        portrait.Add(2002, portraitarr[6]);
        portrait.Add(2003, portraitarr[7]);

    }

    public Sprite GetPortraitData(int id, int portraitindex)
    {

        return portrait[id + portraitindex];

    }

    public string GetTalkData(int id, int talkindex)
	{
		//�ش� ����Ʈ ���� ������ ��簡 ������
		//����Ʈ ó�� ��縦 ������´�.
		if (!talkdata.ContainsKey(id))
		{
            //����Ʈ ó�� ��� ���� �������� �������
            if (!talkdata.ContainsKey(id - id % 10))
                return GetTalkData(id - id % 100, talkindex);    //�⺻ ���
            else
                return GetTalkData(id - id % 10, talkindex);    //����Ʈ ó�� ���
        }

		if (talkindex == talkdata[id].Length)
            return null;
        else
            return talkdata[id][talkindex];
	}

}
