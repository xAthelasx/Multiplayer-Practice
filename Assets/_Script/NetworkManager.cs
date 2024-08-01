using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Singleton 
    public static NetworkManager instance;                                          //Creamos la variable singleton.
    #endregion
    #region Monobehaviour method
    private void Awake()
    {
        if(instance != null && instance != this) { gameObject.SetActive(false); }   //Si la variable está vacia o es diferente de esta desactivamos el objeto.
        else
        {
            instance = this;                                                        //En caso contrario, la variable será lo que contiene este objeto.
            DontDestroyOnLoad(gameObject);                                          //No destruimos la variable.
        }
    }

    private void Start() { PhotonNetwork.ConnectUsingSettings(); }                  //Conectamos con Photon.
    #endregion
    #region Public method
    /// <summary>
    /// Método que crea la sala donde luego se va a jugar.
    /// </summary>
    /// <param name="roomName">Nombre de la sala</param>
    public void createRoom(string roomName) { PhotonNetwork.CreateRoom(roomName); } //Creamos la sala con el nombre que le introducimos por parámetro.
    /// <summary>
    /// Método para unirnos a la sala.
    /// </summary>
    /// <param name="roomName">Nombre de la sala a la que nos vamos a unir.</param>
    public void JoinRoom(string roomName) { PhotonNetwork.JoinRoom(roomName); }     //Usamos el método para unirnos a la sala.
    /// <summary>
    /// Método que se encarga de cambiar de escena.
    /// </summary>
    /// <param name="sceneName">Nombre de la escena a la que hay que cambiar.</param>
    [PunRPC]
    public void ChangeScene(string sceneName) { PhotonNetwork.LoadLevel(sceneName);}//Usamos el método para cambiar de escena.
    #endregion
    #region PhotonCallbacks method
    /// <summary>
    /// Método que se encarga de decirnos que se ha conectado al servidor principal.
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");                                    //Devolvemos un mensaje cuando nos hemos conectado al master server
    }
    /// <summary>
    /// Método que se encarga de devolver el mensaje una vez que ya se ha creado la sala.
    /// </summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);               //Informamos que ya se ha creado la sala.
    }
    #endregion
}
