using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireballRespawnSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject transparentIndicator;

    private float respawnStartTime;
    [SerializeField] private float respawnDuration;

    [SerializeField] private GameObject fireballObj;

    private bool countingDown;

    private void Start()
    {
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (countingDown == false)
            return;

        float timeLeft = respawnDuration - (Time.time - respawnStartTime);
        if(timeLeft <= 0)
        {
            timerText.text = "";
            countingDown = false;
            transparentIndicator.SetActive(false);

            RespawnFireball();
        }
        else
        {
            timerText.text =  (Mathf.Round(timeLeft * 10.0f) * 0.1f).ToString();
        }
    }

    public void RespawnFireball()
    {
        GameObject spawnedFireball = Instantiate(fireballObj, transform);
        spawnedFireball.transform.localPosition = Vector3.zero;

        spawnedFireball.GetComponent<Fireball>().SetSlot(this);
    }

    public void FireballGrabbed()
    {
        //Fireball was grabbed, show empty visual?
        transparentIndicator.SetActive(true);
    }

    public void FireballExploded()
    {
        //Fireball exploded, start countdown
        respawnStartTime = Time.time;
        countingDown = true;
    }
}
