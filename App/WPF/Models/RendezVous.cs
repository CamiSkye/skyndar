using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Models
{
   public class RendezVous
    {
        public User user { get; set; }
        public int UserId { get; set; }
        public Creneau creneau { get; set; }
        public int CreneauId { get; set; }
        public RendezVous (User user, Creneau creneau, int userId, int creneauId )
        {
            this.user = user;
            this.creneau = creneau;
            UserId = userId;
            CreneauId = creneauId;
        }
    }
}
