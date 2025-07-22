using System.Collections.Generic;
using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class PlayerDeckManager : NetworkBehaviour
{
    public static PlayerDeckManager singleton;

    public Transform player1DeckTransform, player2DeckTransform;
    public List<Transform> player1HandsTransforms = new List<Transform>();
    public List<Transform> player2HandsTransforms = new List<Transform>();
    private List<MainPlayerScript> players = new List<MainPlayerScript>();
    private List<GameObject> player1Cards = new List<GameObject>();
    private List<GameObject> player2Cards = new List<GameObject>();

    private void Awake()
    {
        if (singleton != null && singleton != this) { Destroy(this); } else { singleton = this; }
    }

    public void RegisterPlayer(MainPlayerScript player)
    {
        players.Add(player);
    }

    [ClientRpc]
    public void CreatePlayerDecks()
    {
        for (int i = 1; i < 14; i++)
        {
            player1Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Clubs, CardEnhancements.None, CardEditions.Base, CardSeals.None, player1DeckTransform));
            player1Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Diamonds, CardEnhancements.None, CardEditions.Base, CardSeals.None, player1DeckTransform));
            player1Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Hearts, CardEnhancements.None, CardEditions.Base, CardSeals.None, player1DeckTransform));
            player1Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Spades, CardEnhancements.None, CardEditions.Base, CardSeals.None, player1DeckTransform));
        }

        for (int i = 1; i < 14; i++)
        {
            player2Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Clubs, CardEnhancements.None, CardEditions.Base, CardSeals.None, player2DeckTransform));
            player2Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Diamonds, CardEnhancements.None, CardEditions.Base, CardSeals.None, player2DeckTransform));
            player2Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Hearts, CardEnhancements.None, CardEditions.Base, CardSeals.None, player2DeckTransform));
            player2Cards.Add(CardCreator.singleton.CreateCardWithData(i, CardSuits.Spades, CardEnhancements.None, CardEditions.Base, CardSeals.None, player2DeckTransform));
        }
        foreach (GameObject p1Card in player1Cards)
        {
            p1Card.SetActive(false);
        }
        foreach (GameObject p2Card in player2Cards)
        {
            p2Card.SetActive(false);
        }
    }

    public void SetPlayerHands()
    {
        List<int> randomCardIndexes = new List<int>();
        foreach (Transform player1Hand in player1HandsTransforms)
        {
            int randomCardIndex = Random.Range(0, 13);
            if (randomCardIndexes.Contains(randomCardIndex))
            {
                for (int i = 0; i < 13; i++)
                {
                    if (!randomCardIndexes.Contains(i))
                    {
                        randomCardIndex = i;
                        break;
                    }
                }
            }
            randomCardIndexes.Add(randomCardIndex);
            ClientMoveCardToHand(randomCardIndex, player1Hand.position, 1);
        }
        randomCardIndexes.Clear();
        foreach (Transform player2Hand in player2HandsTransforms)
        {
            int randomCardIndex = Random.Range(0, 13);
            if (randomCardIndexes.Contains(randomCardIndex))
            {
                for (int i = 0; i < 13; i++)
                {
                    if (!randomCardIndexes.Contains(i))
                    {
                        randomCardIndex = i;
                        break;
                    }
                }
            }
            randomCardIndexes.Add(randomCardIndex);
            ClientMoveCardToHand(randomCardIndex, player2Hand.position, 2);
        }
    }

    [ClientRpc]
    private void ClientMoveCardToHand(int cardIndex, Vector3 handPos, int player)
    {
        if (player == 1)
        {
            player1Cards[cardIndex].SetActive(true);
            player1Cards[cardIndex].transform.position = handPos;
        }
        else if(player == 2)
        {
            player2Cards[cardIndex].SetActive(true);
            player2Cards[cardIndex].transform.position = handPos;
        }
    }

    public int GetCardIndex(GameObject card)
    {
        for (int i = 0; i < player1Cards.Count; i++)
        {
            if (player1Cards[i] == card || player2Cards[i] == card)
                return i;
        }
        return 0;
    }

    public int GetCardPlayer(GameObject card)
    {
        for (int i = 0; i < player1Cards.Count; i++)
        {
            if (player1Cards[i] == card)
                return 1;
            else if (player2Cards[i] == card)
                return 2;
        }
        return 1;
    }

    public void ClientMoveCard(int cardIndex, int player)
    {
        if(player == 1)
            player1Cards[cardIndex].transform.position = new Vector2(player1Cards[cardIndex].transform.position.x, player1Cards[cardIndex].transform.position.y + 0.5f);
        else if (player == 2)
            player2Cards[cardIndex].transform.position = new Vector2(player2Cards[cardIndex].transform.position.x, player2Cards[cardIndex].transform.position.y + 0.5f);

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
}
