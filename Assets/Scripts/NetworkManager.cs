using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace AllForOne
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        [SerializeField]
        private bool _connectOnAwake = false;

        private bool _hasConnected = false;

        new private void Awake()
        {
            base.Awake();

            if (_connectOnAwake)
                Connect();
        }

        public string ServerURL = "ws://localhost:25565/";

        private WebSocket _webSocket;

        private bool _connectionCoroutine;

        private Queue<string> _messages = new Queue<string>();

        public Queue<string> Messages => _messages;

        public WebSocket WebSocket => _webSocket;

        public void Connect()
        {
            _connectionCoroutine = true;
            StartCoroutine(HandleNetwork(ServerURL));
        }

        public void Close(string reason) => WebSocket.Close(CloseStatusCode.Normal, reason);

        private void OnReceive(MessageEventArgs message)
        {
            try
            {
                if (message.IsText)
                    Messages.Enqueue(message.Data);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        public void SendMessage(Message message)
        {
            if (WebSocket == null)
                return;

            if (WebSocket.IsConnected == false)
                return;

            WebSocket.Send(message.GameData);
        }

        new public void SendMessage(string message)
        {
            WebSocket.Send(message);
        }

        private void OnClose(CloseEventArgs e)
        {
            Debug.Log("Closed Connection: " + e.Reason);
            _connectionCoroutine = false;
        }

        private void OnConnectionSuccess()
        {
            Debug.Log("Connection Successful.");
            _hasConnected = true;
        }

        private IEnumerator HandleNetwork(string url)
        {
            using (_webSocket = new WebSocket(url))
            {
                WebSocket.OnOpen += (sender, e) => OnConnectionSuccess();
                WebSocket.OnError += (sender, e) => Debug.Log("Error: " + e.Message + ".");
                WebSocket.OnMessage += (sender, e) => OnReceive(e);
                WebSocket.OnClose += (sender, e) => OnClose(e);

                Debug.Log("Connecting to + " + url + ".");
                WebSocket.Connect();

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