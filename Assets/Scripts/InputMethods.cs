using UnityEngine;
using UnityEngine.SceneManagement;

public class InputMethods : MonoBehaviour {
    //Script by Dan Urbanczyk
    //These variables are used when referenceing other scripts

    // EDIT: Brian Dornbusch 2/15/2021

    private bool enemyNoteTracking;
    private float normAccuracy;
    private int note;
    
    private int lastBeat; // Used to track when the note passes the player
    private bool acted; // Used to determine whether the player did an action that affects notes during this beat
    private bool blocked; // Used to determine whether the player blocked the current enemy note so damage can be dealt properly



    /// <summary>
    /// // EDIT: Brian Dornbusch 2/15/2021 //////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public PlayerStatsSO playerStatsSO;
    public EnemyStatsSO enemyStatsSO;
    public PlayerManager playerManager;
    public RhythmTracker rhythmTracker;
    public int targetBeat;

    private void Start() {
        playerManager = FindObjectOfType<PlayerManager>();
        enemyStatsSO = FindObjectOfType<Enemy>().enemyStatSO;
        rhythmTracker = GetComponent<RhythmTracker>();
    }
    /// <summary>
    /// ^^^^^// EDIT: Brian Dornbusch 2/15/2021///////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    void Update() {
        enemyNoteTracking = rhythmTracker.NoteIsEnemy();
        normAccuracy = rhythmTracker.GetNormalizedAccuracy();  //All of this is used to reference other scripts
        targetBeat = rhythmTracker.GetTargetBeat();

        if(enemyStatsSO.enemyHealth <= 0 || playerStatsSO.currentHealth <= 0) {
            SceneManager.LoadScene("RewardUI");
        }
        if (normAccuracy < 1f * playerStatsSO.accuracy) { //this should allow the beat to be checked if it falls in between where the note is able to be hit...... ERROR HERE it always returns true

            if (!acted) { // Makes sure the player can't act on a single note more than once
                if(playerStatsSO.amplitude >= .2f) {
                    if (Input.GetKeyDown(KeyCode.A)) {
                        AttackOne();
                        Debug.Log("Attack one");
                    }
                    if (Input.GetKeyDown(KeyCode.S)) {
                        AttackTwo();
                        Debug.Log("Attack two");
                    }
                    if (Input.GetKeyDown(KeyCode.D)) {
                        AttackThree();
                        Debug.Log("Attack three");
                    }
                }
                
                if (Input.GetKeyDown(KeyCode.J)) {
                    Rest();
                    Debug.Log("Rest");
                } else if (normAccuracy == 0 && lastBeat != targetBeat) { // if the player doesn't hit anything and the note passes the player gets these bonuses. Again not working because the note is always
                    playerStatsSO.currentHealth += 5;
                    playerStatsSO.amplitude += .01f;
                }

                if (Input.GetKeyDown(KeyCode.F)) {
                    HealItem();
                    Debug.Log("Healing item used");
                } else if (Input.GetKeyDown(KeyCode.G)) {
                    AmpItem();
                    Debug.Log("Amplitude item used");
                }
            }

            if (!blocked) {
                if (lastBeat != targetBeat) {
                    if (GetComponent<RhythmTracker>().NoteIsEnemy(lastBeat)) { // Checks if the last note was an enemy note for damage
                        playerStatsSO.currentHealth -= enemyStatsSO.enemyDmg/* * TypeAdvantage*/;
                    }
                } else if (enemyNoteTracking && Input.GetKeyDown(KeyCode.Space)) { // Checks if the current note is an enemy note and if the player pressed the block key
                    Debug.Log("You blocked");
                    Guard();
                }
            }
            
            if (lastBeat != targetBeat) {
                acted = false;
                blocked = false;
            }

            lastBeat = targetBeat;
        }
    }
    //These are the methods for input
    void AttackOne() { //WEAPON 1
        acted = true;
        // EDIT: Brian Dornbusch 2/15/2021
        ////////////////////////////////////////////////////////////////////////////
        enemyStatsSO.enemyHealth -=playerManager. WeaponOneAttack/* * TypeAdvantage*/;
        /////////////////////////////////////////////////////////////////////////////
        playerStatsSO.amplitude = playerStatsSO.amplitude - ((.2f - playerStatsSO.efficiency) - ((.2f - playerStatsSO.efficiency) * normAccuracy)); // this calculates amplitde usage. if you time the note perfectly you don't lose any amplitude;
        Debug.Log(playerManager.WeaponOneAttack + "Attack");
        Debug.Log(enemyStatsSO.enemyHealth + "Health");
        //DeleteNote();
    }
    void AttackTwo() { //WEAPOM 2
        acted = true;
        // EDIT: Brian Dornbusch 2/15/2021
        //////////////////////////////////////////////////////////////////////////////
        enemyStatsSO.enemyHealth -= playerManager.WeaponTwoAttack/* * TypeAdvantage*/;
        /////////////////////////////////////////////////////////////////////////////
       playerStatsSO.amplitude = playerStatsSO.amplitude - ((.2f - playerStatsSO.efficiency) - ((.2f - playerStatsSO.efficiency) * normAccuracy));
        Debug.Log(playerManager.WeaponOneAttack + "Attack");
        Debug.Log(enemyStatsSO.enemyHealth + "Health");

        //DeleteNote();
    }
    void AttackThree() { //WEAPON 3
        acted = true;
        // EDIT: Brian Dornbusch 2/15/2021
        //////////////////////////////////////////////////////////////////////////////////////////
        enemyStatsSO.enemyHealth -= playerManager.WeaponThreeAttack/* * TypeAdvantage*/;
        ///////////////////////////////////////////////////////////////////////////////
        playerStatsSO.amplitude = playerStatsSO.amplitude - ((.2f - playerStatsSO.efficiency) - ((.2f - playerStatsSO.efficiency) * normAccuracy));
        Debug.Log(playerManager.WeaponOneAttack + "Attack");
        Debug.Log(enemyStatsSO.enemyHealth + "Health");

        //DeleteNote();
    }
    void Guard() { // both for guarding and enemy attack
        blocked = true;
        acted = true;

        GameObject pStats = GameObject.Find("Player");
        GameObject eStats = GameObject.Find("Enemy");
        PlayerManager playerStats = pStats.GetComponent<PlayerManager>();
        Enemy enemyStats = eStats.GetComponent<Enemy>();
        playerStats.playerStatsSO.currentHealth -= ((enemyStats.enemyStatSO.enemyDmg/* * TypeAdvantage*/) - (playerStats.playerStatsSO.baseDefense * normAccuracy)); //your defense is multiplied by the accuracy of how well you hit the note
        //DeleteNote(); //might need to be a delete enemy note
    }
    void Rest() {
        acted = true;

        GameObject pStats = GameObject.Find("Player");
        PlayerManager playerStats = pStats.GetComponent<PlayerManager>();
        playerStats.playerStatsSO.currentHealth += (50f * normAccuracy) + 10; //determines the health and amplitude recieved based of the accuracy of the note
        playerStats.playerStatsSO.amplitude += .2f * normAccuracy;
        //DeleteNote();
    }

    void HealItem(){
        //placeholder
        GameObject pStats = GameObject.Find("Player");
        PlayerManager playerStats = pStats.GetComponent<PlayerManager>();
        playerStats.playerStatsSO.currentHealth += 100 * normAccuracy;
        //DeleteNote();
    }
    void AmpItem(){
        //placeholder
        GameObject pStats = GameObject.Find("Player");
        PlayerManager playerStats = pStats.GetComponent<PlayerManager>();
        playerStats.playerStatsSO.amplitude += .5f * normAccuracy;
        //DeleteNote();
    }
    /*void DeleteNote() {
        This will delete a note when the input has registered and will be called in each input method
    }*/
}
/* KNOWN ERRORS
 * Normalized accuracy is not working properly as its always returning that there are notes within the limits and needs to be changing when the note is hit
 * the IsEnemyNote check is always returning False
 * Enemy health is not decreasing in the scene
 */
