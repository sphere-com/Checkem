﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace Checkem.Models
{
    public class Todo_DataAccess_Json<T> : IDataAccess<T>
    {
        public Todo_DataAccess_Json()
        {
            inventory = Retrieve();
        }


        //File path for inventory json file
        public string JsonFilePath = @"./Inventory.json";


        //Inventory to save to do items
        private List<T> inventory;
        public List<T> Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                if (inventory != value)
                {
                    inventory = value;
                }
            }
        }


        //Save everthing from inventory list to Inventory.json file
        public void Save(List<T> inventory)
        {
            File.WriteAllText(JsonFilePath, JsonConvert.SerializeObject(inventory));
        }


        //Retrive all to do items from json file and store them into Inventory list
        public List<T> Retrieve()
        {
            try
            {
                //try to read json file
                string json = File.ReadAllText(JsonFilePath);

                //return the list
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            catch (FileNotFoundException)
            {
                //if the file is not found, create a new json file contains a empty list
                File.WriteAllText(JsonFilePath, JsonConvert.SerializeObject(new List<Todo>()));

                //retrive again
                return Retrieve();
            }
        }
    }
}
