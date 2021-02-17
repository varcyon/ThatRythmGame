using UnityEngine;
[System.Serializable]
public class Note : MonoBehaviour {
    public float time;
    public bool enemyNote;
    public Color enemyColor;
    public bool superNote;
    MeshRenderer MeshRenderer;
    public Material noteMaterial;
    public float displacementAmount = 1;
    public float displacement;
    public float displaceSpeed = 1;
    private void Start() {
        MeshRenderer = GetComponent<MeshRenderer>();
        //sets the material because it doesn't save when it saves the prefab
        MeshRenderer.material = noteMaterial;
        //randomizes the mesh displacement 
        displacement = Random.Range(0, displacementAmount);
        //if the note is marked as enemy note it changes the main color of the note
        if (enemyNote) {
            MeshRenderer.material.SetColor("_MainColor", enemyColor);
        }
    }
    private void Update() {
        // this goes back and forth from no displacement to the displacement ammount
        displacement -= Time.deltaTime * displaceSpeed;
        if (displacement <= 0) {
            displacement = displacementAmount;
        }
        //sets the value in the shader
        MeshRenderer.material.SetFloat("_VertexDisplaceAmount", displacement);
    }

    //contructor
    public void SetNote(float time, bool enemyNote, bool superNote) {
        this.time = time;
        this.enemyNote = enemyNote;
        this.superNote = superNote;
    }

}
