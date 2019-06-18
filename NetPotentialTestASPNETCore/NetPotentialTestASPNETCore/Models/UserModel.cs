using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NetPotentialTestASPNETCore.Models
{
    public class UserModel
    {
        public int[] SelectedTeaIds { get; set; }
        public IEnumerable<SelectListItem> TeaList { get; set; }
    }
}
