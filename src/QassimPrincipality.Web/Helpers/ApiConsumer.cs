using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace QassimPrincipality.Web.Helpers
{
    public static class ApiConsumer
    {
        public static string ServiceConsumer(string Url, params ApiHeaders[] headers)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (headers != null && headers.Length > 0)
                    {
                        headers
                            .ToList()
                            .ForEach(c =>
                            {
                                httpClient.DefaultRequestHeaders.Add(c.Name, c.Value);
                            });
                    }
                    using (var response = httpClient.GetAsync(Url).Result)
                    {
                        return response.StatusCode.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                return "false:" + ex.Message;
            }
        }

        public static async Task<string> ServiceConsumerAsync(string Url, params ApiHeaders[] headers)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (headers != null && headers.Length > 0)
                    {
                        headers
                            .ToList()
                            .ForEach(c =>
                            {
                                httpClient.DefaultRequestHeaders.Add(c.Name, c.Value);
                            });
                    }
                    using (var response = await httpClient.GetAsync(Url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<string>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return "false:" + ex.Message;
            }
        }

        public static async Task<T> ServiceConsumerAsync<T>(string Url, params ApiHeaders[] headers)
        {
            var dataToSee = "";
            try
            {

                using (var httpClient = new HttpClient())
                {
                    if (headers != null && headers.Length > 0)
                    {
                        headers
                            .ToList()
                            .ForEach(c =>
                            {
                                httpClient.DefaultRequestHeaders.Add(c.Name, c.Value);
                            });
                    }
                    using (var response = await httpClient.GetAsync(Url))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        dataToSee = apiResponse;
                        var code = response.StatusCode;

                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(dataToSee);
            }

        }

        public static async Task<T> ServicePostConsumerAsync<T>(
            string Url,
            T postObject,
            params ApiHeaders[] headers
        )
        {
            HttpClient httpClient = new HttpClient();

            using (httpClient)
            {
                if (headers != null && headers.Length > 0)
                {
                    headers
                        .ToList()
                        .ForEach(c =>
                        {
                            httpClient.DefaultRequestHeaders.Add(c.Name, c.Value);
                        });
                }
                var serilizedNafathBody = JsonConvert.SerializeObject(postObject);

                var requestContent = new StringContent(
                    serilizedNafathBody,
                    Encoding.UTF8,
                    "application/json"
                );
                try
                {
                    using (var response = await httpClient.PostAsync(Url, requestContent))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(apiResponse);
                    }
                }
                catch (JsonReaderException e)
                {
                    return default(T);
                }
            }
        }

        public static async Task<HttpStatusCode> GetStatusCode(
            string Url,
            object postObject = null,
            params ApiHeaders[] headers
        )
        {
            using (var httpClient = new HttpClient())
            {
                if (headers != null && headers.Length > 0)
                {
                    headers
                        .ToList()
                        .ForEach(c =>
                        {
                            httpClient.DefaultRequestHeaders.Add(c.Name, c.Value);
                        });
                }
                var serilizedNafathBody = JsonConvert.SerializeObject(postObject);

                var requestContent = new StringContent(
                    serilizedNafathBody,
                    Encoding.UTF8,
                    "application/json"
                );
                try
                {
                    if (postObject is null)
                    {
                        using (var response = await httpClient.GetAsync(Url))
                        {
                            return response.StatusCode;
                        }
                    }
                    else
                    {
                        using (var response = await httpClient.PostAsync(Url, requestContent))
                        {
                            return response.StatusCode;
                        }
                    }
                }
                catch (JsonReaderException e)
                {
                    throw;
                }
            }
        }
    }
}
