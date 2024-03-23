using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutApp.Pages
{
    public class SetTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ExistingSetTemplate { get; set; }
        public DataTemplate NewSetTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item == null || item is NewSetMarker ? NewSetTemplate : ExistingSetTemplate;
        }
    }

    public class NewSetMarker { }
}
