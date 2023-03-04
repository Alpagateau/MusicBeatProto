using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject[] playerTypes;
    public KeepObjectAlive ke;

    public Vector3 p1Position;
    public Vector3 p2Position;

    public PlayerProperties Player1Properties;
    public PlayerProperties Player2Properties;

    public Image HealthBarP1;
    public Image HealthBarP2;

    private void Start()
    {
        ke = GameObject.FindObjectOfType<KeepObjectAlive>();
        if (ke != null)
        {
            GameObject Player1 = GameObject.Instantiate(playerTypes[ke.p1 - 1], p1Position, Quaternion.identity);
            GameObject Player2 = GameObject.Instantiate(playerTypes[ke.p2 - 1], p2Position, Quaternion.identity);

<<<<<<< Updated upstream
        player p1 = Player1.GetComponent<player>();
        player p2 = Player2.GetComponent<player>();
        healthControl HealthP1 = Player1.GetComponent<healthControl>();
        healthControl HealthP2 = Player2.GetComponent<healthControl>();
        p1.keyCodes = Player1Properties.keyCodes;
        p2.keyCodes = Player2Properties.keyCodes;
        p1.otherPlayer = p2;
        p2.otherPlayer = p1;
        p1.isLookingRight = true;
        p2.isLookingRight = false;
        HealthP1.HealthBar = HealthBarP1;
        HealthP2.HealthBar = HealthBarP2;
=======
            Player p1 = Player1.GetComponent<Player>();
            Player p2 = Player2.GetComponent<Player>();
            /*Ordi Ordi1 = Player1.GetComponent<Ordi>();
            Ordi Ordi2 = Player2.GetComponent<Ordi>();
            GameObject mouse = GameObject.Find("Mouse");
            GameObject cable = GameObject.Find("Cable");*/
            p1.keyCodes = Player1Properties.keyCodes;
            p2.keyCodes = Player2Properties.keyCodes;
            p1.otherPlayer = p2;
            p2.otherPlayer = p1;
            p1.isLookingRight = true;
            p2.isLookingRight = false;
        }
        /*if (ke.p1==1)
        {
            Ordi1.mouse = mouse;
            Ordi1.mouseCable = cable;
        }
        if (ke.p2==1)
        {
            Ordi2.mouse = mouse;
            Ordi2.mouseCable = cable;
        }*/
>>>>>>> Stashed changes
    }
}
