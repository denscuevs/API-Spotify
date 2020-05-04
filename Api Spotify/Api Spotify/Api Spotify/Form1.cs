using System;
using System.Collections.Specialized;
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
        
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string playlist_url;
            string token;
            playlist_url = "https://api.spotify.com/v1/tracks/7f0vVL3xi4i78Rv5Ptn2s1";
            token = "Bearer BQBRQcZYLDlcJRWslqb6Iiny4_2cnVKYAiNEzfzJ3tynbUnbxeeK4h9Gbm7BwcR" +
                "4ApT80qLWl4O9Ighz1TMUOnrJ7dCS8TNS3No-7YFQjXaQV2AEdFLfvc0iRkm_9U-vqc9GYr8Nh1Vhl" +
                "IhcEQWHkZVVIGeC---ye_GykkdnSxaupj-6KfmrA8e67NoMQIW" +
                "xrs_QtkEWkRtl1ZTjtqTSCw67Yc8VlZOT3UT7K4mGPCMQ0hA6ddHQ5FnerMnfjMGgOEj3";
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            var tokenPost = "Bearer BQCKgKkQR5jyNcS_78T9aM_kxEuuf47JGQKrb6kKQQEjUU_RjxbTqTaNU0Steko8mD_Gq7zTUClJR1eAWJjeWHmF70jjbYFeoR-mu-sYYJJJBIXDEhS8HSXp2x5lOMpDGDAqyaZW36SGW582A_mdjuxs2WXDf104WUQQN3ASfvnVPVO8QYv2tJlyfyEkJOkVlUhUeE_9D99jIQyA0yTYjtoFyjMXhaJ2gmpkfXcOummtFpOPH0huFDGVQDGkZZ7nfDMfrjNDZW_Jal8keLNHL0L-CO3QpDRN";
            var url = "https://api.spotify.com/v1/playlists/1KRYEj2Lw5Mug4A0n6sRkV/tracks?position=1&uris=spotify:track:3NakEPSaVKsqqimJW66xCc";

            string parameters = "position=1&uris=spotify:track:3NakEPSaVKsqqimJW66xCc";
            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(parameters);
            
            // Create a request using a URL that can receive a post.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", tokenPost);
            request.Accept ="application/json";
            
            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();

            // Get the response.
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // Display the status.
                textBox1.Text = response.StatusDescription;

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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
                
            }

        }

        public void GetToken()
        {
            string clientId = "bbc8cf81d7ba4eb6b4ec1a91dd1f9aaa";
            string clientSecret = "a42eb8efe03543d385a1e1aafb5932bb";

            var cliente = new WebClient();

            var parametros = new NameValueCollection();
            parametros.Add("grant_type", "client_credentials");
            //var param = Convert.ChangeType(parametros, "application/x-www-form-urlencoded")

            var permiso = Convert.ToBase64String(Encoding.Default.GetBytes($"{clientId}:{clientSecret}"));
            Console.WriteLine(permiso);
            cliente.Headers.Add(HttpRequestHeader.Authorization, "Beared" + permiso);

            var token = cliente.UploadValues("https://accounts.spotify.com/api/token", parametros);
            var textResponse = Encoding.UTF8.GetString(token);

            Console.WriteLine(textResponse);
        }

    }
}
