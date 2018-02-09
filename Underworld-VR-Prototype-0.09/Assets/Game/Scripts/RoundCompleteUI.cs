using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCompleteUI : MonoBehaviour {

    public Transform playerEye;
    public GameManager gameManager;
    public Material[] mats;
    public GameObject[] triangleMeshes;

    public bool moveBack;
    public bool done;

    public TextMeshPro textSaying;
    public int randomTextNum;

	// Use this for initialization
	void Start () {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        /*foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        StartCoroutine(RoundCurrentTriangleFlash());

        transform.position = new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z);*/

    }

    private void OnEnable()
    {
        foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        for (int i = 1; i <= gameManager.roundCurrent; i++)
        {
            Renderer rend = triangleMeshes[i].GetComponent<Renderer>();
            rend.material = mats[1]; //sets all triangles to complete round UI material
        }

        transform.position = new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z);

        StartCoroutine(RoundCurrentTriangleFlash());

        randomTextNum = Random.Range(1, 20);
        RandomText();

    }

    // Update is called once per frame
    void Update () {

        if (moveBack)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z), Time.deltaTime * 5f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerEye.transform.position + playerEye.transform.forward * 3f, Time.deltaTime * 2.5f);
        }

        if (done)
        {
            moveBack = false;
            done = false;
            this.gameObject.SetActive(false);
        }

        transform.LookAt(playerEye);

    }

    void RandomText()
    {
        switch (randomTextNum)
        {
            case 1:
                textSaying.text = "Ha! I kill me!";
                break;
            case 2:
                textSaying.text = "I have the power!!!";
                break;
            case 3:
                textSaying.text = "Ahh..What a rush!";
                break;
            case 4:
                textSaying.text = "Bozhe moi!";
                break;
            case 5:
                textSaying.text = "I'll be back";
                break;
            case 6:
                textSaying.text = "By the power of Greyskull!";
                break;
            case 7:
                textSaying.text = "Cut, It, Out";
                break;
            case 8:
                textSaying.text = "Eat My Shorts";
                break;
            case 9:
                textSaying.text = "Form blazing sword!";
                break;
            case 10:
                textSaying.text = "Freeze! This is Miami Vice!";
                break;
            case 11:
                textSaying.text = "Go, Joe!";
                break;
            case 12:
                textSaying.text = "Go, Go Gadget Go!";
                break;
            case 13:
                textSaying.text = "I Play For Keeps!";
                break;
            case 14:
                textSaying.text = "It's crime fighting time!";
                break;
            case 15:
                textSaying.text = "Let's Get Dangerous";
                break;
            case 16:
                textSaying.text = "Punky Power!";
                break;
            case 17:
                textSaying.text = "Resistance is not futile.";
                break;
            case 18:
                textSaying.text = "Synchronise Swatches!";
                break;
            case 19:
                textSaying.text = "Thundercats, Ho!";
                break;
            case 20:
                textSaying.text = "Holy macanoli!";
                break;
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

        moveBack = true;
        yield return new WaitForSeconds(1f);

        done = true;

    }
}
