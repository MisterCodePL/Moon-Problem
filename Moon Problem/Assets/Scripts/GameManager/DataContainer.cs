using UnityEngine;
using UnityEngine.SceneManagement;

class DataContainer : MonoBehaviour
{
    private static RocksCounter rocksCounter;
    private static TimeCounter timeCounter;

    public int Rocks { get; private set; }
    public TimeContainer timeContainer { get; private set; }
    public string LevelName { get; private set; }

    public static DataContainer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        var ui = GameObject.Find("Canvas");
        rocksCounter = ui.GetComponentInChildren<RocksCounter>();
        timeCounter = ui.GetComponentInChildren<TimeCounter>();
    }


    private void Store()
    {
        Rocks = rocksCounter.Rocks;
        timeContainer = timeCounter.time;
        if(GameObject.Find("Player")!=null)
        LevelName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        Store();
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
            Destroy(gameObject);
    }
}
