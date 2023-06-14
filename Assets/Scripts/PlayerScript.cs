using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    float h;
    float v;
    bool isHorizonMove;

    Rigidbody2D rigid;
    Animator ani;
    Vector3 dirVec;
    GameObject scanObj;

    public GameManager manager;

    //moblie key
    int up_value;
    int down_value;
    int left_value;
    int right_value;
    bool up_down;
    bool down_down;
    bool left_down;
    bool right_down;
    bool up_up;
    bool down_up;
    bool left_up;
    bool right_up;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        manager.isAction = false;

    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("충돌");
	}

    // Update is called once per frame
    void Update()
    {
        if (!manager.isAction)
        {
            //이동
            h = Input.GetAxisRaw("Horizontal") + (right_value + left_value);
            v = Input.GetAxisRaw("Vertical") + (up_value + down_value);

            bool hdown = Input.GetButtonDown("Horizontal") || (left_down || right_down);
            bool vdown = Input.GetButtonDown("Vertical") || (up_down || down_down);
            bool hup = Input.GetButtonUp("Horizontal") || (left_up || right_up);
            bool vup = Input.GetButtonUp("Vertical") || (up_up || down_up);

            if (hdown)
                isHorizonMove = true;
            else if (vdown)
                isHorizonMove = false;
            else if (hup || vup)
                isHorizonMove = h != 0;


            //애니메이션
            if (ani.GetInteger("hAxisRaw") != h)
            {
                ani.SetBool("isChange", true);
                ani.SetInteger("hAxisRaw", (int)h);
            }
            else if (ani.GetInteger("vAxisRaw") != v)
            {
                ani.SetBool("isChange", true);
                ani.SetInteger("vAxisRaw", (int)v);
            }
            else
                ani.SetBool("isChange", false);

            //방향
            if (vdown && v == 1)
                dirVec = Vector3.up;
            if (vdown && v == -1)
                dirVec = Vector3.down;
            if (hdown && h == 1)
                dirVec = Vector3.right;
            if (hdown && h == -1)
                dirVec = Vector3.left;
        }

        //스캔(스페이스)
        if (Input.GetButtonDown("Jump") && scanObj != null)
            manager.Action(scanObj);

        //mobile var init
        up_down = false; ;
        down_down = false;
        left_down = false;
        right_down = false;
        up_up = false;
        down_up = false;
        left_up = false;
        right_up = false;
    }


    private void FixedUpdate()
	{
        //이동
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * 5;

        //레이캐스트
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObj = rayHit.collider.gameObject;
        else
            scanObj = null;

    }

    public void ButtonDown(string type)
	{
        switch (type)
        {
            case "Up":
                up_value = 1;
                up_down = true;
                break;
            case "Down":
                down_value = -1;
                down_down = true;
                break;
            case "Left":
                left_value = -1;
                left_down = true;
                break;
            case "Right":
                right_value = 1;
                right_down = true;
                break;
            case "Action":
                if (scanObj != null)
                    manager.Action(scanObj);
                break;
            case "Cancel":
                manager.SubMenuActive();
                break;
        }
	}

	public void ButtonUp(string type)
    {
        switch (type)
        {
            case "Up":
                up_value = 0;
                up_up = false;
                break;
            case "Down":
                down_value = 0;
                down_up = false;
                break;
            case "Left":
                left_value = 0;
                left_up = false;
                break;
            case "Right":
                right_value = 0;
                right_up = false;
                break;
        }
    }
}
