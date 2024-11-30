using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void change_button() //change_button‚Æ‚¢‚¤–¼‘O‚É‚µ‚Ü‚·
    {
        SceneManager.LoadScene("Main");//second‚ğŒÄ‚Ño‚µ‚Ü‚·
    }
}