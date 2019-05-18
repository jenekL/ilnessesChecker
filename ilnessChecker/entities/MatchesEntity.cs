using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilnessChecker.entities
{
    class MatchesEntity
    {
        private int id;
        private int diseases_id;
        private int symptoms_id;
        private double confidence_measure;
        private double distrust_measure;

        public MatchesEntity()
        {

        }
        public MatchesEntity(int diseases_id, int symptoms_id, double confidence_measure, double distrust_measure)
        {
            this.diseases_id = diseases_id;
            this.symptoms_id = symptoms_id;
            this.confidence_measure = confidence_measure;
            this.distrust_measure = distrust_measure;
        }

        public MatchesEntity(int id, int diseases_id, int symptoms_id, double confidence_measure, double distrust_measure)
        {
            this.id = id;
            this.diseases_id = diseases_id;
            this.symptoms_id = symptoms_id;
            this.confidence_measure = confidence_measure;
            this.distrust_measure = distrust_measure;
        }

        public int Id { get => id; set => id = value; }
        public int Diseases_id { get => diseases_id; set => diseases_id = value; }
        public int Symptoms_id { get => symptoms_id; set => symptoms_id = value; }
        public double Confidence_measure { get => confidence_measure; set => confidence_measure = value; }
        public double Distrust_measure { get => distrust_measure; set => distrust_measure = value; }
    }
}
