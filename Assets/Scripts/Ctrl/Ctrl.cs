using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;

    [HideInInspector]
    public Camemain camemain;
    [HideInInspector]
    public GameManager Gamemanger;

    [HideInInspector]
    public AudioManager audioManager;

    private FSMSystem fsm;
    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        camemain = GetComponent<Camemain>();
        Gamemanger = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
    }
    void Start()
    {
        MakeFSM();
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();

        foreach (FSMState state in states)
        {
            fsm.AddState(state,this);
        }

        MenuState s = GetComponentInChildren<MenuState>();
        fsm.SetCurrentState(s);


    }
}
