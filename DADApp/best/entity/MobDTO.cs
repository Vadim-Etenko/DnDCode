using System;
using System.Collections.Generic;

namespace DADApp.best.entity
{
    class MobDTO
    {
        public String name;
        public String race;
        public String ideology;
        public int hazard_Lvl;
        public int armor;
        public int hit_points;
        public int speed;
        public int skill_bonus;
        public int strength;
        public int agility;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public int passive_attention;
        public List<String> saving_throws;
        public Dictionary<String, String> ability;
        public List<String> languages;
        public String description;
        public Dictionary<String, String> actions;

    }
}
