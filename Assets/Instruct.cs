using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class Instruct : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadSceneAsync(2);
   }
}
