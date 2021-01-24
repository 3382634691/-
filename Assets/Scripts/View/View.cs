using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class View : MonoBehaviour
{
    // Start is called before the first frame update

    private RectTransform logoName;
    private RectTransform menuUI;
    private RectTransform gameUI;
    private GameObject reset;
    private GameObject gameOverUI;
    private GameObject SettingUI;
    private GameObject RankUI;

    private Text score;
    private Text highScore;
    private Text gameOverScore;
  

    void Awake()
    {
        logoName = transform.Find("Canvas/Logoname") as RectTransform;
        menuUI = transform.Find("Canvas/menuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        reset = GameObject.Find("Canvas/menuUI/Reset_Button").gameObject;
        score = transform.Find("Canvas/GameUI/Scoldtip/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HightScore/Text").GetComponent<Text>();
        SettingUI = transform.Find("Canvas/SettingUI").gameObject;
        gameOverUI = transform.Find("Canvas/GameOver").gameObject;
        RankUI = transform.Find("Canvas/RanckUI").gameObject;
        gameOverScore = transform.Find("Canvas/GameOver/Text").GetComponent<Text>();
 
    }
    

    public void ShowMenu()
    {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(170f, 0.5f);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(50f, 0.5f);
    }
    public void HideMenu()
    {
        logoName.DOAnchorPosY(283f, 0.5f)
            .OnComplete(delegate { logoName.gameObject.SetActive(false); });

        menuUI.DOAnchorPosY(-50f, 0.5f)
           .OnComplete(delegate { menuUI.gameObject.SetActive(false); });

    }

    public void UpdateGameUI(int score, int highScore)
    {

        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }
    public void ShowGameUI(int score=0,int highScore=0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-32.34f,0.5f);
    }


    public void  HideGameUI()
    {
        gameUI.DOAnchorPosY(54.7f, 0.5f)
              .OnComplete(delegate { gameUI.gameObject.SetActive(false); });


    }


    public void ShowRestButton()
    {
        reset.SetActive(true) ;
    
    }
    // Update is called once per frame
public void ShowGameOverUI(int score=0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }

    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    public void ShowSetingUI()
    {
        SettingUI.SetActive(true);
    }
    //重新加载场景
    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ShowRankUI()
    {

        RankUI.SetActive(true);
    }

    public void HideRankUI()
    {

        RankUI.SetActive(false);
    }

    public void OnsettingUIClick()
    {
        SettingUI.SetActive(false);
    }

}
