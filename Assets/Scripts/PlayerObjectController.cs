using Mirror;
using Steamworks;

public class PlayerObjectController : NetworkBehaviour
{
    // Player Data
    [SyncVar] public int ConnectionID;
    [SyncVar] public int PlayerIdNumber;
    [SyncVar] public ulong PlayerSteamID;
    [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;
    [SyncVar(hook = nameof(PlayerReadyUpdate))] public bool Ready;

    // Cosmetics
    [SyncVar(hook = nameof(SendPlayerColor))] public int PlayerColor;

    private CustomNetworkManager manager;

    private CustomNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void PlayerReadyUpdate(bool oldValue, bool newValue)
    {
        if (isServer)
        {
            this.Ready = newValue;
        }
        if (isClient)
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    [Command]
    private void CmdSetPlayerReady()
    {
        this.PlayerReadyUpdate(this.Ready, !this.Ready);
    }

    public void ChangeReady()
    {
        if (hasAuthority)
        {
            CmdSetPlayerReady();
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        LobbyController.Instance.FindLocalPlayer();
        LobbyController.Instance.UpdateLobbyName();
    }

    public override void OnStartClient()
    {
        Manager.GamePlayers.Add(this);
        LobbyController.Instance.UpdateLobbyName();
        LobbyController.Instance.UpdatePlayerList();
    }

    public override void OnStopClient()
    {
        Manager.GamePlayers.Remove(this);
        LobbyController.Instance.UpdatePlayerList();
    }

    [Command]
    private void CmdSetPlayerName(string PlayerName)
    {
        this.PlayerNameUpdate(this.PlayerName, PlayerName);
    }

    public void PlayerNameUpdate(string OldValue, string NewValue)
    {
        if (isServer) // Host
        {
            this.PlayerName = NewValue;
        }
        if (isClient) // Client
        {
            LobbyController.Instance.UpdatePlayerList();
        }
    }

    // Start game
    public void CanStartGame(string SceneName)
    {
        if (hasAuthority)
        {
            CmdCanStartGame(SceneName);
        }
    }

    [Command]
    public void CmdCanStartGame(string SceneName)
    {
        manager.StartGame(SceneName);
    }

    // Cosmetics
    [Command]
    public void CmdUpdatePlayerColor(int newValue)
    {
        SendPlayerColor(PlayerColor, newValue);
    }

    public void SendPlayerColor(int oldValue, int newValue)
    {
        if (isServer) // Host
        {
            PlayerColor = newValue;
        }
        if (isClient && (oldValue != newValue)) // Client
        {
            UpdateColor(newValue);
        }
    }

    void UpdateColor(int message)
    {
        PlayerColor = message;
    }
}
