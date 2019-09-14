using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    public PlayerController player;

    [Header("Level Spawning")]

    private float angleSpawnMin = 50*Mathf.Deg2Rad;
    private float maxSegment = 25;
    private int nbSegmentActive=0;
    private int nbSegmentContinu=0;
    private float currentSpawnAngle;
    private Vector3 laneY;
    private int difficulty = 0;

    public float nbSegmentBeforeTransition;
    public float _radius;

    //List of segment
    public List<SegmentList> availableSegments = new List<SegmentList>();
    public List<SegmentList> availableTransition = new List<SegmentList>();
    private List<Segment> segmentsSpawn = new List<Segment>();


    private void Awake()
    {
        Instance = this;
        currentSpawnAngle = 20*Mathf.Deg2Rad;
    }

    // Start is called before the first frame update
    void Start()
    {
        while (currentSpawnAngle < angleSpawnMin)
            GenerateSegment();
    }

    private void Update()
    {
        if (currentSpawnAngle < angleSpawnMin + player._rotateAngle)
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

        Vector3 position = s.transform.position;
        position.x = Mathf.Cos(currentSpawnAngle) * _radius;
        position.z = Mathf.Sin(currentSpawnAngle) * _radius;

        s.transform.position = position;
        s.transform.eulerAngles = new Vector3(0, 180+(-currentSpawnAngle) * Mathf.Rad2Deg, 0);

        currentSpawnAngle += s.Lenght*Mathf.Deg2Rad;
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
