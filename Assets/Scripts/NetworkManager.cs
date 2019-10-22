using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace MechanicFever
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        [SerializeField]
        private bool _connectOnAwake = false;

        private void Start()
        {
            if (_connectOnAwake)
                Connect();
        }

        public string ServerURL = "ws://localhost:25565/";

        private WebSocket _webSocket;

        private bool _connectionCoroutine;

        private Queue<string> _messages = new Queue<string>();

        public Queue<string> Messages => _messages;

        public delegate void OnConnectionSuccessful();
        public static OnConnectionSuccessful ConnectionSuccessful;

        public delegate void OnError(string reason, string title);
        public static OnError Error;

        public void Connect()
        {
            _connectionCoroutine = true;
            StartCoroutine(HandleNetwork(ServerURL));
        }

        public void Close(string reason) => _webSocket.Close(CloseStatusCode.Normal, reason);

        private void OnReceive(MessageEventArgs message)
        {
            if (message.IsText)
                Messages.Enqueue(message.Data);
        }

        public void SendMessage(Message message)
        {
            if (_webSocket == null)
                return;

            if (_webSocket.IsConnected == false)
                return;

            _webSocket.Send(message.GameData);
        }

        new public void SendMessage(string message)
        {
            _webSocket.Send(message);
        }

        private void OnClose(string e)
        {
            Debug.Log("Closed Connection: " + e);
            Error(e, "Understood");
            _connectionCoroutine = false;
        }

        private void OnConnectionSuccess()
        {
            Debug.Log("Connection Successful.");
            ConnectionSuccessful();
        }

        public void SendMessage(GameData gameData) => SendMessage(new Message(gameData));

        private IEnumerator HandleNetwork(string url)
        {
            using (_webSocket = new WebSocket(url))
            {
                _webSocket.OnOpen += (sender, e) => OnConnectionSuccess();
                _webSocket.OnError += (sender, e) => Debug.Log("Error: " + e.Message + ".");
                _webSocket.OnMessage += (sender, e) => OnReceive(e);
                _webSocket.OnError += (sender, e) => OnClose(e.Message);

                Debug.Log("Connecting to + " + url + ".");
                _webSocket.Connect();

                while (_connectionCoroutine)
                    yield return null;
            }
        }

        private void OnApplicationQuit()
        {
            Close("Player closed");
        }
    }
}