using System;
using System.Collections.Generic;
using System.linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Clients;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace cosmosdb-sol
{

    public class students{
        public string Id{get;set;}
        public string FName{get;set;}
        public string LName{get;set;}
    }

    class cosmosdb
    {
        static void Main(string[] args)
        {
            // create db/coll/items/read/update/delete

            string EndpointUrl="Enter url of cosmos db";
            string accesskey="Enter access key";

            DocumentClient client;
            client = new DocumentClient(new Uri(EndpointUrl), accesskey);

            //create db
            client.CreateDatabaseIfNotExistsAsync(new Database() {Id=dbname}).GetAwaiter().GetResult();

            //create collection
            client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(dbname),new DocumentCollection{Id=collname}).GetAwaiter().GetResult();

            //add data to collection
            var student1= new students(){Id="S001",FName ="Chandan" , LName="Singh"};

            //add collection

            client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(dbname,collname), student1).GetAwaiter().GetResult();

            Console.WriteLine("one record added");

            //query the data
            var query =client.CreateDocumentQuery<students>(UriFactory.CreateDocumentCollectionUri(dbname,collname)).ToList();

            foreach (var obj in query)
            {
                Console.WriteLine(obj.FName);
            }
            Console.Read();

        }
    }
}
