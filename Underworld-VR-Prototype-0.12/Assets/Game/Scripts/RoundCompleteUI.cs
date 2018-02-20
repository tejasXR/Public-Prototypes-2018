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
    public bool turn;

    public TextMeshPro roundText;
    public TextMeshPro enemyEffectsText;
    public int randomTextNum;

    public GameObject UIWhole;

    public int triangleCurrent;

    public float endDelay;

    public EnemyEffectsManager enemyEffectsManager;


	// Use this for initialization
	void Start () {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyEffectsManager = GameObject.Find("EnemyEffectsManager").GetComponent<EnemyEffectsManager>();

        foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        StartCoroutine(RoundCurrentTriangleFlash());

        transform.position = new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z);*/

    }

    private void OnEnable()
    {
        triangleCurrent = gameManager.roundCurrent - 1;

        foreach (GameObject mesh in triangleMeshes)
        {
            Renderer rend = mesh.GetComponent<Renderer>();
            rend.material = mats[0]; //sets all triangles to incomplete round UI material
        }

        for (int i = 0; i <= (triangleCurrent); i++)
        {
            Renderer rend = triangleMeshes[i].GetComponent<Renderer>();
            rend.material = mats[1]; //sets all triangles to complete round UI material
        }

        CheckRound();
        CheckEnemyEffects();

        transform.position = new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z);

        //StartCoroutine(RoundCurrentTriangleFlash());

        //randomTextNum = Random.Range(1, 20);
        //RandomText();



       

    }

    // Update is called once per frame
    void Update () {

        if (moveBack)
        {
            //transform.position = Vector3.Lerp(transform.position, new Vector3(playerEye.transform.position.x, playerEye.transform.position.y + 1, playerEye.transform.position.z), Time.deltaTime * 5f);
            done = true;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerEye.transform.position + playerEye.transform.forward * 3f, Time.deltaTime * 2.5f);
        }

        if (turn)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 3f);
        }

        if (done)
        {
            moveBack = false;
            done = false;
            this.gameObject.SetActive(false);
        }

        transform.LookAt(playerEye);

    }

    void CheckRound()
    {

        switch (gameManager.roundCurrent)
        {
            case 1:
                roundText.text = "Round 1 / 10";
                break;
            case 2:
                roundText.text = "Round 2 / 10";
                break;
            case 3:
                roundText.text = "Round 3 / 10";
                break;
            case 4:
                roundText.text = "Round 4 / 10";
                break;
            case 5:
                roundText.text = "Round 5 / 10";
                break;
            case 6:
                roundText.text = "Round 6 / 10";
                break;
            case 7:
                roundText.text = "Round 7 / 10";
                break;
            case 8:
                roundText.text = "Round 8 / 10";
                break;
            case 9:
                roundText.text = "Round 9 / 10";
                break;
            case 10:
                roundText.text = "Round 10 / 10";
                break;
        }

        /*
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
        */
    }

    void CheckEnemyEffects()
    {
        switch (enemyEffectsManager.randomEffectInt)
        {
            case 0:
                enemyEffectsText.text = "ENEMY HEALTH INCREASED";
                break;
            case 1:
                enemyEffectsText.text = "ENEMY DAMAGE INCREASED";
                break;
            case 2:
                enemyEffectsText.text = "ENEMY ACCURACY INCREASED";
                break;
            case 3:
                enemyEffectsText.text = "ENEMY FIRE RATE INCREASED";
                break;
            case 4:
                enemyEffectsText.text = "NEW ENEMY HAS ARRIVED";
                break;
        }
    }

    IEnumerator RoundCurrentTriangleFlash()
    {
        UIWhole.SetActive(true);

        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);


        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);

        /*
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);

        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(.1f);
        triangleMeshes[triangleCurrent].SetActive(false);
        yield return new WaitForSeconds(.1f);
        */

        triangleMeshes[triangleCurrent].SetActive(true);
        yield return new WaitForSeconds(endDelay);

        turn = true;
        yield return new WaitForSeconds(endDelay);


        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);

        /*
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(true);
        yield return new WaitForSeconds(.05f);
        UIWhole.gameObject.SetActive(false);
        yield return new WaitForSeconds(.05f);
        */

        //moveBack = true;
        //yield return new WaitForSeconds(1f);

        done = true;

    }
}
