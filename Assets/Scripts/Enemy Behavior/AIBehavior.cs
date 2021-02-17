using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIBehavior : MonoBehaviour {
    enum State {
        reset,
        wandering,
        tired,
        chasing
    }
    #region Enemy inspector variables
    [SerializeField] private float sightRange = 7.0f;
    [SerializeField] private float wanderSpeed = 2.0f;
    [SerializeField] private float chaseSpeed = 3.5f;
    [SerializeField] private float chaseTime = 5f;
    #endregion
    #region Enemy Script variables
    [SerializeField]private LayerMask _layerMask;
    private State currentState = State.chasing;
    private float stoppingDistance = 1.5f;
    private Vector3 direction;
    private Vector3 destination;
    private Vector3 resetPosition;
    private Quaternion desiredRotation;
    private GameObject player;
    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    private Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    private float restTime = 3f;
    #endregion

    #region Testing variables only to make seure the enemy changes states
    private Renderer _renderer;
    public Color wanderColor = Color.black;
    public Color resetColor = Color.yellow;
    public Color chaseColor = Color.red;
    public Color restColor = Color.white;
    #endregion

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        resetPosition = this.transform.position;

        _renderer = GetComponent<Renderer>();
    }
    private void Update() {
        switch (currentState) {
            case State.wandering: {//Sets the enemy to wander to random destinations
                if (NeedsDestination()) {
                    GetDestination();
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 3f);
                transform.Translate(Vector3.forward * Time.deltaTime * wanderSpeed);
                var rayColor = PathIsBlocked() ? Color.red : Color.green;
                Debug.DrawRay(transform.position, direction * sightRange, rayColor);
                while (PathIsBlocked()) {
                    Debug.Log("Path is blocked");
                    GetDestination();
                }
                var targetToAlert = CheckForAltert();
                if (Vector3.Distance(transform.position, player.transform.position) < sightRange) {
                    currentState = State.chasing;
                }
                _renderer.material.color = wanderColor; // delete line after testing
                break;
            }
            case State.chasing: { // the enemy chases you
                Debug.Log("Chasing");
                _renderer.material.color = chaseColor; // delete line after testing
                transform.LookAt(player.transform);
                transform.Translate(Vector3.forward * Time.deltaTime * chaseSpeed);
                if(Vector3.Distance(transform.position, player.transform.position) < sightRange && chaseTime >= 0){
                    transform.Translate(Vector3.forward * Time.deltaTime * (chaseSpeed/2));
                    chaseTime -= Time.deltaTime;
                    Debug.Log(chaseTime);
                    if(chaseTime < 0) {
                        currentState = State.tired;
                    }
                }
                else {
                    currentState = State.reset;
                }
                break;
            }
            case State.reset: { // resets the enemy position so it always goes back to its roaming area
                _renderer.material.color = resetColor; // delete line after testing
                if (this.transform.position != resetPosition) {
                    transform.LookAt(resetPosition);
                    transform.Translate(Vector3.forward * Time.deltaTime * wanderSpeed);
                }
                if(Vector3.Distance(this.transform.position, resetPosition) < 2f) {
                    currentState = State.wandering;
                    return;
                }
                break;
            }
            case State.tired:{ //Srts the enemy into a rest state for 3 seconds then resets the timer
                _renderer.material.color = restColor;
                if(restTime  >= 0) {
                    restTime -= Time.deltaTime;
                        Debug.Log(restTime);
                }
                if(restTime < 0) {
                    currentState = State.reset;
                    restTime = 3f;
                    chaseTime = 5f;
                }
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            SceneManager.LoadScene("Pre-Battle"); // this will need changing but for now this should work
        }
    }
    private Transform CheckForAltert() {
        float aggroRadius = 10f;
        RaycastHit hit;
        var _angle = transform.rotation * startingAngle;
        var _direction = _angle * Vector3.forward;
        var _pos = transform.position;
        for (var i = 0; i < 24; i++) {
            if (Physics.Raycast(_pos, _direction, out hit, aggroRadius)) {
                var _player = hit.collider.GetComponent<PlayerMovement>();
                if(_player != null) {
                    Debug.DrawRay(_pos, _direction * hit.distance, Color.red);
                    return _player.transform;
                }
                else {
                    Debug.DrawRay(_pos, _direction * hit.distance, Color.black);
                }
            }
            else {
                Debug.DrawRay(_pos, _direction * aggroRadius, Color.white);
            }
            _direction = stepAngle * _direction;
        }
        return null;
    }
    private bool PathIsBlocked() { // checks to see if its path is blocked by the layermask
        Ray ray = new Ray(transform.position, direction);
        var hitSomething = Physics.RaycastAll(ray, sightRange, _layerMask);
        return hitSomething.Any();
    }
    private void GetDestination() {
        Vector3 testPostition = (transform.position + (transform.forward * 4f)) + new Vector3(Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
        destination = new Vector3(testPostition.x, 1f, testPostition.z);
        direction = Vector3.Normalize(destination - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
    }
    private bool NeedsDestination() {
        if(destination == Vector3.zero) {
            return true;
        }
        var distance = Vector3.Distance(transform.position, destination);
        if(distance <= stoppingDistance) {
            return true;
        }
        return false;
    }
}
