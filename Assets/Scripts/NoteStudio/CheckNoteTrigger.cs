using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNoteTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {   //Checks if the collider hit a note
        if (other.CompareTag("Note"))
        {   
            // looking for the difference between actual song time and the time of the note
            //if the difference is less or equal to the time threshold you can destroy the note
            // or whatever else
            float noteTime = other.GetComponent<Note>().time;
            float songTime = NoteRecording.NR.myTime;
            float difference = songTime - noteTime;
            if(difference <= NoteRecording.NR.noteTimeThreshhold)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
