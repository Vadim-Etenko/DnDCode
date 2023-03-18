using DADApp.forms;
using DADApp.inventory;
using DADApp.inventory.tabTextBox;
using DADApp.services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DADApp
{
    public partial class Инвентарь : Form
    {
        private TabTextBoxForm a;
        private Boolean isChangeConvert = false;
        private Boolean isGenerated = true;
        private Boolean isFardWeight = false;
        private Boolean isIronBack = false;
        private ArrayList inventoryList;
        private String profileName = "Имя";
        private ArrayList coinsList = new ArrayList();
        private ArrayList dataGridMap = new ArrayList();
        private ArrayList helpValuesList = new ArrayList();
        
        public Инвентарь()
        {
            InitializeComponent();
            tabControl1.TabPages.RemoveAt(0);
            Инвентарь_Load();

            CoinsWeightCheckBox.Checked = Boolean.TryParse(ConfigService.getConfig(DADConstants.COINS_CHECK_BOX), out bool isCoins) ? isCoins : false;
            isKGCheckBox.Checked = Boolean.TryParse(ConfigService.getConfig(DADConstants.KG_CHECK_BOX), out bool isKG) ? isKG : false;
            checkBoxWeightType.Checked = Boolean.TryParse(ConfigService.getConfig(DADConstants.WEIGHT_HARD_CHECK_BOX), out bool isFardWeight) ? isFardWeight : false;
            ironBackCheckBox.Checked = Boolean.TryParse(ConfigService.getConfig(DADConstants.IRON_BACK_CHECK_BOX), out bool isIronBack) && isIronBack;

            checkDebuf();
        }

        private void Инвентарь_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Сохранить изменения?",
                "Сохранить",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                createSave();
            }
            ConfigService.setConfig(DADConstants.COINS_CHECK_BOX, CoinsWeightCheckBox.Checked.ToString());
            ConfigService.setConfig(DADConstants.KG_CHECK_BOX, isKGCheckBox.Checked.ToString());
            ConfigService.setConfig(DADConstants.WEIGHT_HARD_CHECK_BOX, checkBoxWeightType.Checked.ToString());
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createSave();
        }

        private void createSave()
        {
            ArrayList inventList = new ArrayList();
            if (tabControl1.SelectedIndex >= 0)
            { 
                coinsList[tabControl1.SelectedIndex] = getCoins();
                helpValuesList[tabControl1.SelectedIndex] = getHelpValues();
                for (int i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    Dictionary<String, String> dictTemp = (Dictionary < String, String >) helpValuesList[i];
                    this.dataGridView1 = (DataGridView)dataGridMap[i];
                    int strenght = int.Parse(dictTemp[DADConstants.STRENGHT_ATTR]);
                    String sizeTemp = dictTemp[DADConstants.SIZE_ATTR];
                    FullInventDAO fullInventDAO = new FullInventDAO(UpdateList(), (Dictionary<String, int>)coinsList[i], strenght, sizeTemp, tabControl1.TabPages[i].Text);
                    inventList.Add(fullInventDAO);
                }
                this.dataGridView1 = (DataGridView)dataGridMap[tabControl1.SelectedIndex];
            }
            XMLService.ParseInventDAOToXML(inventList);
        }

        private void LoadToDataGrid(ArrayList invList, String profileName)
        {
            // создаем новую вкладку и таблицу
            var newTabPage = generateTabPage(profileName);
            var dataGridView1 = generateDataRowTable();
            newTabPage.Controls.Add(dataGridView1);
            dataGridMap.Add(dataGridView1);
            tabControl1.Controls.Add(newTabPage);

            // заполняем таблицу данными
            foreach (InventoryDAO inv in invList)
            {
                var row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[DADConstants.NUMBER_ROW_NAME].Value = inv.Name;
                row.Cells[DADConstants.NUMBER_ROW_COUNT].Value = inv.Count;
                row.Cells[DADConstants.NUMBER_ROW_WEIGHT_FUNT].Value = inv.WeightOne;
                row.Cells[DADConstants.NUMBER_ROW_WEIGHT_KILO].Value = inv.WeightOne * DADConstants.CONVERT_WEIGHT_VAR;

                // проверяем наличие категории в словаре
                var category = DADConstants.colorsVar.ContainsKey(inv.Category) ? inv.Category : "Безделушка";
                row.Cells[DADConstants.NUMBER_ROW_CATEGORY].Value = category;
                row.Cells[DADConstants.NUMBER_ROW_CATEGORY].Style.BackColor = getColorToDropDown(category);
                row.Cells[DADConstants.NUMBER_ROW_CATEGORY].Style.SelectionBackColor = getColorToDropDown(category);

                row.Cells[DADConstants.NUMBER_ROW_TOTAL_WEIGHT].Value = Math.Round(inv.TotalWeight, 2);
                row.Cells[DADConstants.NUMBER_ROW_DISCRIPTION].Value = inv.Discription;
                dataGridView1.Rows.Add(row);
            }

            isGenerated = false;
        }

        public void setCoins()
        {
            Dictionary<String, int> coinL;
            if (coinsList.Count == 0)
            {
                coinL = XMLService.getEmptyCoins();
            } else {
                coinL = (Dictionary<String, int>)coinsList[tabControl1.SelectedIndex];
            }
            
            PlatinaNumerick.Value = coinL[DADConstants.PLATINA_ATTR];
            GoldNumeric.Value = coinL[DADConstants.GOLD_ATTR];
            ElectrumNumeric.Value = coinL[DADConstants.ELECTRUM_ATTR];
            SilverNumeric.Value = coinL[DADConstants.SILVER_ATTR];
            СopperNumeric.Value = coinL[DADConstants.COPPER_ATTR];
        }

        public Dictionary<String, int> getCoins()
        {
            Dictionary<String, int> coinsMap = new Dictionary<string, int>();
            
            coinsMap.Add(DADConstants.PLATINA_ATTR, ((int)PlatinaNumerick.Value));
            coinsMap.Add(DADConstants.GOLD_ATTR, ((int)GoldNumeric.Value));
            coinsMap.Add(DADConstants.ELECTRUM_ATTR, ((int)ElectrumNumeric.Value));
            coinsMap.Add(DADConstants.SILVER_ATTR, ((int)SilverNumeric.Value));
            coinsMap.Add(DADConstants.COPPER_ATTR, ((int)СopperNumeric.Value));

            return coinsMap;
        }

        

        public ArrayList UpdateList()
        {
            var listDAO = new ArrayList();

            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                var name = dataGridView1[DADConstants.NUMBER_ROW_NAME, i]?.Value?.ToString();
                var count = int.TryParse(dataGridView1[DADConstants.NUMBER_ROW_COUNT, i]?.Value?.ToString(), out int parsedCount) ? parsedCount : 0;
                var weightOne = double.TryParse(dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_FUNT, i]?.Value?.ToString(), out double parsedWeightOne) ? parsedWeightOne : 0D;
                var category = dataGridView1[DADConstants.NUMBER_ROW_CATEGORY, i]?.Value?.ToString() ?? "";
                var discription = dataGridView1[DADConstants.NUMBER_ROW_DISCRIPTION, i]?.Value?.ToString() ?? "";

                var dao = new InventoryDAO(name, count, weightOne, category, discription);
                listDAO.Add(dao);
            }

            return listDAO;
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == DADConstants.NUMBER_ROW_COUNT || e.ColumnIndex == DADConstants.NUMBER_ROW_WEIGHT_FUNT)
            {
                dataGridView1.Rows[e.RowIndex].ErrorText = "";
                double newDouble;

                if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
                if (!double.TryParse(e.FormattedValue.ToString(),
                    out newDouble) || newDouble < 0)
                { 
                    e.Cancel = true;
                }
            }
        }

        private void Инвентарь_Load()
        {
            ArrayList inventDAOList = XMLService.ParseXMLToInventDAO();
            int i = 1;
            if (inventDAOList.Count != 0) {
                String profileName = "Пусто";
                foreach (FullInventDAO inventDAO in inventDAOList)
                {
                    profileName = inventDAO.profileName;
                    inventoryList = inventDAO.invent;
                    Dictionary<String, int> coinsMap = inventDAO.coins;
                    Dictionary<String, String> helpMap = new Dictionary<String, String>();
                    if (inventoryList == null)
                    {
                        inventoryList = new ArrayList();
                    }
                    if (coinsMap == null || coinsMap.Count == 0)
                    {
                        coinsMap = XMLService.getEmptyCoins();
                    }
                    coinsList.Add(coinsMap);
                    helpMap.Add(DADConstants.STRENGHT_ATTR, inventDAO.strenghtValue.ToString());
                    helpMap.Add(DADConstants.SIZE_ATTR, inventDAO.size);
                    helpValuesList.Add(helpMap);
                    LoadToDataGrid(inventoryList, profileName);
                }
                dataGridView1 = (DataGridView)dataGridMap[0];
            }
            setCoins();
            updateTotalWeight();
            updateWeightPowerOfHero();
        }            

        public void updateTotalWeight ()
        {
            String weightMetricks;
            double sum = Math.Round(getTotalTotalWeight(), 2);
            if (isKGCheckBox.Checked)
            {
                weightMetricks = " КГ";
            }
            else
            {
                weightMetricks = " Ф";
            }
            labelWeightTotal.Text = sum + weightMetricks;
        }

        public void updateWeightPowerOfHero()
        {
            Dictionary<String, String> helpValues;
            if (helpValuesList.Count == 0)
            {
                strenghtNumericUpDown.Value = 0;
                sizeComboBox.Text = DADConstants.SIZE_NORMAL_DEFAULT;
            }
            else
            {
                helpValues = (Dictionary<String, String>)this.helpValuesList[tabControl1.SelectedIndex];
                strenghtNumericUpDown.Value = int.Parse(helpValues[DADConstants.STRENGHT_ATTR]);
                sizeComboBox.Text = helpValues[DADConstants.SIZE_ATTR].ToString();
            }
            checkDebuf();
        }
        private void CheckedChanged(object sender, EventArgs e)
        {
            isGenerated = true;

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                dataGridView1[DADConstants.NUMBER_ROW_TOTAL_WEIGHT, i].Value = calculateTotalItemWeight(i);
            }
            updateWightKg();
            updateTotalWeight();
            updateWeightPowerOfHero();

            if (isKGCheckBox.Checked)
            {
                foreach (DataGridView a in dataGridMap)
                {
                    a.Columns[DADConstants.NUMBER_ROW_WEIGHT_FUNT].ReadOnly = true;
                    a.Columns[DADConstants.NUMBER_ROW_WEIGHT_KILO].ReadOnly = false;
                }
            } else
            {
                foreach (DataGridView a in dataGridMap)
                {
                    a.Columns[DADConstants.NUMBER_ROW_WEIGHT_FUNT].ReadOnly = false;
                    a.Columns[DADConstants.NUMBER_ROW_WEIGHT_KILO].ReadOnly = true;
                }
            }
            isGenerated = false;
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo info = dataGridView1.HitTest(e.X, e.Y);
                if (info.RowIndex >= 0)
                {
                    DataGridViewRow view = dataGridView1.Rows[info.RowIndex];
                    if (view != null)
                        dataGridView1.DoDragDrop(view, DragDropEffects.Copy);
                }
            }
        }

        //120
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            //Кароч я тут бросил перетаскивание ячейки
            //https://forum.vingrad.ru/topic-88709.html
        }

        private void numericUpDownKG_ValueChanged(object sender, EventArgs e)
        {
            if (!isChangeConvert)
            {
                isChangeConvert = true;
                numericUpDownF.Value =
                    (decimal)(double.Parse(numericUpDownKG.Value.ToString())
                    / DADConstants.CONVERT_WEIGHT_VAR);
            }
            else
            {
                isChangeConvert = false;
            }
        }

        private void numericUpDownF_ValueChanged(object sender, EventArgs e)
        {
            if (!isChangeConvert)
            {
                isChangeConvert = true;
                numericUpDownKG.Value =
                    (decimal)(double.Parse(numericUpDownF.Value.ToString())
                    * DADConstants.CONVERT_WEIGHT_VAR);
            }
            else
            {
                isChangeConvert = false;
            }
            
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dataGridView1[e.ColumnIndex, e.RowIndex].Value = DADConstants.emptyVar[e.ColumnIndex];
        }

        private Color getColorToDropDown(String key)
        {
            return DADConstants.colorsVar[key];
        }

        private double getTotalTotalWeight()
        {
            double sum = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[DADConstants.NUMBER_ROW_TOTAL_WEIGHT, i].Value != null)
                {
                    sum += Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_TOTAL_WEIGHT, i].Value.ToString());
                }
            }
            if (CoinsWeightCheckBox.Checked)
            {
                sum += ((int)PlatinaNumerick.Value +
                    (int)GoldNumeric.Value +
                    (int)ElectrumNumeric.Value +
                    (int)SilverNumeric.Value +
                    (int)СopperNumeric.Value) *
                    (isKGCheckBox.Checked ?
                        DADConstants.ONE_COIN_WEIGHT_KG :
                        DADConstants.ONE_COIN_WEIGHT_FUNT);
            }
            return sum;
        }

        private void updateWightKg()
        {
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_KILO, i].Value =
                  Math.Round(Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_FUNT, i].Value.ToString())
                  * DADConstants.CONVERT_WEIGHT_VAR, 2);
            }

        }

        private void updateWightFunt()
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_FUNT, i].Value =
                  Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_KILO, i].Value.ToString())
                  / DADConstants.CONVERT_WEIGHT_VAR;
            }

        }

        private double calculateTotalItemWeight(int i)
        {
            if (isKGCheckBox.Checked)
            {
                return 
                   Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_COUNT, i].Value.ToString())
                   * Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_KILO, i].Value.ToString());
            }
            else
            {
                return
                    Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_COUNT, i].Value.ToString())
                    * Double.Parse(dataGridView1[DADConstants.NUMBER_ROW_WEIGHT_FUNT, i].Value.ToString());
            }
        }

        private void checkIsEmptyAll(DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1[i, e.RowIndex] != null && dataGridView1[i, e.RowIndex].Value == null)
                {
                    dataGridView1[i, e.RowIndex].Value = DADConstants.emptyVar[i];
                }
            }
        }

        private void validateWeightValue(int row, DataGridViewCellEventArgs e)
        {
            dataGridView1[row, e.RowIndex].Value =
                        dataGridView1[row, e.RowIndex].Value.ToString().Replace(".", ",");

            if (Double.Parse(dataGridView1[row, e.RowIndex].Value.ToString()) % 1 == 0
                && !dataGridView1[row, e.RowIndex].Value.ToString().Contains(","))
            {
                dataGridView1[row, e.RowIndex].Value =
                    dataGridView1[row, e.RowIndex].Value.ToString() + ",00";
            }
            dataGridView1[row, e.RowIndex].Value = Math.Round(Double.Parse(dataGridView1[row, e.RowIndex].Value.ToString()), 2);
        }


        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a = new TabTextBoxForm(profileName);
            a.FormClosed += formClosedRename;
            a.Show();
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a = new TabTextBoxForm(profileName);
            a.FormClosed += formClosed;
            a.Show();
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            if (a.isOk)
            {
                if (!tabControl1.TabPages.ContainsKey(a.newName))
                {
                    TabPage newTabPage = generateTabPage(a.newName);
                    this.dataGridView1 = generateDataRowTable();
                    newTabPage.Controls.Add(this.dataGridView1);
                    dataGridMap.Add(this.dataGridView1);
                    coinsList.Add(XMLService.getEmptyCoins());
                    helpValuesList.Add(generateEmptyHelpValues());
                    tabControl1.Controls.Add(newTabPage);
                } else
                {
                    MessageBox.Show("Такое имя уже существует", "Ошибка имени", MessageBoxButtons.OK);
                }
            }
        }

        private void formClosedRename(object sender, FormClosedEventArgs e)
        {
            if (a.isOk)
            {
                if (!tabControl1.TabPages.ContainsKey(a.newName))
                {
                    tabControl1.SelectedTab.Text = a.newName;
                    tabControl1.SelectedTab.Name = a.newName;
                }
                else
                {
                    MessageBox.Show("Такое имя уже существует", "Ошибка имени", MessageBoxButtons.OK);
                }
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Уверены что желаете удалить выбранный профиль?",
                "Удаление",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                coinsList.RemoveAt(tabControl1.SelectedIndex);
                helpValuesList.RemoveAt(tabControl1.SelectedIndex);
                dataGridMap.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

       
        private void dataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (!isGenerated)
            {
                isGenerated = true;
                checkIsEmptyAll(e);

                if (e.ColumnIndex == DADConstants.NUMBER_ROW_COUNT 
                    || e.ColumnIndex == DADConstants.NUMBER_ROW_WEIGHT_FUNT
                    || e.ColumnIndex == DADConstants.NUMBER_ROW_WEIGHT_KILO)
                {

                    if (isKGCheckBox.Checked)
                    {
                        updateWightFunt();
                    }
                    else
                    {
                        updateWightKg();
                    }
                    dataGridView1[DADConstants.NUMBER_ROW_TOTAL_WEIGHT, e.RowIndex].Value = calculateTotalItemWeight(e.RowIndex);
                    updateTotalWeight();
                    checkDebuf();
                }
                if (e.ColumnIndex == DADConstants.NUMBER_ROW_WEIGHT_FUNT 
                    || e.ColumnIndex == DADConstants.NUMBER_ROW_WEIGHT_KILO)
                {
                    validateWeightValue(DADConstants.NUMBER_ROW_WEIGHT_KILO, e);
                    validateWeightValue(DADConstants.NUMBER_ROW_WEIGHT_FUNT, e);
                }
                dataGridView1[DADConstants.NUMBER_ROW_CATEGORY, e.RowIndex].Style.BackColor = getColorToDropDown(dataGridView1[DADConstants.NUMBER_ROW_CATEGORY, e.RowIndex].Value.ToString());
                dataGridView1[DADConstants.NUMBER_ROW_CATEGORY, e.RowIndex].Style.SelectionBackColor = getColorToDropDown(dataGridView1[DADConstants.NUMBER_ROW_CATEGORY, e.RowIndex].Value.ToString());
                isGenerated = false;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex >= 0)
            {
                dataGridView1 = (DataGridView)dataGridMap[e.TabPageIndex];
            }
            if (coinsList.Count <= e.TabPageIndex)
            {
                coinsList.Add(XMLService.getEmptyCoins());
            }

            if (helpValuesList.Count <= e.TabPageIndex)
            {
                helpValuesList.Add(generateEmptyHelpValues());
            }
            setCoins();
            updateWeightPowerOfHero();
        }

        private void checkDebuf()
        {
            int strenght = int.Parse(strenghtNumericUpDown.Value.ToString());
            double sizeValue = 1;
            double total = getTotalTotalWeight();
            String debuff = String.Empty;
            double much = isKGCheckBox.Checked ? DADConstants.CONVERT_WEIGHT_VAR : 1;
            much *= isIronBack ? 2 : 1;

            switch (sizeComboBox.Text)
            {
                case DADConstants.BIG_SIZE:
                    sizeValue = 2;
                    break;
                case DADConstants.MEDIUM_SIZE:
                    sizeValue = 1;
                    break;
                case DADConstants.SMALL_SIZE:
                    sizeValue = 0.5;
                    break;
            }
            if (isFardWeight)
            {
                if (total <= (strenght * 5 * sizeValue * much))
                {
                    debuff = DADConstants.NOTHING_EFFECT;
                }
                else if (total <= (strenght * 10 * sizeValue * much))
                {
                    debuff = DADConstants.HALF_EFFECT;
                }
                else if (total <= (strenght * 15 * sizeValue * much))
                {
                    debuff = DADConstants.FULL_EFFECT;
                }
                else if (total > (strenght * 15 * sizeValue * much))
                {
                    debuff = DADConstants.DEAD_END_EFFECT;
                }
            } else
            {
                if (total > (strenght * 15 * sizeValue * much))
                {
                    debuff = DADConstants.DEAD_END_EFFECT;
                } else
                {
                    debuff = DADConstants.NOTHING_EFFECT;
                }
            }

            labelEffect.Text = debuff;
        }

        private void tabControl1_Deselecting(object sender, TabControlCancelEventArgs e)
        {  
            if (e.TabPageIndex > 0)
            {
                coinsList[e.TabPageIndex] = getCoins();
                helpValuesList[e.TabPageIndex] = getHelpValues();
            }
        }

        private Dictionary<String, String> getHelpValues()
        {
            Dictionary<String, String> dict = new Dictionary<String, String>();
            dict.Add(DADConstants.STRENGHT_ATTR, strenghtNumericUpDown.Value.ToString());
            dict.Add(DADConstants.SIZE_ATTR, sizeComboBox.Text);
            return dict;
        }


        private Dictionary<String, String> generateEmptyHelpValues()
        {
            Dictionary<String, String> dict = new Dictionary<String, String>();
            dict.Add(DADConstants.STRENGHT_ATTR, "0");
            dict.Add(DADConstants.SIZE_ATTR, DADConstants.SIZE_NORMAL_DEFAULT);
            return dict;
        }

        private TabPage generateTabPage(String name)
        {
            TabPage newTabPage = new TabPage();
            newTabPage.Location = new System.Drawing.Point(4, 22);
            newTabPage.Name = name;
            newTabPage.Padding = new System.Windows.Forms.Padding(3);
            newTabPage.Size = new System.Drawing.Size(908, 580);
            newTabPage.TabIndex = 1;
            newTabPage.Text = name;
            newTabPage.UseVisualStyleBackColor = true;
            return newTabPage;
        }

        private DataGridView generateDataRowTable()
        {
            this.SuspendLayout();
            DataGridView dataGridViewNew;
            DataGridViewTextBoxColumn Column1New;
            DataGridViewTextBoxColumn Column2New;
            DataGridViewTextBoxColumn Column3New;
            DataGridViewTextBoxColumn Column7New;
            DataGridViewComboBoxColumn Column4New;
            DataGridViewTextBoxColumn Column5New;
            DataGridViewTextBoxColumn Column6New;

            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            dataGridViewNew = new DataGridView();
            Column1New = new DataGridViewTextBoxColumn();
            Column2New = new DataGridViewTextBoxColumn();
            Column3New = new DataGridViewTextBoxColumn();
            Column7New = new DataGridViewTextBoxColumn();
            Column4New = new DataGridViewComboBoxColumn();
            Column5New = new DataGridViewTextBoxColumn();
            Column6New = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewNew)).BeginInit();
            // 
            // dataGridView1
            // 
            dataGridViewNew.AllowDrop = true;
            dataGridViewNew.AllowUserToResizeColumns = false;
            dataGridViewNew.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewNew.BackgroundColor = SystemColors.ControlDarkDark;
            dataGridViewNew.Columns.AddRange(new DataGridViewColumn[] {
            Column1New,
            Column2New,
            Column3New,
            Column7New,
            Column4New,
            Column5New,
            Column6New});
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = SystemColors.Window;
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridViewNew.DefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewNew.GridColor = SystemColors.ControlDarkDark;
            dataGridViewNew.Location = new Point(12, 12);
            dataGridViewNew.Name = "dataGridViewNew";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = SystemColors.Control;
            dataGridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dataGridViewNew.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dataGridViewNew.RowsDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewNew.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewNew.Size = new Size(906, 580);
            dataGridViewNew.TabIndex = 1;
            // 
            // Column1
            // 
            Column1New.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.White;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            Column1New.DefaultCellStyle = dataGridViewCellStyle1;
            Column1New.Frozen = true;
            Column1New.HeaderText = "Название";
            Column1New.Name = "Column1";
            Column1New.Width = 82;
            // 
            // Column2
            // 
            Column2New.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            Column2New.DefaultCellStyle = dataGridViewCellStyle2;
            Column2New.Frozen = true;
            Column2New.HeaderText = "Кол-во";
            Column2New.Name = "Column2";
            Column2New.Width = 66;
            // 
            // Column3
            // 
            Column3New.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = Color.White;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            Column3New.DefaultCellStyle = dataGridViewCellStyle3;
            Column3New.Frozen = true;
            Column3New.HeaderText = "Вес 1 шт.";
            Column3New.Name = "Column3";
            Column3New.Width = 79;
            // 
            // Column7
            // 
            Column7New.HeaderText = "Вес 1 шт. (Кг)";
            Column7New.Name = "Column7";
            Column7New.ReadOnly = true;
            // 
            // Column4
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            Column4New.DefaultCellStyle = dataGridViewCellStyle4;
            Column4New.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            Column4New.DisplayStyleForCurrentCellOnly = true;
            Column4New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Column4New.HeaderText = "Категория";
            Column4New.Items.AddRange(new object[] {
            "Одежда",
            "Броня",
            "Рецепт",
            "Книга",
            "Инструмент",
            "Оружие",
            "Пища",
            "Безделушка",
            "Драгоценность",
            "Зелье",
            "Артефакт"});
            Column4New.Name = "Column4";
            Column4New.Resizable = DataGridViewTriState.True;
            Column4New.SortMode = DataGridViewColumnSortMode.Automatic;
            Column4New.Width = 120;
            // 
            // Column5
            // 
            Column5New.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = Color.White;
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            Column5New.DefaultCellStyle = dataGridViewCellStyle5;
            Column5New.HeaderText = "Общий вес";
            Column5New.Name = "Column5";
            Column5New.ReadOnly = true;
            Column5New.Width = 88;
            // 
            // Column6
            // 
            Column6New.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = Color.White;
            dataGridViewCellStyle6.ForeColor = Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = Color.White;
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            Column6New.DefaultCellStyle = dataGridViewCellStyle6;
            Column6New.HeaderText = "Описание";
            Column6New.Name = "Column6";

            ((System.ComponentModel.ISupportInitialize)(dataGridViewNew)).EndInit();
            this.ResumeLayout(false);
            dataGridViewNew.DataError += new DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            dataGridViewNew.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            dataGridViewNew.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged_1);

            return dataGridViewNew;
        }

        private void checkDebuf(object sender, EventArgs e)
        {
            checkDebuf();
        }

        private void strenghtNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ((Dictionary<String, String>)this.helpValuesList[tabControl1.SelectedIndex])[DADConstants.STRENGHT_ATTR] = strenghtNumericUpDown.Value.ToString();
            ((Dictionary<String, String>)this.helpValuesList[tabControl1.SelectedIndex])[DADConstants.SIZE_ATTR] = sizeComboBox.Text;
            checkDebuf();
        }

        private void checkBoxWeightType_CheckedChanged(object sender, EventArgs e)
        {
            isFardWeight = !isFardWeight;
            checkDebuf();
        }

        private void ironBackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isIronBack = ironBackCheckBox.Checked;
            checkDebuf();
        }
    }
}
