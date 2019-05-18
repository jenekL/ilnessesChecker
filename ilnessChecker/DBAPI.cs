using Dapper;
using ilnessChecker.entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilnessChecker
{
    class DBAPI
    {

        public static List<DiseaseEntity> LoadDiseases()
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<DiseaseEntity>("select * from diseases", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveDisease(DiseaseEntity disease)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                cnn.Execute("insert into diseases (disease) values (@Disease)", disease);
            }
        }
        public static void DeleteDisease(DiseaseEntity disease)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                cnn.Execute("delete from diseases where id = @Id", disease);
            }
        }
        public static void DeleteSymptom(SymptomEntity symptom)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                cnn.Execute("delete from symptoms where id = @Id", symptom);
            }
        }
        public static DiseaseEntity getDiseaseID(DiseaseEntity disease)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<DiseaseEntity>("select id from diseases where diseases.disease like @disease", disease);
                return output.ToList().First();
            }
        }
        public static SymptomEntity getSymptomByID(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<SymptomEntity>("select * from symptoms where symptoms.id = @id", new { id });
                return output.ToList().First();
            }
        }
        public static int getMAXDiseaseID()
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<int>("select MAX(id) from diseases", new DynamicParameters());
                return output.ToList().First();
            }
        }
        public static int getMAXSymptomID()
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<int>("select MAX(id) from symptoms", new DynamicParameters());
                return output.ToList().First();
            }
        }
        

        public static List<SymptomEntity> LoadSymptoms()
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<SymptomEntity>("select * from symptoms", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveSymptom(SymptomEntity symptom)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                cnn.Execute("insert into symptoms (symptom) values (@Symptom)", symptom);
            }
        }

        public static List<MatchesEntity> LoadMatches()
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<MatchesEntity>("select * from matches", new DynamicParameters());
                return output.ToList();
            }
        }
         public static List<MatchesEntity> LoadMatchesByDisease(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                var output = cnn.Query<MatchesEntity>("select * from matches where diseases_id = @id", new { id });
                return output.ToList();
            }
        }

        public static void SaveMatch(MatchesEntity matches)
        {
            using (IDbConnection cnn = new MySqlConnection(loadConnectionString()))
            {
                cnn.Execute("insert into matches (diseases_id, symptoms_id, distrust_measure, confidence_measure) values (@Diseases_id," +
                    "@Symptoms_id, @Distrust_measure, @Confidence_measure)", matches);
            }
        }



        private static string loadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

    }
}
