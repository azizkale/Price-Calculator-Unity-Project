using UnityEngine;
using NativeWebSocket;
public class SocketIO : MonoBehaviour
{

    WebSocket websocket;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket("ws://localhost:5000");


        websocket.OnMessage += Websocket_OnMessage1;


        // waiting for messages
        await websocket.Connect();
    }

    private void Websocket_OnMessage1(byte[] data)
    {
        Debug.Log("coming message: ");
        var message = System.Text.Encoding.UTF8.GetString(data);
        Debug.Log("coming message: " + message);
    }

   

    public async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            //// Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("aziz");
            
        }
    }

    

}
