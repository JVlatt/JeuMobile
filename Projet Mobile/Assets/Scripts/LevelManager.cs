﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public PlayerController player;

    [Header("Level Spawning")]

    public float nbSegmentBeforeTransition;
    public float nbSegmentBeforeItem;

    public float distanceSpawnMin = 10;
    public float maxSegment = 20;
    private int nbSegmentActive=0;
    private int nbSegmentContinuTransition=0;
    private int nbSegmentContinuItem = 0;
    private Vector3 nextSpawnPosition;
    private Vector3 laneY;
    private int difficulty = 0;
    private List<Transform> transforms = new List<Transform>();
    private int currentWaypoint;

    //List of segment
    public List<SegmentList> availableSegments = new List<SegmentList>();
    public List<SegmentList> availableTransition = new List<SegmentList>();
    private List<Segment> segmentsSpawn = new List<Segment>();

    [Header("List of Item")]
    public List<Item> items;

    [Header("Score")]
    public float PointPerSecond;
    public float PointPerBoss;

    private float CurentPoint;



    private void Awake()
    {
        transforms = player._paths[1]._wayPoints;
        nextSpawnPosition = transforms[0].position;
    }

    // Start is called before the first frame update
    void Start()
    {
        #region SetupSpawning
        foreach (SegmentList item in availableSegments)
        {
            int i = 0;
            foreach (Segment segment in item.segments)
            {
                segment.ID = i;
                i++;
            }
        }

        foreach (SegmentList item in availableTransition)
        {
            int i = 0;
            foreach (Segment segment in item.segments)
            {
                segment.ID = i;
                i++;
            }
        }
        #endregion

        while ((nextSpawnPosition - transforms[0].position).magnitude < distanceSpawnMin)
            GenerateSegment();
    }

    private void Update()
    {
        if (distanceSpawnMin > (nextSpawnPosition - player.transform.position).magnitude )
            GenerateSegment();
        CurentPoint += Time.deltaTime * PointPerSecond;
    }

    public void BossKilled()
    {
        CurentPoint += PointPerBoss;
    }

    private void GenerateSegment()
    {
        SpawnSegment(false);

        if(Random.Range(0f,nbSegmentBeforeTransition)< (nbSegmentContinuTransition))
        {
            //random Spawn of Transition 
            nbSegmentContinuTransition = 0;
            SpawnSegment(true);
        }
        else
        {
            nbSegmentContinuTransition++;
        }
    }

    private void SpawnSegment(bool isTransition)
    {
        float distance = (nextSpawnPosition - transforms[currentWaypoint].position ).magnitude;

        List<Segment> posibleSeg;

        if (isTransition)
            posibleSeg = availableTransition[difficulty].segments.FindAll(x => (x.startY.x == laneY.x && laneY.x !=-1 || x.startY.y == laneY.y && laneY.y != -1 || x.startY.z == laneY.z && laneY.z != -1) && x.Lenght < distance); 
        else
            posibleSeg = availableSegments[difficulty].segments.FindAll(x => (x.startY.x == laneY.x && laneY.x != -1 || x.startY.y == laneY.y && laneY.y != -1 || x.startY.z == laneY.z && laneY.z != -1) && x.Lenght < distance);

        while(posibleSeg.Count == 0)
        {
            nextSpawnPosition = transforms[currentWaypoint].position;
            currentWaypoint++;
            if (currentWaypoint >= transforms.Count)
                currentWaypoint = 0;

            distance = (nextSpawnPosition - transforms[currentWaypoint].position).magnitude;

            if (isTransition)
                posibleSeg = availableTransition[difficulty].segments.FindAll(x => (x.startY.x == laneY.x && laneY.x != -1 || x.startY.y == laneY.y && laneY.y != -1 || x.startY.z == laneY.z && laneY.z != -1) && x.Lenght < distance);
            else
                posibleSeg = availableSegments[difficulty].segments.FindAll(x => (x.startY.x == laneY.x && laneY.x != -1 || x.startY.y == laneY.y && laneY.y != -1 || x.startY.z == laneY.z && laneY.z != -1) && x.Lenght < distance);
        }

        int id = Random.Range(0, posibleSeg.Count);

        // random difficulty ?

        Segment s = posibleSeg[id];

        s = GetSegment(s.ID, difficulty, isTransition);

        laneY = s.endY;

        s.transform.position = nextSpawnPosition;

        s.transform.rotation = Quaternion.LookRotation(transforms[currentWaypoint].position - s.transform.position);
        s.transform.eulerAngles += new Vector3(0, 180, 0);

        nextSpawnPosition += s.transform.TransformDirection(Vector3.back * s.Lenght);
        nbSegmentActive++;

        if (Random.Range(0, nbSegmentBeforeItem) < nbSegmentContinuItem)
        {
            s.Spawn(items);
            nbSegmentContinuItem = 0;
        }
        else
        {
            s.Spawn();
            nbSegmentContinuItem++;
        }
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
