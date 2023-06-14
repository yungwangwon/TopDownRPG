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

        //Ÿ���� ������
        interval = 1.0f / cps;

    }

    public void Setmsg(string msg)
    {
        //Ÿ���� ����Ʈ�� �۵����϶�
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

    //Ÿ���� ����Ʈ ����
	public void EffectStart()
	{
        //Ŀ�� ��Ȱ��ȭ
        endcursor.SetActive(false);

        msgtext.text = "";
		index = 0;

        Invoke("Effecting", interval);
	}

    //Ÿ���� ����Ʈ...
	public void Effecting()
    {
        //��
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

    //Ÿ���� ����Ʈ ����
    public void EffectEnd()
    {
        iseffecting = false;
        //Ŀ�� Ȱ��ȭ
        endcursor.SetActive(true);
    }

}
