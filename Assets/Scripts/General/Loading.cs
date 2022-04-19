using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    //float time, second;


    // Start is called before the first frame update
    void Start()
    {
        //second = 3;
        Invoke("LoadGame", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(time <5)
        {
            time += Time.deltaTime;
        }*/
    }

    public void LoadGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); 
      
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }
}
