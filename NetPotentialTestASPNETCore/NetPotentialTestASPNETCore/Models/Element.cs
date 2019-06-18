using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NetPotentialTestASPNETCore.Models
{
    public class Element
    {
        /// <summary>
        /// Dynamic arrays to mantein the state between request/response
        /// </summary>
        public List<int> LeftElementsNow { get; set; }
        public List<int> RightElementsNow { get; set; }

        /// <summary>
        /// Arrays to support the elements selected on the listboxes
        /// </summary>
        public int[] SelectedLeftItems { get; set; }
        public int[] SelectedRightItems { get; set; }

        /// <summary>
        /// List to bind the data to the listboxes
        /// </summary>
        public IReadOnlyList<SelectListItem> LeftElements { get; set; }
        public IReadOnlyList<SelectListItem> RightElements { get; set; }
    }
}