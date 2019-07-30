namespace animal_adoption.ModelViews
{
    public class FormPost
    {
        public string name { get; set; }

        public int number_adults { get; set; }

        public int number_children { get; set; }

        public int age_children { get; set; }

        public string pet_race { get; set; }

        public string pets_before { get; set; }

        public string rason_adoption { get; set; }

        public string responsibility_pet { get; set; }

        public bool pet_status_check { get; set; }

        public string report { get; set; }
        
        public int id_adopter { get; set; }
    }
}