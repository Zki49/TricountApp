using prbd_2324_c02.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2324_c02.ViewModel
{
    class detailTricountViewModel : ViewModelBase<User, PridContext>
    {
        public string Title {  get; set; }
        public string description {  get; set; }
        public string CreatedAt {  get; set; }
        public string ballance {  get; set; }
        public string UserFulname {  get; set; }
    }
}
