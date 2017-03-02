using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConsctructor : MonoBehaviour
{

    public GameObject character;

    private bool IsWaveOk = false;
    private GameManager gm = GameManager.instance;

    void Awake()
    {
    }

	void Start ()
    {
        StartCoroutine(InstanciateTheCurrentWave(Random.Range(gm.minCharactersByWave, gm.maxCharactersByWave)));
    }
	
	void Update ()
    {
    }

    private IEnumerator InstanciateTheCurrentWave(int nbrCharacters)
    {
        for(int i = 0; i < nbrCharacters; i++)
        {
            yield return new WaitForSeconds(Random.Range(5, 10) * gm.speedDificulty);
            Instantiate(character, new Vector3(gm.MinX + 5, 0, Random.Range(gm.MinY, gm.MaxY)), Quaternion.identity, transform);
        }

        IsWaveOk = true;
    }

    public bool IsTheWaveOver()
    {
        return transform.childCount == 0 && IsWaveOk;
    }
}
