using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SongScoreDB", menuName = "ScriptableObjects/SongScoreDB")]
public class SongScoreDB : ScriptableObject
{
    public List<SongData> songScores = new List<SongData>();
}
