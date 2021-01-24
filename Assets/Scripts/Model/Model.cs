using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Model : MonoBehaviour
{
    public const int NORMAL_ROWS = 20;//正常行数
    public const int MAX_ROWS = 23;//最大行数
    public const int MAX_COLUMNS = 10;//最大列数

    private Transform[,] map = new Transform[MAX_COLUMNS,MAX_ROWS];

    public int score = 0;
    public int highScore = 0;
    private int nubmersGame = 0;
    public bool isDataUpdate = false;


    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }
    
    public int NumbersGame { get { return NumbersGame; } }

    private void Awake()
    {
        LoadData();
    }

    //用于返回当前这个是否可用
    public bool IsValidMapPosition(Transform transform)
    {
        //遍历当前下落方块的位置
        foreach (Transform child in transform)
        {
            
            //这里的判断是针对 不是放开的posi中心店的 如果tag不为Block就跳到下一个判断
            if (child.tag != "Block") continue;
            //这里调用的是静态类里的静态方法  还可以这样玩啊   参考Vector3Ex类 三维坐标转换为二位坐标系
            Vector2 pos = child.position.Round();
            //判断图形是否超出边界
            if (!IsInsideMap(pos)) return false;
          
            //判断当前这个map二维数组是否有图形存在
            if (map[(int)pos.x-1, (int)pos.y+1] != null) return false;
        }

        return true;
    }


    //判断图形是否超出边界  这个是判断是否在边界之内的
    public bool IsInsideMap(Vector2 pos)
    {

        return pos.x >= 1 && pos.x <MAX_COLUMNS+1 && pos.y >= -1;
       
    }


    //添加方块下落的坐标
    public bool PlaceShape(Transform t)
    {
        foreach (Transform child in t)
        {
            if (child.tag != "Block") continue;
            Vector2 pos = child.position.Round();
            map[(int)pos.x-1, (int)pos.y+1] = child;
        }
        //每次方块落下来检查落下的图形
        return CheckMap();
    }

    //检查地图是否需要消除行
    public bool CheckMap()
    {

        //记录销毁了多上行
        int count = 0;
        for (int i = 0; i < MAX_ROWS; i++)
        {
            bool isFull = CheckIsRowFull(i);
            if (isFull)
            {
                count++;
                //将这一行删除
                DeleteRow(i);
                //将这一行以及下面的删除
                MoveDownRowsAbove(i + 1);
                //这样i保持不变，可以继续判断掉下来的这一行是否可以连续消除
                i--;
            }
        }
        if (count > 0)
        {
            score += (count * 100);
            if(score>highScore)
            {
                highScore = score;
            }
            isDataUpdate = true;
            return true;
        }
          
        else return false;

        
    }


    //判断游戏是否结束
    public bool IsGameOver()
    {
        for(int i=NORMAL_ROWS;i<MAX_ROWS;i++)
        {
            for(int j=0;j<MAX_COLUMNS;j++)
            {
                if (map[j, i] != null)
                {
                    nubmersGame++;
                    SaveData();

                    return true;
                }
                   
            }
        }
        return false;
    }
    //检查这一行是否满了
    private bool CheckIsRowFull(int row)
    {
    
        for (int i = 0; i < MAX_COLUMNS; i++)
        {
           
            if (map[i, row] == null) return false;

        }
        Debug.Log("检查行已经满了");
        return true;
    }


    ///消除一行
    private void DeleteRow(int row)
    {

        for (int i = 0; i < MAX_COLUMNS; i++)
        {
            Destroy(map[i, row].gameObject);
            map[i, row] = null;
        }
    }

    //将这一行以及这一行下面一个向下移动
    private void MoveDownRowsAbove(int row)
    { 
    for(int i=row;i<MAX_ROWS;i++)
        {
            
            MoveDownRow(i);
        }
    
    }


    private void MoveDownRow(int row)
    { 
    for(int i=0;i<MAX_COLUMNS;i++)
        {
            if (map[i, row]!=null)
            {

                map[i, row - 1] = map[i, row];
                map[i, row] = null;
                map[i, row - 1].position += new Vector3(0, -1, 0);



            }

        }
    }

    private void LoadData()
    {
        Debug.Log("读取游戏");
        //默认是0
      highScore=  PlayerPrefs.GetInt("HightScore", 0);
      nubmersGame= PlayerPrefs.GetInt("NumbersGame", 0);
    }

    //保存游戏
    private void SaveData()
    {
        Debug.Log("保存游戏执行了");
        PlayerPrefs.SetInt("HightScore",highScore);
        PlayerPrefs.SetInt("NumbersGame",nubmersGame);

    }

    public void ResetGame()
    {
     for(int i=0;i<MAX_COLUMNS;i++)
        {
            for(int j=0;j<MAX_ROWS;j++)
            {
                if (map[i,j]!=null)
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
            }
        }
        score = 0;
    }

}
