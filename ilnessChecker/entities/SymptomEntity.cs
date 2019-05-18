using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilnessChecker.entities
{
    class SymptomEntity
    {
        private int id;
        private string symptom;

        public SymptomEntity(int id, string symptom)
        {
            this.id = id;
            this.symptom = symptom;
        }

        public string Symptom { get => symptom; set => symptom = value; }
        public int Id { get => id; set => id = value; }
    }
}
