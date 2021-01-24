using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    //判断图形当前状态
    private bool isPause = false;

    //下落时间
    private float timer = 0;
    //时间间隔
    private float stepTime = 0.8f;
    //加速的倍数
    private int multiple = 10;

    private bool issleep = false;
    private Ctrl ctrl;

    private Transform pivot;

    public GameManager gameManager;


    private void Awake()
    {
        pivot = transform.Find("Pivot");
    }

    private void Update()
    {
        if (isPause) return;
        timer += Time.deltaTime;
        if (timer > stepTime)
        {
            timer = 0;
            Fall();
        
        }

        InputControl();
    }

    public void Init(Color color,Ctrl ctrl,GameManager gameManager)
    {
        //遍历所有子类并且把标签为block的变颜色
        foreach (Transform t in transform)
        {
            if (t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }


        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }


    public void Fall() {
        Vector3 vector3 = gameObject.transform.position;
        vector3.y -= 1;
        transform.position = vector3;

        //这里调用 判断是否下落完成
        if (ctrl.model.IsValidMapPosition(this.transform) == false)
        {
            vector3.y += 1;
            transform.position = vector3;
            isPause = true;
            //方块落下 开始置空 并下落下一个方块
         
           bool isLineclear= ctrl.model.PlaceShape(this.transform);
            if (isLineclear) ctrl.audioManager.PlayLineClean();
            gameManager.FallDown();
            return;
        }


        ctrl.audioManager.PlayDrop();
    
    }



    /*控制方块移动*/
    private void InputControl()
    {
     //   if (issleep) return;
        float h = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            h = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            h = 1;
        if (h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;

            }
            else
            {
                ctrl.audioManager.PlayContorl();
            }


        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
               transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayContorl();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            issleep = true;
            stepTime /= multiple;
        }



        
    }
    //暂停
    public void Pause()
    {
        isPause = true;
    }
    //继续

    public void Resume()
    {
        isPause = false;
    }




}
