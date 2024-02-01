using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;

    public Sprite firstImage;
    public Sprite secondImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
