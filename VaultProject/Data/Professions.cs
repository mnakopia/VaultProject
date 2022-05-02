using System.ComponentModel.DataAnnotations;

namespace VaultProject.Data
{
    public enum Professions
    {


        [Display(Name = "მასწავლებელი")]
        Teacher,

        [Display(Name = "მძღოლი")]
        Driver,

        [Display(Name = "სტილისტი")]
        Stylist,

        [Display(Name = "ძიძა")]
        Nanny,

       

    }
}
