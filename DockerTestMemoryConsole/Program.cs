using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DockerTestMemoryConsole
{
    class Program
    {
        const string HTTP_22 = "http://localhost:5000/api/values/";
        const string HTTP_30 = "http://localhost:5003/api/values/";
        private static long _erros30 = 0;
        private static long _erros22 = 0;
        private static bool _continue = true;

        static void Main(string[] args)
        {
            IniciarProcessamento();
        }

        private static void IniciarProcessamento()
        {

            while(_continue)
            {
                Console.Write("Informe o a quantidade de repeticoes ==> ");
                long a = long.Parse(Console.ReadLine());

                GetRequestFramework22(a);
                GetRequestFramework30(a);

                Console.Write("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void GetRequestFramework22(long quantidade)
        {
            try
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Iniciando teste na API 2.2");

                for (long i = 0; i < quantidade; i++)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var result = Task.Run(() => client.GetAsync(HTTP_22)).Result;

                        if (!result.IsSuccessStatusCode)
                        {
                            _erros22++;
                        }
                    }
                }

                using(HttpClient client = new HttpClient())
                {
                    var resultMemory = Task.Run(() => client.GetAsync(HTTP_22 + "mega")).Result;

                    if (resultMemory.IsSuccessStatusCode)
                    {
                        var modelJson = Task.Run(() => resultMemory.Content.ReadAsStringAsync()).Result;
                        var model = JsonConvert.DeserializeObject<dynamic>(modelJson);

                        Console.WriteLine($"Total de memoria usada no API CORE 2.2 ==> { Convert.ToDecimal(model.workSetting64)} megas");
                        Console.WriteLine($"Total de memoria usada no GC no API CORE 2.2 ==> {Convert.ToDecimal(model.memoryGC)} megas");
                    }
                }
                
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Total de erros na API CORE 2.2 ==> {_erros22}");
                Console.WriteLine("Fim do Processamento CORE 2.2");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error 22 ==> " + ex.Message);
            }
        }

        private static void GetRequestFramework30(long quantidade)
        {
            try
            {
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Iniciando teste na API 3.0");

                for (long i = 0; i < quantidade; i++)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var result = Task.Run(() => client.GetAsync(HTTP_30)).Result;

                        if (!result.IsSuccessStatusCode)
                        {
                            _erros30++;
                        }
                    }
                }

                using (HttpClient client = new HttpClient())
                {
                    var resultMemory = Task.Run(() => client.GetAsync(HTTP_30 + "mega")).Result;

                    if (resultMemory.IsSuccessStatusCode)
                    {
                        var modelJson = Task.Run(() => resultMemory.Content.ReadAsStringAsync()).Result;
                        var model = JsonConvert.DeserializeObject<dynamic>(modelJson);

                        Console.WriteLine($"Total de memoria usada no API CORE 3.0 ==> { Convert.ToDecimal(model.workSetting64)} megas");
                        Console.WriteLine($"Total de memoria usada no GC no API CORE 3.0 ==> {Convert.ToDecimal(model.memoryGC)} megas");
                    }
                }

                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Total de erros na API CORE 3.0 ==> {_erros30}");
                Console.WriteLine("Fim do Processamento CORE 3.0");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error 3.0 ==> " + ex.Message);
            }
        }

    }
}
