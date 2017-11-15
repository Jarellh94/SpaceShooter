using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLivesUI : MonoBehaviour {

    public GameObject playerImage;

    PlayerController player;

	public void AddLife()
    {
        Instantiate(playerImage, transform, false);
    }

    public void RemoveLife()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
