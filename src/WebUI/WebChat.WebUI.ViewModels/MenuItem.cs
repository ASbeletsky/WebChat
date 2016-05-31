namespace WebChat.WebUI.ViewModels
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public class MenuItem
    {
        public string Labor { get; set; }
        public string Link { get; set; }
        public IEnumerable<MenuItem> ChildrenItems { get; set; }

        public MenuItem ParrentItem { get; set; }
        public bool HasParrent
        {
            get
            {
                return ParrentItem != null;
            }
        }

        public bool HasChildrens
        {
            get
            {
                if (ChildrenItems != null)
                {
                    return ChildrenItems.Any();
                }

                return false;
            }
        }

        public string Class
        {
            get
            {
                return this.HasChildrens ? "init-arrow-down" : "init-un-active";
            }
        }
    }
}
