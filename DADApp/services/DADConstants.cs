using System;
using System.Collections.Generic;
using System.Drawing;

namespace DADApp.forms
{
    class DADConstants
    {
        public const double ONE_COIN_WEIGHT_FUNT = 0.02;
        public const double ONE_COIN_WEIGHT_KG = 0.009;
        public const double CONVERT_WEIGHT_VAR = 0.453592374536;

        public const int NUMBER_ROW_NAME = 0;
        public const int NUMBER_ROW_COUNT = 1;
        public const int NUMBER_ROW_WEIGHT_FUNT = 2;
        public const int NUMBER_ROW_WEIGHT_KILO = 3;
        public const int NUMBER_ROW_CATEGORY = 4;
        public const int NUMBER_ROW_TOTAL_WEIGHT = 5;
        public const int NUMBER_ROW_DISCRIPTION = 6;

        public const String XML_FILE_INVENT = "invent.xml";
        public const String TXT_FILE_VERSION = "version.txt";
        public const String DADAPP_EXE = "DADApp.exe";
        public const String FILE_CONFIG_NAME = "config";
        public const String URL_TO_EXE= "https://github.com/Vadim-Etenko/DADApp/blob/main/DADApp.exe?raw=true";
        public const String URL_TO_VERSTION = "https://raw.githubusercontent.com/Vadim-Etenko/DADApp/main/version.txt";
        public const String URL_TO_UPDATER = "https://github.com/Vadim-Etenko/DADApp/raw/main/Updater_v_0.1.exe";
        public const String EXE_FILE_UPDATER  = "Updater_v_0.1.exe";
        
        public const String NAME_ATTR = "Name";
        public const String COUNT_ATTR = "Count";
        public const String WEIGHT_ATTR = "WeightOne";
        public const String CATEGORY_ATTR = "Category";
        public const String DISKRIPTION_ATTR = "Discription";
        public const String INVENTORY_ROOT_ATTR = "InventoryObj";
        public const String INVENTORY_ROOT_ROOT_ATTR = "Inventory";
        public const String INVENTORY_ROOT_PROFILE_ATTR = "Profile";
        public const String INVENTORY_ROOT_PROFILE_NAME_ATTR = "ProfileName";

        public const String SIZE_NORMAL_DEFAULT = "Средний";


        public const String COINS_ROOT = "Coins";
       
        public const String PLATINA_ATTR = "Platina";
        public const String GOLD_ATTR = "Gold";
        public const String ELECTRUM_ATTR = "Electrum";
        public const String SILVER_ATTR = "Silver";
        public const String COPPER_ATTR = "Сopper";

        public const String COINS_CHECK_BOX = "enable_coins";
        public const String KG_CHECK_BOX = "enable_kg";
        public const String WEIGHT_HARD_CHECK_BOX = "enable_hard_wight";
        public const String IRON_BACK_CHECK_BOX = "enable_iron_back";

        public const String CATEGORY_BAUBLE = "Безделушка";


        public const String WEIGHT_ROOT = "Weight";

        public const String STRENGHT_ATTR = "Strenght";
        public const String SIZE_ATTR = "Size";

        public const String BIG_SIZE = "Большой+";
        public const String MEDIUM_SIZE = "Средний";
        public const String SMALL_SIZE = "Маленький-";

        public static Dictionary<int, string> emptyVar = new Dictionary<int, string>()
        {
            [NUMBER_ROW_NAME] = "-",
            [NUMBER_ROW_COUNT] = "0",
            [NUMBER_ROW_WEIGHT_FUNT] = "0",
            [NUMBER_ROW_WEIGHT_KILO] = "0",
            [NUMBER_ROW_CATEGORY] = "Безделушка",
            [NUMBER_ROW_TOTAL_WEIGHT] = "0",
            [NUMBER_ROW_DISCRIPTION] = "-"
        };

        public static Dictionary<string, Color> colorsVar = new Dictionary<string, Color>()
        {
            ["Одежда"] = Color.Plum,
            ["Броня"] = Color.DeepSkyBlue,
            ["Книга"] = Color.SandyBrown,
            ["Рецепт"] = Color.WhiteSmoke,
            ["Инструмент"] = Color.RosyBrown,
            ["Оружие"] = Color.LightGray,
            ["Пища"] = Color.YellowGreen,
            ["Безделушка"] = Color.Gray,
            ["Драгоценность"] = Color.IndianRed,
            ["Зелье"] = Color.DarkOliveGreen,
            ["Артефакт"] = Color.Goldenrod
        };

        public const String NOTHING_EFFECT = "Эффект отсутствует";
        public const String HALF_EFFECT = "Скорость -10";
        public const String FULL_EFFECT = "Скорость -20, помеха в проверках характеристик, \nбросках атаки и спасбросках, использующие Силу, \nЛовкость или Телосложение";
        public const String DEAD_END_EFFECT = "Скорость = 0, помеха в проверках\nхарактеристик, бросках атаки и\nспасбросках, использующие Силу,\nЛовкость или Телосложение";

    }

}
