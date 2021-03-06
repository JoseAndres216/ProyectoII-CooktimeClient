﻿using CookTime.Model__Logic_.Data_Structures;
using CookTime.ViewModel__Abstract_UI_;
using MobileClient.Model__Logic_;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms.Internals;

namespace CookTime.Model__Logic_
{
    public class User
    {

        [JsonProperty] public string email { get; set; }
        [JsonProperty] public string name { get; set; }
        [JsonProperty] public string password { get; set; }
        [JsonProperty] public int age { get; set; }

        [JsonProperty] public int rating;
        [JsonProperty] public bool isChef;
        [JsonProperty] public Data_Structures.Stack<string> notifications;
        [JsonProperty] public SimpleList<String> followers;
        [JsonProperty] public Data_Structures.Stack<Recipe> newsFeed;
        [JsonProperty] public MyMenu MyMenu;
        public string image = "StandartUserPic.png";


        public User(string email, string password, string name, int age)
        {
            this.email = email;
            this.password = password;
            this.name = name;
            this.age = age;
            this.newsFeed = new Stack<Recipe>();
            this.notifications = new Stack<string>();
            this.MyMenu = new MyMenu();
            this.followers = new SimpleList<String>();
            this.rating = 0;
            this.isChef = false;
            
        }
        private async System.Threading.Tasks.Task updateFeedAsync()
        {
            try
            {
                HttpClient client = new HttpClient();


                //     ?  ---> para decir que van parametros SOLO EL PRIMERO    
                //     &  ---> para separar un parametro de otro. :)
                client.BaseAddress = new Uri(Client.HTTP_BASE_URL + "user/feed?email=" + this.email );

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                //Get the json from the sever NULL if didn't match
                String json = response.Content.ReadAsStringAsync().Result;
                //Serialize the json object to a User object and assign it to the Client User
                Stack<Recipe> newFeed = JsonConvert.DeserializeObject<Stack<Recipe>>(json);
                this.newsFeed = newFeed;
            }
            catch(Exception e)
            {
                //exception
            }
        }
        public User()
        {

        }
        override
        public String ToString()
        {
            return "User{ name: " + this.name + ", newsFeed: " + this.newsFeed + "}";
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return this.name;
        }

        public Data_Structures.Stack<string> getNotifications()
        {
            if(this.notifications == null)
            {
                return new Stack<string>();
            }
            return this.notifications;
        }

        public SimpleList<Recipe> getMymenu()
        {
            if(this.MyMenu == null)
            {
                return new SimpleList<Recipe>();
            }
            return this.MyMenu.getOwnedrecipes();
        }

        public Data_Structures.Stack<Recipe> getNewsfeed()
        {
            this.updateFeedAsync();

            if (this.newsFeed != null)
            {
                return this.newsFeed;
            }
          
            else
            {   
                Console.Out.WriteLine("Null news feed, User: " + this.email);
                return new Stack<Recipe>();
            }
            
        }

        /* 
         * recipe/create
         * recipe/like
         * recipe/share
         * recipe/rate
         * recipe/comment
         * mymenu/recent
         *          /difficulty
         *          /ranked
         * notifications/
         * followers/
         * followers/
         * request/chef/
         * 
         * NEWSFEED/  
         */
    }
}
