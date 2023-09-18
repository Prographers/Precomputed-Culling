using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;
using System.Linq;
using Unity.Profiling;

/// <summary>
///     Script that would record the stats of the game
/// </summary>
public class ProfileStats : MonoBehaviour
{
    public const string TRIANGLES_COUNT = "Triangles Count";
    public const string VERTICES_COUNT = "Vertices Count";
    public const string BATCHES_COUNT = "Batches Count";
    public const string SETPASSCALLS_COUNT = "SetPass Calls Count";
    
    private ProfilerRecorder _trianglesCount;
    private ProfilerRecorder _verticesCount;
    private ProfilerRecorder _batchesCount;
    private ProfilerRecorder _setPassCallsCount;
    
    private bool _saved = false;
    
    private List<Sample> Samples = new List<Sample>();
    
    private void Start()
    {
        Application.targetFrameRate = 30;
        
        _trianglesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, TRIANGLES_COUNT, 1850);
        _verticesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, VERTICES_COUNT, 1850);
        _batchesCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, BATCHES_COUNT, 1850);
        _setPassCallsCount = ProfilerRecorder.StartNew(ProfilerCategory.Render, SETPASSCALLS_COUNT, 1850);
    }

    /// <summary>
    ///     Collect the stats of the game and save them to a csv file
    /// </summary>
    private void Update()
    {
        if(Mathf.Min(_trianglesCount.Count, _verticesCount.Count, _batchesCount.Count, _setPassCallsCount.Count) >= 1800)
        {
            if (!_saved)
            {
                for (int i = 0; i < Mathf.Min(_trianglesCount.Count, _verticesCount.Count, _batchesCount.Count, _setPassCallsCount.Count); i++)
                {
                    Samples.Add(new Sample()
                    {
                        Frame = i,
                        TrianglesCount = _trianglesCount.GetSample(i).Value,
                        VerticesCount = _verticesCount.GetSample(i).Value,
                        BatchesCount = _batchesCount.GetSample(i).Value,
                        SetPassCallsCount = _setPassCallsCount.GetSample(i).Value
                    });
                }
                
                _saved = true;
                // save to csv
                var header = "Frame,Triangles Count,Vertices Count,Batches Count,SetPass Calls Count";
                var content = string.Join("\n", Samples.Select(s => $"{s.Frame},{s.TrianglesCount},{s.VerticesCount},{s.BatchesCount},{s.SetPassCallsCount}"));
                var csv = $"{header}\n{content}";
                System.IO.File.WriteAllText("profile.csv", csv);
                Debug.Log($"Saved {Samples.Count} samples to {Path.GetFullPath("profile.csv")}");
            }
            return;
        }

    }

    /// <summary>
    ///     Sample of the stats
    /// </summary>
    public class Sample
    {
        public int Frame;
        public long TrianglesCount;
        public long VerticesCount;
        public long BatchesCount;
        public long SetPassCallsCount;
    }
}