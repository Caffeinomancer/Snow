using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    //public    
    public int mapSize = 0;

    public GameObject grassTile;

    public bool lockLevel = false;


    //private

    private const float halfTileSize = 0.5f;
    private const float generationOffSet = 1;
    private float halfSize;

    private bool isAppRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        halfSize = mapSize / 2;
        isAppRunning = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void GenerateLevel()
    {
        if(!lockLevel && mapSize > 0 && mapSize % 2 == 0 && isAppRunning)
        {
            Generate();
        }
        else
        {
            Debug.LogError("Could not generate level, either genertion is locked, size is less than 1 or is not an even number, or app is not running");
        }
    }
    private void Generate()
    {
        if(isAppRunning)
        {
            CleanUpOldLevel();

            int tOffset = 0;
            for (int i = 0; i < mapSize; i++)
            {
                tOffset++;
                for (int j = 0; j < mapSize; j++)
                {
                    GameObject temp = Instantiate(grassTile, new Vector3(-halfSize + halfTileSize + j, halfSize - halfTileSize - i), Quaternion.identity);
                    temp.transform.parent = gameObject.transform;
                }
            }
        }
    }

    private void CleanUpOldLevel()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
