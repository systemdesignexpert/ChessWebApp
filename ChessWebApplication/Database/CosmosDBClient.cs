using Chess_System_Design;
using Microsoft.Azure.Cosmos;

namespace ChessWebApplication.Database
{
	public class CosmosDBClient
	{
        private Container container;
        private string connString = Secrets.COSMOS_SONN_STRING;
        private string dbId = "ChessGameDB";
        private string containerId = "chessgamedb";

        public CosmosDBClient()
        {
            CosmosClient cosmosClient = new CosmosClient(connString);
            this.container = cosmosClient.GetContainer(dbId, containerId);
        }

        async public Task<KeyValue> put(string _id, List<Move> _moves)
        {
            try
            {
                ItemResponse<KeyValue> response;
                var item = new KeyValue(_id, _moves);
                response = await this.container.UpsertItemAsync<KeyValue>(item, new PartitionKey(_id));
                return response;
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new KeyValue("", null);
            }
        }

        async public Task<KeyValue> get(string id)
        {
            try
            {
                ItemResponse<KeyValue> response;
                response = await this.container.ReadItemAsync<KeyValue>(id, new PartitionKey(id));
                return response;
            }
            catch (CosmosException c)
            {
                if (c.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new KeyValue(id, null);
                    
                }
                throw c;
            }
        }
    }



}

