using UnityEngine;

public class PlacaInterativa : MonoBehaviour
{
    public GameObject balaoUI; // Referência ao balão de fala (UI)
    public string textoDaPlaca = "Bem-vindo à Vila!";

    private void Start()
    {
        if (balaoUI != null)
            balaoUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (balaoUI != null)
            {
                balaoUI.SetActive(true);
                // Se quiser alterar o texto dinamicamente:
                // balaoUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = textoDaPlaca;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (balaoUI != null)
                balaoUI.SetActive(false);
        }
    }
}