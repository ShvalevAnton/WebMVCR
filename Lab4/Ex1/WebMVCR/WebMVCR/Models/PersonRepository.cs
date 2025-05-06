namespace WebMVCR.Models
{
    public class PersonRepository
    {
        private List<Person> persons = new List<Person>();
        // Кол-во персон в списке
        public int NumberOfPerson
        {
            get
            {
                return persons.Count();
            }
        }
        // Обобщенная коллекция IEnumerable персон
        public IEnumerable<Person> GetAllResponses
        {
            get
            {
                return persons;
            }
        }
        // Метод для добавления персон в коллекцию
        public void AddResponse(Person pers) 
        { 
            persons.Add(pers); 
        }



    } 
}

