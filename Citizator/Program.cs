using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace Citizator
{
    using System.Collections.Generic;
    using Model;
    using System.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {

            var key = ConfigurationManager.AppSettings["googleApiKey"];
            var place = new GooglePlace(
                key: key,
                place: "вулиця Інститутська, 19, Хмельницкий, Хмельницкая область",
                fields: new List<PlaceFields>() { 
                   PlaceFields.formatted_address, 
                   PlaceFields.place_id,              
                });
            var p = place.PlaceId;

            var pd = new PlaceDetails(
                key,
                place.PlaceId,
                new List<PlaceFields> { 
                    PlaceFields.address_component,
                    PlaceFields.adr_address,
                    PlaceFields.formatted_address,
                    PlaceFields.geometry,
                });

            var f = pd.Result;
            // pd.Result["result"]["geometry"]["location"]["geometry"];
            //var place = new PlaceDetails(
            //    key: key,
            //    place: "ChIJ2QgMLZcIMkcRbKyVcawm3lc"
            //).Get();

            //"locationbias" : circle:500@49.3990371,27.0538333
            var placesClient = new NearbyPlacesSearch(
                key: key,
                location: string.Format("{0},{1}", pd.Result["result"]["geometry"]["location"]["lat"], pd.Result["result"]["geometry"]["location"]["lng"]),
                radius: 100
                );
            var pl = placesClient.Result;

        }

        public abstract class GoogleApiRequest
        {
            protected readonly string api;
            protected readonly string key;
            protected readonly string format = "json";
            public GoogleApiRequest(
                string api,
                string key)
            {
                this.api = api;
                this.key = key;
            }
            private JObject result;
            public virtual JObject Result
            {
                get { return result ?? (result = CachedValue) ?? (result = Get()); }
            }
            protected virtual JObject Get()
            {
                DateTime start = DateTime.Now;
                var r = new WebClient();
                var res = r.DownloadString(new Uri(Url));

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["citizator"].ConnectionString))
                {
                    connection.Open();
                    var cmd = string.Format(
                    @"if(exists(select 1 from [Request] where url = '{0}'))
                    begin
                         insert into [dbo].[RequestHistory](requestid, issued)
                        select
                           r.id, '{2}'
                        from
                            [dbo].[Request] r
                        where
                            r.[url] = '{0}'
                    end
                    else
                    begin
                       begin tran
                        insert into [dbo].[Request](url, response, duration)
                            select '{0}', @response, {1}
                        declare @id int
                        set @id = Ident_Current('Request')
                        insert into [dbo].[RequestHistory](requestid, issued)
                        select
                           @id, '{2}'
                        commit tran
                    end", Url.Replace(api,"<GoogleApiKey>"), (int)((DateTime.Now - start).TotalMilliseconds), start.ToString("yyyy-MM-dd hh:mm:ss"));
                    using (var command = new SqlCommand(cmd, connection))
                    {
                        SqlParameter param = command.Parameters.Add("@response", SqlDbType.VarBinary);
                        param.Value = Encoding.UTF32.GetBytes(res);
                        var c = command.ExecuteNonQuery();
                    }
                }

                return JObject.Parse(res);
            }
            private string url;
            public virtual string Url
            {
                get
                {
                    return url ?? (url = string.Format("https://maps.googleapis.com/maps/api/{0}/{1}/{2}?key={3}&{4}", api, Method, format, key, string.Join("&", Parameters.Select(x => string.Format("{0}={1}", x.Key, x.Value)))));
                }
            }
            public abstract string Method { get; }
            public abstract Dictionary<string, string> Parameters { get; }
            private JObject cachedValue;
            private JObject CachedValue
            {
                get
                {
                    return cachedValue ?? (cachedValue = GetFromCache());
                }
            }
            private JObject GetFromCache()
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["citizator"].ConnectionString))
                {
                    connection.Open();
                    var cmd = string.Format(
                      @"declare @id int
                        select @id = id from [dbo].[Request] where url = '{0}'
                        if(@id > 0)
                        begin
                        insert into [dbo].[RequestHistory](requestid, issued)
                            select
                               @id , '{1}'

                        select response from [dbo].[Request] where id = @id 
                    end

                    ", Url.Replace(api, "<GoogleApiKey>"), DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    using (var command = new SqlCommand(cmd, connection))
                    {
                        byte[] res = (byte[])command.ExecuteScalar();
                        if (res == null)
                        {
                            return null;
                        }
                        return JObject.Parse(Encoding.UTF32.GetString(res));
                    }
                }
            }
        }
        public class GooglePlace : GoogleApiRequest
        {
            private readonly string place;
            private readonly List<PlaceFields> fields;
            public GooglePlace(string key, string place, List<PlaceFields> fields)
                : base(api: "place", key: key)
            {
                this.place = place;
                this.fields = fields;
            }
            public override string Method
            {
                get { return "findplacefromtext"; }
            }
            public override Dictionary<string, string> Parameters
            {
                get
                {
                    return new Dictionary<string, string>
                    {
                        {"input",place},
                        {"inputtype","textquery"},
                         {"fields",string.Join(",",fields)}
                    };
                }
            }
            public string PlaceId
            {
                get
                {
                    return Result["candidates"][0]["place_id"].ToString();
                }
            }
        }
        public class NearbyPlacesSearch : GoogleApiRequest
        {
            protected string location;
            protected int radius;
            public NearbyPlacesSearch(
                string key,
                string location,
                int radius)
                : base(api: "place", key: key)
            {
                this.location = location;
                this.radius = radius;
            }
            public override string Method
            {
                get { return "nearbysearch"; }
            }
            public override Dictionary<string, string> Parameters
            {
                get
                {
                    return new Dictionary<string, string>
                    {
                        {"location",location},
                        {"radius",radius.ToString()}
                    };
                }
            }
        }
        public class PlaceDetails : GoogleApiRequest
        {
            protected string place;
            protected List<PlaceFields> fields;
            public PlaceDetails(
                string key,
                string place,
                 List<PlaceFields> fields)
                : base(api: "place", key: key)
            {
                this.place = place;
                this.fields = fields;
            }
            public override string Method
            {
                get { return "details"; }
            }
            public override Dictionary<string, string> Parameters
            {
                get
                {
                    return new Dictionary<string, string> { 
                        { "placeid", place },
                        {"fields",string.Join(",",fields)}
                    };
                }
            }
        }
    }
}
