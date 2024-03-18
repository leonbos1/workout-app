using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutApp.ViewModels
{
    public class ProfilePageViewModel : BindableObject
    {
        private string _pageTitle = "Profile";

        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                if (_pageTitle != value)
                {
                    _pageTitle = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
