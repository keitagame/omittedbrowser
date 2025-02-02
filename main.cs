using System;
using System.Windows.Forms;
using Gecko;
using System.Drawing;
using System.Runtime.InteropServices;
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
            
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

           this.textBox = new TextBox();
            this.textBox.Location = new System.Drawing.Point(20, 60);
            this.textBox.Size = new System.Drawing.Size(700, 20);
           this.textBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            this.Controls.Add(this.textBox);
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Text = "ob";
            this.Size = new System.Drawing.Size(1900, 1200);
            // Gecko エンジンの初期化
            Xpcom.Initialize("Geckofx60.64.60.0.56/content/Firefox"); // path_to_xulrunner を適切なパスに置き換えてください
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
         tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
          tabControl.SizeMode = TabSizeMode.Fixed;
          tabControl.ItemSize = new System.Drawing.Size(190, 50); 
     
tabControl.DrawItem += new DrawItemEventHandler(tabControl_DrawItem);
   tabControl.DrawItem += new DrawItemEventHandler(tabControl_DrawItems);
     

        // コントロールの配置
       
            // GeckoWebBrowser コントロールの作成と設定
            geckoWebBrowser = new GeckoWebBrowser
            {
               Top = 50,
                Width = 1900,
                Height = 100
            };
         
         
        
         
           tabControl.Size = new System.Drawing.Size(1900, 1150);
         this.Controls.Add(tabControl);
         
         
        }
       
     private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
{
    
    TabPage tabPage = tabControl.TabPages[e.Index];
  
            
    Rectangle tabRect = tabControl.GetTabRect(e.Index);
    tabRect.Inflate(-6, 2);
     
    if (e.Index == tabControl.SelectedIndex)
    {
          
        e.Graphics.FillRectangle(Brushes.Orange, tabRect);
    }
    else
    {
        e.Graphics.FillRectangle(Brushes.LightGray, tabRect);
       
    }
   
    TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
     
}

private void tabControl_DrawItems(object sender, DrawItemEventArgs e)
{
  
}
private void t(object sender, PaintEventArgs e)
        {
          Console.Write("a");
            // タブストリップの背景を黒く塗りつぶす
            
            
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
          newTabPage.Font = new Font("Arial", 8);
          newTabPage.BackColor = Color.Orange;
        newTabPage.Controls.Add(geckoWebBrowser =new GeckoWebBrowser
            {
               Top = 50,
              Left = 20,
                Width = 1840,
                Height = 1150
                 
            });
        tabControl.TabPages.Add(newTabPage);
         geckoWebBrowser.Navigate("https://github.com/keitagame/omittedbrowser");
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
        const int LOGPIXELSX = 88;
    const int LOGPIXELSY = 90;

    [DllImport("user32.dll")]
    extern static bool SetProcessDPIAware();

    [DllImport("user32.dll")]
    extern static IntPtr GetWindowDC(IntPtr hwnd);

    [DllImport("gdi32.dll")]
    extern static int GetDeviceCaps(IntPtr hdc, int index);

    [DllImport("user32.dll")]
    extern static int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [STAThread]
        public static void Main()
        {
           SetProcessDPIAware();
        var hdc = GetWindowDC(IntPtr.Zero);
        Console.WriteLine("DpiX: {0}", GetDeviceCaps(hdc, LOGPIXELSX));
        Console.WriteLine("DpiY: {0}", GetDeviceCaps(hdc, LOGPIXELSY));
        ReleaseDC(IntPtr.Zero, hdc);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
