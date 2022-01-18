using CS.ERP.PL.SYS.DAT;
using OvaCodeTest.Views.ShoppingCart;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace OvaCodeTest.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        bool showMenu = false;
        public bool ShowMenu
        {
            get { return showMenu; }
            set { SetProperty(ref showMenu, value); }
        }

        DataTemplate template;
        public DataTemplate ListViewItemTemplate
        {
            get { return template; }
            set { SetProperty(ref template, value); }
        }

        RES_MENU selectedMenu;
        public RES_MENU SelectedMenu
        {
            get { return selectedMenu; }
            set { SetProperty(ref selectedMenu, value); }
        }

        public Command ShoppingCartCommand { get; }
        public ObservableCollection<RES_MENU> MenuList { get; set; }

        public MenuViewModel()
        {
            ShoppingCartCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(ShoppingCartPage)));
            MenuList = new ObservableCollection<RES_MENU>();
        }

        public void LoadMenu()
        {
            if (App.Menu != null)
            {
                MenuList.Clear();

                App.Menu.ForEach(item => MenuList.Add(item));
            }

            if (MenuList.Count > 0)
            {
                ShowMenu = true;
            }
        }

        public void GenerateTemplate(RES_MENU submenu2 = null)
        {
            int count = 0;
            var tmp = new DataTemplate(() =>
            {
                Label label = new Label
                {
                    Style = (Style)Application.Current.Resources["DisplayLabelStyle"],
                    HorizontalTextAlignment = TextAlignment.Start,
                    FontSize = 14,
                    TextColor = (Color)Application.Current.Resources["Primary"],
                };
                label.SetBinding(Label.TextProperty, new Binding(nameof(RES_MENU.Text)));
                RowDefinition RW = new RowDefinition { Height = GridLength.Auto };
                GridLength GL1 = new GridLength(9, GridUnitType.Star);
                GridLength GL2 = new GridLength(1, GridUnitType.Star);
                var content = new Grid
                {
                    RowSpacing = 10
                };
                content.RowDefinitions.Add(RW);
                content.ColumnDefinitions.Add(new ColumnDefinition { Width = GL1 });
                content.ColumnDefinitions.Add(new ColumnDefinition { Width = GL2 });

                Image im = new Image()
                {
                    Source = MenuList.Count > 0 ? "down_arrow" : "right_arrow_white",
                    Style = (Style)Application.Current.Resources["FrameEndIconStyle"]
                };

                content.Children.Add(label);
                content.Children.Add(im, 1, 0);

                RES_MENU current = MenuList[count];
                if (SelectedMenu != null)
                {
                    if (current.Id == SelectedMenu.Id && current.subMenuList.Count > 0)
                    {
                        StackLayout st = new StackLayout()
                        {
                            Orientation = StackOrientation.Vertical,
                            Spacing = 10
                        };
                        foreach (RES_MENU ms in current.subMenuList)
                        {
                            Button btn = new Button
                            {
                                Text = ms.Text,
                                BackgroundColor = (Color)Application.Current.Resources["BgGray"],
                                TextColor = (Color)Application.Current.Resources["Primary"],
                                FontSize = 12,
                                CornerRadius = 8
                            };
                            btn.Clicked += (s, e) =>
                            {
                                GenerateTemplate(ms);
                            };
                            st.Children.Add(btn);
                            if (submenu2 != null && submenu2.Id == ms.Id && ms.subMenuList.Count > 0)
                            {
                                StackLayout st2 = new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    Spacing = 10
                                };
                                foreach (RES_MENU sub_menu in ms.subMenuList)
                                {
                                    Label lblsub = new Label
                                    {
                                        Style = (Style)Application.Current.Resources["DisplayLabelStyle"],
                                        HorizontalTextAlignment = TextAlignment.Start,
                                        Text = sub_menu.Text,
                                        FontSize = 14,
                                        TextColor = (Color)Application.Current.Resources["Primary"],
                                    };
                                    st2.Children.Add(lblsub);
                                }
                                st.Children.Add(st2);
                            }
                        }
                        ScrollView scrl = new ScrollView()
                        {
                            Orientation = ScrollOrientation.Vertical,
                            VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                            Content = st
                        };
                        content.Children.Add(scrl, 0, 1);
                        Grid.SetColumnSpan(scrl, 2);
                    }
                }
                Frame fr = new Frame()
                {
                    Padding = new Thickness(10),
                    BackgroundColor = Color.Transparent,
                    Style = (Style)Application.Current.Resources["DefaultFrameStyle"],
                    BorderColor = (Color)Application.Current.Resources["AppGrey"],
                    Content = content
                };

                count += 1;

                return new ViewCell
                {
                    View = fr
                };
            });
            ListViewItemTemplate = tmp;
        }
    }
}
