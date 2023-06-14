using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public GameObject endcursor;
    public AudioSource sound;
    public TextMeshProUGUI msgtext;
    public bool iseffecting;
    public int cps;


    int index;
    string targetmsg;
    float interval;

	private void Awake()
	{
        msgtext = GetComponent<TextMeshProUGUI>();
        sound = GetComponent<AudioSource>();

        //타이핑 딜레이
        interval = 1.0f / cps;

    }

    public void Setmsg(string msg)
    {
        //타이핑 이펙트가 작동중일때
        if(iseffecting)
		{
            msgtext.text = targetmsg;
            CancelInvoke();
            EffectEnd();
        }
        else
		{
            targetmsg = msg;
            iseffecting = true;
            EffectStart();
        }
	}

    //타이핑 이펙트 시작
	public void EffectStart()
	{
        //커서 비활성화
        endcursor.SetActive(false);

        msgtext.text = "";
		index = 0;

        Invoke("Effecting", interval);
	}

    //타이핑 이펙트...
	public void Effecting()
    {
        //끝
        if(msgtext.text == targetmsg)
		{
            EffectEnd();
            return;
		}

        msgtext.text += targetmsg[index];
        //sound play
        if(targetmsg[index] != ' ')
		{
            sound.Play();
        }
		index++;

        Invoke("Effecting", interval); ;
    }

    //타이핑 이펙트 종료
    public void EffectEnd()
    {
        iseffecting = false;
        //커서 활성화
        endcursor.SetActive(true);
    }

}
