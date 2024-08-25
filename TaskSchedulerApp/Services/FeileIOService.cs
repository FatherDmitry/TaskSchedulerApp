using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedulerApp.Models;

namespace TaskSchedulerApp.Services
{
    internal class FeileIOService
    {
        private readonly string PATH;

        public FeileIOService(string path)
        {
            PATH = path;
        }

        //Класс передачи файлов из программы
        public BindingList<TaskModel> LoadData()
        {
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                File.CreateText(PATH).Dispose();
                return new BindingList<TaskModel>();
            }

            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TaskModel>>(fileText);
            }
        }

        //Метод сохранения файлов
        public void SaveDate(object _taskDateList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                //Запись файла в JSON
                string output = JsonConvert.SerializeObject(_taskDateList);
                writer.WriteLine(output);
            }
        }
    }
}
