using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace FactionColonies
{
    [StaticConstructorOnStartup]
    internal static class texLoad
    {
        public static readonly Texture2D iconTest100 = ContentFinder<Texture2D>.Get("GUI/100x");
        public static readonly Texture2D questionmark = ContentFinder<Texture2D>.Get("GUI/questionmark");
        public static readonly Texture2D buildingLocked = ContentFinder<Texture2D>.Get("GUI/LockedBuildingSlot");

        //test icons
        public static readonly Texture2D iconHappiness = ContentFinder<Texture2D>.Get("GUI/Happiness");
        public static readonly Texture2D iconLoyalty = ContentFinder<Texture2D>.Get("GUI/Loyalty");
        public static readonly Texture2D iconUnrest = ContentFinder<Texture2D>.Get("GUI/Unrest");
        public static readonly Texture2D iconProsperity = ContentFinder<Texture2D>.Get("GUI/Prosperity");
        public static readonly Texture2D iconMilitary = ContentFinder<Texture2D>.Get("GUI/MilitaryLevel");
        public static readonly Texture2D iconCustomize = ContentFinder<Texture2D>.Get("GUI/customizebutton");


        //UnitCustomization
        public static readonly Texture2D unitCircle = ContentFinder<Texture2D>.Get("GUI/unitCircle");

        //test production icons
        //public static readonly Texture2D iconProdFood = ContentFinder<Texture2D>.Get("GUI/productionfood");
        //public static readonly Texture2D iconProdWeapons = ContentFinder<Texture2D>.Get("GUI/productionweapons");
        //public static readonly Texture2D iconProdApparel = ContentFinder<Texture2D>.Get("GUI/productionapparel");
        //public static readonly Texture2D iconProdAnimals = ContentFinder<Texture2D>.Get("GUI/productionanimals");
        //public static readonly Texture2D iconProdLogging = ContentFinder<Texture2D>.Get("GUI/productionlogging");
        //public static readonly Texture2D iconProdMining = ContentFinder<Texture2D>.Get("GUI/productionmining");



        public static readonly List<KeyValuePair<string, Texture2D>> textures = new List<KeyValuePair<string, Texture2D>>() 
        { 
            new KeyValuePair<string, Texture2D>("food", ContentFinder<Texture2D>.Get("GUI/ProductionFood")), 
            new KeyValuePair<string, Texture2D>("weapons", ContentFinder<Texture2D>.Get("GUI/ProductionWeapons")), 
            new KeyValuePair<string, Texture2D>("apparel", ContentFinder<Texture2D>.Get("GUI/ProductionApparel")), 
            new KeyValuePair<string, Texture2D>("animals", ContentFinder<Texture2D>.Get("GUI/ProductionAnimals")), 
            new KeyValuePair<string, Texture2D>("logging", ContentFinder<Texture2D>.Get("GUI/ProductionLogging")), 
            new KeyValuePair<string, Texture2D>("mining", ContentFinder<Texture2D>.Get("GUI/ProductionMining")),
             new KeyValuePair<string, Texture2D>("medicine", ContentFinder<Texture2D>.Get("GUI/ProductionMedicine")),
              new KeyValuePair<string, Texture2D>("power", ContentFinder<Texture2D>.Get("GUI/ProductionPower")),
               new KeyValuePair<string, Texture2D>("research", ContentFinder<Texture2D>.Get("GUI/ProductionResearch"))
        };

        public static readonly List<KeyValuePair<string, Texture2D>> factionIcons = new List<KeyValuePair<string, Texture2D>>()
        {
            new KeyValuePair<string, Texture2D>("Base", ContentFinder<Texture2D>.Get("FactionIcons/Base")),
            new KeyValuePair<string, Texture2D>("Blue_Phoenix", ContentFinder<Texture2D>.Get("FactionIcons/Blue_Phoenix")),
            new KeyValuePair<string, Texture2D>("Boomalope", ContentFinder<Texture2D>.Get("FactionIcons/Boomalope")),
            new KeyValuePair<string, Texture2D>("Communists", ContentFinder<Texture2D>.Get("FactionIcons/Communists")),
            new KeyValuePair<string, Texture2D>("Dragon", ContentFinder<Texture2D>.Get("FactionIcons/Dragon")),
            new KeyValuePair<string, Texture2D>("Kilroy", ContentFinder<Texture2D>.Get("FactionIcons/Kilroy")),
            new KeyValuePair<string, Texture2D>("New_Star", ContentFinder<Texture2D>.Get("FactionIcons/New_Star")),
            new KeyValuePair<string, Texture2D>("Old_Star", ContentFinder<Texture2D>.Get("FactionIcons/Old_Star")),
            new KeyValuePair<string, Texture2D>("Pattern_1", ContentFinder<Texture2D>.Get("FactionIcons/Pattern_1")),
            new KeyValuePair<string, Texture2D>("Pattern_2", ContentFinder<Texture2D>.Get("FactionIcons/Pattern_2")),
            new KeyValuePair<string, Texture2D>("Pattern_3", ContentFinder<Texture2D>.Get("FactionIcons/Pattern_3")),
            new KeyValuePair<string, Texture2D>("Pattern_4", ContentFinder<Texture2D>.Get("FactionIcons/Pattern_4")),
            new KeyValuePair<string, Texture2D>("Prisoner", ContentFinder<Texture2D>.Get("FactionIcons/Prisoner")),
            new KeyValuePair<string, Texture2D>("Red_Phoenix", ContentFinder<Texture2D>.Get("FactionIcons/Red_Phoenix")),
            new KeyValuePair<string, Texture2D>("Royal_Owl", ContentFinder<Texture2D>.Get("FactionIcons/Royal_Owl")),
            new KeyValuePair<string, Texture2D>("Slightly_Less_Royal_Owl", ContentFinder<Texture2D>.Get("FactionIcons/Slightly_Less_Royal_Owl")),
            new KeyValuePair<string, Texture2D>("Thrumbo", ContentFinder<Texture2D>.Get("FactionIcons/Thrumbo")),
            new KeyValuePair<string, Texture2D>("Wolf", ContentFinder<Texture2D>.Get("FactionIcons/Wolf"))
        };


    }
}
