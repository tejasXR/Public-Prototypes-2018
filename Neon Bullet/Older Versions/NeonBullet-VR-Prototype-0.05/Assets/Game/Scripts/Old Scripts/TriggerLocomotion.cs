using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLocomotion : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj; //defines a tracked object
    public Vector3 controllerForward;
    public Vector2 touchPad;
    public float triggerAxis;
    public GameObject cameraRig;
    public GameObject player; //camera Eye object
    //public AudioSource sprintingAudio;
    //public AudioSource afterSprintingAudio;

    public GameObject bodyCollider;
    //private Rigidbody bodyRb; //rigid body of the body Collider;

    //Movement Speeds
    //public float sprintInertia;
    //public float sprintInertiaSpeed;

    public float moveSpeed;
    //public float staminaStart;
    //public float stamina;
    //public float staminaRecovery;
    //public float staminaDrain;
    //public float sprintSpeed;

    //private bool isSprinting;
    //private bool sprintSoundPlayed;

    // Use this for initialization
    void Start()
    {
        //stamina = staminaStart;
    }

    // Update is called once per frame
    void Update()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object
        triggerAxis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //Gets depth of trigger press    

        /*if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad) && stamina >= 0f)
        {
            stamina -= staminaDrain * Time.deltaTime;
            sprintInertia = Mathf.Lerp(sprintInertia, 1, Time.deltaTime * sprintInertiaSpeed);
            isSprinting = true;
            sprintSpeed = (stamina / staminaStart);

            if (stamina >= .1f)
            {
                SprintSound();
                sprintingAudio.volume = Mathf.Lerp(sprintingAudio.volume, .07f, Time.deltaTime / 2);
            }
        }
        else
        {
            sprintInertia = Mathf.Lerp(sprintInertia, 0, Time.deltaTime);
            sprintingAudio.volume = Mathf.Lerp(sprintingAudio.volume, 0f, Time.deltaTime);
            sprintSpeed = 0;
            stamina += staminaRecovery * Time.deltaTime;
            if (stamina >= staminaStart)
            {
                stamina = staminaStart;
            }
            sprintSoundPlayed = false;

        }*/
    }

    private void FixedUpdate()
    {
        //Make the bodyCollider follow the player around
        bodyCollider.transform.position = new Vector3(player.transform.position.x, bodyCollider.transform.position.y, player.transform.position.z); //always follow the player around
        cameraRig.transform.position = new Vector3(cameraRig.transform.position.x, bodyCollider.transform.position.y, cameraRig.transform.position.z);

        if (triggerAxis > .15f) //If the trigger is pressed passed a certain threshold
        {
            //Assemble beginning variables
            controllerForward = trackedObj.transform.forward;
            moveSpeed = triggerAxis;// + (sprintSpeed * sprintInertia);
            Vector3 direction = new Vector3(controllerForward.x, 0, controllerForward.z);

            //If the bodyCollider hits something within a .5 unit distance, stop, else, move the whole cameraRig
            RaycastHit hit;
            if (!Physics.SphereCast(new Vector3(bodyCollider.transform.position.x, bodyCollider.transform.position.y + .5f, bodyCollider.transform.position.z), .35f, direction, out hit, .35f, -1, QueryTriggerInteraction.Ignore))
            {
                cameraRig.transform.position = Vector3.Lerp(new Vector3(cameraRig.transform.position.x, bodyCollider.transform.position.y, cameraRig.transform.position.z), cameraRig.transform.position + direction, Time.deltaTime * moveSpeed * 5);
            }
            //Debug.DrawRay(new Vector3(bodyCollider.transform.position.x, bodyCollider.transform.position.y + .5f, bodyCollider.transform.position.z), direction, Color.green);
        }
    }

    /*private void SprintSound()
    {
        if (!sprintSoundPlayed)
        {
            sprintingAudio.Play();
            sprintSoundPlayed = true;
        }
    }*/
}
