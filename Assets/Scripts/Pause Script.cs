
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PheobiNash;

namespace PheobiNash
{
    /// <summary>
    /// Author: Pheobi Nash
    /// Description: This script demonstrates how to ... in Unity
    /// </summary>
    public class PauseScript : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        public bool isPaused = false;
        public void PausePressed()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pauseMenu.SetActive(isPaused);
        }
        //private void Pause()
        //{
        //    pauseMenu.SetActive(true);
        //    Time.timeScale = 0;
        //}
        //private void Unpause()
        //{
        //    pauseMenu.SetActive(false);
        //    Time.timeScale = 1;
        //}
    }
}