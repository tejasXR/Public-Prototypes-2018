using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCompleteUI : MonoBehaviour {

    public GameManager gameManager;
    public Material[] mats;
    public GameObject[] triangleMeshes;
    public bool done;

	// Use this for initialization
	void Start () {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        StartCoroutine(RoundCurrentTriangleFlash());

    }

    private void OnEnable()
    {
        foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        for (int i = 0; i <= gameManager.roundCurrent; i++)
        {
            Renderer rend = triangleMeshes[i].GetComponent<Renderer>();
            rend.material = mats[1]; //sets all triangles to complete round UI material
        }

        StartCoroutine(RoundCurrentTriangleFlash());

    }

    // Update is called once per frame
    void Update () {

        if (done)
        {
            this.gameObject.SetActive(false);
        }
		
	}

    IEnumerator RoundCurrentTriangleFlash()
    {
        triangleMeshes[gameManager.roundCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[gameManager.roundCurrent].SetActive(true);
        yield return new WaitForSeconds(1f);

        //done = true;

    }
}
