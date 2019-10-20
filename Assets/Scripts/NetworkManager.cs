using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace AllForOne
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private bool _hasConnected = false;

        public string ServerURL = "ws://localhost:25565/";

        private WebSocket _webSocket;

        private Coroutine _connectionCoroutine;

        private Queue<string> _messages = new Queue<string>();

        public Queue<string> Messages => _messages;

        public delegate void OnConnectionSuccessful();
        public static OnConnectionSuccessful ConnectionSuccessful;

        public delegate void OnConnectionFailed(string reason);
        public static OnConnectionFailed ConnectionFailed;

        public delegate void OnDisconnect(string reason);
        public static OnDisconnect Disconnect;

        public void Connect()
        {
            _connectionCoroutine = StartCoroutine(HandleNetwork(ServerURL));
        }

        public void Close(string reason) => _webSocket.Close(CloseStatusCode.Normal, reason);

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

        private void OnError(string reason)
        {
            ConnectionFailed(reason);
            StopCoroutine(_connectionCoroutine);
            _connectionCoroutine = null;
        }

        private void OnClose(string reason)
        {
            Disconnect(reason);
            StopCoroutine(_connectionCoroutine);
            _connectionCoroutine = null;
        }

        private void OnConnectionSuccess()
        {
            ConnectionSuccessful();
            _hasConnected = true;
        }

        private IEnumerator HandleNetwork(string url)
        {
            using (_webSocket = new WebSocket(url))
            {
                _webSocket.OnOpen += (sender, e) => OnConnectionSuccess();
                _webSocket.OnOpen += (sender, e) => Debug.Log("Connection successful.");

                _webSocket.OnError += (sender, e) => OnError(e.Message);

                _webSocket.OnMessage += (sender, e) => OnReceive(e);
                _webSocket.OnClose += (sender, e) => OnClose(e.Reason);

                Debug.Log("Connecting to + " + url + ".");
                _webSocket.Connect();

                while (_connectionCoroutine != null)
                {
                    Debug.Log("Still running");
                    yield return null;
                }
            }
        }

        private void OnApplicationQuit()
        {
            Close("Player closed");
        }
    }
}