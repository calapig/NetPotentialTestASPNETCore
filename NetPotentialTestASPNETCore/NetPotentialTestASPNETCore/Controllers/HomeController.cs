using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetPotentialTestASPNETCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetPotentialTestASPNETCore.Controllers
{
    public class HomeController : Controller
    {
        ///////////// INIT OF ACTIONS FOR THE EXERCISE ///////////////

        public ActionResult Test()
        {
            List<int> baseList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            var model = new Element
            {
                LeftElements = LoadElements(baseList),
                RightElements = new List<SelectListItem>(),
                LeftElementsNow = baseList,
                RightElementsNow = { }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ToRight(Element model)
        {
            // Update/load state of elements in the dynamic array for the hidden field - remove elements from left
            model.LeftElementsNow = UpdateStateRemove("Left", model.SelectedLeftItems);
            // Update of the model with the updated list for the left listbox
            model.LeftElements = LoadElements(model.LeftElementsNow);

            // Update/load state of elements in the dynamic array for the hidden field - add elements to the right
            model.RightElementsNow = UpdateStateAdd("Right", model.SelectedLeftItems);
            // Update of the model with the updated list for the right listbox
            model.RightElements = LoadElements(model.RightElementsNow);

            return View("Test", model);
        }

        [HttpPost]
        public ActionResult ToLeft(Element model)
        {
            // Update/load state of elements in the dynamic array for the hidden field - remove elements from right
            model.RightElementsNow = UpdateStateRemove("Right", model.SelectedRightItems);
            // Update of the model with the updated list for the right listbox
            model.RightElements = LoadElements(model.RightElementsNow);

            // Update/load state of elements in the dynamic array for the hidden field - add elements to the left
            model.LeftElementsNow = UpdateStateAdd("Left", model.SelectedRightItems);
            // Update of the model with the updated list for the left listbox
            model.LeftElements = LoadElements(model.LeftElementsNow);

            return View("Test", model);
        }

        /// <summary>
        /// Load the elements in the state removing the selected items
        /// </summary>
        /// <param name="requestVar">Name to locate the state in the request</param>
        /// <param name="selectedItems">Array with the selected items</param>
        /// <returns></returns>
        private List<int> UpdateStateRemove(string requestVar, int[] selectedItems)
        {
            //Json left convert to list
            Newtonsoft.Json.Linq.JArray stateArray = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(Request.Form[requestVar]);
            //list remove the selected items
            List<int> newState = new List<int>();
            if (stateArray != null)
            {
                foreach (var li in stateArray)
                {
                    if (!selectedItems.Contains((int)li)) // exclude the value if was selected to move
                        newState.Add((int)li);
                }
            }

            return newState;
        }

        /// <summary>
        /// Load the elements in the state adding the selected items
        /// </summary>
        /// <param name="requestVar">Name to locate the state in the request</param>
        /// <param name="selectedItems">Array with the selected items</param>
        /// <returns></returns>
        private List<int> UpdateStateAdd(string requestVar, int[] selectedItems)
        {
            //Json left convert to list
            Newtonsoft.Json.Linq.JArray leftArray = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(Request.Form[requestVar]);
            //list remove the selected items
            List<int> leftUpdated = new List<int>();
            if (leftArray != null)
            {
                foreach (var li in leftArray)
                {
                    leftUpdated.Add((int)li);
                }
            }

            foreach (int selIt in selectedItems)
            {
                leftUpdated.Add(selIt);
            }

            return leftUpdated;
        }

        /// <summary>
        /// Convert a dynamic array of integer elements into a dynamic array of SelectListItem with the
        /// same value and text for each integer
        /// </summary>
        /// <param name="listElements">dynamic integer array</param>
        /// <returns></returns>
        private List<SelectListItem> LoadElements(List<int> listElements)
        {
            List<SelectListItem> Set = new List<SelectListItem>();
            SelectListItem Ele;
            foreach (object IndexE in listElements)
            {
                Ele = new SelectListItem
                {
                    Text = IndexE.ToString(),
                    Value = IndexE.ToString()
                };

                Set.Add(Ele);
            }

            return Set;
        }

        ///////////// INIT OF ACTIONS FOR THE EXERCISE ///////////////


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
