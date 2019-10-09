using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : System.Object
{
    private static Controller _instance;

    public Model currentModel {get; set;}
    public bool running{get; set;}

    public static Controller getInstance(){
        if(_instance == null){
            _instance = new Controller();
        }
        return _instance;
    }

    public int getFPS(){
        return Application.targetFrameRate;
    }

    public void setFPS(int fps){
        Application.targetFrameRate = fps;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
