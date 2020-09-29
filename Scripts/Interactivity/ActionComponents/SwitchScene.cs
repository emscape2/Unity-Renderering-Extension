using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : Consequence
{
    public int Ind_scene;

    public override void Engage()
    {
        SceneManager.LoadScene(Ind_scene);
    }
}
