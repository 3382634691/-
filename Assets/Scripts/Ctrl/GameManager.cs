using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isPause = true;//游戏是否暂停

    private Shape currentShape = null;//将currentShape变量直接在生成下赋值，用空与非空来判断物体是否落地了

    public Shape[] shapes;

    public Color[] colors;

    public Ctrl ctrl;


    public Transform blockHolder;
    private void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        blockHolder = transform.Find("BlockHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause) return;

        //用于一个一个方块的降落
        if (currentShape == null)
        {
            SpawnShape();
        
        }
    }


    public void StartGame()
    {
        isPause = false;
        try {
            currentShape.Resume();
        }
        catch
        {

        }
      
    }


    public void PauseGame()
    {
        isPause = true;
        if (currentShape != null)
            currentShape.Pause();

    }
    //控制方块的生成
    void SpawnShape()
    {
        //随机数  确定生成的图形   
        int index = Random.Range(0, shapes.Length);
        //随机颜色
        int indexColor = Random.Range(0, colors.Length);
        currentShape = Instantiate(shapes[index]);
        currentShape.transform.parent = blockHolder;
        currentShape.Init(colors[indexColor],ctrl,this);
    
    }
    //方块落下来了
    public void FallDown()
    {
        currentShape = null;

        if (ctrl.model.isDataUpdate)
            ctrl.view.UpdateGameUI(ctrl.model.Score, ctrl.model.HighScore);


        //这里是方块被清楚后销毁一下这个物体
        foreach(Transform t in blockHolder)
        {
            if(t.childCount<=1)
            {
                Destroy(t.gameObject);
            }
        }
        if(ctrl.model.IsGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }
}
