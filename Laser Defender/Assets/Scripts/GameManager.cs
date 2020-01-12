using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public static UnityEvent OnScoreChange = new UnityEvent();

    int score;
    int Score{
        get { return score; } 
        set 
        {
            score = value;
            OnScoreChange.Invoke();
        }
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Score = 0;
    }

    public int GetScore()
    {
        return Score;
    }

    public void AddToScore(int amount)
    {
        Score += amount;
    }

    public void ResetGame()
    {
        Score = 0;
    }

}
