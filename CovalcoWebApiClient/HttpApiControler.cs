using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CovalcoWebApiClient
{
    class HttpApiControler
    {
        static HttpClient client;
        public HttpApiControler() { }
        static HttpApiControler()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:30085/");
        }

        public async Task<List<AlumnoViewModel>> GetCall()
        {
            IEnumerable<AlumnoViewModel> listaAlumnos = new List<AlumnoViewModel>();
            try
            {

                HttpResponseMessage response = client.GetAsync(Resource1.GetApi).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(Resource1.ReqMsgInfo + response.RequestMessage + Resource1.n);
                    Console.WriteLine(Resource1.ReqMsgHeader + response.Content.Headers + Resource1.n);
                    // Get the response
                    var alumnoJsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(Resource1.RespData + alumnoJsonString);

                    // Deserialise the data (include the Newtonsoft JSON Nuget package if you don't already have it)
                    var deserialized = JsonConvert.DeserializeObject<IEnumerable<AlumnoViewModel>>(alumnoJsonString);
                    listaAlumnos = deserialized;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return listaAlumnos.ToList();

        }


        // create method POST

        public async void AñadirAlumnos(AlumnoViewModel alumno)
        {
            // Serializacion del objeto alumno
            var alumnoJSON = JsonConvert.SerializeObject(alumno);

            try
            {
                // Creacion de objeto de contenido para enviar la informacion
                var encodingToBytes = System.Text.Encoding.UTF8.GetBytes(alumnoJSON);
                var byteContent = new ByteArrayContent(encodingToBytes);

                // Especificamos en el header que se trata de un tipo JSON
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await client.PostAsync("api/Alumnoes", byteContent);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Put method

        public async void EditarAlumnos(int id, AlumnoViewModel alumno)
        {
            // Serializacion del objeto alumno
            var alumnoJSON = JsonConvert.SerializeObject(alumno);

            try
            {
                // Creacion de objeto de contenido para enviar la informacion
                var encodingToBytes = System.Text.Encoding.UTF8.GetBytes(alumnoJSON);
                var byteContent = new ByteArrayContent(encodingToBytes);

                // Especificamos en el header que se trata de un tipo JSON
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await client.PutAsync(String.Concat("api/Alumnoes/", id), byteContent);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // Delete Method

        public async void EliminarAlumnos(int id)
        {
            try
            {
                var result = await client.DeleteAsync(String.Concat("api/Alumnoes/", id));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

}
