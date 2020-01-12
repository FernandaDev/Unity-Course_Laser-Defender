using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer musicPlayer { get; private set; }

    private void Awake()
    {
        if (musicPlayer == null)
        {
            musicPlayer = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
