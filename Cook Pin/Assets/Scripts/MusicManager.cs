using UnityEngine;

public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Tiny little script that ensures that there are no more then two music players at once
    /// </summary>

    static MusicManager instance;

    // Use this for initialization
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}