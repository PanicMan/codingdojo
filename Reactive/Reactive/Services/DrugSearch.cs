using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive.Services
{
    public class DrugSearch : IDrugSearch
    {
        readonly IDbConnection _dbConnection;

        public DrugSearch(IDbConnection dbConnection) => _dbConnection =
            dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));

        public Task<IEnumerable<Drug>> SearchDrug(string name, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            return Task.Run(() => InternalSearchDrug(name, token));
        }
        private IEnumerable<Drug> InternalSearchDrug(string name, CancellationToken token)
        {
            const string select = @"
SELECT
    BEZEICHNUNG, HERSTELLER, FREMDSCHLUESSEL_II, ATC_CODE, WIRKSTOFFE, KATALOG_NR
FROM
    TABLE(MF_MEDI_DATA_SELECT_P.SEARCHDRUG (:search_, 2, 99, 0, 0))";

            using var command = _dbConnection.CreateCommand();

            command.CommandText = select;
            var param = command.CreateParameter();
            param.ParameterName = "search_";
            param.Value = name;
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;

            command.Parameters.Add(param);

            var result = command.ExecuteReader();
            token.ThrowIfCancellationRequested();

            var list = new List<Drug>();
            while (result.Read())
            {
                token.ThrowIfCancellationRequested();
                var drug = ReadDrug(result);
                list.Add(drug);
            }

            return list;
        }

        private Drug ReadDrug(IDataReader result)
        {
            var drug = new Drug();
            for (int i = 0; i < result.FieldCount; i++)
            {
                var name = result.GetName(i).ToUpperInvariant();
                switch (name)
                {
                    case "BEZEICHNUNG":
                        drug.Name = result.IsDBNull(i) ? string.Empty : result.GetString(i);
                        break;
                    case "HERSTELLER":
                        drug.Manufacturer = result.IsDBNull(i) ? string.Empty : result.GetString(i);
                        break;
                    case "FREMDSCHLUESSEL_II":
                        drug.Id = result.IsDBNull(i) ? string.Empty : result.GetString(i);
                        break;
                    case "ATC_CODE":
                        drug.Atc = result.IsDBNull(i) ? string.Empty : result.GetString(i);
                        break;
                    case "WIRKSTOFFE":
                        drug.Agents = result.IsDBNull(i) ? string.Empty : result.GetString(i);
                        break;
                    case "KATALOG_NR":
                        drug.Catalog = result.IsDBNull(i) ? -1 : result.GetInt32(i);
                        break;
                }
            }

            return drug;
        }
    }
}
