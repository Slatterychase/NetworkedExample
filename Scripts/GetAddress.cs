using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Networking;

public class GetAddress : NetworkManager {

    // Use this for initialization
    public GameObject serverManager;
	void Start () {
        
        string address = "";
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        using (WebResponse response = request.GetResponse())
        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        {
            address = stream.ReadToEnd();
        }

        int first = address.IndexOf("Address: ") + 9;
        int last = address.LastIndexOf("</body>");
        address = address.Substring(first, last - first);
        Debug.Log(address);
        // serverManager.GetComponent<NetworkManager>().networkAddress = address;
        

    }

    public override void OnStartHost()
    {
        //base.OnStartHost();
        string address = "";
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        using (WebResponse response = request.GetResponse())
        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        {
            address = stream.ReadToEnd();
        }

        int first = address.IndexOf("Address: ") + 9;
        int last = address.LastIndexOf("</body>");
        address = address.Substring(first, last - first);
        Debug.Log(address);

        networkAddress = address;

    }
    public override void OnStartClient(NetworkClient client)
    {
        
        string address = "";
        WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
        using (WebResponse response = request.GetResponse())
        using (StreamReader stream = new StreamReader(response.GetResponseStream()))
        {
            address = stream.ReadToEnd();
        }

        int first = address.IndexOf("Address: ") + 9;
        int last = address.LastIndexOf("</body>");
        address = address.Substring(first, last - first);
        Debug.Log(address);

        networkAddress = address;
        
    }
    
}
