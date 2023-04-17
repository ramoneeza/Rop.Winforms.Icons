using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons
{
    internal partial class dummy{}
    public class OrderIcon : IconIndexLabel
    {
        public event EventHandler SortOrderChanged;
        
        public int ColumnIndex { get; set; }
        public bool Selectable { get; set; } = true;
        public bool Selected
        {
            get => SortOrder != SortOrder.None;
            set
            {
                if (!Selectable)
                {
                    SortOrder = SortOrder.None;
                    return;
                }
                if (Selected == value) return;
                SortOrder = (value) ? SortOrder.Ascending : SortOrder.None;
            }
        }
        public bool Ascending
        {
            get => SortOrder != SortOrder.Descending;
            set => SortOrder = value ? SortOrder.Ascending : SortOrder.Descending;
        }

        public SortOrder SortOrder
        {
            get =>base.SelectedIcon switch
            {
                0=> SortOrder.Ascending,
                1=> SortOrder.Descending,
                _=> SortOrder.None
            };
            set
            {
                if (SortOrder == value) return;
                base.SelectedIcon = value switch
                {
                    SortOrder.Ascending => 0,
                    SortOrder.Descending => 1,
                    _ => 2
                };
                SortOrderChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public OrderIcon() : base()
        {
            ColorItems = new[]
            {
                DuoToneColor.OneTone(Color.Black),
                DuoToneColor.OneTone(Color.Black),
                DuoToneColor.OneTone(Color.Silver),
            };
            base.SelectedIcon = 2;
        }
        
        public new int SelectedIcon
        {
            get=>base.SelectedIcon;
            set{}
        }
    }
}
