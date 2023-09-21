using System;
using Newtonsoft.Json.Linq;

namespace SeleniumNUnit.util
{
	public class jsonReader
	{
		public jsonReader()
		{
		}


        public string extractData(String tokenName)
        {

            String myJsonString = File.ReadAllText("util/testData.json");

            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] extractDataArray(String tokenName)
        {

            String myJsonString = File.ReadAllText("util/testData.json");

            var jsonObject = JToken.Parse(myJsonString);
            List<String> productsList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productsList.ToArray();
        }

    }
}

