using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public GameObject target;
    public GameObject waves;
    public GameObject wave;

    public int numberOfWave;
    public int minCharactersByWave;
    public int maxCharactersByWave;
    [Range(0.1f, 1f)]
    public float speedDificulty;

    public static GameManager instance = null;

    private GameObject _currentWave;
    
    private float _minX, _maxX, _minY, _maxY;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        InitialGame();
        //TEST
    }

    void Start ()
    {
    }
	
	void Update ()
    {
        GameLoop();
    }

    private void InitialGame()
    {
        // Set the level
        LoadRandomLevel();

        // Set the boundaries
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector3 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        _minX = bottomCorner.x;
        _maxX = topCorner.x;

        _minY = bottomCorner.z;
        _maxY = topCorner.z;

        // Set the initial random position of target
        target.transform.position = new Vector3(_maxX - 5, 5, Random.Range(_minY, _maxY));
    }

    private void LoadRandomLevel()
    {
        GameObject wa;
        for (int i = 0; i < numberOfWave; i++)
        {
            wa = Instantiate(wave, waves.transform);
            wa.name = "Wave" + i;
        }
        
        // Set the first Wave
        _currentWave = waves.transform.GetChild(0).gameObject;
        _currentWave.SetActive(true);
    }

    private void GameLoop()
    {
        if (_currentWave != _currentWave.transform.parent.GetChild(waves.transform.childCount-1).gameObject && _currentWave.GetComponent<WaveConsctructor>().IsTheWaveOver())
        {
            _currentWave = _currentWave.transform.parent.GetChild(_currentWave.transform.GetSiblingIndex() + 1).gameObject;
            _currentWave.SetActive(true);
        }
    }

    public float MinX
    {
        get{return _minX;}
        set{_minX = value;}
    }

    public float MaxX
    {
        get{return _maxX;}
        set{_maxX = value;}
    }

    public float MinY
    {
        get{return _minY;}
        set{_minY = value;}
    }

    public float MaxY
    {
        get{return _maxY;}
        set{_maxY = value;}
    }
}
