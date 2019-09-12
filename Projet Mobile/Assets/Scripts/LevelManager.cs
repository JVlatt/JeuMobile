using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    public PlayerController player;

    [Header("Level Spawning")]

    private float distanceSpawnMin = 50;
    private float maxSegment = 15;
    private int nbSegmentActive=0;
    private int nbSegmentContinu=0;
    private float currentSpawnZ;
    private Vector3 laneY;
    private int difficulty = 0;

    public float nbSegmentBeforeTransition;

    //List of segment
    public List<SegmentList> availableSegments = new List<SegmentList>();
    public List<SegmentList> availableTransition = new List<SegmentList>();
    private List<Segment> segmentsSpawn = new List<Segment>();


    private void Awake()
    {
        Instance = this;
        currentSpawnZ = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        while (currentSpawnZ < distanceSpawnMin)
            GenerateSegment();
    }

    private void Update()
    {
        if (currentSpawnZ < distanceSpawnMin + player.transform.position.z)
            GenerateSegment();
    }


    private void GenerateSegment()
    {
        SpawnSegment(false);

        if(Random.Range(0f,nbSegmentBeforeTransition)< (nbSegmentContinu))
        {
            //random Spawn of Transition 
            nbSegmentContinu = 0;
            SpawnSegment(true);
        }
        else
        {
            nbSegmentContinu++;
        }
    }

    private void SpawnSegment(bool isTransition)
    {
        List<Segment> posibleSeg;

        if (isTransition)
            posibleSeg = availableTransition[difficulty].segments.FindAll(x => x.startY.x == laneY.x || x.startY.y == laneY.y || x.startY.z == laneY.z); 
        else
            posibleSeg = availableSegments[difficulty].segments.FindAll(x => x.startY.x == laneY.x || x.startY.y == laneY.y || x.startY.z == laneY.z);

        int id = Random.Range(0, posibleSeg.Count);

        // random difficulty ?

        Segment s = posibleSeg[id];

        s = GetSegment(s.ID, difficulty, isTransition);

        laneY = s.endY;

        s.transform.position = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.Lenght;
        nbSegmentActive++;
        s.Spawn();
        if (nbSegmentActive > maxSegment)
        {
            int i = 1;
            while (!segmentsSpawn[segmentsSpawn.Count - i].gameObject.activeSelf)
            {
                i++;
            }
            segmentsSpawn[segmentsSpawn.Count -i].DeSpawn();
            nbSegmentActive--;
        }
    }


    public Segment GetSegment(int id,int sDifficulty,bool transition)
    {
        Segment s = null;
        s = segmentsSpawn.Find(x => x.ID == id && x.difficulty == sDifficulty && x.isTransition == transition && !x.gameObject.activeSelf);

        if(s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransition[sDifficulty].segments[id].gameObject : availableSegments[sDifficulty].segments[id].gameObject) as GameObject ;
            s = go.GetComponent<Segment>();

            s.ID = id;
            s.isTransition = transition;
            s.difficulty = sDifficulty;

            segmentsSpawn.Insert(0, s);
        }
        else
        {
            segmentsSpawn.Remove(s);
            segmentsSpawn.Insert(0, s);
        }

        return s;
    }


    [System.Serializable]
    public class SegmentList
    {
        public List<Segment> segments;
    }
}
