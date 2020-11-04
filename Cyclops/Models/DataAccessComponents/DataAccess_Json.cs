﻿using Cyclops.Models.Interface;
using Cyclops.Models.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Cyclops.Models.DataAccessComponents
{
    public abstract class DataAccess_Json : IDataAccess
    {
        //Inventory to save to do items
        protected List<ToDoItem> Inventory = new List<ToDoItem>();

        //File path for inventory json file
        private string JsonFilePath = "Inventory.json";


        //Save everthing from inventory list to Inventory.json file
        private static void SaveToJson(List<ToDoItem> Inventory)
        {
            File.WriteAllText(@"Inventory.json", JsonConvert.SerializeObject(Inventory));
        }



        #region test data
        public void StoreTestData()
        {
            Inventory.Clear();

            //Inventory.Add(new ToDoItem { Id = 0, Title = "Item 0", Description = "Item 0's details !" });
            //Inventory.Add(new ToDoItem { Id = 1, Title = "Item 1", Description = "Item 1's details !" });
            //Inventory.Add(new ToDoItem { Id = 2, Title = "Item 2", Description = "Item 2's details !" });

            //Inventory.Add(new ToDoItem { Id = 0, Title = "Notify Test", Description = "It works !" });

            Inventory.Add(new ToDoItem { ID = 1, Title = "Update", Description = "windows 10 update", IsReminderOn = true, BeginDateTime = DateTime.Now.AddDays(5) });
            Inventory.Add(new ToDoItem { ID = 2, Title = "Meeting", IsStarred = true });
            Inventory.Add(new ToDoItem { ID = 3, Title = "Math Homework", Description = "kinda hard", IsReminderOn = true, BeginDateTime = DateTime.Now.AddMinutes(30) });
            Inventory.Add(new ToDoItem { ID = 4, Title = "Read Pro Angular 6", IsCompleted = true, IsReminderOn = true, IsAdvanceReminderOn = true, BeginDateTime = DateTime.Now.AddMinutes(1), EndDateTime = DateTime.Now.AddHours(1) });
            Inventory.Add(new ToDoItem { ID = 5, Title = "Call someone", Description = "I forgot who it was ;p", IsStarred = true });
            Inventory.Add(new ToDoItem { ID = 6, Title = "Review electronic", Description = "prepare for the test", IsStarred = true });
            Inventory.Add(new ToDoItem { ID = 7, Title = "Program", Description = "Angular + JS", IsCompleted = true, IsStarred = true });
            Inventory.Add(new ToDoItem { ID = 8, Title = "electronic test", Description = "1-1:RC coupler", IsStarred = true });
            Inventory.Add(new ToDoItem { ID = 9, Title = "Math test", Description = "test ?! again ?!", IsCompleted = true, IsReminderOn = true, BeginDateTime = DateTime.Now.AddDays(1) });

            Inventory.Add(new ToDoItem
            {
                ID = 10,
                Title = "Detail test",
                Description = "Ten years ago, I was nearly 30 and over $90,000 in debt. I had spent my twenties trying to build an interesting life; I had two degrees; I had lived in New York and the Bay Area; I had worked in a series of interesting jobs;I spent a lot of time traveling overseas. But I had also made a couple of critically stupid and shortsighted decisions. I had invested tens of thousands of dollars in a master's degree in landscape architecture that I realized I didn't want halfway through. While maxing out my student loans, I had also collected a toxic mix of maxed-out credit cards, personal loans, and $2,000 I had borrowed from my father for a crisis long since forgotten. My life consisted of loan deferments and minimum payments.Like so many other lost children,I had fallen into a career in IT.The work was boring,but led to jobs with cool organizations — a lot of jobs,because I kept quitting them.As soon as I had any money in the bank,I'd quit and go backpacking in Southeast Asia. My adventures were life-changing experiences, but I was eventually left with a CV that was pretty scattershot.My luck securing interesting jobs dried up.In 2001,I ended up living with my dad for four months and working at a banking infrastructure company in suburban Pittsburgh.I should have taken that as a warning that I needed to get it together, but I thought it was just an aberration.It was not.A year later, I was living in a one room Brooklyn apartment with a cat that compulsively peed on my bathmat. (This was a questionable upgrade to living with Dad.) I was trapped in a low paying IT job that I hated, I was in a relationship with a woman who hated me, and I was barely getting by, never mind getting control over my financial situation.I was finally reckoning with the fact that I was facing decades of unaffordable minimum debt payments. Any career ambitions I might have had would be put on hold indefinitely, possibly permanently. Travel was over.The full amount that I would have to pay off at the end of it all was too awful to contemplate.I felt trapped and hopeless.I was in a dark place.And so like many people in dark places, I looked for an escape hatch.I came up with two options: going back to graduate school or getting a job with a non - governmental organization(NGO) overseas.Graduate school would be the ultimate escape — it was better than working, and it would allow me to continue to defer my existing student loans(notwithstanding, of course, the small fact that I would potentially be doubling them). But while school was attractive in the short term, the financial implications were absolutely insane."
            });

            SaveToJson(Inventory);
        }
        #endregion



        //Rearrange id according to the index in inventory list
        public void ResetId()
        {
            foreach (var item in Inventory)
            {
                item.ID = Inventory.IndexOf(item);
            }

            SaveToJson(Inventory);
        }


        //Create new to do item
        public void Create(ToDoItem toDoItem)
        {
            Inventory.Add(toDoItem);

            SaveToJson(Inventory);
        }


        //Retrive all to do items from Inventory.json file and store them into Inventory list
        public void Retrieve()
        {
            string json = File.ReadAllText(JsonFilePath);

            Inventory = JsonConvert.DeserializeObject<List<ToDoItem>>(json);
        }


        //Update to do item
        public void Update(ToDoItem toDoItem)
        {
            //Find to do item's index in Inventory list with the property:ID
            int index = Inventory.FindIndex(x => x.ID == toDoItem.ID);

            Inventory[index] = toDoItem;

            SaveToJson(Inventory);
        }


        //Remove to do item
        public void Remove(ToDoItem toDoItem)
        {
            Inventory.Remove(toDoItem);

            SaveToJson(Inventory);
        }
    }
}
