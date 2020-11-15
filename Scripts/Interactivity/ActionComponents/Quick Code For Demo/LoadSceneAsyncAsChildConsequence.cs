using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Threading;

public class LoadSceneAsyncAsChildConsequence : Consequence
{
    public string sceneToLoad;
    Scene Scene;
    bool loading;
    public bool unloadDirectly;
    public override bool CanEngage()
    {
        return true;
    }

    public override void Disengage()
    {
        //base.Disengage();
        engaged = false;

    }

    public override void Engage()
    {
        if (!engaged)
        {
            engaged = true;
            if (!loading)
            {
                loading = true;
                StartCoroutine("SceneLoadCoroutine");
            }
            else
            {
                SceneManager.UnloadSceneAsync(Scene);
                loading = false;


            }
        }
    }

    public IEnumerator SceneLoadCoroutine()
    {
        //yield return new WaitForSeconds(0.1f);
        engaged = new GameObject();
        var task = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        task.allowSceneActivation = true;
        yield return new WaitUntil(() => task.isDone);
        Scene = SceneManager.GetSceneByName(sceneToLoad);
        if (unloadDirectly)
            SceneManager.UnloadSceneAsync(Scene);
        engaged = false;

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