// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to handle typical game management requirements
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
// using Photon2DNetworking;


/// <summary>
/// Game manager.
/// Connects and watch Photon Status, Instantiate Player
/// Deals with quiting the room and the game
/// Deals with level loading (outside the in room synchronization)
/// </summary>
public class NetworkManager : MonoBehaviour
{


	static public NetworkManager Instance;
	// static public Photon2DRealTime photon2DRealTimeInstance;


	private GameObject instance;
	public bool InRoom = false;
	public bool IsConnected = false;
	public bool IsMasterClient = false;

	[Tooltip("The prefab to use for representing the player")]
	[SerializeField]
	private GameObject playerPrefab;

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during initialization phase.
	/// </summary>
	void Start()
	{
		// photon2DRealTimeInstance = new Photon2DRealTime();

		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!IsConnected)
		{
			//SceneManager.LoadScene("PunBasics-Launcher");

			return;
		}

		if (playerPrefab == null) { // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

			Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
		} else {


			if (InRoom)
			{
				Debug.LogFormat("We are Instantiating LocalPlayer from {0}");

				// we're in a room. spawn a character for the local player. it gets synced by using Photon2DRealTime.Instantiate
				//photon2DRealTimeInstance.Instantiate();
			}else{

				Debug.LogFormat("Ignoring scene load for {0}");
			}


		}

	}

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity on every frame.
	/// </summary>
	void Update()
	{
		// "back" button of phone equals "Escape". quit app if that's pressed
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			QuitApplication();
		}
	}

	public void OnJoinedRoom()
	{

		Debug.LogFormat("We are Instantiating LocalPlayer from {0}");

		// we're in a room. spawn a character for the local player. it gets synced by using Photon2DRealTime.Instantiate
		//photon2DRealTimeInstance.Instantiate();

	}

	/// <summary>
	/// Called when a Photon Player got connected. We need to then load a bigger scene.
	/// </summary>
	/// <param name="other">Other.</param>
	public void OnPlayerEnteredRoom( Player other  )
	{
		Debug.Log( "OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

		if ( IsMasterClient )
		{
			Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}"); // called before OnPlayerLeftRoom

			LoadArena();
		}
	}

	/// <summary>
	/// Called when a Photon Player got disconnected. We need to load a smaller scene.
	/// </summary>
	/// <param name="other">Other.</param>
	public void OnPlayerLeftRoom( Player other  )
	{
		Debug.Log( "OnPlayerLeftRoom() " + other.NickName ); // seen when other disconnects

		// if ( photon2DRealTimeInstance.IsMasterClient )
		// {
		// 	Debug.LogFormat( "OnPlayerEnteredRoom IsMasterClient {0}", photon2DRealTimeInstance.IsMasterClient ); // called before OnPlayerLeftRoom

		// 	LoadArena(); 
		// }
	}

	public void OnLeftRoom()
	{
		SceneManager.LoadScene("PunBasics-Launcher");
	}


	public void LeaveRoom() 
	{
	}

	public void QuitApplication()
	{
		Application.Quit();
	}

	void LoadArena()
	{
		// if ( ! photon2DRealTimeInstance.IsMasterClient )
		// {
		// 	Debug.LogError( "Photon2DRealTime : Trying to Load a level but we are not the master Client" );
		// 	return;
		// }

		// Debug.LogFormat( "Photon2DRealTime : Loading Level : {0}");

	}


}

public class Player
{
    public Player(string nickName, int actorNumber, bool isLocal) 
    {
    }

    // You can add custom properties here later if needed
    public string NickName = "";
    public int ActorNumber = 1;
    public bool IsLocal = false;
    public bool IsMasterClient = false;
}