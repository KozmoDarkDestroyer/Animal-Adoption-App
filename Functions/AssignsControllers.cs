using animal_adoption.Models;
using animal_adoption.ModelViews;

namespace animal_adoption.Functions
{
    public class AssignsControllers
    {
        public static Foundation AssingFoundation (FoundationPost model, Foundation foundation){
            foundation.name = model.name;
            foundation.association = model.association;
            foundation.address = model.address;
            foundation.email = model.email;
            foundation.web = model.web;
            return foundation;
        }

        public static User AssingUser (UserPost model, User user, string action){
            action = action.ToUpper();
            if (action == "POST")
            {
                user.name = model.name;
                user.email = model.email;
                user.password = Encrypt.Encrypted(model.password);
                user.role = model.role;
                user.status = model.status;
                return user;
            }
            else if (action == "PUT")
            {
                user.name = model.name;
                user.password = Encrypt.Encrypted(model.password);
                user.role = model.role;
                user.status = model.status;
                return user;
            }
            else{
                return null;
            }
        }

        public static Adopter AssingAdopter (AdopterPost model, Adopter adopter){
            adopter.name = model.name;
            adopter.email = model.email;
            adopter.identification = model.identification;
            adopter.address = model.address;
            adopter.id_pet = model.id_pet;
            adopter.phone = model.phone;
            return adopter;
        }

        public static Pet AssingPet (PetPost model, Pet pet){
            pet.name = model.name;
            pet.species = model.species;
            pet.age = model.age;
            pet.race = model.race;
            pet.sex = model.sex;
            pet.id_foundation = model.id_foundation;
            return pet;
        }

        public static Form AssigForm (FormPost model, Form form){
            form.name = model.name;
            form.id_adopter = model.id_adopter;
            form.number_adults = model.number_adults;
            form.number_children = model.number_children;
            form.pet_race = model.pet_race;
            form.pet_status_check = model.pet_status_check;
            form.pets_before = model.pets_before;
            form.responsibility_pet = model.responsibility_pet;
            form.age_children = model.age_children;
            form.rason_adoption = model.rason_adoption;
            return form;
        }
    }
}