using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.ReactExtensions.ViewModels
{
    public class TreeSelectViewModel
    {
        public TreeSelectViewModel(string t, int val, int k, IEnumerable<TreeSelectViewModel> ch, bool leaf, bool disCh, bool selecta, bool checkebla)
        {
            title = t;
            value = val;
            key = k;
            children = ch;
            isLeaf = leaf;
            disableCheckbox = disCh;
            selectable = selecta;
            checkable = checkebla;
        }

        public string title { get; set; }
        public int value { get; set; }
        public int key { get; set; }

        //disabled, disableCheckbox, selectable, checkable
        public IEnumerable<TreeSelectViewModel> children { get; set; }
        public bool isLeaf { get; set; }

        public bool disableCheckbox { get; set; }
        public bool selectable { get; set; }
        public bool checkable { get; set; }
    }
}
