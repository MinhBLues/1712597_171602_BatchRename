using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class ImageClass
    {
        public ImageClass()
        {
            GetAll();
        }
        public void  GetAll()
        {
            var lines = File.ReadAllLines("Images.txt");
            var count = int.Parse(lines[0]);

            if (count > 0)
            {
                for (int i = 1; i <= count; i++)
                {
                     var temp =  $"{AppDomain.CurrentDomain.BaseDirectory}images\\{lines[i]}";
                    icon.Add(
                        new MyString()
                        {

                            Value = temp

                        }); ;
                        
                }
            }
        }
        public BindingList<MyString> icon { get; set; } = new BindingList<MyString>();
    }
}
