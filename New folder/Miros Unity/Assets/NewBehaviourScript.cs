using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour {

    public int index, hotstarttime, hotendtime, coldstarttime, coldendtime, scentstarttime, scentendtime;
    public float duration;
    
    public List<Transform> spots = new List<Transform>();
    public List<float> secs = new List<float>();
    public List<int> zooms = new List<int>();
    public List<AudioClip> clips = new List<AudioClip>();

    public Material mat, mat2, mat3, mat4, mat5, mat6;
    public ParticleSystem hotair, coldair, scentair;

    public Transform back;


    IEnumerator next(){
        
        yield return new WaitForSeconds(secs[index-1]);
        
        transform.DOMove(spots[index].position, duration, false);
        transform.DORotateQuaternion(spots[index].rotation, duration);
        
        Camera.main.DOFieldOfView(zooms[index], duration);
        
        GetComponent<AudioSource>().clip = clips[index];
        GetComponent<AudioSource>().Play();        
        
        if(index+1 == spots.Count){
            index = 0;
            GameObject.Find("screen").GetComponent<Renderer>().material = mat6;
        }else{
            index++;
        }     

        if(index == 3){
            back.DORotate(new Vector3(back.transform.rotation.eulerAngles.x, back.transform.rotation.eulerAngles.y-90, back.transform.rotation.eulerAngles.z), duration/2, RotateMode.Fast);
        }

        StartCoroutine("next");

    }
    
    void Start() {

        Invoke("start", 3);
        
        //if(Input.GetMouseButtonUp(0)){



       // }        

    }

    void start(){

            Invoke("hotstart", hotstarttime);
            Invoke("hotend", hotendtime);
            Invoke("coldstart", coldstarttime);
            Invoke("coldend", coldendtime);
            Invoke("scentstart", scentstarttime);
            Invoke("scentend", scentendtime);

            mat.DOColor(Color.white, duration);

            GetComponent<AudioSource>().clip = clips[index];
            GetComponent<AudioSource>().Play();

            index++;

            StartCoroutine("next");

    }

    void hotstart(){
        GameObject.Find("screen").GetComponent<Renderer>().material = mat2;
        hotair.Play();
    }

    void hotend(){
        hotair.Stop();
    }

    void coldstart(){
        GameObject.Find("screen").GetComponent<Renderer>().material = mat3;
        coldair.Play();
    }

    void coldend(){
        coldair.Stop();
    }

    void scentstart(){
        GameObject.Find("screen").GetComponent<Renderer>().material = mat4;
        scentair.Play();
    }

    void scentend(){
        GameObject.Find("screen").GetComponent<Renderer>().material = mat5;
        scentair.Stop();
    }

}
