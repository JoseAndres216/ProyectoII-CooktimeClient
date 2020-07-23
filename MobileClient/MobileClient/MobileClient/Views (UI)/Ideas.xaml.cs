﻿using CookTime.Views__UI_;
using MobileClient.Model__Logic_;
using MobileClient.ViewModel__Abstract_UI_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Views__UI_
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ideas : ContentPage
    {
        MyRecipesRecomendationsVM MRRVM;
        MyUsersRecomendationsVM MURVM;
        MyEnterprisesRecomendationsVM MERVM;
        public Ideas()
        {
            InitializeComponent();
            MRRVM = new MyRecipesRecomendationsVM();
            MURVM = new MyUsersRecomendationsVM();
            MERVM = new MyEnterprisesRecomendationsVM();
            RecommendationList.ItemsSource = MRRVM.getMyrecipesRecomendationsIL();
        }

        private void RecommendationList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var details = e.Item as Recipe;
            this.Navigation.PushModalAsync(new PostInfo(details.name, details.author, details.type, details.servings, details.duration.ToString(),
                details.timing, details.difficulty.ToString(), details.tags, details.ingredients, details.steps, details.price, details.image));
        }

        private void recipesButton_Clicked(object sender, EventArgs e)
        {
            recipesButton.IsEnabled = false;
            usersButton.IsEnabled = true;
            companiesButton.IsEnabled = true;
            RecommendationList.ItemsSource = MRRVM.getMyrecipesRecomendationsIL();
        }

        private void usersButton_Clicked(object sender, EventArgs e)
        {
            recipesButton.IsEnabled = true;
            usersButton.IsEnabled = false;
            companiesButton.IsEnabled = true;
            RecommendationList.ItemsSource = MURVM.getUserMyrecomendationsIL();
        }

        private void companiesButton_Clicked(object sender, EventArgs e)
        {
            recipesButton.IsEnabled = true;
            usersButton.IsEnabled = true;
            companiesButton.IsEnabled = false;
            RecommendationList.ItemsSource = MERVM.getMyenterpriseRecomendationsIL();
        }

        private void btnSearch_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new SearchPage());
        }
    }
}