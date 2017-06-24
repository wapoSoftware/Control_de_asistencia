using AssistanceControl_BLL.AssistanceService;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcGenerico<T> where T : class
    {
        AssistanceControlEntities service;
        public async Task<T> getData(String url)
        {
            T respuesta;
            Encoding encoding;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponse = null;
            string contentType = "application/json";
            try
            {
                httpClient = new HttpClient();
                encoding = Encoding.GetEncoding("utf-8");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                httpResponse = await httpClient.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);
                    try
                    {
                        respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsonObject["value"].ToString()).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(jsonObject.ToString());
                    }
                    jsonObject = null;
                }
                else
                {
                    throw new Exception(string.Format("Server error (HTTP {0} URL {1}).", httpResponse.StatusCode, url));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                encoding = null;
                httpClient = null;
                httpResponse = null;
                contentType = string.Empty;
            }
            return respuesta;
        }
        public async Task<List<T>> getDataList(String url)
        {
            List<T> respuesta;
            Encoding encoding;
            HttpClient httpClient = null;
            HttpResponseMessage httpResponse = null;
            string contentType = "application/json";
            try
            {
                httpClient = new HttpClient();
                encoding = Encoding.GetEncoding("utf-8");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                httpResponse = await httpClient.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(httpResponse.Content.ReadAsStringAsync().Result);
                    try
                    {
                        respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsonObject["value"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(jsonObject.ToString());
                    }
                    jsonObject = null;
                }
                else
                {
                    throw new Exception(string.Format("Server error (HTTP {0} URL {1}).", httpResponse.StatusCode, url));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                encoding = null;
                httpClient = null;
                httpResponse = null;
                contentType = string.Empty;
            }
            return respuesta;
        }


        public async Task<DataServiceResponse> insert(Uri url, T entidad)
        {
            try
            {
                service = new AssistanceControlEntities(url);
                service.AddObject(entidad.GetType().Name, entidad);
                var queryTask = Task.Factory.FromAsync(service.BeginSaveChanges(null, null),
                  (queryAsyncResult) =>
                  {
                      var results = service.EndSaveChanges(queryAsyncResult);
                      return results;
                  });
                return await queryTask;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} Server error ( HTTP {1} )", ex.Message, ((DataServiceClientException)ex.InnerException).StatusCode.ToString()));
            }
        }
        public async Task<DataServiceResponse> insert(Uri url, List<T> entidades)
        {
            try
            {
                service = new AssistanceControlEntities(url);
                foreach (T item in entidades)
                {
                    service.AddObject(item.GetType().Name, item);
                }
                var queryTask = Task.Factory.FromAsync(service.BeginSaveChanges(null, null),
                  (queryAsyncResult) =>
                  {
                      var results = service.EndSaveChanges(queryAsyncResult);
                      return results;
                  });
                return await queryTask;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} Server error ( HTTP {1} )", ex.Message, ((DataServiceClientException)ex.InnerException).StatusCode.ToString()));
            }
        }
        public async Task<DataServiceResponse> delete(Uri url, T entidad)
        {
            try
            {
                service = new AssistanceControlEntities(url);
                service.AttachTo(entidad.GetType().Name, entidad);
                service.DeleteObject(entidad);
                var queryTask = Task.Factory.FromAsync(service.BeginSaveChanges(null, null),
                  (queryAsyncResult) =>
                  {
                      var results = service.EndSaveChanges(queryAsyncResult);
                      return results;
                  });
                return await queryTask;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} Server error ( HTTP {1} )", ex.Message, ((DataServiceClientException)ex.InnerException).StatusCode.ToString()));
            }
        }
        public async Task<DataServiceResponse> update(Uri url, T entidad)
        {
            try
            {
                service = new AssistanceControlEntities(url);
                service.AttachTo(entidad.GetType().Name, entidad);
                service.UpdateObject(entidad);
                var queryTask = Task.Factory.FromAsync(service.BeginSaveChanges(null, null),
                  (queryAsyncResult) =>
                  {
                      var results = service.EndSaveChanges(queryAsyncResult);
                      return results;

                  });
                return await queryTask;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0} Server error ( HTTP {1} )", ex.Message, ((DataServiceClientException)ex.InnerException).StatusCode.ToString()));
            }
        }
    }
}
