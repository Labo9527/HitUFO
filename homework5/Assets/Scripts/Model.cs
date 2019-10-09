using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ForModel{
    void LoadResources();
    void Pause();
    void Resume();
}

public class Model : MonoBehaviour, ForModel
{
    public int score;
    public int round;
    public int mytime;
    public int trial;
    Controller controller;
    public Factory myfactory;

    void Awake(){
        controller = Controller.getInstance();
        controller.setFPS(60);
        controller.currentModel = this;
        controller.running=true;
        LoadResources();
        score=0;
        round=1;
        trial=0;
        mytime=0;
        myfactory=Sing.Instance;
        InvokeRepeating("updatetime",1f,1f);
        InvokeRepeating("gentiral",1f,3f);
    }

    public void Start(){
        Debug.Log(myfactory);
        for(int i=0; i<10;i++){
            myfactory.genUFO();
        }
    }

    public void Restart(){
        controller.running=false;
        score=0;
        round=1;
        trial=0;
        mytime=0;
        List<GameObject> ufos = myfactory.UFO_on;
        List<GameObject> t= new List<GameObject>();
        foreach(GameObject ufo in ufos){
            t.Add(ufo);
        }
        ufos.Clear();
        foreach(GameObject st in t){
            myfactory.recycleUFO(st);
        }
        for(int i=0; i<10;i++){
            myfactory.genUFO();
        }
        controller.running=true;
    }

    public void Resume(){

    }

    public void Pause(){
        controller.running=!controller.running;
    }

    public void LoadResources(){

    }

    public void gentiral(){
        if(controller.running == false)
            return;
        trial+=1;
        while (myfactory.UFO_on.Count < 10){
            myfactory.genUFO();
        }
    }

    public void updatetime(){
        if(controller.running == false)
            return;
        mytime+=1;
        if(mytime>=30){
            mytime=0;
            trial=0;
            round+=1;
        }
        if(round == 4){
            Pause();
        }
        Debug.Log(mytime);
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(controller.running == false)
            return;
        List<GameObject> ufos = myfactory.UFO_on;
        try{
        foreach(GameObject ufo in ufos) 
        {
            Vector3 v = new Vector3(ufo.transform.localRotation.x,ufo.transform.localRotation.y,ufo.transform.localRotation.z);
            ufo.transform.Translate(v*Time.deltaTime*round*2);
            if(ufo.transform.position.x > 10 || ufo.transform.position.x < -10){
                myfactory.recycleUFO(ufo);
            }
            if(ufo.transform.position.y > 10 || ufo.transform.position.y < -10){
                myfactory.recycleUFO(ufo);
            }
            if(ufo.transform.position.z < -10){
                myfactory.recycleUFO(ufo);
            }
        }
        }
        catch{

        }


        if (Input.GetButtonDown("Fire1")) {
			// Debug.Log ("Fired Pressed");
			Debug.Log (Input.mousePosition);

			Vector3 mp = Input.mousePosition; //get Screen Position

			//create ray, origin is camera, and direction to mousepoint
			Camera ca;
			ca = Camera.main;

			Ray ray = ca.ScreenPointToRay(Input.mousePosition);

			//Return the ray's hits
			RaycastHit[] hits = Physics.RaycastAll (ray);

			foreach (RaycastHit hit in hits) {
                if(hit.transform.gameObject == null){
                    continue;
                }
                if(hit.transform.gameObject.name == "ufo1(Clone)"){
                    score += 1;
                }
                if(hit.transform.gameObject.name == "ufo2(Clone)"){
                    score += 2;
                }
                if(hit.transform.gameObject.name == "ufo3(Clone)"){
                    score += 3;
                }
				print (hit.transform.gameObject.name);
				if (hit.collider.gameObject.tag.Contains("Finish")) { //plane tag
					// Debug.Log ("hit " + hit.collider.gameObject.name +"!" ); 
				}
                myfactory.recycleUFO(hit.transform.gameObject);
                Debug.Log("on:"+myfactory.UFO_on.Count);
                Debug.Log("off"+myfactory.UFO_off.Count);
				// Destroy (hit.transform.gameObject.transform.parent.gameObject);
			}
		}	

    }
}
