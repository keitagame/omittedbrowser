
using System;
using System.Windows.Forms;
using Gecko;
using System.Drawing;
namespace GeckoFxBrowser
{
    public class MainForm : Form
    {
        private GeckoWebBrowser geckoWebBrowser;
         private TextBox textBox;
        private TabControl tabControl;
    private Button addButton;
      private Button destroybutton;
        public MainForm()
        {
            // フォームの設定
            
          
           this.textBox = new TextBox();
            this.textBox.Location = new System.Drawing.Point(20, 60);
            this.textBox.Size = new System.Drawing.Size(700, 20);
           this.textBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            this.Controls.Add(this.textBox);
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Text = "ob";
            this.Size = new System.Drawing.Size(1900, 1200);
            // Gecko エンジンの初期化
            Xpcom.Initialize("Geckofx45.45.0.34/content/Firefox"); // path_to_xulrunner を適切なパスに置き換えてください
           tabControl = new TabControl();
        destroybutton = new Button();
        destroybutton.Text = "✕";
       destroybutton.Click += tabdestroy;    
      destroybutton.Size =new System.Drawing.Size(100, 30);
        destroybutton.Location = new System.Drawing.Point(900, 60);
           this.Controls.Add(destroybutton);
           addButton = new Button() { Text = "+",Top = 60,Left = 750 };
            addButton.Size =new System.Drawing.Size(100, 30);
            addButton.Click += AddButton_Click;
         this.Controls.Add(addButton);
        // デフォルトのタブページ
        TabPage tabPage1 = new TabPage("start");
       tabControl.ItemSize = new System.Drawing.Size(190, 50); 

        

        // コントロールの配置
       
            // GeckoWebBrowser コントロールの作成と設定
            geckoWebBrowser = new GeckoWebBrowser
            {
               Top = 50,
                Width = 1900,
                Height = 1150
            };
         
         
        
         tabControl.TabPages.Add(tabPage1);
           tabControl.Size = new System.Drawing.Size(1900, 1150);
         this.Controls.Add(tabControl);
         
        }
  private void tabdestroy(object sender, EventArgs e)
    {
        
               TabPage activeTab = tabControl.SelectedTab;
           
           
                GeckoWebBrowser geckoWebBrowsers = (GeckoWebBrowser)activeTab.Controls[0];
                
            
                
         tabControl.TabPages.Remove(activeTab);
    }    
private void AddButton_Click(object sender, EventArgs e)
    {
        int newIndex = tabControl.TabPages.Count + 1;
        TabPage newTabPage = new TabPage("tab" + newIndex);
        newTabPage.Controls.Add(geckoWebBrowser =new GeckoWebBrowser
            {
               Top = 50,
                Width = 1900,
                Height = 1150
                 
            });
        tabControl.TabPages.Add(newTabPage);
         geckoWebBrowser.Navigate("");
    }
        protected override void OnLoad(EventArgs e)
        {
              
            base.OnLoad(e);
            // 指定した URL にナビゲート
            
        }
       private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               TabPage activeTab = tabControl.SelectedTab;
            if (activeTab != null)
            {
                GeckoWebBrowser geckoWebBrowsers = (GeckoWebBrowser)activeTab.Controls[0];
                
            
                geckoWebBrowsers.Navigate(this.textBox.Text);
            }
            }
        }
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
