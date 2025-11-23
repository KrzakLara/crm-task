using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_Property_Functions
{
    public class GetPropertiesService
    {
        private readonly DataverseService _dataverse;

        public GetPropertiesService(DataverseService dataverse)
        {
            _dataverse = dataverse;
        }

        public async Task<List<object>> FetchPropertiesAsync()
        {
            var client = _dataverse.GetClient();
            var result = new List<object>();

            var query = new QueryExpression("cr971_property")
            {
                ColumnSet = new ColumnSet(
                    "cr971_propertyname",
                    "cr971_propertytype",       
                    "cr971_location",
                    "cr971_numberofrooms",
                    "cr971_averagedailyrate",
                    "cr971_rating"
                )
            };

            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);

            var records = client.RetrieveMultiple(query);

            foreach (var r in records.Entities)
            {
                var typeOption = r.GetAttributeValue<OptionSetValue>("cr971_propertytype");

                result.Add(new
                {
                    Name = r.GetAttributeValue<string>("cr971_propertyname"),
                    Location = r.GetAttributeValue<string>("cr971_location"),
                    NumberOfRooms = r.GetAttributeValue<int?>("cr971_numberofrooms"),
                    AverageDailyRate = r.GetAttributeValue<decimal?>("cr971_averagedailyrate"),
                    Rating = r.GetAttributeValue<decimal?>("cr971_rating"),

                    PropertyType = typeOption?.Value
                });
            }

            return result;
        }
    }
}
