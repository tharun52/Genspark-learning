using System;

namespace FirstApi.Models
{

    public class Doctor : IComparable<Doctor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Specialization { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;

            Doctor other = (Doctor)obj;
            return Id == other.Id;
        }

       
        public int CompareTo(Doctor other)
        {
            if (other == null) return 1;  // this instance is greater than null
            return Id.CompareTo(other.Id);
        }
    }

}