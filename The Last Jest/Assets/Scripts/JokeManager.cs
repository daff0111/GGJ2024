using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeManager : MonoBehaviour
{
    public Character Jester;
    public Character King;
    public Character Queen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Joke A Told");
            King.AddAngryMeter(20);
            Queen.AddAngryMeter(-20);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Joke B Told");
            King.AddAngryMeter(-40);
            Queen.AddAngryMeter(20);
        }
    }
}
