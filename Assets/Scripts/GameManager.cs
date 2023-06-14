using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Animator talkpanel;
    public TextMeshProUGUI talkbox;//;;
    public GameObject scanobj;
    public GameObject menuset;
    public GameObject cursor;
    public TalkManager talkmanager;
	public Image portraitimg;
    public Animator portraitani;
    public QuestManager questmanager;
    public Sprite preportrait;
    public TypingEffect talk;
    public TextMeshProUGUI questtext;


    public bool isAction;
    public int talkindex = 0;

	void Start()
	{
        //저장된 내용 불러오기
        GameLoad();
        //퀘스트 내용 가져오기
        questtext.text = questmanager.CheckQuest();
    }

	void Update()
	{
        // ESC버튼
		if(Input.GetButtonDown("Cancel"))
		{
            SubMenuActive();
            isAction = menuset.activeSelf;
        }
    }

    //메뉴창
    public void SubMenuActive()
	{
        if (menuset.activeSelf)
            menuset.SetActive(false);
        else
            menuset.SetActive(true);
        isAction = menuset.activeSelf;
    }

	public void Action(GameObject scan)
	{
        scanobj = scan;
        ObjectData objdata = scanobj.GetComponent<ObjectData>();
        Talk(objdata.id, objdata.isNpc);

        talkpanel.SetBool("isShow", isAction);
    }

    //대화 상호작용
    private void Talk(int id, bool isnpc)
    {
        int questtalkindex = 0;
        string talkdata = "";

        if (talk.iseffecting)
        { 
            talk.Setmsg("");
            return;
        }
        else
		{
            //토크데이터 생성
            questtalkindex = questmanager.GetQuestTalkIndex(id);
            talkdata = isnpc ? talkmanager.GetTalkData(id + questtalkindex, talkindex)
            : talkmanager.GetTalkData(id, talkindex);
        }

        //토크데이터가 없을경우
        if (talkdata == null)
        {
            cursor.SetActive(false);
            isAction = false;
            talkindex = 0;
            //퀘스트 내용 가져오기(퀘스트진행)
            questtext.text = questmanager.CheckQuest(id);
        }
        else//토크데이터가 있을경우
        {
            //npc인경우
            if (isnpc)
            {
                talk.Setmsg(talkdata.Split(':')[0]);
                portraitimg.sprite = talkmanager.GetPortraitData(id, int.Parse(talkdata.Split(':')[1]));
                portraitimg.color = new Color(1, 1, 1, 1);
                if(preportrait != portraitimg.sprite)
				{
                    portraitani.SetTrigger("move");
                    preportrait = portraitimg.sprite;
                }
            }
            else
            {
                talk.Setmsg(talkdata);
                portraitimg.color = new Color(1, 1, 1, 0);
            }
            isAction = true;
            talkindex++;
        }
    }

    //게임 저장
    public void GameSave()
    {
        //정보 저장 플레이어위치, 퀘스트id, 퀘스트actionindex
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questmanager.questid);
        PlayerPrefs.SetInt("QuestActionIndex", questmanager.questactionindex);
        PlayerPrefs.Save();

        //메뉴창 닫기
        menuset.SetActive(false);
    }

    //게임 불러오기
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
		
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questid = PlayerPrefs.GetInt("QuestId");
        int questactionindex = PlayerPrefs.GetInt("QuestActionIndex");

        player.transform.position = new Vector3(x, y, 0);
        questmanager.questid = questid;
        questmanager.questactionindex = questactionindex;
        questmanager.ControlObject();

        menuset.SetActive(false);
    }

    //게임 종료
    public void GameExit()
	{
        Application.Quit();
	}
}
