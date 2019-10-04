using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bir_kod : MonoBehaviour
{
     void Start()
    {
        AnaMenuDon();
    }

    void AnaMenuDon()
    {
        StartCoroutine(cagrilan_metot());
    }

    IEnumerator cagrilan_metot()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("ana_menu");
    }

}
