using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPS : CharacterProperty
{
    public float SmoothSpeed = 10.0f;
    public Transform myWaist;
    public SpringArm mySpringArm;
    public AudioClip gunSound;
    public AudioClip bgmSound;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Inst.PlayMusic(bgmSound);
    }

    // Update is called once per frame
    void Update()
    {        
        float x = Mathf.Lerp(myAnim.GetFloat("x") , Input.GetAxisRaw("Horizontal"), Time.deltaTime * SmoothSpeed);
        float y = Mathf.Lerp(myAnim.GetFloat("y"), Input.GetAxisRaw("Vertical"), Time.deltaTime * SmoothSpeed);

        myAnim.SetFloat("x", x);
        myAnim.SetFloat("y", y);

        if(Input.GetMouseButtonDown(0))
        {
            myAnim.SetBool("IsFiring", true);
        }
        if(Input.GetMouseButtonUp(0))
        {
            myAnim.SetBool("IsFiring", false);
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            //bgm volume down
            SoundManager.Inst.bgmSpeaker.volume -= 0.2f;
            //effect volume down
            SoundManager.Inst.EffectVolume -= 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            //bgm volume up
            SoundManager.Inst.bgmSpeaker.volume += 0.2f;
            //effect volume up
            SoundManager.Inst.EffectVolume += 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SoundManager.Inst.PauseMusic(SoundManager.Inst.bgmSpeaker.isPlaying);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            //Pitch down
            SoundManager.Inst.bgmSpeaker.pitch -= 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            //Pitch up
            SoundManager.Inst.bgmSpeaker.pitch += 0.2f;
        }
    }

    private void LateUpdate()
    {
        myWaist.localRotation *= mySpringArm.transform.localRotation;
    }

    public void OnFire()
    {
        if (!myAnim.GetBool("IsFiring")) return;
        AudioSource mySpeeker = GetComponent<AudioSource>();
        mySpeeker.volume = SoundManager.Inst.EffectVolume;
        mySpeeker.PlayOneShot(gunSound);
    }
}
