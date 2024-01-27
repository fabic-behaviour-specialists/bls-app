using Fabic.Core.Helpers;
using Fabic.Core.Models;
using Fabic.iOS.Controllers;
//using Microsoft.Data.Sqlite;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace Fabic.Core.Controllers
{
    public static class FabicDatabaseController
    {
        private static SQLiteConnection _DBConnection;
        public static bool Initialised { get; set; } = false;
        //private static MobileServiceSQLiteStore _Store;
        //private static MobileServiceUser _User;
        //public static MobileServiceClient _Client;
        public const string MOBILE_APP_URL = "https://fabicapp.fierydevelopment.com:8080/"; //"http://172.23.115.123/BodyLifeSkillsPlatform.API/";//
        public async static Task<bool> InitialiseDatabase(bool withSync = true)
        {
            try
            {
                var path = MobileServiceClient.DefaultDatabasePath + "/" + iOS.Controllers.SecurityController.CurrentUser.UserID + ".db";
                _DBConnection = new SQLiteConnection(path);
                _DBConnection.CreateTable<BehaviourScale>();
                _DBConnection.CreateTable<BehaviourScaleItem>();
                _DBConnection.CreateTable<ItemHighlight>();
                _DBConnection.CreateTable<IChooseChart>();
                _DBConnection.CreateTable<IChooseChartItem>();
                _DBConnection.CreateTable<FabicBehaviourScale>();
                _DBConnection.CreateTable<FabicIChooseChart>();
                _DBConnection.CreateTable<FabicBehaviourScaleItem>();
                _DBConnection.CreateTable<FabicIChooseChartItem>();
                _DBConnection.CreateTable<AboutFabicVideo>();
                _DBConnection.CreateTable<AboutBLSVideo>();

                //var bs = db.Table<BehaviourScale>().ToList();

                Fabic.iOS.External.Reachability.ReachabilityChanged += Reachability_ReachabilityChanged;

                #region Behaviour Scale and Behaviour Scale Items
                {
                    //IMobileServiceSyncTable<FabicBehaviourScale> table = _Client.GetSyncTable<FabicBehaviourScale>();
                    //IMobileServiceSyncTable<FabicBehaviourScaleItem> itemsTable = _Client.GetSyncTable<FabicBehaviourScaleItem>();
                    //IMobileServiceSyncTable<BehaviourScale> table2 = _Client.GetSyncTable<BehaviourScale>();
                    //IMobileServiceSyncTable<BehaviourScaleItem> itemsTable2 = _Client.GetSyncTable<BehaviourScaleItem>();
                    //List<FabicBehaviourScale> fabicScales = new List<FabicBehaviourScale>();
                    //List<FabicBehaviourScaleItem> fabicScaleItems = new List<FabicBehaviourScaleItem>();
                    //if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
                    //{
                    var fabicScales = _DBConnection.Table<BehaviourScale>().Where(x => x.FabicExample == true && x.Archived == false).ToList();
                    if (fabicScales.Count == 0)
                    {
                        //        Task.Run(() => table.PullAsync("bsFabic", table.Where(x => x.FabicExample == true))).Wait();
                        //        Task.Run(() => itemsTable.PullAsync("bsiFabic", itemsTable.Where(x => x.Archived == false))).Wait();
                        //        Task.Run(() => table2.PullAsync("bs", table2.Where(x => x.FabicExample == false && x.UserID == SecurityController.CurrentUser.UserID))).Wait();
                        //        Task.Run(() => itemsTable2.PullAsync("bsi", itemsTable2.Where(x => x.UserID == SecurityController.CurrentUser.UserID))).Wait();
                        //    }
                        //}


                        //        //{
                        #region Child in school setting
                        // Initialise the Behaviour Scale Parents
                        BehaviourScale scale = new BehaviourScale
                        {
                            Archived = false,
                            Name = "Child in School Setting",
                            FabicExample = true
                        }; _DBConnection.Insert(scale);
                        // Life
                        BehaviourScaleItem item = new BehaviourScaleItem(scale.Id, "Only I get detention", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being bullied", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Teacher yells at me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "A lot of school work", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Whole class detention", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "End of term (lots of changes)", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "School work on Friday afternoon", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lots of instructions at once", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Told I'm wrong", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Making a mistake", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Reading", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids being silly in class", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relief Teacher", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not getting picked for team", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't know how to do something", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Maths", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Teacher giving us work before bell rings", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Rainy day at school", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Doing something new or unfamiliar", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Writing", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Free time", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Break times", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lego", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "PE", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Playing soccer with my friends", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Run out of classroom", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell at teacher", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tell teacher 'no'", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Refuse to do school work", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Laugh loudly at other kids (forced)", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Pushing objects off other student's desks", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Walking around classroom", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell out in class", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Scratching at the ground", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Warm neck", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Louder Voice", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fidgety and difficult to sit", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Continual standing up, sitting down", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Flick objects", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fuzzy and unclear thoughts", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice tone changes (not my voice)", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shoulders tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ignore person calling my name", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Muscles start to tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breathing speeds up", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "I feel relaxed, comfortable, and I have clear focus", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I feel like I am enjoying life", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breathing is gentle", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to listen and participate", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Parents anxiety leading to alcohol abuse
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Parents anxiety leading to alcohol abuse";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Feeling I don't understand my children", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Rejected by people", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Failing", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids fighting with each other", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Everything feels 'too much'", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "The thought of needing to have medication", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not coping", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids saying they hate me", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not completing tasks", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids demanding things", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Other people don't meet my expectations", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Judgement from others", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Taking kids shopping", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids start niggling with each other", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Having to repeat myself", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Leaving the house in a rush", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Household chores", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids don't listen to me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lots of rainy days and can't get washing done", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Time on my own by myself", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Playing with my kids with no fights", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "When my house is in order", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Family time", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relaxing", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Feeling like I don't want to be here any more", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Unstoppable crying", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Drink alcohol", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Scream at my kids", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't want to get out of bed", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Think my life will always be tragic", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Snap at people", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think about drinking", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Very fuzzy head", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Wish my kids were somewhere else", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Tension in my body", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ache behind my ears", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Avoid leaving the house", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tired constantly", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breathing is shallow in my body", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Fidgety", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think I am bored", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Flick my fingers regularly", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Empty feeling in my tummy", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't feel 'good enough'", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Gentle body (not tense)", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Talk clearly and fluently", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel fun and find humour in life", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Clear focus", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I feel joyous", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Mother Stuggling
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Mother struggling";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Children being suspended", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Argument with close friend", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feeling judged by my own mother", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Life feels overwhelming", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I don't know where to start", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Many bills due", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People tell me I don't have skills to care for my children", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People tell me I'm a bad mum", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think I'm not good enough", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Teachers wanting to speak with me", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Kids yell at me", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "School calls but not sure why", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Going to shops with loud and demanding kids", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids will not eat what I cook", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Strategies I try do not work", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Children are sick", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feeling tired", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids don't help out", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Jobs piling up at home", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Extra tasks at work", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Children follow instructions", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "'Child-free' times", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Everyone at home is at 'cold blue'", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Having my children giggle together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Walk in the sunshine", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Ring department to relinquish care", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think - 'I have no control'", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Take extra prescription medication", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Migraines", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like giving up", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Hit children", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell and swear at my children", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tension all over my body", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lots of crying", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kick furniture and objects", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Think - 'I am a bad parent'", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like things will never change", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ignore children", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel helpless", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tension and headaches start", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Be snappy with children", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not sleeping well", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Racy in my body", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Start leaving things messy", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fuzzy head", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Feel gentle in my body", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel calm and at ease", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Speak calmly and lovingly to my children", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Enjoy being with my children", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Juvenile leading to illegal behaviours
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Juvenile leading to illegal behaviours";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Dad hits me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Mum tells me to leave", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Boss is cranky with me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Girlfriend breaks up with me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "School suspends me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "My parents judge me without hearing my side", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel unaccepted by the world", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Going to court", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't feel good about me", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think I fail at everything", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Feel like no one cares about me", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People are not interested in my life", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People tell me what to do all the time", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Dad tells me I'm a failure", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I don't get any good grades", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Making mistakes at work", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Dad tells me what to do all the time", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Hearing dad yell at mum", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "See mum cry", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tension at home", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "My parents are interested in what I am doing", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People genuinely asking about me and my life", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Time with my girlfriend", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Surfing", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Family all having fun together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Steal cars", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Take drugs", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Physically assault people who challenge me", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Abusive towards my family", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Drink more than normal", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Yell", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Drive fast and aggressively", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Smash hand into wall", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Threaten or hurt people", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Drinking alcohol begins", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Swear", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel tension in back and arms", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think of destroying things", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Start planning a party so I can drink", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell at my parents", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Snappy", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Walk away from people", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't want to talk to anyone", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tight jaw", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Voice gets louder and agitated", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Enjoy being with me", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Calm", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Sometimes smiling", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Enjoying talking and being with other people", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel light in my body", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Student struggling with self har,
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Student feels they are not coping and are leading to self-harm";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "People tell lies about me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I can't change what people say about me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People don't like me and I don't know why", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Can't do anything right according to other people", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "People not answering my phone calls and messages", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Failing at something", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Making mistakes all the time", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Friends are being mean to me", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People are always telling me what to do", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Needing to make a decision and it might be wrong", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People change plans", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't know how to start my assignment", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Assignments are building up", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like I don't belong anywhere", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "People moving my belongings in the house", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Given a assignment", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being told to eat when I am not hungry", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Know there is a party coming up", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Watching 'Big Bang Theory'", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Spending time at home", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Friends coming to visit", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Knowing all assignments are complete", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Term break", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Cut myself", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lock myself in a darkened room", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Refuse to eat anything", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Take excessive medication", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Avoid all people", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Scratch at skin", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Verbally abuse random people", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Refuse to leave the house", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Barely eat anything", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feeling constantly angry at the world", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Pacing", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tension in my shoulders", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Take Valium", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Drink alcohol", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feeling like it's all too much", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Scrub the bathroom and the kitchen", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Reorganise bookshelf", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Head feels fuzzy", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Knots in my stomach", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Anxiety in my stomach", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Laugh and smile", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shoulders are relaxed", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Go outdoors", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to be with people", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Just enjoy being with me", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Teenager leading to drug taking
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Teenager leading to drug taking";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Dealer threatening me as I have no money to pay", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Dealer puts up price", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Threatened to be kicked out of home", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Friends judge me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feeling like a 'no hoper'", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "'Coming down' pain is all over my body", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Loose my job", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "No access to drugs", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Argument with dad", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Mum 'getting on my case'", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Being around people I've not met before", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Making a mistake at work", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Workmates laugh at me and I'm not sure why", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People make sarcastic comments", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Alone time", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "No phone credit", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Family not seeing me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People giving me 'angry looks'", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People saying comments I don't understand", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Asked to do jobs", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Spending time with mates", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Going to the skate park", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Just hanging out and having fun", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being with my sister", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Going to the beach", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Take drugs to excess", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Push mum into walls", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kick animals", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Swear at everyone", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Physically threaten those who try to 'calm me down'", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Steal wallets", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell and scream", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Threatening others", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Work out how I can get my next hit", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Have a 'couple of lines'", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Can't stop thinking about drugs", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Heart beats fast", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tension in my shoulders", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kick the couch", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Sweats in the body for no reason", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Just irritated at everything", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Threatening looks directed at people", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Drink alcohol", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Short tempered", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Start avoiding people", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Feel calm and relaxed", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Smile", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Enjoy time with others", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Clear focus", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Zero thoughts about drugs", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Person with an ASD
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Child with an Autism Spectrum Disorder";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Told I'm wrong in front of other people", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Told 'No'", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Rules change", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People laugh at me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "School sports", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "End of term (lots of changes)", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Assemblies", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Other people break rules", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being corrected", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Making a mistake", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Writing", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids making noise in class", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relief teacher or new teacher", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Loud noise I can't stop", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not knowing how to do something", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Having to wear an itchy sports uniform", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids don't listen to me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Rainy days at school", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Doing something unfamiliar", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids laugh and I don't know why", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Technology time", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Break times", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lego", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Science experiments", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "When other kids include me", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Barricade myself in the classroom", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Throw furniture", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Spit", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Threaten to hurt people", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Clenching my fists", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tight jaw/biting teeth", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Avoid eye contact", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Scratch at the ground", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Face feels hot", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice volume 4", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fidgety and difficult being still", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Keep getting out of my chair", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Flick objects", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fuzzy and unclear thoughts", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice tone changes", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shoulders tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ignore people calling my name", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Have a tight jaw", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breath faster", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Comfortable for me to talk", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relaxed/soft muscles", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Using my 'normal' voice", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to listen and participate", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Clear focus", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Child in home setting
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Child in home setting";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Hearing my parents argue", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fight with my brother and sister", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Told I am grounded", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Teased by my siblings", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yelling from anyone", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "People argue with each other", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People taking my things without asking", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "My sister or brother are breaking the house rules", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I Get in trouble", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Homework", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Being told 'No'", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People not at code blue", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Eating a dinner I don't like", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Mum gives me lots of jobs", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "My sister saying she is 'better than me'", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Doing jobs I see no value in", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "My brother is changes the channel on TV", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People being rude to each other", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Noise when I want to sleep", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Siblings are laughing and I don't know why", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Playing in the back yard with my siblings at code blue", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Family walks", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Family dinners with all family together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Playing with the neighbours", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Playing together with friends and family", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Sobbing", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lock myself in my bedroom", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Yell at my parents", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Run away from other people", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fight back with other people", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Clench my fists", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tighten my jaw and bite my teeth", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lots of butterflies in my stomach and feel sick", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel sad", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Refuse to do homework", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Louder voice volume", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel irritated", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ask 'why' all the time", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Comment back to my sister", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fuzzy and unclear thoughts", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice tone changes (not my voice)", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fingers stiffen", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "'I don't want to'", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Muscles tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Faster breathing", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Muscles are soft like jelly", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Gentle breathing", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Using my 'normal' voice", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Hands relaxed", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Playing joyfully", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Parent in home setting
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Parent in home setting";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Kids say I am a bad mum", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids laugh at me when I am angry", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like I am the worst parent in the world", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids refuse to help", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Really loud music", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Constant requests of me", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Last minute homework due", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Other people break rules", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being told 'no' by my kids", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Make a mistake", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Loud noises I can't stop", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids make rude comments", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids disrespect others", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not having all the ingredients I need for cooking", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not knowing how to do something", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Kids run crazy through the house", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids don't listen to me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Rainy days when the kids are at home", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Doing something unfamiliar", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Kids laugh AT me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Free time to read my books", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Watching movies all together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Go for walks", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fun family time", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Everybody working together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Yell and scream at kids", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like running away from home", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ground my kids harshly", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Dramatic reaction to kids' behaviour", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Threaten to never do anything for the kids again", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Tighten my jaw and bite my teeth", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tell myself 'I am not a good parent'", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like crying", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Thoughts about leaving the house", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Head is fuzzy and thoughts are unclear", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Heavy voice tone", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Body tightens and hardens", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Easily agitated by the children", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feel like I am not enjoying being wherever I am", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice tone is strained", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breathing fastens", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ignore child calling my name", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shoulders tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Expand and tighten fingers", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Enjoy talking", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lightness in my body", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Clarity in my head", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Body is relaxed and soft", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Enjoy being with family", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Person with intellectual impairment
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Person with Intellectual Impairment";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "People take my belongings", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Having to shower and I get confused", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being told 'No'", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Having to go to my room", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "House mates attacking me", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Not getting the dinner I like", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Going to noisy places", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Other house mates barging in on me", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Told I'm wrong", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Interrupted when I am talking", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Other house mates screaming", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not being included", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People talk about me", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Loud noises I can't stop", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't know how to do something", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Wear clothes I don't like", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People not listening to me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Rainy days and I can't go to the park", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Going somewhere new", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lots of activity - too much", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Going on excursions", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Break times", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I have something to do", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "I feel included", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Carers listen to me", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Bang head against the wall", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Throw or push furniture", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Attack others", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Poke myself in the eye", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Scratch my own arms and skin", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Yell", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Swear", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Refuse to eat dinner", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Pinch shirt", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Bite own hand", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Increase the volume of my voice", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Jump up and down", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Glazed look", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ask 'what's wrong with me?'", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Ignore carers", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Eyes down", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Frowning", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shallow breathing", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Repetitively asking questions like - 'Who? What? When? Where? Why?'", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Sometimes smiley or laughing", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relaxed and soft muscles", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Using my 'normal' voice", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to listen to others", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Agree with what others say", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Boss in a workplace
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Boss in a workplace";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Staff not taking responsibility for themselves", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People imposing negatively on each other", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People think they are superior to others", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Refusal to complete task", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Staff not doing their duties", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Staff being rude to clients", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People break rules", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People don't listen to each other", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People don't owning up to their mistakes", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Blame each other", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Customers unhappy with service", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Staff talk about other people negatively", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "People say they will do something and then do not do it", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Feel too busy", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Complaints", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Misinterpretations", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Constant interruptions", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not getting what was needed as interrupted", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Working on tasks I enjoy doing", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Workplace is running smoothly", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Getting jobs done", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Staff all working together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Each person taking responsibility of themselves", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Ready to sack staff", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Angry and yelling at people", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Withdrawal from people", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Body feels agitated", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Body feels hard", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Avoid eye contact", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Difficult to concentrate", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Eye strain obvious", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Start withdrawing", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fidgeting and difficult to be still", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Takes longer than normal to do tasks", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Difficult to focus on tasks", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Thoughts are not clear", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice tone changes (not my normal voice)", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shoulders tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Focus starts to blur", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Jaw tightens a little bit", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breathing shallow and rapid", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "I feel comfortable to express", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relaxed and soft muscles", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Using my 'normal' voice", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to listen and participate", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Clear focus", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                        #region Employee in a workplace
                        scale = new BehaviourScale();
                        scale.Archived = false;
                        scale.Name = "Employee in a workplace";
                        scale.FabicExample = true;
                        _DBConnection.Insert(scale);

                        // Life
                        item = new BehaviourScaleItem(scale.Id, "Being told I'm wrong in front of colleagues", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Don't agree with my boss", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Have to do something I don't like", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Always working overtime", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Customers complaints", 5, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Feeling like I'm left to do everything", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Other people break rules", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Being told I'm wrong", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Make a mistake", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Feeling like I'm the only person contributing", 4, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "People talking badly about my colleagues", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "At work when I know my kids are sick", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Changes in my workplace", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Not knowing how to do something", 3, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Having to wear uniforms", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Other staff don't listen to me", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Really slow days", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Doing something unfamiliar", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Lots of new things to learn", 2, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Doing tasks I enjoy", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Getting on with my colleagues", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Everyone working together", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Everyone contributing equally", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "We are all having fun", 1, Enumerations.BehaviourScaleItemType.Life); _DBConnection.Insert(item);

                        //Body
                        item = new BehaviourScaleItem(scale.Id, "Think about resigning", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Taking days off with no true reason", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Verbally attacking other staff", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Think everyone is against me", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Severe headaches", 5, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Clenched fists", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to be away from other people", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Avoid eye contact", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Eat food I know is bad for me", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Sick feeling in my stomach", 4, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Body feels agitated", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fidgety and difficult to be still", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Misinterpret what others are saying", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Easily jump to conclusions", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Fuzzy unclear thoughts", 3, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Voice starts to become louder", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Shoulders tighten", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Voice feels strained", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Tight arm muscles", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Breathing fastens", 2, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);

                        item = new BehaviourScaleItem(scale.Id, "Enjoy talking freely", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Relaxed and soft muscles", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Using my 'normal' voice", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Want to listen and participate", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        item = new BehaviourScaleItem(scale.Id, "Clear focus in my head", 1, Enumerations.BehaviourScaleItemType.Body); _DBConnection.Insert(item);
                        #endregion
                    }
                }
                #endregion
                #region I Choose Chart and Items
                {
                    {
                        // Initialise the I Choose Chart Parents and Items
                        IChooseChart chart = new IChooseChart();
                        IChooseChartItem item; //= new TheIChooseChartItem(chart.Id, "Loving and Self-Supportive" + Environment.NewLine + "a) Eat food that feels good in my mouth and body" + Environment.NewLine + "b) Drink fluids that support my body" + Environment.NewLine + "c) Rest and sleep when my body naturally says it is tired" + Environment.NewLine + "d) Consume only substances that support my body" + Environment.NewLine + "e) Do activity that is gentle and supportive to my body" + Environment.NewLine + "f) Nurture myself the way I would nurture a baby", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        ItemHighlight itemHighlight;
                        #region Taking Responsibility vs. Blaming Another
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "Taking responsibility vs. blame and victim";
                        chart.FabicExample = true;
                        chart.Description1 = chart.Description2 = "";
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Know that I am responsible for all my own behaviours, choices and all my outcomes in life", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Self-Statements:" + Environment.NewLine + "✔ 'It is for other people to choose how they behave, they make their own choices in life. If other people don't like me or something I have done, that is OK … that is their choice.'" + Environment.NewLine + "✔ 'I only do what feels true for me to do.'" + Environment.NewLine + "✔ 'I embrace learning how to master this part of life.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✔", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'It is for other people to choose how they behave, they make their own choices in life. If other people don't like me or something I have done, that is OK … that is their choice.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I only do what feels true for me to do.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I embrace learning how to master this part of life.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Statements bases on:" + Environment.NewLine + "✔ 'I take responsibility for all my behaviours and all my outcomes in life.'" + Environment.NewLine + "✔ 'Allowing others to take responsibility for their behaviours and their life outcomes.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✔", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("I take responsibility for all my behaviours and all my outcomes in life.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'Allowing others to take responsibility for their behaviours and their life outcomes.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I know I am responsible and I do not give my power away to anyone else", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Blame other people, events or things for my life circumstances", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Self-Statements:" + Environment.NewLine + " 'I want other people to think differently, act differently or be different.Others should be the way I want them to be.'" + Environment.NewLine + "✘ 'I do what others expect of me, even if it does not feel true to me.'" + Environment.NewLine + "✘ 'I don't know how to…' or 'I don't want to'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✘", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I want other people to think differently, act differently or be different. Others should be the way I want them to be.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I do what others expect of me, even if it does not feel true to me.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I don't know how to…'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I don't want to'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Statements bases on:" + Environment.NewLine + "✘ 'Holding other people's behaviours responsible for my emotional state and outcomes.'" + Environment.NewLine + "✘ 'Me trying to take responsibility for other people's behaviours and their life outcomes.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✘", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Holding other people's behaviours responsible for my emotional state and outcomes.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'Me trying to take responsibility for other people's behaviours and their life outcomes.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I give my power away to other people", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "I choose to feel:" + Environment.NewLine + "✔ Powerful" + Environment.NewLine + "✔ Responsible for influencing my experience of life" + Environment.NewLine + "✔ There is a way to embrace life and all that life offers", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✔", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I create a safe and supportive environment for myself to live my potential", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "I choose to feel:" + Environment.NewLine + "✘ Helpless" + Environment.NewLine + "✘ Like a victim of life" + Environment.NewLine + "✘ That I have minimal control over myself and life", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✘", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I create an unsafe and unsupportive environment for myself, I bury my issues and give up on my potential", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When I Am Higher Than Code Blue
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I am higher than Code Blue";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Listen to my body and identify when it tells me it is no longer at Code Blue", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Listen", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Identify what part of life I perceive as not yet being completely equipped to respond to (ask for support if needed)", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Identify", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Identify what is needed to change my experience of life and seek support as needed", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("support", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Identify", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Use new strategies and skills to respond to the part of life I perceive as challenging", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("respond", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "Ignore my body when it tells me that I am higher than Code Blue", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Ignore my body", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Deny that there is a part of life that I perceive as challenging", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Deny", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Ignore what is needed to change my experience of life and refuse to seek support", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Ignore", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("refuse", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Do not change the way I react when my challenging situation is presented", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("react", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "My experience with the challenging situation changes", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I have a higher level of perceived control over my challenging situation", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I feel equipped to manage life", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "My experience with the challenging situation does not change", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I feel like I have minimal control over my challenging situation", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I feel helpless in life", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When I Make A Mistake
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I make a mistake";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "I acknowledge that I made a mistake" + Environment.NewLine + " - 'Yes, I made a mistake.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Yes, I made a mistake", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "And I say:" + Environment.NewLine + " - 'Whoops … what can I learn here?'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Whoops … what can I learn here?", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I listen with my ears, look with my eyes, connect to myself and … learn some new skills" + Environment.NewLine + Environment.NewLine + " And then...", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I practise", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I made more mistakes", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I keep on practising", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Deny I made a mistake", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Pretend no mistake was made", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Blame someone else for the mistake", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "I do learn from this mistake", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I stop making the same mistake when life presents this same lesson", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "People do not trust my apology", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Other people go closer to Code Red and do not enjoy being with me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I am less likely to enjoy being with me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do not learn from my mistake", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When I Spend Time With Other People
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I spend time with other people (50:50)";
                        chart.FabicExample = true;
                        chart.Description1 = "50:50" + Environment.NewLine + "Equal contribution" + Environment.NewLine + "Taking Turns:";
                        chart.Description2 = "Not 50:50" + Environment.NewLine + "Unequal contribution" + Environment.NewLine + "Dominating:";
                        itemHighlight = new ItemHighlight("Taking Turns:", Data.Enums.FabicColour.Black, false, true, false, 1); itemHighlight.IChooseChart = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Equal contribution", Data.Enums.FabicColour.Blue, false, false, true, 1); itemHighlight.IChooseChart = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("50:50", Data.Enums.FabicColour.Blue, false, false, true, 1); itemHighlight.IChooseChart = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Not 50:50", Data.Enums.FabicColour.Red, false, false, true, 2); itemHighlight.IChooseChart = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Unequal contribution", Data.Enums.FabicColour.Red, false, false, true, 2); itemHighlight.IChooseChart = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Dominating:", Data.Enums.FabicColour.Black, false, true, false, 2); itemHighlight.IChooseChart = item.Id; _DBConnection.Insert(itemHighlight);
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "50:50 choices of things we do (first you choose, then I choose)", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "50:50 of how many things we get", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Make it equal whenever possible", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "50:50 of time talking (first you talk about your day while I listen, then I talk about my day while you listen)", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "50:50 helping out", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "50:50 paying for things (first you pay, then I pay or we share the cost)", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "I always choose what to do", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I try to get more than or less than another person", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do not make things equal", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I always talk about what I want to talk about without listening to others", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I don't help and let others do everything", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I always get you to pay or I always pay so you like me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        // Outcome
                        item = new IChooseChartItem(chart.Id, "People are more likely to enjoy my behaviour", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are more likely to spend time with me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "People do not enjoy my behaviour", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are less likely to spend time with me, they might even avoid me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When Other People Use Behaviours I Don't Like
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When people use non-preferred behaviours";
                        chart.Description1 = ""; chart.Description2 = "";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Understand there is a reason for the behaviour/s", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Understand", Data.Enums.FabicColour.Blue, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Ask: What is this behaviour communicating to me?", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("communicating", Data.Enums.FabicColour.Blue, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Observe and know there is a lesson on offer", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Observe", Data.Enums.FabicColour.Blue, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Understand these behaviours are about how a person is experiencing life – they are not about me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Understand", Data.Enums.FabicColour.Blue, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Respond to the behaviours by staying closer to code blue and asking, 'What lesson is here to be learnt?'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Respond", Data.Enums.FabicColour.Blue, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "Judge the behaviours to be wrong", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Judge", Data.Enums.FabicColour.Red, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Do not hear what is being communicated, but rather wish the behaviour would stop", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("stop", Data.Enums.FabicColour.Red, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Absorb and take on the other person's issue", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Absorb", Data.Enums.FabicColour.Red, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Personalise these behaviours and make them about me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Personalise", Data.Enums.FabicColour.Red, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "React to the unwanted behaviours by judging them to be wrong", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("React", Data.Enums.FabicColour.Red, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "People around me feel safer to learn from their life lesson", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("code blue", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I choose to be closer to code blue in my own body", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "People around me feel judged and continue to react to their life lesson/s", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I choose to be closer to code red in my own body", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("code red", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        #endregion
                        #region When I find something difficult
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I find something difficult";
                        chart.Description1 = ""; chart.Description2 = "";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Say \"I can\"", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I can", Data.Enums.FabicColour.Blue, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Try another way", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Say \"I can't\"", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I can't", Data.Enums.FabicColour.Red, false, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Give zero effort and give up", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "The problem is no longer a problem for me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "The problem remains a problem for me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When people make judgmental comments about me
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When people make judgmental comments about me";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Statements I say to myself or out loud:" + Environment.NewLine + "a. 'Other people's opinion of me belongs to them. How I feel about me is what is important.'" + Environment.NewLine + "b. 'I am awesome, complete and enough for being me.'" + Environment.NewLine + "c. I don't need everyone to like everything I do.'" + Environment.NewLine + "d. 'I don't need everyone to like who I am. I know I'm amazing just for being me.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("a. 'Other people's opinion of me belongs to them. How I feel about me is what is important.'" + Environment.NewLine + "b. 'I am awesome, complete and enough for being me.'" + Environment.NewLine + "c. I don't need everyone to like everything I do.'" + Environment.NewLine + "d. 'I don't need everyone to like who I am. I know I'm amazing just for being me.'", Data.Enums.FabicColour.Black, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Statements based on:" + Environment.NewLine + "a. Taking responsibility for all my behaviours and how my behaviours may have contributed to these comments" + Environment.NewLine + "b. Allowing others to take responsibility for all their behaviours, opinions and outcomes in life", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("responsibility", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("allowing", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I accept I am responsible for me and my behaviours", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I accept I am responsible for me and my behaviours", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "Statements I say to myself or out loud:" + Environment.NewLine + "a. 'I want to change other people's opinion of me.'" + Environment.NewLine + "b. 'No matter what I do, it is never enough.'" + Environment.NewLine + "c. 'I want people to like everything I do.'" + Environment.NewLine + "d. 'I don't want people to not like who I am.'", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("a. 'I want to change other people's opinion of me.'" + Environment.NewLine + "b. 'No matter what I do, it is never enough.'" + Environment.NewLine + "c. 'I want people to like everything I do.'" + Environment.NewLine + "d. 'I don't want people to not like who I am.'", Data.Enums.FabicColour.Black, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Statements based on:" + Environment.NewLine + "a. Blaming other people and not acknowledging how my behaviours may have contributed to these comments" + Environment.NewLine + "b. Blaming and judging others for all their behaviours, opinions and for my outcomes in life", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Blaming", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("judging", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I play the blame and victim game", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I play the blame and victim game", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "I am responsible for my behaviours", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I am worthy just for being me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I accept who I am just for being me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I create a safe and supportive environment to be Me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I create a safe and supportive environment to be Me", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "I am helpless and that is everyone else's fault", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I am not worthy just for being me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do not like who I am and try to be more", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do not create a safe and supportive environment to be Me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I DO NOT create a safe and supportive environment to be Me", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        #endregion
                        #region When I Think People Are Laughing At Me For Making Silly Mistakes
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I think people are laughing at me";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Keep trying new things knowing that:" + Environment.NewLine + "✔ I won't be perfect at all things I do" + Environment.NewLine + "✔ I will make mistakes" + Environment.NewLine + "✔ I will learn new things" + Environment.NewLine + "✔ I'm just like everyone else and we all make mistakes", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✔", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Say to myself, 'Awesome work for trying, well done'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Awesome work for trying, well done", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Know I'm awesome and amazing just for being me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I accept myself just for being me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Laugh with myself when I do silly things and just say, 'Whoops, what can I learn here?'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Whoops, what can I learn here?", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Accept people for who they are and not what they do", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Avoid doing things where people might judge me as 'silly'", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Say to myself, 'I'm silly, stupid, wrong, bad ...'", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("I'm silly, stupid, wrong, bad ...", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Believe I am lesser than other people for doing things they laugh at", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I judge myself based on what I do", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I get upset that I have done something silly and am embarrassed that people are laughing at me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I try to make people stop laughing at me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "People are more likely to enjoy being with me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I enjoy being with me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "People are less likely to enjoy being with me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I don't enjoy being with me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When other people use behaviours I don't like
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When other people use behaviours I don't like";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Know there is a reason for this behaviour", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Know", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Use my normal speaking voice", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Use words I like to hear to explain what I didn't like", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Allow people to use their behaviours", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Allow", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Move away from them and do another activity", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Touch only my body", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Understand that someone else's behaviour is not about me, even if it is directed at me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Understand", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "Ignore that there is a reason for this behaviour", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Ignore", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Yell at them", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Yell", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Swear at them", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Swear", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Try to make people do what I want them to do", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Stay near them so I am near behaviours I do not like", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Physically touch them (e.g. hit, kick, punch, push)", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Take behaviour personally and think what they are doing is a direct attempt to upset me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "I am not controlled by someone else's behaviour", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I take responsibility for my own behaviours and my own outcomes", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "I allow myself to be controlled by someone else's behaviour", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do not take responsibility for my own behaviours and my own outcomes", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When people say or do something I do not agree with
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When people say or do something I do not agree with";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Say to the person that I do not agree with them", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "State what I do not agree with", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "If the other person gets angry because of my comment, I know:" + Environment.NewLine + "a. That is their challenge, not mine." + Environment.NewLine + "b. That is their choice to react that way.", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("a. That is their challenge, not mine." + Environment.NewLine + "b. That is their choice to react that way.", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Know that the other person is entitled to their opinion and does not need to think the same way I do ", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I know:" + Environment.NewLine + "a. I am responsible for my behaviours and my outcomes" + Environment.NewLine + "Other people are responsible for their behaviours and their outcomes", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("know", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("responsible", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "Agree with the person even though on the inside I don't agree with them", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Say nothing", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Try not to make a person angry/upset or react by not telling them what I don't agree with", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Try to make the other person agree with my opinion so it is the same as mine", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "How I react:" + Environment.NewLine + "a. I blame other people" + Environment.NewLine + "b. I take on responsibility for other people's reactions", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("react", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("blame", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("reactions", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        // Outcome
                        item = new IChooseChartItem(chart.Id, "The challenging situation is released from my body", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are no longer able to get me to agree with things I do not agree with", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People get to know what is true for me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "The challenging situation stays in my body and my body keeps filling up with issues that do not belong to me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I allow people to control me and I do things I do not agree with", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People do not get to know what is true for me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When I Lose
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I Lose";
                        chart.FabicExample = true;
                        chart.Description1 = "Congratulate the winner:";
                        chart.Description2 = "Try to make the winner feel lesser than me:";
                        itemHighlight = new ItemHighlight("Congratulate the winner:", Data.Enums.FabicColour.Blue, false, false, true, 1); itemHighlight.IChooseChart = chart.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Try to make the winner feel lesser than me:", Data.Enums.FabicColour.Red, false, false, true, 2); itemHighlight.IChooseChart = chart.Id; _DBConnection.Insert(itemHighlight);
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Know that being with people is about connecting to people", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Say to myself:" + Environment.NewLine + "'I tried my hardest.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("'I tried my hardest.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Say to the other person: " + Environment.NewLine + "'Well done... congratulations.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("'Well done... congratulations.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Know that I am an equal and that winning or losing does not make one person better or lesser than another", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Use Volume 2 or 3 voice (my normal voice)", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Play again confidently", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Think that being with people is about being better or lesser than them", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Say to myself:" + Environment.NewLine + "'It's not fair'", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("'It's not fair.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Say to the other person:" + Environment.NewLine + "'I hate playing with you.'" + Environment.NewLine + "'I'm never playing again.'" + Environment.NewLine + "'This is dumb.'" + Environment.NewLine + "'You cheated.'", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("'I hate playing with you.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'I'm never playing again.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'This is dumb.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'You cheated.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Feel that I am lesser than another person because I did not win", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Use voice volume 4 or 5 (yell and scream)", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Sabotage what I was doing", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "Other people are more likely to enjoy playing with me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are more likely to want to play with me again", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do enjoy my time", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Others will not enjoy playing with me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are less likely to want to play me again", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I will not enjoy my time", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When I Win
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I Win";
                        chart.FabicExample = true;
                        chart.Description1 = "Hold the person who lost as equal to me:";
                        chart.Description2 = "Try to make the person who lost feel lesser than me:";
                        itemHighlight = new ItemHighlight("Hold the person who lost as equal to me:", Data.Enums.FabicColour.Blue, false, false, true, 1); itemHighlight.IChooseChart = chart.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Try to make the person who lost feel lesser than me:", Data.Enums.FabicColour.Red, false, false, true, 2); itemHighlight.IChooseChart = chart.Id; _DBConnection.Insert(itemHighlight);
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Know that being with people is about connecting to people", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Say to the other person: " + Environment.NewLine + "✔ 'Well done... I enjoyed playing that game with you.'" + Environment.NewLine + "✔ 'Thank you for playing with me.'", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("'Well done... I enjoyed playing that game with you.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("'Thank you for playing with me.'", Data.Enums.FabicColour.Black, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("✔", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Smile at the person", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Think that being with people is about being better or lesser than them", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Boast about my win:" + Environment.NewLine + "✘ 'I'm so good because I won'" + Environment.NewLine + "✘ Laugh at the person who lost" + Environment.NewLine + "✘ Get overexcited and not consider how the other person may feel", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✘", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Pull an unwelcoming face at the person:" + Environment.NewLine + "✘ Stick tongue out" + Environment.NewLine + "✘ Smirk", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✘", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "The person is more likely to want to play with me again", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Other people will see me as a good sport", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("good sport", Data.Enums.FabicColour.Blue, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I am more likely to have people to play with in the future", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "The person is less likely to want to play with me again", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Other people will see me as a bad sport", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("bad sport", Data.Enums.FabicColour.Red, true, false, false); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I won't have many people to play with in the future", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When other people are talking
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When other people are talking";
                        chart.FabicExample = true;
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "I connect to the person and what they are saying", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I listen with:" + Environment.NewLine + "✔ My eyes" + Environment.NewLine + "✔ My ears" + Environment.NewLine + "✔ My whole body" + Environment.NewLine + "✔ An open heart to what other people are saying", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✔", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I wait until the other person has clearly finished before I begin talking", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "I do not connect to the person or what they are saying", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "I do not pay 100% attention to what other people are saying and I:" + Environment.NewLine + "✘ Look away" + Environment.NewLine + "✘ Stop listening" + Environment.NewLine + "✘ Stop listening" + Environment.NewLine + "✘ Judge what other people are saying", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("✘", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "I start talking while other people are still saying what they want to say", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "People are more likely to listen to what I have to say", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are more likely to enjoy being with me", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "People are less likely to want to listen to what I have to say", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "People are less likely to enjoy being with me", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                        #region When I Am Teaching or Learning New Behaviours
                        chart = new IChooseChart();
                        chart.Archived = false;
                        chart.Name = "When I Am Teaching or Learning New Behaviours";
                        chart.FabicExample = true;
                        chart.Description1 = "Before teaching new replacement behaviours, first understand the reason for unwanted behaviour. Based on this understanding we teach new behaviours using the following guidelines:";
                        chart.Description2 = "Not bringing understanding to the reason/s for the old unwanted behaviour/s means that any strategies are likely to fail:";
                        itemHighlight = new ItemHighlight("understand", Data.Enums.FabicColour.Blue, false, false, true, 1); itemHighlight.IChooseChart = chart.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("reason", Data.Enums.FabicColour.Blue, false, false, true, 1); itemHighlight.IChooseChart = chart.Id; _DBConnection.Insert(itemHighlight);
                        _DBConnection.Insert(chart);

                        // Behaviour
                        item = new IChooseChartItem(chart.Id, "Say Stop and Start" + Environment.NewLine + "Know that new behaviours must be taught and not expected", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Stop", Data.Enums.FabicColour.Blue, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("Start", Data.Enums.FabicColour.Blue, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Use concrete words to ensure all people have exactly the same message", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("concrete words", Data.Enums.FabicColour.Blue, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("exactly", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Teach new skills to respond to life. Teach new behaviorus that support self-mastery over what the individual perceives they are not yet equipped to manage", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("new skills", Data.Enums.FabicColour.Blue, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Teach and celebrate each step" + Environment.NewLine + " i. Teach each new behaviour step-by-step so that the student feels equipped to try it out and practise it" + Environment.NewLine + " ii. Celebrate all steps and attempts, no matter how small", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Teach and celebrate each step", Data.Enums.FabicColour.Blue); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);

                        item = new IChooseChartItem(chart.Id, "Only say No and Stop" + Environment.NewLine + "New behaviours are expected without being taught first", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Stop", Data.Enums.FabicColour.Red, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        itemHighlight = new ItemHighlight("No", Data.Enums.FabicColour.Red, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Use abstract words that are likely to be interpreted differently by different people", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Use abstract words", Data.Enums.FabicColour.Red, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Use strategies that do not equip the person with new skills; do not support self - mastery and only suggest the behaviours you/ we want to be used", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("you/we want", Data.Enums.FabicColour.Red, true); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        item = new IChooseChartItem(chart.Id, "Do not teach and celebrate each step" + Environment.NewLine + " i. Teach new behaviours in steps that the person perceives are too big to achieve" + Environment.NewLine + " ii. Emphasise how far the person is from achieving the goal rather than appreciating the progress that has already been made", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Behaviour); _DBConnection.Insert(item);
                        itemHighlight = new ItemHighlight("Do not teach and celerate each step", Data.Enums.FabicColour.Red); itemHighlight.IChooseChartItem = item.Id; _DBConnection.Insert(itemHighlight);
                        // Outcome
                        item = new IChooseChartItem(chart.Id, "Use of old unwanted/non-preferred behaviours will decrease", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Use of new wanted/preferred behaviours will increase", Enumerations.IChooseChartOption.Option1, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);

                        item = new IChooseChartItem(chart.Id, "Use of old unwanted/non-preferred behaviours continues and will remain high", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        item = new IChooseChartItem(chart.Id, "Use of new wanted/preferred behaviours will remain low", Enumerations.IChooseChartOption.Option2, Enumerations.IChooseChartItemType.Outcome); _DBConnection.Insert(item);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return false;
            }
            #endregion
            if (withSync)
                await SyncTablesAsync();
            Initialised = true;
            return true;
        }

        public static async Task SyncTablesAsync()
        {
            try
            {
                SyncData syncData = new SyncData();
                syncData.UserID = SecurityController.CurrentUser.UserID;
                syncData.Charts = _DBConnection.Table<IChooseChart>().Where(x => !x.FabicExample).ToList();
                var chartIds = syncData.Charts.Select(y => y.Id).ToList();
                syncData.ChartItems = _DBConnection.Table<IChooseChartItem>().Where(x => chartIds.Contains(x.IChooseChart)).ToList();
                syncData.Scales = _DBConnection.Table<BehaviourScale>().Where(x => !x.FabicExample).ToList();
                var scaleIds = syncData.Scales.Select(y => y.Id).ToList();
                syncData.ScaleItems = _DBConnection.Table<BehaviourScaleItem>().Where(x => scaleIds.Contains(x.BehaviourScale)).ToList();
                

                var client = new RestClient(MOBILE_APP_URL + "/api/management");
                var request = new RestRequest();
                request.AddJsonBody(syncData);
                request.AddHeader("access_token", SecurityController.AccessToken);
                request.AddHeader("userId", SecurityController.CurrentUser.UserID);
                request.AddHeader("ZUMO-API-VERSION", "2.0.0");
                var response = await client.PostAsync(request);

                var r = response.Content.ToString().Replace("\\\"", "\"");

                if (!string.IsNullOrEmpty(r) && r.Length > 2)
                { 
                    r = r.Substring(1, r.Length - 2);

                    dynamic json = JsonConvert.DeserializeObject(r);

                    if (json != null)
                    {
                        if (json.Charts != null)
                        {
                            foreach (var chartJson in json.Charts)
                            {
                                IChooseChart chart = new IChooseChart();
                                chart.Id = chartJson.Id;
                                chart.Archived = chartJson.Archived;
                                chart.CreatedAt = chartJson.CreatedAt;
                                chart.Description1 = chartJson.Description1;
                                chart.Description2 = chartJson.Description2;
                                chart.FabicExample = chartJson.FabicExample;
                                chart.Name = chartJson.Name;
                                chart.UserID = chartJson.UserID;
                                chart.UpdatedAt = chartJson.UpdatedAt;
                                SaveOrUpdateIChooseChart(chart);
                            }
                        }
                        if (json.ChartItems != null)
                        {
                            foreach (var chartJson in json.ChartItems)
                            {
                                IChooseChartItem chart = new IChooseChartItem();
                                chart.Id = chartJson.Id;
                                chart.Archived = chartJson.Archived;
                                chart.CreatedAt = chartJson.CreatedAt;
                                chart.UserID = chartJson.UserID;
                                chart.UpdatedAt = chartJson.UpdatedAt;
                                chart.ChartOption = chartJson.ChartOption;
                                chart.ChartType = chartJson.ChartType;
                                chart.IChooseChart = chartJson.IChooseChart;
                                chart.ItemText = chartJson.ItemText;
                                SaveOrUpdateIChooseChartItem(chart);
                            }
                        }
                        if (json.Scales != null)
                        {
                            foreach (var chartJson in json.Scales)
                            {
                                BehaviourScale chart = new BehaviourScale();
                                chart.Id = chartJson.Id;
                                chart.Archived = chartJson.Archived;
                                chart.CreatedAt = chartJson.CreatedAt;
                                chart.FabicExample = chartJson.FabicExample;
                                chart.Name = chartJson.Name;
                                chart.UserID = chartJson.UserID;
                                chart.UpdatedAt = chartJson.UpdatedAt;
                                SaveOrUpdateBehaviourScale(chart);
                            }
                        }
                        if (json.ScaleItems != null)
                        {
                            foreach (var chartJson in json.ScaleItems)
                            {
                                BehaviourScaleItem chart = new BehaviourScaleItem();
                                chart.Id = chartJson.Id;
                                chart.Archived = chartJson.Archived;
                                chart.CreatedAt = chartJson.CreatedAt;
                                chart.UserID = chartJson.UserID;
                                chart.UpdatedAt = chartJson.UpdatedAt;
                                chart.BehaviourScale = chartJson.BehaviourScale;
                                chart.BehaviourScaleLevel = chartJson.BehaviourScaleLevel;
                                chart.BehaviourScaleType = chartJson.BehaviourScaleType;
                                chart.Name = chartJson.Name;
                                SaveOrUpdateBehaviourScaleItem(chart);
                            }
                        }
                    }
                }

                if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
                {
                    //try
                    //{
                    //    if (_Client.SyncContext.PendingOperations > 0)
                    //        Task.Run(() => Core.Controllers.FabicDatabaseController._Client.SyncContext.PushAsync()).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                    //try
                    //{
                    //    IMobileServiceSyncTable<BehaviourScale> bsTable = _Client.GetSyncTable<BehaviourScale>();
                    //    Task.Run(() => bsTable.PullAsync("bs", bsTable.Where(x => x.FabicExample == false))).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                    //try
                    //{
                    //    IMobileServiceSyncTable<BehaviourScaleItem> bsiTable = _Client.GetSyncTable<BehaviourScaleItem>();
                    //    Task.Run(() => bsiTable.PullAsync("bsi", bsiTable.Where(x => x.Id != string.Empty))).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                    //try
                    //{
                    //    IMobileServiceSyncTable<IChooseChart> iChooseTable = _Client.GetSyncTable<IChooseChart>();
                    //    Task.Run(() => iChooseTable.PullAsync("icc", iChooseTable.Where(x => x.FabicExample == false))).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                    //try
                    //{
                    //    IMobileServiceSyncTable<IChooseChartItem> iChooseiTable = _Client.GetSyncTable<IChooseChartItem>();
                    //    Task.Run(() => iChooseiTable.PullAsync("icci", iChooseiTable.Where(x => x.Id != string.Empty))).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                    //try
                    //{
                    //    IMobileServiceSyncTable<AboutFabicVideo> videoTable = _Client.GetSyncTable<AboutFabicVideo>();
                    //    Task.Run(() => videoTable.PullAsync("fabicvideo", videoTable.Where(x => x.Id != string.Empty))).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                    //try
                    //{
                    //    IMobileServiceSyncTable<AboutBLSVideo> videoTable = _Client.GetSyncTable<AboutBLSVideo>();
                    //    Task.Run(() => videoTable.PullAsync("blsvideo", videoTable.Where(x => x.Id != string.Empty))).Wait();
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.HandleBLSException();
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }

        private async static void Reachability_ReachabilityChanged(object sender, EventArgs e)
        {
            if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
            {
                try
                {
                    // first update the tokens
                    if (Fabic.iOS.Controllers.SecurityController.AccessTokenExpiry < DateTime.Now)
                    {
                        await Fabic.iOS.Controllers.SecurityController.RefreshAccessTokenAsync();
                    }

                    if (!Initialised)
                    {
                        await SyncTablesAsync();
                    }
                }
                catch (Exception ex)
                {
                    ex.HandleBLSException();
                }
            }
        }

        //private static async Task AzureLoginAsync()
        //{
        //    try
        //    {
        //        if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
        //        {
        //            var zumoPayload = new JObject();
        //            zumoPayload["access_token"] = Fabic.iOS.Controllers.SecurityController.AccessToken;
        //            if (Fabic.iOS.Controllers.SecurityController.AccessTokenExpiry < DateTime.Now)
        //                await Fabic.iOS.Controllers.SecurityController.RefreshAccessTokenAsync();
        //            Task.Run(() => _User = _Client.InvokeApiAsync(.LoginAsync("auth0", zumoPayload).Result).Wait();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await Fabic.iOS.Controllers.SecurityController.RefreshAccessTokenAsync();
        //    }
        //}

        #region Behaviour Scale
        /// <summary>
        /// Fetches the fabic behaviour scale templates.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>The fabic behaviour scale templates.</returns>
        public static List<BehaviourScale> FetchFabicBehaviourScaleTemplates()
        {
            try
            {
                List<BehaviourScale> items = new List<BehaviourScale>();
                items = _DBConnection.Table<BehaviourScale>().Where(x => x.FabicExample == true).ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Fetches the archived behaviour scales.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>All the archived behaviour scales.</returns>
        public static List<BehaviourScale> FetchArchivedBehaviourScales()
        {
            try
            {
                List<BehaviourScale> items = new List<BehaviourScale>();
                items = _DBConnection.Table<BehaviourScale>().Where(x => x.FabicExample == false && x.Archived == true && x.UserID == Fabic.iOS.Controllers.SecurityController.CurrentUser.UserID).ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Fetches the active behaviour scales.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>All the active behaviour scales.</returns>
        public static List<BehaviourScale> FetchActiveBehaviourScales()
        {
            try
            {
                List<BehaviourScale> items = new List<BehaviourScale>();
                items = _DBConnection.Table<BehaviourScale>().Where(x => x.FabicExample == false && x.Archived == false && x.UserID == Fabic.iOS.Controllers.SecurityController.CurrentUser.UserID).ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Fetches the active behaviour scale items for a behaviour scale.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>All the active behaviour scale items for a behaviour scale.</returns>
        public static List<BehaviourScaleItem> FetchActiveBehaviourScalesItems(BehaviourScale scale)
        {
            try
            {
                List<BehaviourScaleItem> items = new List<BehaviourScaleItem>();
                items = _DBConnection.Table<BehaviourScaleItem>().Where(x => x.BehaviourScale == scale.Id && x.Archived == false).ToList();
                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Updates/Inserts a behaviour scale item.
        /// </summary>
        /// <param name="_ClientPath">_Client path.</param>
        /// <param name="item">Item.</param>
        public static void SaveOrUpdateBehaviourScaleItem(BehaviourScaleItem item)
        {
            try
            {
                item.UserID = Fabic.iOS.Controllers.SecurityController.CurrentUser.UserID;
                item.UpdatedAt = DateTime.Now;
                var list = _DBConnection.Table<BehaviourScaleItem>().ToList();
                var count = 0;
                foreach ( var item2 in list)
                {
                    if (item2.Id == item.Id)
                    {
                        count++;
                    }
                }

                if (count <= 0 || string.IsNullOrWhiteSpace(item.Id))
                {
                    _DBConnection.Insert(item);
                }
                else
                {
                    _DBConnection.Update(item);
                }
                //if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
                //    Task.Run(() => { _Client.SyncContext.PushAsync(); });
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }

        /// <summary>
        /// Deletes a behaviour scale item.
        /// </summary>
        /// <param name="_ClientPath">_Client path.</param>
        /// <param name="item">Item.</param>
        public static void DeleteBehaviourScaleItem(BehaviourScaleItem item)
        {
            try
            {
                //IMobileServiceSyncTable<BehaviourScaleItem> table = _Client.GetSyncTable<BehaviourScaleItem>();
                //await _Client.SyncContext.PushAsync();
                _DBConnection.Delete(item);
                //if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
                //    Task.Run(() => { _Client.SyncContext.PushAsync(); });
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }

        /// <summary>
        /// Updates/Inserts a behaviour scale.
        /// </summary>
        /// <param name="_ClientPath">_Client path.</param>
        /// <param name="item">Item.</param>
        public static void SaveOrUpdateBehaviourScale(BehaviourScale item, bool background = true, bool throwException = false)
        {
            try
            {
                //IMobileServiceSyncTable<BehaviourScale> table = _Client.GetSyncTable<BehaviourScale>();

                item.UserID = Fabic.iOS.Controllers.SecurityController.CurrentUser.UserID;
                item.UpdatedAt = DateTime.Now;

                var list = _DBConnection.Table<BehaviourScale>().ToList();
                var count = 0;
                foreach (var item2 in list)
                {
                    if (item2.Id == item.Id)
                    {
                        count++;
                    }
                }

                if (count <= 0 || string.IsNullOrWhiteSpace(item.Id))
                {
                    _DBConnection.Insert(item);
                }
                else
                {
                    _DBConnection.Update(item);
                }
                //if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
                //{
                //    if (background)
                //        Task.Run(() => { _Client.SyncContext.PushAsync(); });
                //    else
                //        Task.Run(() => { _Client.SyncContext.PushAsync(); }).Wait();
                //}
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                if (throwException)
                    throw ex;
            }
        }

        #endregion
        #region Item Highlight
        /// <summary>
        /// Fetches the item highlights related to an I Choose Chart
        /// </summary>
        /// <returns></returns>
        public async static Task<List<ItemHighlight>> FetchItemHighlightsForIChooseChart(string iChooseChartId, int itemType)
        {
            try
            {
                List<ItemHighlight> items = new List<ItemHighlight>();
                items = _DBConnection.Table<ItemHighlight>().Where(x => x.IChooseChart == iChooseChartId && x.ItemType == itemType).ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }
        /// <summary>
        /// Fetches the item highlights related to an I Choose Chart Item
        /// </summary>
        /// <returns></returns>
        public async static Task<List<ItemHighlight>> FetchItemHighlightsForIChooseChartItem(string iChooseChartItemId)
        {
            try
            {
                List<ItemHighlight> items = new List<ItemHighlight>();
                items = _DBConnection.Table<ItemHighlight>().Where(x => x.IChooseChartItem == iChooseChartItemId).ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }
        #endregion
        #region I Choose Chart
        /// <summary>
        /// Fetches the fabic i choose chart templates.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>The fabic i choose chart templates.</returns>
        public async static Task<List<IChooseChart>> FetchFabicIChooseChartsTemplates()
        {
            try
            {
                List<IChooseChart> items = new List<IChooseChart>();
                List<IChooseChart> results = _DBConnection.Table<IChooseChart>().Where(x => x.FabicExample == true).ToList();

                if (results != null)
                {
                    foreach (IChooseChart chart in results)
                    {
                        chart.Keywords1 = await FetchItemHighlightsForIChooseChart(chart.Id, 1);
                        chart.Keywords2 = await FetchItemHighlightsForIChooseChart(chart.Id, 2);
                        items.Add(chart);
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Fetches the active I choose charts.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>All the active behaviour scales.</returns>
        public async static Task<List<IChooseChart>> FetchActiveIChooseCharts()
        {
            try
            {
                List<IChooseChart> items = _DBConnection.Table<IChooseChart>().Where(x => x.FabicExample == false && x.Archived == false && x.UserID == SecurityController.CurrentUser.UserID).ToList();

                if (items != null)
                {
                    foreach (IChooseChart chart in items)
                    {
                        chart.Keywords1 = await FetchItemHighlightsForIChooseChart(chart.Id, 1);
                        chart.Keywords2 = await FetchItemHighlightsForIChooseChart(chart.Id, 2);
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Fetches the archived I Choose Charts.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>All the archived I choose charts.</returns>
        public async static Task<List<IChooseChart>> FetchArchivedIChooseCharts()
        {
            try
            {
                List<IChooseChart> items = _DBConnection.Table<IChooseChart>().Where(x => x.FabicExample == false && x.Archived == true && x.UserID == SecurityController.CurrentUser.UserID).ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Fetches the active chart items for an i choose chart.
        /// </summary>
        /// <param name="_ClientPath">The file path of the database</param>
        /// <returns>All the active chart items for an i choose chart.</returns>
        public async static Task<List<IChooseChartItem>> FetchActiveIChooseChartItems(IChooseChart chart)
        {
            try
            {
                List<IChooseChartItem> items = _DBConnection.Table<IChooseChartItem>().Where(x => x.IChooseChart == chart.Id && x.Archived == false).ToList();

                if (items != null)
                {
                    foreach (IChooseChartItem chartItem in items)
                    {
                        chartItem.Keywords = await FetchItemHighlightsForIChooseChartItem(chartItem.Id);
                    }
                }
                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }

        /// <summary>
        /// Updates/Inserts an i choose chart.
        /// </summary>
        /// <param name="_ClientPath">_Client path.</param>
        /// <param name="item">Item.</param>
        public async static void SaveOrUpdateIChooseChart(IChooseChart item, bool throwException = false)
        {
            try
            {
                item.UserID = SecurityController.CurrentUser.UserID;
                item.UpdatedAt = DateTime.Now;

                var count = _DBConnection.Table<IChooseChart>().Count(x => x.Id == item.Id);

                if (count <= 0 || string.IsNullOrWhiteSpace(item.Id))
                {
                    _DBConnection.Insert(item);
                }
                else
                {
                    _DBConnection.Update(item);
                }
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                if (throwException)
                    throw ex;
            }
        }

        /// <summary>
        /// Updates/Inserts an I Choose Chart item.
        /// </summary>
        /// <param name="_ClientPath">_Client path.</param>
        /// <param name="item">Item.</param>
        public static void SaveOrUpdateIChooseChartItem(IChooseChartItem item)
        {
            try
            {
                item.UserID = SecurityController.CurrentUser.UserID;
                item.UpdatedAt = DateTime.Now;

                var count = _DBConnection.Table<IChooseChartItem>().Count(x => x.Id == item.Id);

                if (count <= 0 || string.IsNullOrWhiteSpace(item.Id))
                {
                    _DBConnection.Insert(item);
                }
                else
                {
                    _DBConnection.Update(item);
                }
                //if (Fabic.iOS.External.Reachability.InternetConnectionStatus() != Fabic.iOS.External.NetworkStatus.NotReachable)
                //    Task.Run(() => { _Client.SyncContext.PushAsync(); });
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }

        /// <summary>
        /// Deletes an I choose chart item.
        /// </summary>
        /// <param name="_ClientPath">_Client path.</param>
        /// <param name="item">Item.</param>
        public static void DeleteIChooseChartItem(IChooseChartItem item)
        {
            try
            {
                _DBConnection.Delete(item);
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
            }
        }
        #endregion
        #region Fabic Video
        /// <summary>
        /// Fetches the videos related to Fabic
        /// </summary>
        /// <returns></returns>
        public async static Task<List<AboutFabicVideo>> FetchAboutFabicVideos()
        {
            try
            {
                List<AboutFabicVideo> items = new List<AboutFabicVideo>();
                items = _DBConnection.Table<AboutFabicVideo>().ToList();

                return items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }
        /// <summary>
        ///  Fetches the videos related to BLS
        /// </summary>
        /// <returns></returns>
        public async static Task<List<FabicVideo>> FetchAboutBLSVideos()
        {
            try
            {
                //IMobileServiceSyncTable<AboutBLSVideo> table = _Client.GetSyncTable<AboutBLSVideo>();
                List<AboutBLSVideo> items = new List<AboutBLSVideo>();
                items = _DBConnection.Table<AboutBLSVideo>().ToList();
                return new List<FabicVideo>(); // items;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return null;
            }
        }
        #endregion
        //public static async Task SyncAsync(bool pullData = false)
        //{
        //    try
        //    {
        //        await client.SyncContext.PushAsync();

        //        if (pullData) {
        //            await todoTable.PullAsync("allTodoItems", todoTable.CreateQuery()); // query ID is used for incremental sync
        //        }
        //    }

        //    catch (MobileServiceInvalidOperationException e)
        //    {
        //        Console.Error.WriteLine(@"Sync Failed: {0}", e.Message);
        //    }
        //}

        //public static async Task<List<ToDoItem>> RefreshDataAsync()
        //{
        //    try
        //    {
        //        // Update the local store
        //        await SyncAsync(pullData: true);

        //        // This code refreshes the entries in the list view by querying the local TodoItems table.
        //        // The query excludes completed TodoItems
        //        Items = await todoTable
        //                .Where(todoItem => todoItem.Complete == false).ToListAsync();

        //    }
        //    catch (MobileServiceInvalidOperationException e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //        return null;
        //    }

        //    return Items;
        //}

        //public static async Task InsertTodoItemAsync(ToDoItem todoItem)
        //{
        //    try
        //    {
        //        await todoTable.InsertAsync(todoItem); // Insert a new TodoItem into the local database.
        //        await SyncAsync(); // Send changes to the mobile app backend.
        //        Items.Add(todoItem);

        //    }
        //    catch (MobileServiceInvalidOperationException e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //}

        //public static async Task CompleteItemAsync(ToDoItem item)
        //{
        //    try
        //    {
        //        item.Complete = true;
        //        await todoTable.UpdateAsync(item); // Update todo item in the local database
        //        await SyncAsync(); // Send changes to the mobile app backend.
        //        Items.Remove(item);

        //    }
        //    catch (MobileServiceInvalidOperationException e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //}
    }

    class SyncHandler : IMobileServiceSyncHandler
    {
        readonly MobileServiceClient client;
        const string LOCAL_VERSION = "Use local version";
        const string SERVER_VERSION = "Use server version";

        public SyncHandler(MobileServiceClient client)
        {
            this.client = client;
        }

        public virtual Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            return Task.FromResult(0);
        }

        public virtual async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            MobileServiceInvalidOperationException error;
            Func<Task<JObject>> tryOperation = operation.ExecuteAsync;

            do
            {
                error = null;

                try
                {
                    JObject result = await operation.ExecuteAsync();
                    return result;
                }
                catch (MobileServiceConflictException ex)
                {
                    error = ex;
                }
                catch (MobileServicePreconditionFailedException ex)
                {
                    error = ex;
                }

                if (error != null)
                {
                    var localItem = operation.Item.ToObject<BehaviourScale>();
                    var serverValue = error.Value;
                    if (serverValue == null) // 409 doesn't return the server item
                    {
                        serverValue = await operation.Table.LookupAsync(localItem.Id) as JObject;
                    }

                    var serverItem = serverValue.ToObject<BehaviourScale>();

                    //if (serverItem.Complete == localItem.Complete &&
                    //    serverItem.Text == localItem.Text)
                    //{
                    //    // items are same so we can ignore the conflict
                    //    return serverValue;
                    //}

                    int command = await ShowConflictDialog(localItem, serverValue);

                    if (command == 1)
                    {
                        // Overwrite the server version and try the operation again by continuing the loop
                        operation.Item[MobileServiceSystemColumns.Version] = serverValue[MobileServiceSystemColumns.Version];
                        if (error is MobileServiceConflictException) // change operation from Insert to Update
                        {
                            tryOperation = async () => await operation.Table.UpdateAsync(operation.Item) as JObject;
                        }
                        continue;
                    }
                    else if (command == 2)
                    {
                        return (JObject)serverValue;
                    }
                    else
                    {
                        operation.AbortPush();
                    }
                }
            } while (error != null);

            return null;
        }

        private async Task<int> ShowConflictDialog(BehaviourScale localItem, JObject serverValue)
        {
            var dialog = new UIAlertView("Conflict between local and server versions",
                    "How do you want to resolve this conflict?\n\n" + "Local item: \n" + localItem +
                    "\n\nServer item:\n" + serverValue.ToObject<BehaviourScale>(), null, "Cancel", LOCAL_VERSION, SERVER_VERSION);

            var clickTask = new TaskCompletionSource<int>();
            dialog.Clicked += (sender, e) =>
            {
                clickTask.SetResult((int)e.ButtonIndex);
            };

            dialog.Show();

            return await clickTask.Task;
        }
    }

    public class AzureHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken token)
        {
            try
            {
                // Do any pre-request requirements here
                if (!string.IsNullOrWhiteSpace(Fabic.iOS.Controllers.SecurityController.AccessToken))
                {
                    if (Fabic.iOS.Controllers.SecurityController.AccessTokenExpiry < DateTime.Now)
                        Fabic.iOS.Controllers.SecurityController.RefreshAccessToken();

                    message.Headers.Add("access_token", Fabic.iOS.Controllers.SecurityController.AccessToken);
                    if (Fabic.iOS.Controllers.SecurityController.CurrentUser != null)
                        message.Headers.Add("userId", Fabic.iOS.Controllers.SecurityController.CurrentUser.UserID);
                }

                // Request happens here
                HttpResponseMessage response = null;
                Task.Run(() => { response = base.SendAsync(message, token).Result; }).Wait();

                // Do any post-request requirements here
                if (!response.IsSuccessStatusCode)
                    return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                ex.HandleBLSException();
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
        }
    }

    public class SyncData
    {
        public string UserID { get; set; }
        public List<IChooseChart> Charts { get; set; }
        public List<IChooseChartItem> ChartItems { get; set; }
        public List<BehaviourScale> Scales { get; set; }
        public List<BehaviourScaleItem> ScaleItems { get; set; }
    }
}
