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
            var locPlace = new Place(JObject.Parse(pd.Result["result"].ToString()));
            var placesClient = new NearbyPlacesSearch(
                key: key,
                location: locPlace.geometry.formattedLocation,
                radius: 100
                );
            var pl = placesClient.Result;
            /*
             * "{\r\n  \"html_attributions\": [],\r\n  \"results\": [\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.422983,\r\n          \"lng\": 26.987133099999991\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.4638529,\r\n            \"lng\": 27.093279\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.357251,\r\n            \"lng\": 26.8972381\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/geocode-71.png\",\r\n      \"id\": \"4078be90d885157845d101a8b6895e0572720076\",\r\n      \"name\": \"Khmelnytskyi\",\r\n      \"photos\": [\r\n        {\r\n          \"height\": 431,\r\n          \"html_attributions\": [\r\n            \"<a href=\\\"https://maps.google.com/maps/contrib/107447613389815807970/photos\\\">РњР°СЂРёРЅР° РџСѓСЃС‚РѕРІР°</a>\"\r\n          ],\r\n          \"photo_reference\": \"CmRaAAAA6eGpehwOx7y3QHFws6vNPmoV3rynBXnfIkX_qGkbjs5mve2OWVcn
JH1CnMAySF5c1FNoO9kYnusnkc7Eo6yMhnOJD2MdZlZJxN5k4aHpEnAqt8bNxTzHSzcyVcH1DR-MEhBLDkpprQJkGA9ES1muyGGfGhQFrRRr6ny5mUbQFuScafb83_IATw\",\r\n          \"width\": 588\r\n        }\r\n      ],\r\n      \"place_id\": \"ChIJixe7REMGMkcRZMnZJDsL89k\",\r\n      \"reference\": \"ChIJixe7REMGMkcRZMnZJDsL89k\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"locality\",\r\n        \"political\"\r\n      ],\r\n      \"vicinity\": \"Khmelnytskyi\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4106608,\r\n          \"lng\": 26.9547318\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.4120066302915,\r\n            \"lng\": 26.9559897302915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.4093086697085,\r\n            \"lng\": 26.9532917697085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/cafe-71.png\",\r\n      \"id\": \"e4d
5b730b076fc3b6b34a93753ccc85a0493b1ca\",\r\n      \"name\": \"РљР°С„Рµ - РїРµРєР°СЂРЅСЏ \\\"Biskvit\\\"\",\r\n      \"opening_hours\": {\r\n        \"open_now\": false\r\n      },\r\n      \"photos\": [\r\n        {\r\n          \"height\": 4160,\r\n          \"html_attributions\": [\r\n            \"<a href=\\\"https://maps.google.com/maps/contrib/105518403308838129593/photos\\\">Alex Kit</a>\"\r\n          ],\r\n          \"photo_reference\": \"CmRaAAAAMchQA1fFZaHycCvTkMYqiHfVMKNsGLPPKYfRWcMZl8w7ZdT6gyY9Wxqxhm-ynzT66CMnNiwVA3ZBRlUvc6VdrwShTIr_qTzCTPXRd8BHUlM4EqJnbaY__JQHptsEcdhvEhDMhBqsc7aWNb_RWy5HuQhXGhSPZ1trOhTku81tviIcGNPgEMzeIA\",\r\n          \"width\": 3120\r\n        }\r\n      ],\r\n      \"place_id\": \"ChIJxwh01JEGMkcRSMy_bClijHc\",\r\n      \"plus_code\": {\r\n        \"compound_code\": \"CX63+7V Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+7V\"\r\n      },\r\n      \"rating\": 4.4,\r\n      \"reference\": \"ChIJxwh01JEGMkcRSMy_bClijHc\",\r\n      \"scope\":
 \"GOOGLE\",\r\n      \"types\": [\r\n        \"cafe\",\r\n        \"food\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"Instytuts'ka Street, 17, Khmelnytskyi\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4110216,\r\n          \"lng\": 26.953743\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.412370580291487,\r\n            \"lng\": 26.9550919802915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.409672619708488,\r\n            \"lng\": 26.9523940197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/shopping-71.png\",\r\n      \"id\": \"73cc419f09b84254ccb823e70e0e7860345d299a\",\r\n      \"name\": \"Mahazyn Koshyk\",\r\n      \"opening_hours\": {\r\n        \"open_now\": false\r\n      },\r\n      \"place_id\": \"ChIJndxiyZEGMkcR7R3HxWWrbcI\",\r\n      \"plus_code\":
 {\r\n        \"compound_code\": \"CX63+CF Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+CF\"\r\n      },\r\n      \"rating\": 4.2,\r\n      \"reference\": \"ChIJndxiyZEGMkcR7R3HxWWrbcI\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"grocery_or_supermarket\",\r\n        \"store\",\r\n        \"food\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"РІСѓР». Р†РЅСЃС‚РёС‚СѓС‚СЃСЊРєР°, 21/1, Khmelnytskyi\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.409987000000008,\r\n          \"lng\": 26.954914\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.4113359802915,\r\n            \"lng\": 26.9562629802915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.4086380197085,\r\n            \"lng\": 26.9535650197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gsta
tic.com/mapfiles/place_api/icons/generic_business-71.png\",\r\n      \"id\": \"7a7d326f38549692ed5f285db9b1b02bb9921280\",\r\n      \"name\": \"Keramycheskye heater Energy hmelnickiy\",\r\n      \"opening_hours\": {\r\n        \"open_now\": true\r\n      },\r\n      \"photos\": [\r\n        {\r\n          \"height\": 422,\r\n          \"html_attributions\": [\r\n            \"<a href=\\\"https://maps.google.com/maps/contrib/101506628712662380018/photos\\\">Р©РµРґСЂРёР№ РљСЂР°Р№</a>\"\r\n          ],\r\n          \"photo_reference\": \"CmRaAAAANvK7H9M8CoA3l8WqbZJ9mX3AadzJZ-XnSJPrPGC3AdP8HKrsy4WzJwYLj27rCEC9h_svgmQMLhDsvYZMQVe5NAEra-3Lidz_bVP3Z6AFo8x6laB7uH-PZSLNqa4TSOrmEhCMEy0liNRM3KYJmMKQ6aGSGhQ6LdFlZs_D3nUoccQainvONIIO3Q\",\r\n          \"width\": 600\r\n        }\r\n      ],\r\n      \"place_id\": \"ChIJn-eP1o8GMkcRThlcK5u5l_U\",\r\n      \"rating\": 5,\r\n      \"reference\": \"ChIJn-eP1o8GMkcRThlcK5u5l_U\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"general_contractor\",\r\n        
\"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"РРЅСЃС‚РёС‚СѓС‚СЃРєР°СЏ,17/1 РєРІ 18, РҐРјРµР»СЊРЅРёС†РєРёР№\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4113177,\r\n          \"lng\": 26.9547429\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.412553230291493,\r\n            \"lng\": 26.9560097802915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.409855269708487,\r\n            \"lng\": 26.9533118197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/generic_business-71.png\",\r\n      \"id\": \"0bcd8a7dd9dc8ad12b638ca5014be2381350d8a7\",\r\n      \"name\": \"Ukrklimat\",\r\n      \"photos\": [\r\n        {\r\n          \"height\": 500,\r\n          \"html_attributions\": [\r\n            \"<a href=\\\"https://maps.google.com/maps/contrib/110587188137608643701/photos\\\">РЈРљР РљР
›Р†РњРђРў</a>\"\r\n          ],\r\n          \"photo_reference\": \"CmRaAAAAOPsJcZwvymDqrJnkD2RML6ta3ki2rpbW9vqGEfMqrdbnQcraRfRbP9-vD2mQjPN6mQDF4l8PFmvxA57AZiLUKbRM_IxAGF0jO-Y9petpISeZevDP1bj_y7Uzbev8zys8EhCWgEr6cTWofNTVMfUenWAfGhQDQyFlGD3vMlkCp4G6bD1j6Ua45w\",\r\n          \"width\": 400\r\n        }\r\n      ],\r\n      \"place_id\": \"ChIJr5eCxJEGMkcRynx1zNIs7_U\",\r\n      \"rating\": 5,\r\n      \"reference\": \"ChIJr5eCxJEGMkcRynx1zNIs7_U\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"Instytuts'ka Street, 22, Khmelnytskyi\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4108266,\r\n          \"lng\": 26.953561999999991\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.4124099302915,\r\n            \"lng\": 26.9550745802915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.409711
9697085,\r\n            \"lng\": 26.9523766197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/shopping-71.png\",\r\n      \"id\": \"3455dffeb0c13f37f5ff019a4fc865a459bc3bdf\",\r\n      \"name\": \"РђРїС‚РµРєР° \\\"РђРїС‚РµРєРё Р·РґРѕСЂРѕРІ'СЏ\\\"\",\r\n      \"place_id\": \"ChIJh0cRy5EGMkcRI_w_6T3Paw0\",\r\n      \"plus_code\": {\r\n        \"compound_code\": \"CX63+8C Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+8C\"\r\n      },\r\n      \"rating\": 4.8,\r\n      \"reference\": \"ChIJh0cRy5EGMkcRI_w_6T3Paw0\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"pharmacy\",\r\n        \"store\",\r\n        \"health\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"1,, РІСѓР»РёС†СЏ Р†РЅСЃС‚РёС‚СѓС‚СЃСЊРєР°, 21, РҐРјРµР»СЊРЅРёС†СЊРєРёР№\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.411037,
\r\n          \"lng\": 26.9543131\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.4123859802915,\r\n            \"lng\": 26.955662080291511\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.4096880197085,\r\n            \"lng\": 26.9529641197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/bus-71.png\",\r\n      \"id\": \"caa2fddd447051c109b8abe8515f9ddef2914ddf\",\r\n      \"name\": \"Instytutska St\",\r\n      \"photos\": [\r\n        {\r\n          \"height\": 4160,\r\n          \"html_attributions\": [\r\n            \"<a href=\\\"https://maps.google.com/maps/contrib/113178893831110055492/photos\\\">Р‘РѕСЂРёСЃ РњРµР»СЊРЅРёС†РєРёР№</a>\"\r\n          ],\r\n          \"photo_reference\": \"CmRaAAAA2srzu92N0hce64OuIAuw7H1YMbIoRRe3CpZMjvQLBVVXVQyevE5gncP0yduskgI4P9h5moHDQeSZsSlZLFrGqQw_f-s5dgj_8e79zq_PjRWMMW0_CXn0Rk2k6hPKTsWiEhDQeXbSdF0y8F4BFhMHKbD2GhQmAf308ft-cY3GZ6lIx228j
_JLOg\",\r\n          \"width\": 3120\r\n        }\r\n      ],\r\n      \"place_id\": \"ChIJN6dSzpEGMkcR43JvsywqUsw\",\r\n      \"plus_code\": {\r\n        \"compound_code\": \"CX63+CP Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+CP\"\r\n      },\r\n      \"rating\": 3.7,\r\n      \"reference\": \"ChIJN6dSzpEGMkcR43JvsywqUsw\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"bus_station\",\r\n        \"transit_station\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"Ukraine\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.410926,\r\n          \"lng\": 26.953205\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.412274980291492,\r\n            \"lng\": 26.954553980291511\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.4095770197085,\r\n            \"lng\": 26.9518560197085\r\n     
     }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/bus-71.png\",\r\n      \"id\": \"0232f466dec539bc3f1c91e3e97eb0abfe4ef9f9\",\r\n      \"name\": \"Poliklinika #4\",\r\n      \"place_id\": \"ChIJ5aRvtZEGMkcRzogJ-4mNqpk\",\r\n      \"plus_code\": {\r\n        \"compound_code\": \"CX63+97 Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+97\"\r\n      },\r\n      \"rating\": 4,\r\n      \"reference\": \"ChIJ5aRvtZEGMkcRzogJ-4mNqpk\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"bus_station\",\r\n        \"transit_station\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"Ukraine\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4113177,\r\n          \"lng\": 26.9547429\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.412553230291493,\r\n            \"lng\": 
26.9560097802915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.409855269708487,\r\n            \"lng\": 26.9533118197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/shopping-71.png\",\r\n      \"id\": \"633669465c2a3d58076d9cf9a702639662110754\",\r\n      \"name\": \"РњРђРЎРўР•Р РћРљ\",\r\n      \"opening_hours\": {\r\n        \"open_now\": false\r\n      },\r\n      \"photos\": [\r\n        {\r\n          \"height\": 2848,\r\n          \"html_attributions\": [\r\n            \"<a href=\\\"https://maps.google.com/maps/contrib/114855471116789686602/photos\\\">Alexander Shpak</a>\"\r\n          ],\r\n          \"photo_reference\": \"CmRaAAAA9eqnZnI6W5TsqWNvC8IRe4mfjzozNn6-xg3xNdPwenUphWPLNa5Tu3R1PNZZpUhTa-iupYNyZGCBVd33cVffCdJPLmtzfhpzXlA3k2GxO_VMFh-abI1LVMN_JKTOq11XEhBSNKDsj9iPu7ihP_Yi4-ReGhRidqWzakEfoc1nQxE-Lgk6A7KJMg\",\r\n          \"width\": 4288\r\n        }\r\n      ],\r\n      \"place_id\": \"ChIJbe58mQYHMkcRvdO
ZF4kp97s\",\r\n      \"plus_code\": {\r\n        \"compound_code\": \"CX63+GV Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+GV\"\r\n      },\r\n      \"reference\": \"ChIJbe58mQYHMkcRvdOZF4kp97s\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"store\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"Instytuts'ka Street, 22, Khmelnytskyi\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4113177,\r\n          \"lng\": 26.9547429\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.412553230291493,\r\n            \"lng\": 26.9560097802915\r\n          },\r\n          \"southwest\": {\r\n            \"lat\": 49.409855269708487,\r\n            \"lng\": 26.9533118197085\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/post_office-71.png\",\r\n      \"id\
": \"f18213dcabadec791ed3d7401bb20268fec36a83\",\r\n      \"name\": \"Ukrposhta\",\r\n      \"opening_hours\": {\r\n        \"open_now\": false\r\n      },\r\n      \"place_id\": \"ChIJA7PtKDUHMkcRFVd4pnH-NcA\",\r\n      \"plus_code\": {\r\n        \"compound_code\": \"CX63+GV Khmelnytskyi, Khmelnytskyi Oblast, Ukraine\",\r\n        \"global_code\": \"8GX8CX63+GV\"\r\n      },\r\n      \"reference\": \"ChIJA7PtKDUHMkcRFVd4pnH-NcA\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"post_office\",\r\n        \"finance\",\r\n        \"point_of_interest\",\r\n        \"establishment\"\r\n      ],\r\n      \"vicinity\": \"Instytuts'ka Street, 22, Khmelnytskyi\"\r\n    },\r\n    {\r\n      \"geometry\": {\r\n        \"location\": {\r\n          \"lat\": 49.4143336,\r\n          \"lng\": 26.9500561\r\n        },\r\n        \"viewport\": {\r\n          \"northeast\": {\r\n            \"lat\": 49.4242623,\r\n            \"lng\": 26.973238\r\n          },\r\n          \"southwest\": {\r\n            \"
lat\": 49.395893,\r\n            \"lng\": 26.9341851\r\n          }\r\n        }\r\n      },\r\n      \"icon\": \"https://maps.gstatic.com/mapfiles/place_api/icons/geocode-71.png\",\r\n      \"id\": \"b609129595f7c81d47b07369bc10a0816d2e9cf2\",\r\n      \"name\": \"Mikrorayon Pivdenno-Zakhidnyy\",\r\n      \"place_id\": \"ChIJhSc2LY4GMkcREoGeLH-FFhA\",\r\n      \"reference\": \"ChIJhSc2LY4GMkcREoGeLH-FFhA\",\r\n      \"scope\": \"GOOGLE\",\r\n      \"types\": [\r\n        \"neighborhood\",\r\n        \"political\"\r\n      ],\r\n      \"vicinity\": \"Khmelnytskyi\"\r\n    }\r\n  ],\r\n  \"status\": \"OK\"\r\n}"

             */

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
                    end", Url.Replace(api, "<GoogleApiKey>"), (int)((DateTime.Now - start).TotalMilliseconds), start.ToString("yyyy-MM-dd hh:mm:ss"));
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
