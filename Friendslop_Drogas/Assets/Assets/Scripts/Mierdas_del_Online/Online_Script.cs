using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Online_Script : MonoBehaviour
{
    public static Online_Script Instance;

    [Header("UI Referencias (Escena Menú)")]
    public TMP_InputField inputCrearSala;
    public TMP_InputField inputUnirseSala;

    [Header("Configuración")]
    public string nombreSalaActual;
    [SerializeField] private string escenaGameplay; 

    private const int MAX_JUGADORES = 3; // Límite de 3 jugadores

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Botón: Crear Partida (Host en Mirror)
    public void CrearPartida()
    {
        string nombre = inputCrearSala.text;
        if (string.IsNullOrEmpty(nombre))
        {
            Debug.LogWarning("Introduce un nombre para la sala.");
            return;
        }

        nombreSalaActual = nombre;
        Debug.Log($"Creando sala como Host: {nombreSalaActual}...");

        // Configuramos el límite de conexiones en el NetworkManager de Mirror
        NetworkManager.singleton.maxConnections = MAX_JUGADORES;

        // Iniciamos como Host (Servidor + Cliente local)
        NetworkManager.singleton.StartHost();

        // Cambiamos a la escena de gameplay usando el NetworkManager de Mirror
        NetworkManager.singleton.ServerChangeScene(escenaGameplay);
    }

    // Botón: Unirse a Partida (Cliente en Mirror)
    public void UnirsePartida()
    {
        string codigoONombre = inputUnirseSala.text;
        if (string.IsNullOrEmpty(codigoONombre))
        {
            Debug.LogWarning("Introduce la IP o nombre de la sala.");
            return;
        }

        nombreSalaActual = codigoONombre;
        Debug.Log($"Uniéndose a la sala: {nombreSalaActual}...");

        // Si usas red local o IP directa, Mirror se conecta a la Network Address especificada
        NetworkManager.singleton.networkAddress = codigoONombre;

        // Iniciamos como Cliente
        NetworkManager.singleton.StartClient();
    }
}
