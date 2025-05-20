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
        public Prestation prestation { get; set; }
        public int PrestationId { get; set; }
        public RendezVous (User user, Prestation prestation, int userId, int prestationId)
        {
            this.user = user;
            this.prestation = prestation;
            UserId = userId;
            PrestationId = prestationId;
        }
    }
}
