using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.InputSystem;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class MainPlayerScript : NetworkBehaviour
{
    [Header("NameTag")]
    public TextMeshPro nameTagObject;
    [SyncVar(hook = nameof(NameChanged))]
    private string username;

    [Header("NameTag")]
    private RoundUIManager roundUIManager;


    private MainMenuScript mainMenuPanel;

    public float cursorSpeed;

    #region Unity Callbacks

    /// <summary>
    /// Add your validation code here after the base.OnValidate(); call.
    /// </summary>
    protected override void OnValidate()
    {
        base.OnValidate();
    }

    // NOTE: Do not put objects in DontDestroyOnLoad (DDOL) in Awake.  You can do that in Start instead.
    void Awake()
    {
    }

    void Start()
    {

    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        MoveMouse();
        if (Input.GetKeyDown(KeyCode.Mouse0))
            //KnowPlayers();
            CommandTemporalCreateCard();
    }

    #endregion

    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer() 
    {
        //CommandRegisterPlayerMoney();
    }

    /// <summary>
    /// Invoked on the server when the object is unspawned
    /// <para>Useful for saving object data in persistent storage</para>
    /// </summary>
    public override void OnStopServer() { }

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient() { }

    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient() { }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer() 
    {
        base.OnStartLocalPlayer();
        Cursor.visible = false;
        mainMenuPanel = FindAnyObjectByType<MainMenuScript>();
        CommandChangeName(mainMenuPanel.GetPlayerName());
        mainMenuPanel.gameObject.SetActive(false);
        roundUIManager = FindAnyObjectByType<RoundUIManager>();
        CommandRegisterPlayerMoney();
        //CommandRegisterPlayer();
        //playerHUD = FindFirstObjectByType<PlayerHUD>(FindObjectsInactive.Include);
        //playerHUD.gameObject.SetActive(true);
    }

    /// <summary>
    /// Called when the local player object is being stopped.
    /// <para>This happens before OnStopClient(), as it may be triggered by an ownership message from the server, or because the player object is being destroyed. This is an appropriate place to deactivate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStopLocalPlayer()
    {
        Cursor.visible = true;
        mainMenuPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
    /// <para>This is called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
    /// <para>When <see cref="NetworkIdentity.AssignClientAuthority">AssignClientAuthority</see> is called on the server, this will be called on the client that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnectionToClient parameter included, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStartAuthority() { }

    /// <summary>
    /// This is invoked on behaviours when authority is removed.
    /// <para>When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStopAuthority() { }

    #endregion

    private void CreateCard()
    {
        CommandTemporalCreateCard();
        //ClientTemporalCreateCard(myCard);
    }

    [Command]
    public void CommandUpdateMoneyUI()
    {
        //PlayerEconomy.singleton.UpdateMoneyUI();
        ClientUpdateMoneyUI();
    }

    [ClientRpc]
    public void ClientUpdateMoneyUI()
    {
        //Debug.Log("Esto lo deberian decir ambos y actualizarse");
        //PlayerEconomy.singleton.UpdateMoneyUI();
    }

    [Command]
    private void CommandTemporalCreateCard()
    {
        CardData newCardData = CardCreator.singleton.GetRandomCardData();
        //GameObject newCard = Instantiate(CardCreator.singleton.cardPrefab, cursorObject.transform.position, cursorObject.transform.rotation);
        //GameObject myCard = CardCreator.singleton.CreateRandomCard(true, transform);
        ClientTemporalCreateCard(newCardData.rank, newCardData.suit, newCardData.enhancement, newCardData.edition, newCardData.seal);
    }

    [ClientRpc]
    private void ClientTemporalCreateCard(int rank, CardSuits suit, CardEnhancements enhancement, CardEditions edition, CardSeals seal)
    {
        CardCreator.singleton.CreateCardWithData(rank, suit, enhancement, edition, seal, transform);
    }

    private void MoveMouse()
    {
        Vector3 newCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newCursorPos.z = 0;
        transform.position = Vector3.MoveTowards(transform.position, newCursorPos, cursorSpeed * Time.deltaTime);
    }

    [Command]
    private void CommandChangeName(string myName)
    {
        username = myName;
    }

    private void NameChanged(string oldName, string newName)
    {
        nameTagObject.text = newName;
        name = newName;
    }

    [Command]
    private void CommandRegisterPlayerMoney()
    {
        PlayerEconomy.singleton.RegisterPlayer(this);
        //ClientUpdateMoneyUI(this);
    }

    [Command]
    private void KnowPlayers()
    {
        if(PlayerEconomy.singleton.GetPlayers() == 2)
        {
            Debug.Log("El jugador 1 tiene : $" + PlayerEconomy.singleton.GetPlayerMoney(0) + " y el 2 tiene: $" + PlayerEconomy.singleton.GetPlayerMoney(1));
            //roundUIManager.UpdatePlayersMoneyUI(PlayerEconomy.singleton.GetPlayerMoney(0), PlayerEconomy.singleton.GetPlayerMoney(1));
            //PlayerEconomy.singleton.UpdateMoneyUI(PlayerEconomy.singleton.GetPlayerMoney(0), PlayerEconomy.singleton.GetPlayerMoney(1));
            ClientSetPlayersMoneyUI(PlayerEconomy.singleton.GetPlayerMoney(0), PlayerEconomy.singleton.GetPlayerMoney(1));
        }
    }

    [ClientRpc]
    private void ClientSetPlayersMoneyUI(int player1Money, int player2Money)
    {
        PlayerEconomy.singleton.UpdateMoneyUI(player1Money, player2Money);
    }
}
