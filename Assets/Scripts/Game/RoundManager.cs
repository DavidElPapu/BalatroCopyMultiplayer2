using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class RoundManager : NetworkBehaviour
{
    public static RoundManager singleton;

    [Header("Rounds")]
    public TextMeshPro roundText;
    private int currentRound;
    [Header("Ante")]
    public TextMeshPro anteText;
    public int maxAnte;
    public float[] anteBaseScores = new float[9];
    private int currentAnte, currentAnteRound;
    [Header("Blinds")]
    public List<BlindSO> blindsSO = new List<BlindSO>();
    public SpriteRenderer blindIconRenderer;
    public TextMeshPro blindNameText, blindScoreText;
    private int currentBlindIndex;
    private float currentBlindScore;


    private List<MainPlayerScript> players = new List<MainPlayerScript>();

    private void Awake()
    {
        if (singleton != null && singleton != this) { Destroy(this); } else { singleton = this; }
    }

    public void OnGameStart()
    {
        currentRound = 1;
        currentAnte = 1;
        currentAnteRound = 1;
        currentBlindIndex = GetBlindIndex();
        currentBlindScore = anteBaseScores[currentAnte] * blindsSO[currentBlindIndex].baseScoreMultiplier;
        UpdatePlayersUI(currentRound, currentAnte, currentBlindIndex, currentBlindScore, PlayerEconomy.singleton.GetPlayerMoney(0), PlayerEconomy.singleton.GetPlayerMoney(1));
        PlayerDeckManager.singleton.CreatePlayerDecks();
        PlayerDeckManager.singleton.SetPlayerHands();
    }

    [ClientRpc]
    private void UpdatePlayersUI(int currentRound, int currentAnte, int currentBlindIndex, float currentBlindScore, int player1Money, int player2Money)
    {
        roundText.text = currentRound.ToString();
        anteText.text = currentAnte.ToString() + "/" + maxAnte;
        blindNameText.text = blindsSO[currentBlindIndex].blindName;
        blindIconRenderer.sprite = blindsSO[currentBlindIndex].icon;
        blindScoreText.text = currentBlindScore.ToString();
        PlayerEconomy.singleton.UpdateMoneyUI(player1Money, player2Money);
    }

    public void RegisterPlayer(MainPlayerScript player)
    {
        players.Add(player);
        if (players.Count == NetworkManager.singleton.maxConnections)
            OnGameStart();
    }

    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer() { }

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
    public override void OnStartLocalPlayer() { }

    /// <summary>
    /// Called when the local player object is being stopped.
    /// <para>This happens before OnStopClient(), as it may be triggered by an ownership message from the server, or because the player object is being destroyed. This is an appropriate place to deactivate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStopLocalPlayer() {}

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

    private int GetBlindIndex()
    {
        if (currentAnteRound <= 2)
            return currentAnteRound - 1;
        else
            return Random.Range(2, blindsSO.Count);
    }
}
