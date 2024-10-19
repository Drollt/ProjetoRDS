using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntityStats : MonoBehaviour
{
    public Scrollbar lifeScrollBar;

    public float maxLife;
    public float life;

    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        //Controla a barra de vida
        lifeScrollBar.size = life / maxLife;

        if(life <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
