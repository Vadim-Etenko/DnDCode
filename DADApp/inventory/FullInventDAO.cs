using DADApp.forms;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DADApp.inventory
{
    class FullInventDAO
    {
        public String profileName;
        public ArrayList invent = new ArrayList();
        public Dictionary<String, int> coins = new Dictionary<String, int>();
        public int strenghtValue = 0;
        public String size = DADConstants.SIZE_NORMAL_DEFAULT;

        public FullInventDAO(ArrayList inventList, Dictionary<String, int> coins, String profileName)
        {
            this.invent = inventList;
            this.coins = coins;
            this.profileName = profileName;
        }

        public FullInventDAO(ArrayList inventList, Dictionary<String, int> coins, int strenght, String size, String profileName)
        {
            this.invent = inventList;
            this.coins = coins;
            this.strenghtValue = strenght;
            this.size = size;
            this.profileName = profileName;
        }
    }
}
