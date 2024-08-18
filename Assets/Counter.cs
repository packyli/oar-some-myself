using UnityEngine;
using UnityEngine.SceneManagement;


public class Counter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string countAsText;
    private int count;
    private float timeElasped;
    bool gameover = false;

    // Update is called once per frame
    void Update()
    {
        timeElasped += Time.deltaTime;
        if (timeElasped >= 1.0f)
        {
            ++count;
            countAsText = count.ToString();
            timeElasped = 0;
            if (countAsText == "10")
            {
                SceneManager.LoadScene("GameEnds");
            }
        }
    }
}