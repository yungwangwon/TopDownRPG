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
        //토크 데이터
        // npcA : 1000, npcB : 2000
        // frointobj : 100
        talkdata.Add(1000, new string[] { "안녕:0", "Hi22222:1" });
        talkdata.Add(2000, new string[] { "이 호수는 정말 아름답지?:0", "사실 이 호수에는 비밀이 숨겨져있어:1" });
        talkdata.Add(100, new string[] { "This is front onject" });
        talkdata.Add(5000, new string[] { "근처에서 동전을 찾았다." });


        //퀘스트 데이터
        talkdata.Add(1000 + 10, new string[] 
        {"어서와:0",
        "이 마을에 놀라운 전설이 있다는데 : 1",
        "오른쪽 호수 앞 루도가 알려줄거야.:3"});

        talkdata.Add(2000 + 11, new string[]
        {"동전:0",
        "주워:1 ",
        "임마:2"});
        talkdata.Add(2000 + 21, new string[]
        {"엇, 찾아줘서 고마워ㅣ:0"});

        //Npc_A
        //0 : 기본, 1 : 화,
        //2 : 웃는모습, 3 : 대화
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
		//해당 퀘스트 진행 순서에 대사가 없을때
		//퀘스트 처음 대사를 가지고온다.
		if (!talkdata.ContainsKey(id))
		{
            //퀘스트 처음 대사 또한 존재하지 않을경우
            if (!talkdata.ContainsKey(id - id % 10))
                return GetTalkData(id - id % 100, talkindex);    //기본 대사
            else
                return GetTalkData(id - id % 10, talkindex);    //퀘스트 처음 대사
        }

		if (talkindex == talkdata[id].Length)
            return null;
        else
            return talkdata[id][talkindex];
	}

}
