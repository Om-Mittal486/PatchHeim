using UnityEngine;

public class Quit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Quit triggered");
        Application.Quit();
    }
}
