using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState :FSMState
{
    private void Awake()
    {
        stateID = StateID.Menu;

        //添加一个状态转换
        AddTransition(Transition.StartButtonClick, StateID.Play);

    }

    public override void DoBeforeEntering()
    {
        ctrl.view.ShowMenu();
        ctrl.camemain.ZoomOut();
    }
    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenu();
    }
    public void StartButtonOnclik()
    {
        ctrl.audioManager.PlayCursor();

        //执行状态转换
        fsm.PerformTransition(Transition.StartButtonClick);
    }

   

}
