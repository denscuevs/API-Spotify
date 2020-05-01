using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;

namespace Api_Spotify
{
    public partial class Form1 : Form
    {
        string playlist_url;
        string token;
        public Form1()
        {
            InitializeComponent();
            playlist_url = "https://api.spotify.com/v1/tracks/7f0vVL3xi4i78Rv5Ptn2s1";
            token = "Bearer BQBRQcZYLDlcJRWslqb6Iiny4_2cnVKYAiNEzfzJ3tynbUnbxeeK4h9Gbm7BwcR" +
                "4ApT80qLWl4O9Ighz1TMUOnrJ7dCS8TNS3No-7YFQjXaQV2AEdFLfvc0iRkm_9U-vqc9GYr8Nh1Vhl" +
                "IhcEQWHkZVVIGeC-UQXTDnr7df6HPY6sbL0QOCRvI0JiU-ye_GykkdnSxaupj-6KfmrA8e67NoMQIW" +
                "xrs_QtkEWkRtl1ZTjtqTSCw67Yc8VlZOT3UT7K4mGPCMQ0hA6ddHQ5FnerMnfjMGgOEj3";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crear la peticion a la URL. 

            WebRequest request = WebRequest.Create(playlist_url);

            request.Headers.Add("Authorization", token);

            //credenciales en caso de ser necesarias (se colocan default en caso de que no)
            //request.Credentials = CredentialCache.DefaultCredentials;

            // obtener la respuesta
            WebResponse response = request.GetResponse();

            // Mostrar el status
            textBox1.Text = ((HttpWebResponse)response).StatusDescription;

            // obtener la informacion que regreso la peticion 
            using (Stream dataStream = response.GetResponseStream())
            {
                // crear un lector
                StreamReader reader = new StreamReader(dataStream);
                // leer el contenido
                string responseFromServer = reader.ReadToEnd();
                // mostrar el contenido
                textBox1.Text += responseFromServer;
            }

            // cerrar respuesta
            response.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a request using a URL that can receive a post.
            WebRequest request = WebRequest.Create("http://www.contoso.com/PostAccepter.aspx ");
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Create POST data and convert it to a byte array.
            string postData = "This is a test that posts this string to a Web server.";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.

            textBox1.Text = ((HttpWebResponse)response).StatusDescription;
            // Get the stream containing content returned by the server.
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                textBox1.Text += responseFromServer;
            }


            // Close the response.
            response.Close();

        }
    }
}
