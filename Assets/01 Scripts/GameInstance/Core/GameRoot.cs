using UnityEngine;
using UnityEngine.XR;

public class GameRoot : MonoBehaviour
{
    private static GameRoot _instance;
    public static GameRoot Instance => _instance;
    public SpawnSystem SpawnSystem { get; private set; }
    public SaveSystem SaveSystem { get; private set; }
    public AudioSystem AudioSystem { get; private set; }
    public IEventBus GameEventBus { get; private set; }
    public GameInputSystem InputSystem { get; private set; }


    [SerializeField] private SpawnDatabase _spawnDatabase;
    [SerializeField] private AudioDatabase _audioDatabase;
    [SerializeField] private GameObject _audioPrefab;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            InitSystems();
            return;
        }
        if(_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
    private void InitSystems()
    {
        GameEventBus = new GameEventBus();

        SpawnSystem = new SpawnSystem(_spawnDatabase);

        SaveSystem = new SaveSystem(new JsonFileSaveProvider());

        AudioSystem = new AudioSystem(_audioDatabase,_audioPrefab);
    
        InputSystem = new GameInputSystem();
        InputSystem.EnableGameplay();
    }
 
   
}
