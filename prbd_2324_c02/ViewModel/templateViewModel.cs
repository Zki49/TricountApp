using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace prbd_2324_c02.ViewModel
{
    public class templateViewModel : PRBD_Framework.ViewModelBase<User, PridContext>
    {
        public Template template { get; set; }
        public String Title { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditTemplateCommand { get; set; }
        public templateViewModel(Template temp) { 
            template = temp;
            this.Title = temp.Title;
            DeleteCommand = new RelayCommand(() => { NotifyColleagues(App.Messages.MSG_TEMPLATE_DELETE, this.template); });
            EditTemplateCommand = new RelayCommand(() => { NotifyColleagues(App.Messages.MSG_EDIT_TEMPLATE, this.template); });
        }
        public void deleteTemplate() {
            template.RemoveTemplate();
            RaisePropertyChanged();
        }
    }
}
