
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PheobiNash;
using System;

namespace PheobiNash
{
    /// <summary>
    /// Author: Pheobi Nash
    /// Description: This script demonstrates how to ... in Unity
    /// </summary>
    public class SceneTransition : MonoBehaviour
    {


        public void SceneChange(string sceneToChangeTo)
        {
            SceneManager.LoadSceneAsync(sceneToChangeTo, LoadSceneMode.Single);

        }

        public void StopGame()
        {
            Application.Quit();
        }


    }
}

