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
    }
}