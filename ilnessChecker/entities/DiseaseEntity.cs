using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ilnessChecker.entities
{
    class DiseaseEntity
    {
        private int id;
        String disease;

        public DiseaseEntity(string disease)
        {
            this.disease = disease;
        }

        public DiseaseEntity(int id, string disease)
        {
            this.id = id;
            this.disease = disease;
        }

        public String Disease { get => disease; set => disease = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
