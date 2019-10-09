using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void GameOver();
}

public class View : MonoBehaviour
{
    private Model action;
    private GUIStyle fontstyle = new GUIStyle();
    private GUIStyle fontstyle1 = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        action = Controller.getInstance().currentModel as Model;
        fontstyle.fontSize = 30;
        fontstyle.normal.textColor = Color.yellow;
        fontstyle1.fontSize = 30;
        fontstyle1.normal.textColor = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGUI(){
        GUI.Label(new Rect(10,40,200,200),"Score: " + action.score, fontstyle);
        GUI.Label(new Rect(10,10,200,200),"Round: " + action.round, fontstyle);
        GUI.Label(new Rect(10,70,200,200),"Trial: " + action.trial, fontstyle);
        if(action.round != 4){
        if (GUI.Button(new Rect(10, 110, 100, 50), "Pause"))
        {
            action.Pause();
        }
        }
        // if (GUI.Button(new Rect(10, 170, 100, 50), "Start"))
        // {
        //     action.Start();
        // }
        if (GUI.Button(new Rect(10, 170, 100, 50), "Restart"))
        {
            action.Restart();
        }        
    }

}
